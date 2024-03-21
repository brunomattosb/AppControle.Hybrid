
using APICatalogo.Repositories;
using AppControle.API.Context;
using Shared.Entities;

namespace AppControle.API.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private IProductRepository? _produtoRepo;
    private ICategoryRepository? _categoriaRepo;

    public DataContext _context;
    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
    public IProductRepository ProductRepository
    {
        get
        {
            return _produtoRepo ??= new ProductRepository(_context);
            //if (_produtoRepo == null)
            //{
            //    _produtoRepo = new ProdutoRepository(_context);
            //}
            //return _produtoRepo;
        }
    }
    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoriaRepo ??= new CategoryRepository(_context);
        }
    }
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}
