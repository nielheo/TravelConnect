using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelConnect.Web.Models;
using TravelConnect.Sabre;

namespace TravelConnect.Web.Controllers
{
    public class HomeController : Controller
    {
        ISabreConnector _SabreConnector;

        public HomeController(ISabreConnector _SabreConnector)
        {
            this._SabreConnector = _SabreConnector;
        }

        public async Task<IActionResult> Index()
        {
            //var getCountryTasks = new List<Task<string>>();

            //getCountryTasks.Add(_SabreConnector.SendRequest("/v1/lists/supported/countries"
            //    , "pointofsalecountry=US", false));

            //getCountryTasks.Add(_SabreConnector.SendRequest("/v1/lists/supported/countries"
            //    , "pointofsalecountry=DE", false));

            //getCountryTasks.Add(_SabreConnector.SendRequest("/v1/lists/supported/countries"
            //    , "pointofsalecountry=IT", false));

            //await Task.WhenAll(getCountryTasks);

            ///var result = await _SabreConnector.SendRequestAsync("/v1/lists/supported/countries"
            //    , "pointofsalecountry=IT", false);

            TravelConnect.uAPI.AirService airService = new uAPI.AirService();
            airService.Ping();

            return View(airService);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
