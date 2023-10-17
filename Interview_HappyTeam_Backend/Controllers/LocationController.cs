using AutoMapper;
using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Location;
using Interview_HappyTeam_Backend.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Interview_HappyTeam_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private AppDbContext _context { get; set; }
        private IMapper _mapper { get; }

        public LocationController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateLocation([FromBody] LocationCreateDTO location)
        {
            try
            {
                Country? country = await _context.Countries.FindAsync(location.CountryId);
                if (country == null) 
                {
                    return BadRequest($"Country with id = {location.CountryId} does not exist!");
                }

                Location newLocation = _mapper.Map<Location>(location);              
                newLocation.Country = country;
                await _context.Locations.AddAsync(newLocation);
                await _context.SaveChangesAsync();
                return Ok(newLocation);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadAll")]
        public async Task<ActionResult<IEnumerable<LocationReadDTO>>> ReadAllLocations()
        {
            try
            {
                List<Location> locations = await _context.Locations.ToListAsync();
                IEnumerable<LocationReadDTO> mappedLocations = _mapper.Map<IEnumerable<LocationReadDTO>>(locations);

                return Ok(mappedLocations);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadById")]
        public async Task<ActionResult<LocationReadDTO>> ReadLocationById(Guid id)
        {
            try
            {
                Location? location = _context.Locations.Find(id);
                if (location != null)
                {
                    LocationReadDTO mappedLocation = _mapper.Map<LocationReadDTO>(location);
                    return Ok(mappedLocation);
                }

                return BadRequest($"Location with that id = {id} does not exist!");

            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadByName")]
        public async Task<ActionResult<IEnumerable<LocationReadDTO>>> ReadAllLocationsByName(string name)
        {
            try
            {
                List<Location> locations = await _context.Locations.Where(location => location.Name == name).ToListAsync();
                IEnumerable<LocationReadDTO> mappedLocations = _mapper.Map<IEnumerable<LocationReadDTO>>(locations);

                if (mappedLocations.Count() > 0)
                {
                    return Ok(mappedLocations);
                }
                return BadRequest($"Location with name = {name} does not exist!");

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadByCountryId")]
        public async Task<ActionResult<IEnumerable<LocationReadDTO>>> ReadAllLocationsByCountryId(Guid id)
        {
            try
            {
                List<Location> locations = await _context.Locations.Where(location => location.CountryId == id).ToListAsync();
                IEnumerable<LocationReadDTO> mappedLocations = _mapper.Map<IEnumerable<LocationReadDTO>>(locations);

                if (mappedLocations.Count() > 0)
                {
                    return Ok(mappedLocations);
                }
                return BadRequest($"Location with countryId = {id} does not exist!");

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadAvailable")]
        public async Task<ActionResult<IEnumerable<LocationReadDTO>>> ReadAllLocationsAvailable(bool isAvailable = true)
        {
            try
            {
                List<Location> locations = await _context.Locations.Where(location => location.isAvailable == isAvailable).ToListAsync();
                IEnumerable<LocationReadDTO> mappedLocations = _mapper.Map<IEnumerable<LocationReadDTO>>(locations);

                return Ok(mappedLocations);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("UpdateName")]
        public async Task<IActionResult> UpdateLocationName(Guid id, string name)
        {
            try
            {
                if (name.IsNullOrEmpty())
                {
                    return BadRequest($"Location name cannot be empty!");
                }

                Location? location = _context.Locations.Find(id);
                if (location != null)
                {
                    location.Name = name;
                    _context.Locations.Entry(location).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();
                }

                return BadRequest($"Location with id = {id} does not exist!");
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("DeleteById")]
        public async Task<IActionResult> DeleteLocationById(Guid id)
        {
            try
            {
                Location? location = _context.Locations.Find(id);
                if (location != null)
                {
                    _context.Locations.Remove(location);
                    return Ok();
                }

                return BadRequest($"Location with id = {id} does not exist!");
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
