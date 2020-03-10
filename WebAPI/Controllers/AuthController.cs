using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Services;
using WebAPI.Models;
using System.Linq;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _authService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _authService.GetAll();
            return Ok(users);
        }
    }
}
