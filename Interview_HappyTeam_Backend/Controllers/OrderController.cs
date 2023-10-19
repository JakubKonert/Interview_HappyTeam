using AutoMapper;
using Interview_HappyTeam_Backend.Core.Builders.OrderBuilder;
using Interview_HappyTeam_Backend.Core.CalcMath;
using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.Core;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Order;
using Interview_HappyTeam_Backend.Core.Entities;
using Interview_HappyTeam_Backend.Core.Enums;
using Interview_HappyTeam_Backend.Core.ErrorLogger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Interview_HappyTeam_Backend.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private AppDbContext _context { get; set; }
        private IMapper _mapper { get; }
        private Lazy<OrderBuilder> _builder = new Lazy<OrderBuilder>(() => OrderBuilder.Instance);
        public OrderController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            Setter.Set(context);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO order)
        {
            try
            {
                Order newOrder =  _builder.Value.StartBuilding()
                    .WithLocationStart(order.LocationStart)
                    .WithLocationEnd(order.LocationEnd)
                    .WithCar(order.Car)
                    .WithCountry(order.Country)
                    .WithStartDate(order.StartDate)
                    .WithEndtDate(order.EndDate)
                    .Build();

                newOrder.TotalPrice = CalcMath.CalcTotalPrice(newOrder.Car, newOrder.StartDate, newOrder.EndDate);

                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                ErrorLogger.LogError("OrderController -> CreateOrder", ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadAll")]
        public async Task<ActionResult<IEnumerable<OrderReadDTO>>> ReadAllOrders()
        {
            try
            {
                List<Order> orders = await _context.Orders.ToListAsync();
                IEnumerable<OrderReadDTO> mappedOrders = _mapper.Map<IEnumerable<OrderReadDTO>>(orders);

                return Ok(mappedOrders);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("OrderController -> ReadAllOrders", ex.Message);
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
            catch (Exception ex)
            {
                ErrorLogger.LogError("OrderController -> ReadOrderById", ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadByStatus")]
        public async Task<ActionResult<IEnumerable<OrderReadDTO>>> ReadAllOrdersByStatus(OrderStatus status)
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
                ErrorLogger.LogError("OrderController -> ReadAllOrdersByStatus", ex.Message);
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
                ErrorLogger.LogError("OrderController -> DeleteOrderById", ex.Message);
                return StatusCode(500);
            }
        }
    }
}
