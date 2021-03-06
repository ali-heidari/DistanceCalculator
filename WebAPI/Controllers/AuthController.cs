﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Services;
using WebAPI.Models;
using Core.Models;

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
        /// Validate token
        /// </summary>
        /// <param name="jwt">Incoming token to validate</param>
        /// <returns>Returns 200 if token valid, otherwise 404</returns>
        [AllowAnonymous]
        [HttpPost("validate")]
        public IActionResult Validate([FromBody]JWTModel jwt)
        {
            bool res = _authService.Validate(jwt.token);

            if (!res)
                return this.NotFound(new { message = "Token is not valid" });

            return Ok(true);
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

            return Ok(user.Token);
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
        /// <summary>
        /// Logout the user
        /// </summary>
        /// <param name="jwt">Token belongs to user</param>
        /// <returns>Return 200 if successfull otherwise bad request</returns>
        [AllowAnonymous]
        [HttpPost("Logout")]
        public IActionResult Logout([FromBody]JWTModel jwt)
        {
            bool res = _authService.Logout(jwt.token);

            if (!res)
                return this.BadRequest(new { message = "Failed to logout" });

            return Ok(res);
        }
    }
}
