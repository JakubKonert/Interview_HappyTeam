using AutoMapper;
using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.Core;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Config;
using Interview_HappyTeam_Backend.Core.Entities;
using Interview_HappyTeam_Backend.Core.ErrorLogger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interview_HappyTeam_Backend.Controllers
{
    [Route("api/[controller]")]
    public class ConfigController : Controller
    {
        private AppDbContext _context { get; set; }
        private IMapper _mapper;
        public ConfigController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            Setter.Set(context);
        }


        [HttpGet]
        [Route("ReadAll")]
        public async Task<ActionResult<IEnumerable<Config>>> ReadAllConfigs()
        {
            try
            {
                List<Config> configs = await _context.Configs.Where(config => config.isActive).ToListAsync();

                IEnumerable<ConfigReadDTO> mappedConfigs = _mapper.Map<IEnumerable<ConfigReadDTO>>(configs);

                return Ok(mappedConfigs);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("ConfigController -> ReadAllConfigs", ex.Message);
                return StatusCode(500);
            }
        }
    }
}
