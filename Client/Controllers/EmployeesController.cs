using Client.Base;
using Client.Repositories.Data;
using Exercises0.Models;
using Exercises0.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetRegistered()
        {
            var result = await repository.GetRegistered();
            return Json(result);
        }

        [HttpGet("Employees/GetRegisteredById/{NIK}")]

        public async Task<JsonResult> GetRegisteredById(string NIK)
        {
            var result = await repository.GetRegisteredById(NIK);
            return Json(result);
        }

        [HttpPost("Employees/Register")]
        public JsonResult Register(Register register)
        {
            var result = repository.Register(register);
            return Json(result);
        }

        [HttpPut("Employees/Register")]
        public JsonResult UpdateRegisteredData(Register register)
        {
            var result = repository.UpdateRegisteredData(register);
            return Json(result);
        }

        [HttpDelete("Employees/DeleteRegisteredData/{NIK}")]
        public JsonResult DeleteRegisteredData(string NIK)
        {
            var result = repository.DeleteRegisteredData(NIK);
            return Json(result);
        }
    }
}
