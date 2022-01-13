using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
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
