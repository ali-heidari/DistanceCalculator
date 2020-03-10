using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Services;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    /// <summary>
    /// This controller handle requests of authentication as login, register and forgotPass
    /// </summary>
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

        /// <summary>
        /// Login interface
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns 200 if user existed, otherwise 404</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]AuthenticateModel model)
        {
            var user = _authService.Authenticate(model.Email, model.Password);

            if (user == null)
                return this.NotFound(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        /// <summary>
        /// Register interface
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns 200 if user created</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterModel model)
        {
            bool res = _authService.Register(model.Email, model.Username, model.Password);

            if (!res)
                return this.BadRequest(new { message = "Failed to register user" });

            return Ok(res);
        }
    }
}
