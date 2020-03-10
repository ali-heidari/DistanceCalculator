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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Default form is calculating distance of 2 geo-points, therefore loads CalculateDistance form 
        /// </summary>
        /// <returns>
        /// Returns CalculateDistance.cshtml as view
        /// </returns>
        public IActionResult Index()
        {
            return View("CalculateDistance");
        }

        /// <summary>
        /// Loads CalculatesDistance form 
        /// </summary>
        /// <returns>
        /// Returns CalculateDistance.cshtml as view
        /// </returns>
        public IActionResult CalculateDistance()
        {
            return View();
        }

        /// <summary>
        /// Loads the form which shows the history of calculated distances
        /// </summary>
        /// <returns>
        /// Returns DistancesHistory.cshtml as view
        /// </returns>
        public IActionResult DistancesHistory()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
