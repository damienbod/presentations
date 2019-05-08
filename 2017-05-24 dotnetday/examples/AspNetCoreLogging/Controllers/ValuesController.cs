using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreLogging.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
            _logger.LogTrace("constructor ValuesController");           
        }

        //public ValuesController(ILoggerFactory loggerFactory)
        //{
        //    _logger = loggerFactory.CreateLogger("superCoolLogger");
        //    _logger.LogTrace("constructor ValuesController");
        //}

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Getting all items information");
            _logger.LogWarning(LoggingEvents.LIST_ITEMS, "Getting all items warning test");
            _logger.LogError(LoggingEvents.LIST_ITEMS, "Getting all items errror test");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _logger.LogTrace(LoggingEvents.GET_ITEM, "Getting trace info item {id}", id);
            _logger.LogDebug(LoggingEvents.GET_ITEM, "Getting debug info item {id}", id);
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting info item {id}", id);
            _logger.LogWarning(LoggingEvents.GET_ITEM, "Getting warning item {id}", id);
            _logger.LogError(LoggingEvents.GET_ITEM, "Getting error item {id}", id);
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            _logger.LogInformation(LoggingEvents.INSERT_ITEM, "create item {value}", value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Update item {id}, value {value}", id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Delete item {id}", id);
        }
    }
}
