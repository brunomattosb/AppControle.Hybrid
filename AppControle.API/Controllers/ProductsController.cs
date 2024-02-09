using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppControle.API.Context;
using AppControle.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using AppControle.Shared.DTO;

namespace AppControle.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        //private readonly IFileStorage _fileStorage;

        public ProductsController(DataContext context) //, IFileStorage fileStorage)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? filter)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            //if (user == null)
            //{
            //    return BadRequest("User not valid.");
            //}

            var queryable = _context.Products
                //.Include(x => x.lProductImages)
                .Include(x => x.lProductCategories!)
                .ThenInclude(x => x.Category)
                .AsQueryable();

            //queryable = queryable.Where(s => s.User!.Email == User.FindFirstValue(ClaimTypes.Email)!);

            if (!string.IsNullOrEmpty(filter))
            {
                queryable = queryable.Where(x => x.Name!.Contains(filter));
            }

            var lProducts = await queryable
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .ToListAsync();

            if(lProducts is null)
            {
                return NotFound();
            }
            return lProducts;
        }

        [HttpGet("combobox")]
        public async Task<ActionResult<IEnumerable<ProductCbbDTO>>> GetCombo()
        {
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            //if (user == null)
            //{
            //    return BadRequest("User not valid.");
            //}

            if (_context.Products == null)
            {
                return NotFound();
            }
            var queryable = _context.Products
                .Where(x => x.IsService == true)
                .AsQueryable();
            //queryable = queryable.Where(s => s.User!.Email == User.FindFirstValue(ClaimTypes.Email)!);

            var lProducts = await queryable
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .ToListAsync();

            List<ProductCbbDTO> productCbbDTO = new();
            
            foreach (var prodDTO in lProducts)
            {
                productCbbDTO.Add(new ProductCbbDTO { 
                    Description = prodDTO.Description,
                    Name = prodDTO.Name,
                    Id = prodDTO.Id,
                    Price = prodDTO.Price,
                });
            }
            return productCbbDTO;
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            //if (user == null)
            //{
            //    return BadRequest("User not valid.");
            //}

            var product = await _context.Products
                //.Include(x => x.lProductImages)
                .Include(x => x.lProductCategories!)
                .ThenInclude(x => x.Category)
                //.Where(x => x.User!.Id == user.Id)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                //if (product.UserId != user.Id)
                //{
                //    return BadRequest("User not valid.");
                //}
            }

            return product;
        }


        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            //if (user == null)
            //{
            //    return BadRequest("User not valid.");
            //}

            var productExists = await _context.Products
                .Include(x => x.lProductCategories)
                //.Include(x => x.lProductImages)
                //.Where(x => x.User!.Id == user.Id)
                .FirstOrDefaultAsync(x => x.Id == productDTO.Id);

            if (productExists == null)
            {
                return NotFound();
            }

            //if (productExists.lProductImages is null)
            //{
            //    productExists.lProductImages = new List<ProductImage>();
            //}

            //foreach (string img in productDTO.ProductImages)
            //{
            //    if (!img.StartsWith("https://sales2023.blob.core.windows.net/products/"))
            //    {
            //        var photoProduct = Convert.FromBase64String(img);
            //        product.lProductImages!.Add(new ProductImage { Image = await _fileStorage.SaveFileAsync(photoProduct, ".jpg", "products") });
            //    }
            //}

            productExists.Name = productDTO.Name;
            productExists.Description = productDTO.Description;
            productExists.Price = productDTO.Price;
            productExists.Stock = productDTO.Stock;
            productExists.IsActive = productDTO.IsActive;
            productExists.IsService = productDTO.IsService;
            productExists.lProductCategories = productDTO.ProductCategoryIds!.Select(x => new ProductCategory { CategoryId = x }).ToList();

            try
            {
                _context.Entry(productExists).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(productExists);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
                {
                    return BadRequest("Já existe uma categoria com o mesmo nome.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDTO productDTO)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            //if (user == null)
            //{
            //    return BadRequest("User not valid.");
            //}
            Product newProduct = new()
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                lProductCategories = new List<ProductCategory>(),
                //lProductImages = new List<ProductImage>(),
                //User = user,
                //UserId = user.Id,
                IsActive = productDTO.IsActive,
                IsService = productDTO.IsService,
            };

            try
            {
                //foreach (var productImage in productDTO.ProductImages!)
                //{
                //    var photoProduct = Convert.FromBase64String(productImage);
                //    newProduct.lProductImages.Add(new ProductImage { Image = await _fileStorage.SaveFileAsync(photoProduct, ".jpg", "products") });
                //}

                foreach (var productCategoryId in productDTO.ProductCategoryIds!)
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == productCategoryId);
                    newProduct.lProductCategories.Add(new ProductCategory { Category = category! });
                }

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProduct", new { id = newProduct.Id }, newProduct);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
                {
                    return BadRequest("Já existe uma produto com o mesmo nome.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            //if (user == null)
            //{
            //    return BadRequest("User not valid.");
            //}

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            //if(user.Id != category.UserId)
            //{
            //    return NotFound();
            //}

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}


//[HttpPost("addImages")]
//public async Task<ActionResult> PostAddImagesAsync(ImageDTO imageDTO)
//{
//    var product = await _context.Products
//        .Include(x => x.lProductImages)
//        .FirstOrDefaultAsync(x => x.Id == imageDTO.ProductId);
//    if (product == null)
//    {
//        return NotFound();
//    }

//    if (product.lProductImages is null)
//    {
//        product.lProductImages = new List<ProductImage>();
//    }

//    for (int i = 0; i < imageDTO.Images.Count; i++)
//    {
//        if (!imageDTO.Images[i].StartsWith("https://sales2023.blob.core.windows.net/products/"))
//        {
//            var photoProduct = Convert.FromBase64String(imageDTO.Images[i]);
//            imageDTO.Images[i] = await _fileStorage.SaveFileAsync(photoProduct, ".jpg", "products");
//            product.lProductImages!.Add(new ProductImage { Image = imageDTO.Images[i] });
//        }
//    }

//    _context.Update(product);
//    await _context.SaveChangesAsync();
//    return Ok(imageDTO);
//}

//[HttpPost("removeLastImage")]
//public async Task<ActionResult> PostRemoveLastImageAsync(ImageDTO imageDTO)
//{
//    var product = await _context.Products
//        .Include(x => x.lProductImages)
//        .FirstOrDefaultAsync(x => x.Id == imageDTO.ProductId);
//    if (product == null)
//    {
//        return NotFound();
//    }

//    if (product.lProductImages is null || product.lProductImages.Count == 0)
//    {
//        return Ok();
//    }

//    var lastImage = product.lProductImages.LastOrDefault();
//    await _fileStorage.RemoveFileAsync(lastImage!.Image, "products");
//    product.lProductImages.Remove(lastImage);
//    _context.Update(product);
//    await _context.SaveChangesAsync();
//    imageDTO.Images = product.lProductImages.Select(x => x.Image).ToList();
//    return Ok(imageDTO);
//}