using APICatalogo.Repositories;
using Library.Domain.Entities;

namespace Library.Domain.Abstractions
{
    public interface ICountryRepository : IRepository<Country>
    {

    }
}

//TODO: Alterar todos os gets para GetCountryAsync