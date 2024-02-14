using APICatalogo.Repositories;
using AppControle.Shared.Entities;
using AppControle.Shared.Entities.Pagination;
using AppControle.Shared.Entities.Pagination.Pagination;
using X.PagedList;

namespace AppControle.API.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IPagedList<Category>> GetCategoryPaginationAsync(FiltersCategory pagination);
    }
}
