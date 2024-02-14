using APICatalogo.Repositories;
using AppControle.Shared.Entities;
using AppControle.Shared.Entities.Pagination;
using AppControle.Shared.Entities.Pagination.Pagination;
using X.PagedList;

namespace AppControle.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IPagedList<Product>> GetProductsFullAsync(FiltersProduct pagination);
        Task<Product?> GetProductFullAsync(int id);

    }
}
