using APICatalogo.Repositories;
using AppControle.API.Context;
using Shared.Entities;
using X.PagedList;
using Shared.DTO.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace AppControle.API.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {

        public CityRepository(DataContext context) : base(context) 
        {

        }

        public async Task<List<City>> GetCityByStateIdAsync(int stateId)
        {
            var queryable = _context.Cities
                .OrderBy(x => x.Name)
                .AsQueryable();

             queryable = queryable.Where(x => x.State!.Id == stateId);
            
            var lCities = await queryable.ToListAsync();

            if (lCities == null)
            {
                return null!;
            }

            return lCities!;

        }

    }
}
