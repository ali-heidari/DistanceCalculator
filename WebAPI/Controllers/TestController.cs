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
            // other opreation  

            // if above operation succeed, publish a message to RabbitMQ  

            var num = new System.Random().Next(9000);
            // publish message  
            _manager.Publish(new MyMessage()
            { 
                SomeProperty = $"Hello-{num}"
            }, "Samples.RabbitMQ.NativeIntegration", "fanout", "Samples.RabbitMQ.NativeIntegration");
            return Ok("Message sent to endpoint");
        }
    }
}
