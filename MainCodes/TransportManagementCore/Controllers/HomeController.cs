using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TransportManagementCore.Models;

namespace TransportManagementCore.Controllers
{
    [Area("Home")]
    [Route("Home")]
    public class HomeController : Controller
    {
         private readonly IWebHostEnvironment _env;
        private readonly Microsoft.Extensions.Configuration.IConfiguration Configuration;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env, Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _logger = logger;
            _env = env;
            this.Configuration = config;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {

            return View("~/Views/Home/AccessDenied.cshtml");
        }


        //[HttpGet]
        //[Route("Session/{LoginId}")]
        //public async Task<IActionResult> Session(string LoginId) {
        //    try
        //    {
        //        HttpContext.Session.SetString("LoginId", LoginId);
        //        //HttpContext.Session.SetString("UserId", Convert.ToString(Userdt.Rows[0]["id"]));
        //     return   Ok("API response");
        //    }
        //    catch (Exception ex) {

        //    }
        //}
    }
}
