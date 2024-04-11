using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Pagination;
using Shared.Entities;
using X.PagedList;

namespace AppControle.API.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<List<City>> GetCityByStateIdAsync(int stateId);
    }
}
