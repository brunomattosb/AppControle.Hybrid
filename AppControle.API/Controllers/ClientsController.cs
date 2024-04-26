using AppControle.API.Extensions;
using AppControle.API.Repositories;
using AppControle.Shared.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using Newtonsoft.Json;
using Shared.DTO.EntitiesDTO;
using Shared.DTO.Pagination;
using Shared.Entities;
using Shared.Response.Headers;
using System.Linq;
using System.Security.Claims;

namespace AppControle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ClientsController(IUnitOfWork uof, ILogger<CitiesController> logger, IMapper mapper)
        {
            _logger = logger;
            _uof = uof;
            _mapper = mapper;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClientPagination([FromQuery] FiltersClient pagination)
        {
            var username = User.FindFirstValue(ClaimTypes.Email);

            var lClients = await _uof.ClientRepository.GetAllPaginationByUserAsync(pagination, username);
            Response.Headers.Append("X-pagination", JsonConvert.SerializeObject(new ResponseHeaderPagination
            {
                Count = lClients.Count,
                PageSize = lClients.PageSize,
                PageCount = lClients.PageCount,
                TotalItemCount = lClients.TotalItemCount,
                HasNextPage = lClients.HasNextPage,
                HasPreviousPage = lClients.HasPreviousPage
            }));

            return Ok(lClients);

        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var Sid = User.FindFirstValue(ClaimTypes.Sid);

            var Client = await _uof.ClientRepository.GetFullClientAsync(id, Sid!);

            if (Client is null)
            {
                return NotFound("Cliente não encontrado...");
            }

            return Ok(Client);
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut()]
        public async Task<IActionResult> PutClient(Client client)
        {
            var username = User.FindFirstValue(ClaimTypes.Email);
            //TODO: Fazer as validações para deixar realizar as operacoes apenas do usuario
            //Acho que se eu enviar o usernam no Update e depois verificar da certo!

            var produtoAtualizado = _uof.ClientRepository.Update(client);
            await _uof.CommitAsync();

            return Ok(produtoAtualizado);


        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientDTO>> PostClient(ClientDTO clientDTO)
        {

            if (clientDTO is null)
                return BadRequest();

            var client = _mapper.Map<Client>(clientDTO);

            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

            client.RegisterDate = DateTime.UtcNow;
            client.UserId = userId;

            var newClient = _uof.ClientRepository.Create(client);


            await _uof.CommitAsync();

            return new CreatedAtRouteResult("GetClient",
                new { id = newClient.Id }, newClient);


            //    catch (DbUpdateException dbUpdateException)
            //    {
            //        if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
            //        {
            //            return BadRequest("Já existe um cliente com o mesmo CPF.");
            //        }
            //        else
            //        {
            //            return BadRequest(dbUpdateException.InnerException.Message);
            //        }
            //    }
            //    catch (Exception exception)
            //    {
            //        return BadRequest(exception.Message);
            //    }


        }
        //// DELETE: api/Clients/5
        //[HttpDelete("deleteclientservice/{id}")]
        //public async Task<IActionResult> DeleteClientService(int id)
        //{
        //    if (_context.ClientService == null)
        //    {
        //        return NotFound();
        //    }
        //    var service = await _context.ClientService.FindAsync(id);
        //    if (service == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ClientService.Remove(service);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _uof.ClientRepository.GetAsync(x => x.Id == id);

            if (client is null)
            {
                return NotFound("Cliente não encontrado...");
            }

            var deletedClient = _uof.ClientRepository.Delete(client);
            await _uof.CommitAsync();

            return Ok(deletedClient);
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("saveclientservice")]
        //public async Task<ActionResult<ClientService>> PostClientService(ClientService clientProd)
        //{
        //    try
        //    {
        //        clientProd.StartDate = DateTime.Now;

        //        if (_context.ClientService == null)
        //        {
        //            return Problem("Entity set 'DataContext.Client'  is null.");
        //        }
        //        _context.ClientService.Add(clientProd);
        //        await _context.SaveChangesAsync();

        //        return Ok();
        //    }
        //    catch (DbUpdateException dbUpdateException)
        //    {
        //        if (dbUpdateException.InnerException!.Message.ToLower().Contains("duplicate"))
        //        {
        //            return BadRequest("Já existe um cliente com o mesmo CPF.");
        //        }
        //        else
        //        {
        //            return BadRequest(dbUpdateException.InnerException.Message);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        return BadRequest(exception.Message);
        //    }


        //}
    }
}
