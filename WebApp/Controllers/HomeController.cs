using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WebApp.Helpers;
using WebApp.Helpers.Auth;
using WebApp.Models;
using Core.Models;
using System.Net;

namespace WebApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly IAuth _auth;

        public HomeController(IAuth auth)
        {
            _auth = auth;
        }

        /// <summary>
        /// Default form is calculating distance of 2 geo-points, therefore loads CalculateDistance form 
        /// </summary>
        /// <returns>
        /// Returns CalculateDistance.cshtml as view
        /// </returns>
        public async Task<IActionResult> IndexAsync()
        {
            return await CalculateDistanceAsync();
        }

        /// <summary>
        /// Loads CalculatesDistance form 
        /// </summary>
        /// <returns>
        /// Returns CalculateDistance.cshtml as view
        /// </returns>
        public async Task<IActionResult> CalculateDistanceAsync()
        {
            if (await _auth.ValidateAsync(HttpContext.Session.GetString(Constants.TOKEN)))
            {
                if (Request.Method.ToUpper() == "POST")
                {
                    RequestSender requestSender = new RequestSender(HttpContext.Session.GetString(Constants.TOKEN));
                    var response = await requestSender.Post("/geo/GetDistance", new GetDistanceModel()
                    {
                        startingLat = float.Parse(Request.Form["start_lat"][0].ToString()),
                        startingLng = float.Parse(Request.Form["start_lng"][0].ToString()),
                        endingLat = float.Parse(Request.Form["end_lat"][0].ToString()),
                        endingLng = float.Parse(Request.Form["end_lng"][0].ToString())
                    });
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return Ok(await response.Content.ReadAsStringAsync());
                    }
                    return Error();
                }
                return View("CalculateDistance");
            }
            return RedirectToAction("Login", "Auth");
        }

        /// <summary>
        /// Loads the form which shows the history of calculated distances
        /// </summary>
        /// <returns>
        /// Returns DistancesHistory.cshtml as view
        /// </returns>
        public async Task<IActionResult> DistancesHistoryAsync()
        {
            if (await _auth.ValidateAsync(HttpContext.Session.GetString(Constants.TOKEN)))
            {
                return View();
            }
            return RedirectToAction("Login", "Auth");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
