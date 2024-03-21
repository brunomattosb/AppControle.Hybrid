using APICatalogo.Repositories;
using Shared.Entities;
using Shared.Entities.Pagination;
using Shared.Entities.Pagination.Pagination;
using X.PagedList;

namespace AppControle.API.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IPagedList<Category>> GetCategoryPaginationAsync(FiltersCategory pagination);
    }
}
