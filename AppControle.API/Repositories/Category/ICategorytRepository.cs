using APICatalogo.Repositories;
using Shared.DTO.Pagination;
using Shared.Entities;
using X.PagedList;

namespace AppControle.API.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IPagedList<Category>> GetCategoryPaginationAsync(FiltersCategory pagination);
    }
}
