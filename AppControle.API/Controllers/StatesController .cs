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
public class StatesController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public StatesController(IUnitOfWork uof, ILogger<CategoriesController> logger, IMapper mapper)
    {
        _logger = logger;
        _uof = uof;
        _mapper = mapper;
    }

    // GET: api/States/combobox/1
    [AllowAnonymous]
    [HttpGet("combobox/{countryId:int}")]
    public async Task<ActionResult> GetCombo(int countryId)
    {
        var lState = await _uof.StateRepository.GetStateByCountryIdAsync(countryId);

        if (lState is null)
        {
            return NotFound();
        }

        var lStateDTO = _mapper.Map<IEnumerable<StateDTO>>(lState);

        return Ok(lStateDTO);
    }

    // GET: api/States/5
    [HttpGet("{id}")]
    public async Task<ActionResult<State>> GetState(int id)
    {
        var state = await _uof.StateRepository.GetAsync(c => c.Id == id);

        if (state == null)
        {
            return NotFound();
        }

        var StateReturn = _mapper.Map<StateDTO>(state);

        return Ok(StateReturn);


    }


}
