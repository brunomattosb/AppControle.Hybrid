using APICatalogo.Repositories;
using Shared.Entities;

namespace AppControle.API.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
       
    }
}

//TODO: Alterar todos os gets para GetCountryAsync