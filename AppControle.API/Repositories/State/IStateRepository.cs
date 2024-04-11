using APICatalogo.Repositories;
using Shared.Entities;

namespace AppControle.API.Repositories
{
    public interface IStateRepository : IRepository<State>
    {
        Task<List<State>> GetStateByCountryIdAsync(int countryId);
    }
}
