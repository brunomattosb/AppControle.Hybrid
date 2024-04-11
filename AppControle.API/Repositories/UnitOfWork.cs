using AppControle.API.Context;

namespace AppControle.API.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private IProductRepository? _produtoRepo;
    private ICategoryRepository? _categoriaRepo;
    private ICityRepository? _cityRepository;
    private IStateRepository? _stateRepository;
    private ICountryRepository? _countryRepository;

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
    public ICityRepository CityRepository
    {
        get
        {
            return _cityRepository ??= new CityRepository(_context);
        }
    }public IStateRepository StateRepository
    {
        get
        {
            return _stateRepository ??= new StateRepository(_context);
        }
    }public ICountryRepository CountryRepository
    {
        get
        {
            return _countryRepository ??= new CountryRepository(_context);
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
