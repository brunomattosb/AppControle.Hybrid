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
    public class StatesController : ControllerBase
    {
        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }
        // GET: api/States/all
        [AllowAnonymous]
        [HttpGet("combobox/{countryId:int}")]
        public async Task<ActionResult> GetCombo(int countryId)
        {
            return Ok(await _context.States
                .Where(x => x.CountryId == countryId)
                .ToListAsync());
        }
        // GET: api/States
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<State>>> GetStates([FromQuery] Pagination pagination, [FromQuery] string? filter)
        //{
        //    if (_context.States == null)
        //    {
        //        return NotFound();
        //    }
        //    var queryable = _context.States
        //        .Include(x => x.Cities)
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

        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(int id)
        {
            if (_context.States == null)
            {
                return NotFound();
            }
            var State = await _context.States
                .Include(x => x.Cities!)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (State == null)
            {
                return NotFound();
            }

            return State;
        }

        // PUT: api/States/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutState(State State)
        //{
        //    _context.Entry(State).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StateExists(State.Id))
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
        //            return BadRequest("Já existe um Estado com o mesmo nome.");
        //        }
        //        return BadRequest(e.InnerException.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    return NoContent();
        //}

        // POST: api/States
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<State>> PostState(State State)
        //{
        //    if (_context.States == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.States'  is null.");
        //    }
        //    try
        //    {
        //        _context.States.Add(State);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetState", new { id = State.Id }, State);
        //    }
        //    catch (DbUpdateException e)
        //    {
        //        if (e.InnerException!.Message.Contains("Duplicate"))
        //        {
        //            return BadRequest("Já existe um Estado com o mesmo nome.");
        //        }
        //        return BadRequest(e.InnerException.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        // DELETE: api/States/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteState(int id)
        //{
        //    if (_context.States == null)
        //    {
        //        return NotFound();
        //    }
        //    var State = await _context.States.FindAsync(id);
        //    if (State == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.States.Remove(State);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool StateExists(int id)
        //{
        //    return (_context.States?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
