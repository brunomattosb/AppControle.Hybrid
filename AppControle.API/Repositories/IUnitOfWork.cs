using APICatalogo.Repositories;
using AppControle.API.Repositories;
using Shared.Entities;

namespace AppControle.API.Repositories;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ICityRepository CityRepository { get; }
    IStateRepository StateRepository { get; }
    ICountryRepository CountryRepository { get; }
    Task CommitAsync();
}
