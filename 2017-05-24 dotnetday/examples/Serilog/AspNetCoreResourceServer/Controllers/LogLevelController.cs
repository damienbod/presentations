using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace AspNetCoreResourceServer.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class LogLevelController : Controller
    {
        private readonly ILogger _logger;


        public LogLevelController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("DataEventRecordsController");
        }

        [HttpGet("Verbose")]
        public IActionResult Verbose(long id)
        {
            Startup.MyLoggingLevelSwitch.MinimumLevel = LogEventLevel.Verbose;
            return Ok("set to Verbose");
        }

        [HttpGet("Information")]
        public IActionResult Information(long id)
        {
            Startup.MyLoggingLevelSwitch.MinimumLevel = LogEventLevel.Information;
            return Ok("set to Information");
        }

        [HttpGet("Warning")]
        public IActionResult Warning(long id)
        {
            Startup.MyLoggingLevelSwitch.MinimumLevel = LogEventLevel.Warning;
            return Ok("set to Warning");
        }
    }
}
