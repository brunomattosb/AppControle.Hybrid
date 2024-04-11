using APICatalogo.Repositories;
using AppControle.API.Context;
using Shared.Entities;
using X.PagedList;
using Shared.DTO.Pagination;

namespace AppControle.API.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        public CategoryRepository(DataContext context) : base(context)
        {

        }

        public async Task<IPagedList<Category>> GetCategoryPaginationAsync(FiltersCategory pagination)
        {
            var queryable = _context.Categories
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Name))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Name.ToLower()));
            }

            var category = await queryable.ToPagedListAsync(pagination.PageNumber,
                                                                pagination.PageSize);

            if (category == null)
            {
                return null!;
            }

            return category!;

        }

    }
}
