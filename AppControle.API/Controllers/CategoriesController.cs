using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppControle.API.Context;
using AppControle.Shared.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AppControle.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(string? filter)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            //if (user == null)
            //{
            //    return BadRequest("User not valid.");
            //}

            var queryable = _context.Categories.AsQueryable();
            //queryable = queryable.Where(s => s.User!.Email == User.FindFirstValue(ClaimTypes.Email)!);

            if (!string.IsNullOrEmpty(filter))
            {
                queryable = queryable.Where(x => x.Name!.Contains(filter));
            }

            var lCategories = await queryable
                .OrderBy(x => x.Name)
                .ToListAsync();

            if(lCategories is null)
            {
                return NotFound();
            }
            return lCategories;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            //if (user == null)
            //{
            //    return BadRequest("User not valid.");
            //}
            var category = await _context.Categories.FindAsync(id);


            if (category == null)
            {
                return NotFound();
            }
            else
            {
                //if (category.UserId != user.Id)
                //{
                //    return BadRequest("User not valid.");
                //}
            }

            return category;
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProduto(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var categoryExists = await _context.Categories.FindAsync(id);

            if (categoryExists == null)
            {
                return NotFound();
            }

            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            //if (user == null || user.Id != categoryExists.UserId)
            //{
            //    return BadRequest("User not valid.");
            //}

            categoryExists.Name = category.Name;

            try
            {
                _context.Entry(categoryExists).State = EntityState.Modified;
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            //if (user == null)
            //{
            //    return BadRequest("User not valid.");
            //}
            
            _context.Add(category);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCategory", new { id = category.Id }, category);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
                {
                    return BadRequest("Já existe uma catgoria com o mesmo nome.");
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            //if (user == null)
            //{
            //    return BadRequest("User not valid.");
            //}

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            //if(user.Id != category.UserId)
            //{
            //    return NotFound();
            //}

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
