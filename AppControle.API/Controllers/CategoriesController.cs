﻿using Shared.Entities;
using AppControle.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shared.DTO.EntitiesDTO;
using Shared.DTO.Pagination;
using Shared.Response.Headers;
using System.Drawing.Printing;

namespace AppControle.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork uof, ILogger<CategoriesController> logger, IMapper mapper)
        {
            _logger = logger;
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        //TODO: Exemplo de um filtro
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var lCategories = await _uof.CategoryRepository.GetAllNoPaginationAsync();

            if (lCategories is null)
            {
                return NotFound();
            }

            var lCategoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(lCategories);

            return Ok(lCategoriesDTO);
        }

        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesPagination([FromQuery] FiltersCategory pagination)
        {
            var lCategories = await _uof.CategoryRepository.GetCategoryPaginationAsync(pagination);

            Response.Headers.Append("X-pagination", JsonConvert.SerializeObject(new ResponseHeaderPagination
            {
                Count = lCategories.Count,
                PageSize = lCategories.PageSize,
                PageCount = lCategories.PageCount,
                TotalItemCount = lCategories.TotalItemCount,
                HasNextPage = lCategories.HasNextPage,
                HasPreviousPage = lCategories.HasPreviousPage
            }));

            if (lCategories is null)
            {
                return NotFound();
            }

            var lCategoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(lCategories);

            return Ok(lCategoriesDTO);
        }
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _uof.CategoryRepository.GetAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var CategoriesDTO = _mapper.Map<CategoryDTO>(category);

            return Ok(CategoriesDTO);
        }


        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var categoryExists = await _uof.CategoryRepository.GetAsync(c => c.Id == id);

            if (categoryExists == null)
            {
                return NotFound();
            }

            categoryExists.Name = category.Name;

            _uof.CategoryRepository.Update(categoryExists);
            await _uof.CommitAsync();

            var categoryDTO = _mapper.Map<CategoryDTO>(categoryExists);

            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
            {
                return BadRequest("Dados inválidos");
            }

            var category = _mapper.Map<Category>(categoryDTO);

            var newCategory = _uof.CategoryRepository.Create(category);
            await _uof.CommitAsync();
            return CreatedAtAction("GetCategory", new { id = newCategory.Id }, newCategory);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<CategoryDTO>> DeleteCategory(int id)
        {
            var category = await _uof.CategoryRepository.GetAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDeleted = _mapper.Map<CategoryDTO>(_uof.CategoryRepository.Delete(category));
            await _uof.CommitAsync();

            return Ok(categoryDeleted);
        }
    }
}
