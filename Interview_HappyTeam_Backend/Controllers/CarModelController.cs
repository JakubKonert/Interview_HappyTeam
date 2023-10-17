using AutoMapper;
using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.DataTransferObject.CarModel;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Country;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Location;
using Interview_HappyTeam_Backend.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Interview_HappyTeam_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : Controller
    {
        private AppDbContext _context { get; set; }
        private IMapper _mapper { get; }

        public CarModelController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCarModel([FromBody] CarModelCreateDTO carModel)
        {
            try
            {
                CarModel newCarModel = _mapper.Map<CarModel>(carModel);

                await _context.CarModels.AddAsync(newCarModel);
                await _context.SaveChangesAsync();
                return Ok(newCarModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadAll")]
        public async Task<ActionResult<IEnumerable<CarModelReadDTO>>> ReadAllCarModels()
        {
            try
            {
                List<CarModel> carModels = await _context.CarModels.ToListAsync();
                IEnumerable<CarModelReadDTO> mappedCarsModels = _mapper.Map<IEnumerable<CarModelReadDTO>>(carModels);

                return Ok(mappedCarsModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadById")]
        public async Task<ActionResult<CarModelReadDTO>> ReadCarModelById(Guid id)
        {
            try
            {
                CarModel? carModel = _context.CarModels.Find(id);
                if (carModel != null)
                {
                    CarModelReadDTO mappedCarModel = _mapper.Map<CarModelReadDTO>(carModel);
                    return Ok(mappedCarModel);
                }

                return BadRequest($"CarModel with that id = {id} does not exist!");

            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadByBrand")]
        public async Task<ActionResult<IEnumerable<CarModelReadDTO>>> ReadAllCarModelsByBrand(string brand)
        {
            try
            {
                List<CarModel> carModels = await _context.CarModels.Where(carModel => carModel.Brand == brand).ToListAsync();
                IEnumerable<CarModelReadDTO> mappedCarModels = _mapper.Map<IEnumerable<CarModelReadDTO>>(carModels);

                if (mappedCarModels.Count() > 0)
                {
                    return Ok(mappedCarModels);
                }
                return BadRequest($"CarModel with brand = {brand} does not exist!");

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadByModel")]
        public async Task<ActionResult<IEnumerable<CarModelReadDTO>>> ReadAllCarModelsByModel(string model)
        {
            try
            {
                List<CarModel> carModels = await _context.CarModels.Where(carModel => carModel.Model == model).ToListAsync();
                IEnumerable<CarModelReadDTO> mappedCarModels = _mapper.Map<IEnumerable<CarModelReadDTO>>(carModels);

                if (mappedCarModels.Count() > 0)
                {
                    return Ok(mappedCarModels);
                }
                return BadRequest($"CarModel with model = {model} does not exist!");

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadAvailable")]
        public async Task<ActionResult<IEnumerable<LocationReadDTO>>> ReadAllCarModelAvailable(bool isAvailable = true)
        {
            try
            {
                List<CarModel> carModels = await _context.CarModels.Where(carModel => carModel.IsAvailable == isAvailable).ToListAsync();
                IEnumerable<CarModelReadDTO> mappedCarModels = _mapper.Map<IEnumerable<CarModelReadDTO>>(carModels);

                return Ok(mappedCarModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("UpdateBrand")]
        public async Task<IActionResult> UpdateCarModelBrand(Guid id, string brand)
        {
            try
            {
                if (brand.IsNullOrEmpty())
                {
                    return BadRequest($"CarModel brand cannot be empty!");
                }

                CarModel? carModel = _context.CarModels.Find(id);
                if (carModel != null)
                {
                    carModel.Brand = brand;
                    _context.CarModels.Entry(carModel).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();
                }

                return BadRequest($"CarModel with id = {id} does not exist!");
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("UpdateModel")]
        public async Task<IActionResult> UpdateCarModelModel(Guid id, string model)
        {
            try
            {
                if (model.IsNullOrEmpty())
                {
                    return BadRequest($"CarModel model cannot be empty!");
                }

                CarModel? carModel = _context.CarModels.Find(id);
                if (carModel != null)
                {
                    carModel.Model = model;
                    _context.CarModels.Entry(carModel).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();
                }

                return BadRequest($"CarModel with id = {id} does not exist!");
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("DeleteById")]
        public async Task<IActionResult> DeleteCarModelById(Guid id)
        {
            try
            {
                CarModel? carModel = _context.CarModels.Find(id);
                if (carModel != null)
                {
                    _context.CarModels.Remove(carModel);
                    return Ok();
                }

                return BadRequest($"CarModel with id = {id} does not exist!");
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
