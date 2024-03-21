using APICatalogo.Repositories;
using AppControle.API.Context;
using Shared.Entities;
using Shared.Entities.Pagination;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AppControle.API.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(DataContext context) : base(context)
        {

        }

        public async Task<IPagedList<Product>> GetProductsFullAsync(FiltersProduct pagination)
        {

            var queryable = _context.Products
                .Include(x => x.lProductCategories!)
                .ThenInclude(x => x.Category)
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (pagination.Price.HasValue && !string.IsNullOrEmpty(pagination.PriceCriterion))
            {
                if (pagination.PriceCriterion.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    queryable = queryable.Where(p => p.Price > pagination.Price.Value).OrderBy(p => p.Price);
                }
                else if (pagination.PriceCriterion.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    queryable = queryable.Where(p => p.Price < pagination.Price.Value).OrderBy(p => p.Price);
                }
                else if (pagination.PriceCriterion.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    queryable = queryable.Where(p => p.Price == pagination.Price.Value).OrderBy(p => p.Price);
                }
            }

            var product = await queryable.ToPagedListAsync(pagination.PageNumber,
                                                                pagination.PageSize);

            if (product == null)
            {
                return null!;
            }

            return product!;
        }

        public async Task<Product?> GetProductFullAsync(int id)
        {
            var product = await _context.Products
                //.Include(x => x.lProductImages)
                .Include(x => x.lProductCategories!)
                .ThenInclude(x => x.Category)
                //.Where(x => x.User!.Id == user.Id)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return null;
            }

            return product;
        }

    }
}
