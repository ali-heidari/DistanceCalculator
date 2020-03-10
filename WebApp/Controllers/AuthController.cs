using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AuthController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Login action
        /// </summary>
        /// <returns>
        /// Returns the Auth/Login.cshtml as view
        /// </returns>
        public IActionResult Login()
        {
            return View();
        }
        
        /// <summary>
        /// Register action, shows the page to let user register new account
        /// </summary>
        /// <returns>
        /// Returns the Auth/Register.cshtml as view
        /// </returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// ForgotPass action, shows the page to let user to recover his/her password
        /// </summary>
        /// <returns>
        /// Returns the Auth/ForgotPass.cshtml as view
        /// </returns>
        public IActionResult ForgotPass()
        {
            return View();
        }
    }
}
