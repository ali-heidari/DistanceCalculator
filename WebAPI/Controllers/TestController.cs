using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NServiceBus;
using WebAPI.Helpers.RabbitMQ;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private IRabbitManager _manager;

        public TestController(IRabbitManager manager)
        {
            this._manager = manager;
        }

        [AllowAnonymous]
        [HttpGet("get")]
        public IActionResult Get()
        {
            // publish message  
            _manager.Publish(new GeoPoints()
            {  StartingLat=20.1f,StartingLng=30.66f, EndingLat=21,EndingLng=30
            }, "RabbitMQ", "fanout", "RabbitMQ");
            return Ok("Message sent to endpoint");
        }
    }
}
