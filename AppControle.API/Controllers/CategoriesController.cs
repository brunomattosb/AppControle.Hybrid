//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using AppControle.API.Data;
//using AppControle.API.Extensions;
//using AppControle.Shared.Entities;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace AppControle.API.Controllers
//{
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CategoriesController : ControllerBase
//    {
//        private readonly DataContext _context;

//        public CategoriesController(DataContext context)
//        {
//            _context = context;

//        }

//        // GET: api/Categories
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(string? filter)
//        {
//            if (_context.Categories == null)
//            {
//                return NotFound();
//            }
//            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
//            if (user == null)
//            {
//                return BadRequest("User not valid.");
//            }

//            var queryable = _context.Categories.AsQueryable();
//            queryable = queryable.Where(s => s.User!.Email == User.FindFirstValue(ClaimTypes.Email)!);

//            if (!string.IsNullOrEmpty(filter))
//            {
//                queryable = queryable.Where(x => x.Name.Contains(filter));
//            }

//            return await queryable
//                .OrderBy(x => x.Name)
//                .ToListAsync();
//        }

//        // GET: api/Categories/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Category>> GetCategory(int id)
//        {
//            if (_context.Categories == null)
//            {
//                return NotFound();
//            }
//            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
//            if (user == null)
//            {
//                return BadRequest("User not valid.");
//            }
//            var category = await _context.Categories.FindAsync(id);

            
//            if (category == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                if (category.UserId != user.Id)
//                {
//                    return BadRequest("User not valid.");
//                }
//            }

//            return category;
//        }

//        // PUT: api/Categories/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut]
//        public async Task<IActionResult> PutCategory(Category category)
//        {
//            _context.Update(category);
//            try
//            {
//                await _context.SaveChangesAsync();
//                return Ok(category);
//            }
//            catch (DbUpdateException dbUpdateException)
//            {
//                if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
//                {
//                    return BadRequest("Já existe um registro com o mesmo nome.");
//                }
//                else
//                {
//                    return BadRequest(dbUpdateException.InnerException.Message);
//                }
//            }
//            catch (Exception exception)
//            {
//                return BadRequest(exception.Message);
//            }
//        }

//        // POST: api/Categories
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Category>> PostCategory(Category category)
//        {
//            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
//            if (user == null)
//            {
//                return BadRequest("User not valid.");
//            }
//            category.User = user;
//            category.UserId = user.Id;

//            _context.Add(category);
//            try
//            {
//                await _context.SaveChangesAsync();
//                return Ok(category);
//            }
//            catch (DbUpdateException dbUpdateException)
//            {
//                if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
//                {
//                    return BadRequest("Já existe um registro com o mesmo nome.");
//                }
//                else
//                {
//                    return BadRequest(dbUpdateException.InnerException.Message);
//                }
//            }
//            catch (Exception exception)
//            {
//                return BadRequest(exception.Message);
//            }
//        }

//        // DELETE: api/Categories/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteCategory(int id)
//        {
//            if (_context.Categories == null)
//            {
//                return NotFound();
//            }
//            var category = await _context.Categories.FindAsync(id);
//            if (category == null)
//            {
//                return NotFound();
//            }

//            _context.Categories.Remove(category);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool CategoryExists(int id)
//        {
//            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
