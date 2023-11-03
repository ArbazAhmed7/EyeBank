using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Controllers
{
    [Route("Autorefraction")]
    public class AutorefractionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
