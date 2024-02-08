using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppControle.API.Context;
using AppControle.Shared.Entities;
using Microsoft.AspNetCore.Authorization;

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

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Product>> GetProduct(int id)
        //{
        //    //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
        //    //if (user == null)
        //    //{
        //    //    return BadRequest("User not valid.");
        //    //}

        //    var product = await _context.Products
        //        //.Include(x => x.lProductImages)
        //        .Include(x => x.lProductCategories!)
        //        .ThenInclude(x => x.Category)
        //        //.Where(x => x.User!.Id == user.Id)
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        //if (category.UserId != user.Id)
        //        //{
        //        //    return BadRequest("User not valid.");
        //        //}
        //    }

        //    return product;
        //}


        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> PutProduto(int id, Category category)
        //{
        //    if (id != category.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var categoryExists = await _context.Categories.FindAsync(id);

        //    if (categoryExists == null)
        //    {
        //        return NotFound();
        //    }

        //    //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
        //    //if (user == null || user.Id != categoryExists.UserId)
        //    //{
        //    //    return BadRequest("User not valid.");
        //    //}

        //    categoryExists.Name = category.Name;

        //    try
        //    {
        //        _context.Entry(categoryExists).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException dbUpdateException)
        //    {
        //        if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
        //        {
        //            return BadRequest("Já existe uma categoria com o mesmo nome.");
        //        }
        //        else
        //        {
        //            return BadRequest(dbUpdateException.InnerException.Message);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        return BadRequest(exception.Message);
        //    }

        //    return NoContent();
        //}

        //[HttpPost]
        //public async Task<ActionResult<Category>> PostCategory(Category category)
        //{
        //    //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
        //    //if (user == null)
        //    //{
        //    //    return BadRequest("User not valid.");
        //    //}
            
        //    _context.Add(category);

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        //    }
        //    catch (DbUpdateException dbUpdateException)
        //    {
        //        if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
        //        {
        //            return BadRequest("Já existe uma catgoria com o mesmo nome.");
        //        }
        //        else
        //        {
        //            return BadRequest(dbUpdateException.InnerException.Message);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        return BadRequest(exception.Message);
        //    }
        //}

        [HttpDelete("{id}")]
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
