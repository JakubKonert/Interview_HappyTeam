using AutoMapper;
using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Car;
using Interview_HappyTeam_Backend.Core.DataTransferObject.CarModel;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Location;
using Interview_HappyTeam_Backend.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Interview_HappyTeam_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private AppDbContext _context { get; set; }
        private IMapper _mapper { get; }
        public CarController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCar([FromBody] CarCreateDTO car)
        {
            try
            {
                CarModel? carModel = _context.CarModels.Find(car.CarModelId);
                if (carModel == null)
                {
                    return BadRequest($"CarModel with that id = {car.CarModelId} does not exist!");
                }
                Car newCar = _mapper.Map<Car>(car);
                newCar.CarModel = carModel;

                await _context.Cars.AddAsync(newCar);
                await _context.SaveChangesAsync();
                return Ok(newCar);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadAll")]
        public async Task<ActionResult<IEnumerable<CarReadDTO>>> ReadAllCarModels()
        {
            try
            {
                List<Car> cars = await _context.Cars.ToListAsync();
                IEnumerable<CarReadDTO> mappedCars = _mapper.Map<IEnumerable<CarReadDTO>>(cars);

                return Ok(mappedCars);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadById")]
        public async Task<ActionResult<CarModelReadDTO>> ReadCarById(Guid id)
        {
            try
            {
                Car? car = _context.Cars.Find(id);
                if (car != null)
                {
                    CarModelReadDTO mappedCar = _mapper.Map<CarModelReadDTO>(car);
                    return Ok(mappedCar);
                }

                return BadRequest($"CarM with that id = {id} does not exist!");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadByCarModelId")]
        public async Task<ActionResult<IEnumerable<CarReadDTO>>> ReadAllCarByCarModelId(Guid id)
        {
            try
            {
                List<Car> cars = await _context.Cars.Where(car => car.CarModel.CarModelId == id).ToListAsync();
                IEnumerable<CarReadDTO> mappedCars = _mapper.Map<IEnumerable<CarReadDTO>>(cars);

                if (mappedCars.Count() > 0)
                {
                    return Ok(mappedCars);
                }
                return BadRequest($"CarModel with that id = {id} does not exist!");

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadAvailable")]
        public async Task<ActionResult<IEnumerable<CarReadDTO>>> ReadAllCarsAvailable(bool isAvailable = true)
        {
            try
            {
                List<Car> cars = await _context.Cars.Where(car => car.CarModel.IsAvailable == isAvailable).ToListAsync();
                IEnumerable<CarReadDTO> mappedCars = _mapper.Map<IEnumerable<CarReadDTO>>(cars);

                return Ok(mappedCars);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("AssignOrder")]
        public async Task<IActionResult> AssignOrder(Guid orderId, Guid carId)
        {
            try
            {
                Order? order = _context.Orders.Find(orderId);
                Car? car = _context.Cars.Find(carId);
                if (order == null || car == null)
                {
                    return BadRequest($"Order with that id = {orderId} or Car with id = {carId} does not exist!");
                }

                car.OrderId = orderId;
                car.Order = order;
                _context.Cars.Entry(car).State = EntityState.Modified;
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
        public async Task<IActionResult> DeleteCarById(Guid id)
        {
            try
            {
                Car? car = _context.Cars.Find(id);
                if (car != null)
                {
                    _context.Cars.Remove(car);
                    return Ok();
                }

                return BadRequest($"Car with id = {id} does not exist!");
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
