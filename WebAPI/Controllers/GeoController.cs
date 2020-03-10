using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NServiceBus;
using WebAPI.Helpers.RabbitMQ;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GeoController : ControllerBase
    {
        private IRabbitManager _manager;

        public GeoController(IRabbitManager manager)
        {
            this._manager = manager;
        }

        [AllowAnonymous]
        [HttpGet("getDistance")]
        public IActionResult GetDistance(GeoPoints model)
        {
            // publish message  
            _manager.Publish(model, "RabbitMQ", "fanout", "RabbitMQ");
            return Ok("Message sent to endpoint");
        }
    }
}
