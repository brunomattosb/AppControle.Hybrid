using AppControle.API.Data;
using AppControle.API.Extensions;
using AppControle.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AppControle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientsController : ControllerBase
    {

        private readonly DataContext _context;

        public ClientsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient([FromQuery] Pagination pagination, [FromQuery] string? filter)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }

            if (_context.Clients == null)
            {
                return NotFound();
            }
            var queryable = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                queryable = queryable.Where(x => x.Name!.Contains(filter));
            }

            queryable = queryable.Where(s => s.User!.Email == User.FindFirstValue(ClaimTypes.Email)!);


            await HttpContext.InsertParamsInPageResponse(queryable, pagination.QuantityPerPage);

            return await queryable
                .OrderBy(x => x.Name)
                .Paginar(pagination)
                .ToListAsync();
        }

        //// GET: api/Clients/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Client>> GetClient(int id)
        //{

        //    if (_context.Clients == null)
        //    {
        //        return NotFound();
        //    }

        //    var client = await _context.Clients
        //        .Include(u => u.City!)
        //        .ThenInclude(c => c.State!)
        //        .ThenInclude(s => s.Country!)
        //        .Include(u => u.User!)
        //        .FirstOrDefaultAsync(x => x.Id == id && x.User!.Email == User.FindFirstValue(ClaimTypes.Email)!);

        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    return client;
        //}

        //// PUT: api/Clients/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut()]
        //public async Task<IActionResult> PutClient(Client client)
        //{
        //    _context.Entry(client).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    //catch (DbUpdateConcurrencyException)
        //    //{
        //    //    //if (!CityExists(city.Id))
        //    //    //{
        //    //    //    return NotFound();
        //    //    //}
        //    //    //else
        //    //    //{
        //    //    //    throw;
        //    //    //}
        //    //}
        //    catch (DbUpdateException e)
        //    {
        //        if (e.InnerException!.Message.Contains("Duplicate"))
        //        {
        //            return BadRequest("Já existe uma cliente com o mesmo nome.");
        //        }
        //        return BadRequest(e.InnerException.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    return NoContent();
        //}

        //// POST: api/Clients
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Client>> PostClient(Client client)
        //{
        //    try
        //    {
        //        if (_context.Clients == null)
        //        {
        //            return Problem("Entity set 'DataContext.Client'  is null.");
        //        }
        //        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email)!);
        //        if (user == null)
        //        {
        //            return BadRequest("Usuario no válido");

        //        }
        //        client.User = user;
        //        client.UserId = user.Id;

        //        client.RegisterDate = DateTime.UtcNow;
        //        _context.Clients.Add(client);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetClient", new { id = client.Id }, client);
        //    }
        //    catch (DbUpdateException dbUpdateException)
        //    {
        //        if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
        //        {
        //            return BadRequest("Já existe um cliente com o mesmo CPF.");
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

        //// DELETE: api/Clients/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteClient(int id)
        //{
        //    if (_context.Clients == null)
        //    {
        //        return NotFound();
        //    }
        //    var client = await _context.Clients.FindAsync(id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Clients.Remove(client);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ClientExists(int id)
        //{
        //    return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
        
    }
}
