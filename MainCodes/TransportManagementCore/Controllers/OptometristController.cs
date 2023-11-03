using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Controllers
{
    [Route("Optometrist")]
    public class OptometristController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
