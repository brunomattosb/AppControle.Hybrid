using APICatalogo.Repositories;
using Library.Domain.Entities;
using Library.Domain.Filters;
using X.PagedList;

namespace Library.Domain.Abstractions
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IPagedList<Category>> GetCategoryPaginationAsync(FiltersCategory pagination);
    }
}
