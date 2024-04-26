using APICatalogo.Repositories;
using AppControle.API.Context;
using Shared.Entities;
using X.PagedList;
using Shared.DTO.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Linq.Expressions;
using System.Drawing;

namespace AppControle.API.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {

        public ClientRepository(DataContext context) : base(context)
        {

        }

        public async Task<IPagedList<Client>?> GetAllPaginationByUserAsync(FiltersClient pagination, string username)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == username);
            if (user == null || _context.Clients == null)
            {
                return null;
            }

            var queryable = _context.Clients
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Name))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Name.ToLower()));
            }
            queryable = queryable.Where(x => x.User!.Email == username);

            var Client = await queryable.ToPagedListAsync(pagination.PageNumber,
                                                                pagination.PageSize);

            if (Client == null)
            {
                return null!;
            }

            return Client!;

        }
        public async Task<Client?> GetFullClientAsync(int ClientId, string sid)
        {
            var client = await _context.Clients
                .Include(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country!)
                .Include(u => u.User!)
                //.Include(u => u.lClientService!)
                //.ThenInclude(x => x.Product)
                .Where(x => x.User!.Id == sid)
                .FirstOrDefaultAsync(x => x.Id == ClientId);

            return client;
        }

    }
}
