using APICatalogo.Repositories;
using AppControle.API.Context;
using Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppControle.API.Repositories
{
    public class StateRepository : Repository<State>, IStateRepository
    {

        public StateRepository(DataContext context) : base(context) 
        {

        }

        public async Task<List<State>> GetStateByCountryIdAsync(int countryId)
        {
            var queryable = _context.States!
                .OrderBy(x => x.Name)
                .AsQueryable();

             queryable = queryable.Where(x => x.CountryId == countryId);
            
            var lStates = await queryable.ToListAsync();

            if (lStates == null)
            {
                return null!;
            }

            return lStates!;

        }

    }
}
