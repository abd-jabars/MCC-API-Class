using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult NotFound404()
        {
            return View();
        }
        
        [AllowAnonymous]
        public IActionResult Error401()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error403()
        {
            return View();
        }

        //[AllowAnonymous]
        //public IActionResult Error500()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult JsJqueryExercises()
        {
            return View();
        }

        public IActionResult BootstrapExercises()
        {
            return View();
        }

        public IActionResult PokemonAPI()
        {
            return View();
        }

        public IActionResult EmployeeAPI()
        {
            return View();
        }

        public IActionResult DataTableEmployee()
        {
            return View();
        }

        [Authorize(Roles = "Director, Manager")]
        public IActionResult DataTableRegister()
        {
            return View();
        }

        public IActionResult SbAdmin()
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
