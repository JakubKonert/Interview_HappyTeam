using AutoMapper;
using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Client;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Country;
using Interview_HappyTeam_Backend.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interview_HappyTeam_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private AppDbContext _context { get; set; }
        private IMapper _mapper { get; }

        public ClientController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateClient([FromBody] ClientCreateDTO client)
        {
            try
            {
                Client newClient = _mapper.Map<Client>(client);
                await _context.Clients.AddAsync(newClient);
                await _context.SaveChangesAsync();
                return Ok(newClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadAll")]
        public async Task<ActionResult<IEnumerable<ClientReadDTO>>> ReadAllClients()
        {
            try
            {
                List<Client> clients = await _context.Clients.ToListAsync();
                IEnumerable<ClientReadDTO> mappedClients = _mapper.Map<IEnumerable<ClientReadDTO>>(clients);

                return Ok(mappedClients);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadById")]
        public async Task<ActionResult<CountryReadDTO>> ReadClientById(Guid id)
        {
            try
            {
                Client? client = _context.Clients.Find(id);
                if (client != null)
                {
                    ClientReadDTO mappedClient = _mapper.Map<ClientReadDTO>(client);
                    return Ok(mappedClient);
                }

                return BadRequest($"Client with that id = {id} does not exist!");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadByName")]
        public async Task<ActionResult<IEnumerable<ClientReadDTO>>> ReadAllClientsByName(string nick)
        {
            try
            {
                List<Client> countries = await _context.Clients.Where(client => client.Nick.ToLower().Contains(nick.ToLower())).ToListAsync();
                IEnumerable<CountryReadDTO> mappedCountries = _mapper.Map<IEnumerable<CountryReadDTO>>(countries);

                if (mappedCountries.Count() > 0)
                {
                    return Ok(mappedCountries);
                }
                return BadRequest($"Client with nick = {nick} does not exist!");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("AddOrderToUser")]
        public async Task<IActionResult> AddOrderToUser(Guid clientId, Guid orderId)
        {
            try
            {
                Client? client = _context.Clients.Find(clientId);
                Order? order = _context.Orders.Find(orderId);
                if (client == null || order == null)
                {
                    return BadRequest($"Client with id = {clientId} or Order with id = {orderId} does not exist!");
                }

                client.Orders.Add(order);
                _context.Clients.Entry(client).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("RemoveOrder")]
        public async Task<IActionResult> RemoveOrderFromClient(Guid clientId, Guid orderId)
        {
            try
            {
                Client? client = _context.Clients.Find(clientId);
                Order? order = _context.Orders.Find(orderId);
                if (client == null || order == null)
                {
                    return BadRequest($"Client with id = {clientId} or Order with id = {orderId} does not exist!");
                }
                client.Orders = new List<Order>(client.Orders.Where(order => order.Id != orderId));
                    _context.Clients.Entry(client).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();   
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("DeleteById")]
        public async Task<IActionResult> DeleteClientById(Guid id)
        {
            try
            {
                Client? client = _context.Clients.Find(id);
                if (client != null)
                {
                    _context.Clients.Remove(client);
                    return Ok();
                }

                return BadRequest($"Client with id = {id} does not exist!");
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
