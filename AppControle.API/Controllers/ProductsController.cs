using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AppControle.API.Data;
using AppControle.API.Extensions;
using AppControle.API.Helpers;
using AppControle.Shared.DTO;
using AppControle.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SisVendas.API.Data;


namespace AppControle.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;
        public ProductsController(DataContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories([FromQuery] Pagination pagination, [FromQuery] string? filter)
        {
            var queryable = _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.ProductCategories)
                .AsQueryable();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }

            if (!string.IsNullOrWhiteSpace(filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
            }
            queryable = queryable.Where(s => s.User!.Email == User.FindFirstValue(  ClaimTypes.Email)!);

            await HttpContext.InsertParamsInPageResponse(queryable, pagination.QuantityPerPage);
            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginar(pagination)
                .ToListAsync());
        }
        [AllowAnonymous]
        [HttpGet("combobox")]
        public async Task<ActionResult<IEnumerable<Product>>> GetCombo()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var queryable = _context.Products
                .Where(x => x.IsService == true)
                .AsQueryable();

            return Ok(await queryable
                .OrderBy(x => x.Name)
                .ToListAsync());
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }

            var product = await _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.ProductCategories!)
                .ThenInclude(x => x.Category)
                .Where(x => x.User!.Id == user.Id)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(ProductDTO productDTO)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
                if (user == null)
                {
                    return BadRequest("Usuario no válido");

                }
                Product newProduct = new()
                {
                    Name = productDTO.Name,
                    Description = productDTO.Description,
                    Price = productDTO.Price,
                    Stock = productDTO.Stock,
                    ProductCategories = new List<ProductCategory>(),
                    ProductImages = new List<ProductImage>(),
                    User = user,
                    UserId = user.Id,
                };
                
                foreach (var productImage in productDTO.ProductImages!)
                {
                    var photoProduct = Convert.FromBase64String(productImage);
                    newProduct.ProductImages.Add(new ProductImage { Image = await _fileStorage.SaveFileAsync(photoProduct, ".jpg", "products") });
                }

                foreach (var productCategoryId in productDTO.ProductCategoryIds!)
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == productCategoryId);
                    newProduct.ProductCategories.Add(new ProductCategory { Category = category! });
                }

                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                return Ok(productDTO);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
                {
                    return BadRequest("Já existe um produto com o mesmo nome");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("addImages")]
        public async Task<ActionResult> PostAddImagesAsync(ImageDTO imageDTO)
        {
            var product = await _context.Products
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == imageDTO.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            if (product.ProductImages is null)
            {
                product.ProductImages = new List<ProductImage>();
            }

            for (int i = 0; i < imageDTO.Images.Count; i++) 
            {
                if (!imageDTO.Images[i].StartsWith("https://sales2023.blob.core.windows.net/products/"))
                {
                    var photoProduct = Convert.FromBase64String(imageDTO.Images[i]);
                    imageDTO.Images[i] = await _fileStorage.SaveFileAsync(photoProduct, ".jpg", "products");
                    product.ProductImages!.Add(new ProductImage { Image = imageDTO.Images[i] });
                }
            }

            _context.Update(product);
            await _context.SaveChangesAsync();
            return Ok(imageDTO);
        }

        [HttpPost("removeLastImage")]
        public async Task<ActionResult> PostRemoveLastImageAsync(ImageDTO imageDTO)
        {
            var product = await _context.Products
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == imageDTO.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            if (product.ProductImages is null || product.ProductImages.Count == 0)
            {
                return Ok();
            }

            var lastImage = product.ProductImages.LastOrDefault();
            await _fileStorage.RemoveFileAsync(lastImage!.Image, "products");
            product.ProductImages.Remove(lastImage);
            _context.Update(product);
            await _context.SaveChangesAsync();
            imageDTO.Images = product.ProductImages.Select(x => x.Image).ToList();
            return Ok(imageDTO);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(ProductDTO productDTO)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
                if (user == null)
                {
                    return BadRequest("Usuario no válido");

                }
                var product = await _context.Products
                    .Include(x => x.ProductCategories)
                    .Include(x => x.ProductImages)
                    .Where(x => x.User!.Id == user.Id)
                    .FirstOrDefaultAsync(x => x.Id == productDTO.Id);
                if (product == null)
                {
                    return NotFound();
                }

                if(product.ProductImages is null)
                {
                    product.ProductImages = new List<ProductImage>();
                }

                foreach(string img in productDTO.ProductImages)
                {
                    if (!img.StartsWith("https://sales2023.blob.core.windows.net/products/"))
                    {
                        var photoProduct = Convert.FromBase64String(img);
                        product.ProductImages!.Add(new ProductImage { Image = await _fileStorage.SaveFileAsync(photoProduct, ".jpg", "products") });
                    }
                }

                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.Stock = productDTO.Stock;
                product.ProductCategories = productDTO.ProductCategoryIds!.Select(x => new ProductCategory { CategoryId = x }).ToList();

                _context.Update(product);
                await _context.SaveChangesAsync();
                return Ok(productDTO);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
                {
                    return BadRequest("Ya existe una ciudad con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            //TODO: Validar se userid é igual ao do token
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}