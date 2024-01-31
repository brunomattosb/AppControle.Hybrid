using AppControle.API.Data;
using AppControle.API.Extensions;
using AppControle.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SisVendas.API.Data.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class MonthlyFeesController : ControllerBase
    {
        private readonly DataContext _context;

        public MonthlyFeesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonthlyFee>>> GetCategories([FromQuery] Pagination pagination, [FromQuery] string? filter, [FromQuery] int? month, [FromQuery] int? year)
        {
            var queryable = _context.MonthlyFee
                .Include(x => x.Client)
                .AsQueryable();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }

            if (!string.IsNullOrWhiteSpace(filter))
            {
                queryable = queryable.Where(x => x.Client.Name.ToLower().Contains(filter.ToLower()));
            }
            queryable = queryable.Where(s => s.User!.Email == User.FindFirstValue(ClaimTypes.Email)!);

            queryable = queryable.Where(s => s.Reference!.Value.Month == month!);
            queryable = queryable.Where(s => s.Reference!.Value.Year == year!);

            await HttpContext.InsertParamsInPageResponse(queryable, pagination.QuantityPerPage);
            return Ok(await queryable
                .OrderBy(x => x.Client.Name)
                .Paginar(pagination)
                .ToListAsync());
        }

        // GET: api/Cities/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<City>> GetCity(int id)
        //{
        //    if (_context.Cities == null)
        //    {
        //        return NotFound();
        //    }
        //    var City = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);

        //    if (City == null)
        //    {
        //        return NotFound();
        //    }

        //    return City;
        //}

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCity(City city)
        //{
        //    _context.Entry(city).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CityExists(city.Id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    catch (DbUpdateException e)
        //    {
        //        if (e.InnerException!.Message.Contains("Duplicate"))
        //        {
        //            return BadRequest("Já existe uma Cidade com o mesmo nome.");
        //        }
        //        return BadRequest(e.InnerException.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    return NoContent();
        //}

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<City>> PostCity(City City)
        //{
        //    if (_context.Cities == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.Cities'  is null.");
        //    }
        //    try
        //    {
        //        _context.Cities.Add(City);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetCity", new { id = City.Id }, City);
        //    }
        //    catch (DbUpdateException e)
        //    {
        //        if (e.InnerException!.Message.Contains("Duplicate"))
        //        {
        //            return BadRequest("Já existe um Cidade com o mesmo nome.");
        //        }
        //        return BadRequest(e.InnerException.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        // DELETE: api/Cities/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCity(int id)
        //{
        //    if (_context.Cities == null)
        //    {
        //        return NotFound();
        //    }
        //    var City = await _context.Cities.FindAsync(id);
        //    if (City == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Cities.Remove(City);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CityExists(int id)
        //{
        //    return (_context.Cities?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
