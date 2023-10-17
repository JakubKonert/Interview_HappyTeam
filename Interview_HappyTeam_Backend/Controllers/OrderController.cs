using AutoMapper;
using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Client;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Order;
using Interview_HappyTeam_Backend.Core.Entities;
using Interview_HappyTeam_Backend.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interview_HappyTeam_Backend.Controllers
{
    public class OrderController : Controller
    {
        private AppDbContext _context { get; set; }
        private IMapper _mapper { get; }
        public OrderController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO order)
        {
            try
            {

                if (order.StartDate > order.EndDate)
                {
                    return BadRequest($"Start date cannot be later than end date");
                }

                Order newOrder = _mapper.Map<Order>(order);

                Client? client = _context.Clients.Find(order.ClientId);
                Location? locationStart = _context.Locations.Find(order.LocationIdStart);
                Location? locationEnd = _context.Locations.Find(order.LocationIdEnd);
                Car? car = _context.Cars.Find(order.CarId);

                if (client == null)
                {
                    return BadRequest($"Client with provided id = {order.ClientId} does not exist!");
                }
                else if (locationStart == null) 
                {
                    return BadRequest($"Location start with provided id = {order.LocationIdStart} does not exist!");
                }
                else if (locationEnd == null) 
                {
                    return BadRequest($"Location end with provided id = {order.LocationIdEnd} does not exist!");
                }
                else if (car == null)
                {
                    return BadRequest($"Car with provided id = {order.CarId} does not exist!");
                }

                int days = (order.EndDate - newOrder.StartDate).Days;
                double calculatedPrice = days > 1 ? days * car.CarModel.Price : car.CarModel.Price;

                newOrder.Client = client;
                newOrder.LocationStart = locationStart;
                newOrder.LocationEnd = locationEnd;
                newOrder.Car = car;
                newOrder.TotalPrice = calculatedPrice;
                
                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadAll")]
        public async Task<ActionResult<IEnumerable<ClientReadDTO>>> ReadAllOrders()
        {
            try
            {
                List<Order> orders = await _context.Orders.ToListAsync();
                IEnumerable<OrderReadDTO> mappedOrders = _mapper.Map<IEnumerable<OrderReadDTO>>(orders);

                return Ok(mappedOrders);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadById")]
        public async Task<ActionResult<OrderReadDTO>> ReadOrderById(Guid id)
        {
            try
            {
                Order? order = _context.Orders.Find(id);
                if (order != null)
                {
                    OrderReadDTO mappedOrder = _mapper.Map<OrderReadDTO>(order);
                    return Ok(mappedOrder);
                }

                return BadRequest($"Order with that id = {id} does not exist!");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadByStatus")]
        public async Task<ActionResult<IEnumerable<ClientReadDTO>>> ReadAllOrdersByStatus(OrderStatus status)
        {
            try
            {
                List<Order> orders = await _context.Orders.Where(order => order.Status == status).ToListAsync();
                IEnumerable<OrderReadDTO> mappedOrders = _mapper.Map<IEnumerable<OrderReadDTO>>(orders);

                if (mappedOrders.Count() > 0)
                {
                    return Ok(mappedOrders);
                }
                return BadRequest($"Orders with status = {status} does not exist!");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("DeleteById")]
        public async Task<IActionResult> DeleteOrderById(Guid id)
        {
            try
            {
                Order? order = _context.Orders.Find(id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    return Ok();
                }

                return BadRequest($"Order with id = {id} does not exist!");
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
