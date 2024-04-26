using Library.Domain.Abstractions;

namespace AppControle.API.Repositories;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ICityRepository CityRepository { get; }
    IStateRepository StateRepository { get; }
    ICountryRepository CountryRepository { get; }
    IClientRepository ClientRepository { get; }
    Task CommitAsync();
}
