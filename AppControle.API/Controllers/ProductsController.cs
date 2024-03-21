using Microsoft.AspNetCore.Mvc;
using Shared.Entities;
using AppControle.API.Repositories;
using AutoMapper;
using Newtonsoft.Json;
using Shared.Entities.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shared.DTO.EntitiesDTO;

namespace AppControle.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        //private readonly IFileStorage _fileStorage;

        public ProductsController(IUnitOfWork uof, IMapper mapper) //, IFileStorage fileStorage)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet("combobox")]
        public async Task<ActionResult<IEnumerable<ProductDTOCbb>>> GetCombo()
        {
            var lProducts = await _uof.ProductRepository.GetAllAsync();

            if (lProducts is null)
            {
                return NotFound();
            }

            var lProductsDTO = _mapper.Map<IEnumerable<ProductDTOCbb>>(lProducts);

            return Ok(lProductsDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTOList>>> GetProducts([FromQuery] FiltersProduct pagination)
        {
            var lProducts = await _uof.ProductRepository.GetProductsFullAsync(pagination);

            if (lProducts is null)
            {
                return NotFound();
            }

            //TODO: Melhorar essa forma de adicionar os parametros
            var metadata = new
            {
                lProducts.Count,
                lProducts.PageSize,
                lProducts.PageCount,
                lProducts.TotalItemCount,
                lProducts.HasNextPage,
                lProducts.HasPreviousPage
            };

            Response.Headers.Append("X-pagination", JsonConvert.SerializeObject(metadata));

            var lProductsDTO = _mapper.Map<IEnumerable<ProductDTOList>>(lProducts);

            return Ok(lProductsDTO);
        }
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _uof.ProductRepository.GetProductFullAsync(id);

            if (product == null)
            {
                return NotFound();
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

            var productExists = await _uof.ProductRepository.GetProductFullAsync(id);

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


            _uof.ProductRepository.Update(productExists);
            await _uof.CommitAsync();

            return Ok(productExists);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDTO productDTO)
        {
            Product newProduct = _mapper.Map<Product>(productDTO);

            if (newProduct is null)
            {
                return NotFound();
            }

            //foreach (var productImage in productDTO.ProductImages!)
            //{
            //    var photoProduct = Convert.FromBase64String(productImage);
            //    newProduct.lProductImages.Add(new ProductImage { Image = await _fileStorage.SaveFileAsync(photoProduct, ".jpg", "products") });
            //}

            foreach (var productCategoryId in productDTO.ProductCategoryIds!)
            {
                var category = await _uof.CategoryRepository.GetAsync(x => x.Id == productCategoryId);

                newProduct.lProductCategories!.Add(new ProductCategory { Category = category! });
            }

            _uof.ProductRepository.Create(newProduct);
            await _uof.CommitAsync();

            return CreatedAtAction("GetProduct", new { id = newProduct.Id }, newProduct);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
        {
            var product = await _uof.ProductRepository.GetAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productDeleted = _mapper.Map<ProductDTO>(_uof.ProductRepository.Delete(product));
            await _uof.CommitAsync();

            return Ok(productDeleted);
        }
    }
}


