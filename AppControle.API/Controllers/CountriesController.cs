using AppControle.API.Data;
using AppControle.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SisVendas.API.Data.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Countries/combobox
        [AllowAnonymous]
        [HttpGet("combobox")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCombo()
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            return await _context.Countries.ToListAsync();
        }

        // GET: api/Countries
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Country>>> GetCountries([FromQuery] Pagination pagination, [FromQuery] string? filter)
        //{
        //    if (_context.Countries == null)
        //    {
        //        return NotFound();
        //    }
        //    var queryable = _context.Countries
        //        .Include(x => x.States)
        //        .AsQueryable();

        //    if (!string.IsNullOrEmpty(filter))
        //    {
        //        queryable = queryable.Where(x => x.Name.Contains(filter));
        //    }

        //    await HttpContext.InsertParamsInPageResponse(queryable, pagination.QuantityPerPage);

        //    return await queryable
        //        .OrderBy(x => x.Name)
        //        .Paginar(pagination)
        //        .ToListAsync();
        //}

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            var country = await _context.Countries
                .Include(x => x.States!)
                .ThenInclude(x => x.Cities)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCountry(Country country)
        //{
        //    _context.Entry(country).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CountryExists(country.Id))
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
        //            return BadRequest("Já existe um Pais com o mesmo nome.");
        //        }
        //        return BadRequest(e.InnerException.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    return NoContent();
        //}

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Country>> PostCountry(Country country)
        //{
        //    if (_context.Countries == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.Countries'  is null.");
        //    }
        //    try
        //    {
        //        _context.Countries.Add(country);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        //    }
        //    catch (DbUpdateException e)
        //    {
        //        if (e.InnerException!.Message.Contains("Duplicate"))
        //        {
        //            return BadRequest("Já existe um Pais com o mesmo nome.");
        //        }
        //        return BadRequest(e.InnerException.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        // DELETE: api/Countries/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCountry(int id)
        //{
        //    if (_context.Countries == null)
        //    {
        //        return NotFound();
        //    }
        //    var country = await _context.Countries.FindAsync(id);
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Countries.Remove(country);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CountryExists(int id)
        //{
        //    return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
