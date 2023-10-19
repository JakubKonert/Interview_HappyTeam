using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.Core;
using Interview_HappyTeam_Backend.Core.ErrorLogger;
using Microsoft.AspNetCore.Mvc;

namespace Interview_HappyTeam_Backend.Controllers
{
    [Route("api/[controller]")]
    public class ErrorController : Controller
    {
        private AppDbContext _context { get; set; }
        public ErrorController(AppDbContext context)
        {
            _context = context;
            Setter.Set(context);
        }

        [HttpPost]
        [Route("LogError")]
        public async Task<IActionResult> LogError(string name, string message)
        {
            try
            {
                ErrorLogger.LogError(name, message);
                return Ok();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("ErrorController -> LogError", ex.Message);
                return StatusCode(500);
            }
        }

    }
}
