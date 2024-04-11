using APICatalogo.Repositories;
using Shared.DTO.Pagination;
using Shared.Entities;
using X.PagedList;

namespace AppControle.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IPagedList<Product>> GetProductsFullAsync(FiltersProduct pagination);
        Task<Product?> GetProductFullAsync(int id);

    }
}
