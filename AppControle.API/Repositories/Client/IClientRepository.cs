using APICatalogo.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Pagination;
using Shared.Entities;
using System;
using X.PagedList;

namespace AppControle.API.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<IPagedList<Client>> GetAllPaginationByUserAsync(FiltersClient pagination, string sid);
        Task<Client> GetFullClientAsync(int ClientId, string sid);

    }
}
