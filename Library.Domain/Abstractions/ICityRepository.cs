using APICatalogo.Repositories;
using Library.Domain.Entities;
namespace Library.Domain.Abstractions
{
    public interface ICityRepository : IRepository<City>
    {
        Task<List<City>> GetCityByStateIdAsync(int stateId);
    }
}
