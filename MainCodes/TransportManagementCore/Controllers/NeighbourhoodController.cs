using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Controllers
{
    [Route("Neighbourhood")]
    public class NeighbourhoodController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
