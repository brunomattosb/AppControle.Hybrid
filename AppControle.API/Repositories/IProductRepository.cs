using APICatalogo.Repositories;
using Shared.Entities;
using Shared.Entities.Pagination;
using Shared.Entities.Pagination.Pagination;
using X.PagedList;

namespace AppControle.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IPagedList<Product>> GetProductsFullAsync(FiltersProduct pagination);
        Task<Product?> GetProductFullAsync(int id);

    }
}
