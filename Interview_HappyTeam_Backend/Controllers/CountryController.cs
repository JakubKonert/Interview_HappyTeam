using AutoMapper;
using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Country;
using Interview_HappyTeam_Backend.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Interview_HappyTeam_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private AppDbContext _context { get; set; }
        private IMapper _mapper { get; }

        public CountryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCountry([FromBody] CountryCreateDTO country)
        {
            try
            {
                Country newCountry = _mapper.Map<Country>(country);
                await _context.Countries.AddAsync(newCountry);
                await _context.SaveChangesAsync();
                return Ok(newCountry);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadAll")]
        public async Task<ActionResult<IEnumerable<CountryReadDTO>>> ReadAllCountries()
        {
            try
            {
                List<Country> countries = await _context.Countries.ToListAsync();
                IEnumerable<CountryReadDTO> mappedCountries = _mapper.Map<IEnumerable<CountryReadDTO>>(countries);

                return Ok(mappedCountries);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("ReadById")]
        public async Task<ActionResult<CountryReadDTO>> ReadCountryById(Guid id)
        {
            try
            {
                Country? country = _context.Countries.Find(id);
                if (country != null)
                {
                    CountryReadDTO mappedCountry = _mapper.Map<CountryReadDTO>(country);
                    return Ok(mappedCountry);
                }
                
                return BadRequest($"Country with that id = {id} does not exist!");

            }
            catch 
            { 
                return StatusCode(500); 
            }
        }

        [HttpGet]
        [Route("ReadByName")]
        public async Task<ActionResult<IEnumerable<CountryReadDTO>>> ReadAllCountriesByName(string name)
        {
            try
            {
                List<Country> countries = await _context.Countries.Where(country => country.Name == name).ToListAsync();
                IEnumerable<CountryReadDTO> mappedCountries = _mapper.Map<IEnumerable<CountryReadDTO>>(countries);

                if (mappedCountries.Count() > 0)
                {
                    return Ok(mappedCountries);
                }
                return BadRequest($"Country with name = {name} does not exist!");

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("UpdateName")]
        public async Task<IActionResult> UpdateCountryName(Guid id, string name)
        {
            try
            {
                if (name.IsNullOrEmpty())
                {
                    return BadRequest($"Country name cannot be empty!");
                }

                Country? country = _context.Countries.Find(id);
                if (country != null)
                {
                    country.Name = name;
                    _context.Countries.Entry(country).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();
                }

                return BadRequest($"Country with id = {id} does not exist!");
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("AddLocation")]
        public async Task<IActionResult> AddLocationToCountry(Guid countryId, Guid locationId)
        {
            try
            {
                Country? country = _context.Countries.Find(countryId);
                Location? location = _context.Locations.Find(locationId);
                if (country == null || location == null)
                { 
                    return BadRequest($"Country with id = {countryId} or location with id = {locationId} does not exist!");

                }

                country.Locations.Add(location);
                _context.Countries.Entry(country).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("RemoveLocation")]
        public async Task<IActionResult> RemoveLocationFromCountry(Guid countryId, Guid locationId)
        {
            try
            {
                Country? country = _context.Countries.Find(countryId);
                Location? location = _context.Locations.Find(locationId);
                if (!(country == null || location == null))
                {
                    country.Locations = new List<Location>(country.Locations.Where(location => location.Id != locationId));
                    _context.Countries.Entry(country).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();
                }

                return BadRequest($"Country with id = {countryId} or location with id = {locationId} does not exist!");
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("DeleteById")]
        public async Task<IActionResult> DeleteCountryById(Guid id)
        {
            try
            {
                Country? country = _context.Countries.Find(id);
                if (country != null)
                {
                    _context.Countries.Remove(country);
                    return Ok();
                }

                return BadRequest($"Country with id = {id} does not exist!");
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
