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
public class CountriesController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public CountriesController(IUnitOfWork uof, ILogger<CategoriesController> logger, IMapper mapper)
    {
        _logger = logger;
        _uof = uof;
        _mapper = mapper;
    }

    // GET: api/Countries/combobox
    [AllowAnonymous]
    [HttpGet("combobox")]
    public async Task<ActionResult<IEnumerable<Country>>> GetCombo()
    {
        var lCountry = await _uof.CountryRepository.GetAllNoPaginationAsync();

        if (lCountry is null)
        {
            return NotFound();
        }

        var lCountryDTO = _mapper.Map<IEnumerable<CountryDTO>>(lCountry);

        return Ok(lCountryDTO);

    }


}
