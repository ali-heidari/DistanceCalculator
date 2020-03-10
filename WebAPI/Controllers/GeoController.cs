using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NServiceBus;
using WebAPI.Helpers.RabbitMQ;
using System.Linq;

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

        /// <summary>
        /// Get distance between 2 geo points
        /// </summary>
        /// <param name="model">Geopoints object</param>
        /// <returns>Returns ok if data send to geoService</returns>
        [AllowAnonymous]
        [HttpGet("getDistance")]
        public IActionResult GetDistance(GeoPoints model)
        {
            // publish message  
            _manager.Publish(model, "RabbitMQ", "fanout", "RabbitMQ");
            return Ok("Distance inserted, you can check GetAllDistances");
        }

        /// <summary>
        /// Get all the distances for the given user
        /// </summary>
        /// <param name="user">user, always has only guid </param>
        /// <returns>Returns list or not found.</returns>
        [AllowAnonymous]
        [HttpGet("GetAllDistances")]
        public IActionResult GetAllDistances(Entities.User user)
        {
            try
            {
                Core.Data.DataProvider db = Core.Data.DataProvider.DataProviderFactory();
                return Ok(db._data["GeoData"].Where(x => x.Key == System.Guid.Parse(user.GUID)));
            }
            catch (System.Exception)
            {
                return NotFound("Nothing found");
            }
        }
    }
}
