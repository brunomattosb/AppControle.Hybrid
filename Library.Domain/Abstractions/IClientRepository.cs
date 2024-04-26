using APICatalogo.Repositories;
using Library.Domain.Filters;
using Shared.Entities;
using X.PagedList;

namespace Library.Domain.Abstractions
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<IPagedList<Client>> GetAllPaginationByUserAsync(FiltersClient pagination, string sid);
        Task<Client> GetFullClientAsync(int ClientId, string sid);

    }
}
