using APICatalogo.Repositories;
using AppControle.API.Context;
using Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppControle.API.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {

        public CountryRepository(DataContext context) : base(context) 
        {

        }


    }
}
