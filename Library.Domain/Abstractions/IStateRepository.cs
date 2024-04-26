using APICatalogo.Repositories;
using Library.Domain.Entities;
using Shared.Entities;

namespace Library.Domain.Abstractions
{
    public interface IStateRepository : IRepository<State>
    {
        Task<List<State>> GetStateByCountryIdAsync(int countryId);
    }
}
