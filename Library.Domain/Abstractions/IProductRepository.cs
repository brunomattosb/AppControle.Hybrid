using APICatalogo.Repositories;
using Library.Domain.Entities;
using Library.Domain.Filters;
using X.PagedList;

namespace Library.Domain.Abstractions
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IPagedList<Product>> GetProductsFullAsync(FiltersProduct pagination);
        Task<Product?> GetProductFullAsync(int id);

    }
}
