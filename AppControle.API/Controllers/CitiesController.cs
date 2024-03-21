using AppControle.API.Context;
using Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppControle.API.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly DataContext _context;

    public CitiesController(DataContext context)
    {
        _context = context;
    }

    // GET: api/Cities/combobox
    [AllowAnonymous]
    [HttpGet("combobox/{stateId:int}")]
    public async Task<ActionResult> GetCombo(int stateId)
    {
        return Ok(await _context.Cities
            .Where(x => x.StateId == stateId)
            .ToListAsync());
    }

    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetCity(int id)
    {
        if (_context.Cities == null)
        {
            return NotFound();
        }
        var City = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);

        if (City == null)
        {
            return NotFound();
        }

        return City;
    }

}

