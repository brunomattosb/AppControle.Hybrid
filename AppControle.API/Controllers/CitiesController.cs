using AppControle.API.Context;
using Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppControle.API.Repositories;
using AutoMapper;
using Shared.DTO.EntitiesDTO;

namespace AppControle.API.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    public CitiesController(IUnitOfWork uof, ILogger<CategoriesController> logger, IMapper mapper)
    {
        _logger = logger;
        _uof = uof;
        _mapper = mapper;
    }

    // GET: api/Cities/combobox
    [AllowAnonymous]
    [HttpGet("combobox/{stateId:int}")]
    public async Task<ActionResult> GetCombo(int stateId)
    {
        var lCities = await _uof.CityRepository.GetCityByStateIdAsync(stateId);

        if (lCities is null)
        {
            return NotFound();
        }

        var lCitiesDTO = _mapper.Map<IEnumerable<CityDTO>>(lCities);

        return Ok(lCitiesDTO);
    }

    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetCity(int id)
    {
        var city = await _uof.CityRepository.GetAsync(c => c.Id == id);

        if (city == null)
        {
            return NotFound();
        }

        var CityReturn = _mapper.Map<CityDTO>(city);

        return Ok(CityReturn);

    }

}

