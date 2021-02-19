using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using store_car_web_project.Models;

namespace store_car_web_project.Controllers
{   //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult contact()
        {
            return View();
        }
        public IActionResult about()
        {
            return View();
        }
        public IActionResult team()
        {
            return View();
        }
        public IActionResult counter()
        {
            return View();
        }
        public IActionResult services()
        {
            if (User.Identity.IsAuthenticated)
            { 
                return View();
            }
            else
            {
                return RedirectToAction("login1", "Account");
            }
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
