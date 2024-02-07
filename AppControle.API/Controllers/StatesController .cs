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

        // GET: api/States/combobox/1
        [AllowAnonymous]
        [HttpGet("combobox/{countryId:int}")]
        public async Task<ActionResult> GetCombo(int countryId)
        {
            return Ok(await _context.States
                .Where(x => x.CountryId == countryId)
                .ToListAsync());
        }        

        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(int id)
        {
            if (_context.States == null)
            {
                return NotFound();
            }
            var State = await _context.States
                .Include(x => x.lCities!)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (State == null)
            {
                return NotFound();
            }

            return State;
        }

        
    }
}
