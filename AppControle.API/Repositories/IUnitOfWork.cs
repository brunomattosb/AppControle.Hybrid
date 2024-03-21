using APICatalogo.Repositories;
using Shared.Entities;

namespace AppControle.API.Repositories;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    Task CommitAsync();
}
