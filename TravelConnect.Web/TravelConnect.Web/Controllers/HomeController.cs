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
        public IActionResult Index()
        {
            //TravelConnect.Sabre.SabreConnector connector = new Sabre.SabreConnector();
            //var result = AccessTokenManager.GetAccessToken();
            //result = AccessTokenManager.GetAccessToken();
            //result = AccessTokenManager.GetAccessToken();
            //result = AccessTokenManager.GetAccessToken();
            //result = AccessTokenManager.GetAccessToken();

            SabreConnector.SendRequest("/v1/lists/supported/countries"
                , "pointofsalecountry=US", false);

            SabreConnector.SendRequest("/v1/lists/supported/countries"
                , "pointofsalecountry=DE", false);

            var result = SabreConnector.SendRequest("/v1/lists/supported/countries"
                , "pointofsalecountry=IT", false);


            return View(result);
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
