using Exercises0.Context;
using Exercises0.Models;
using Exercises0.Repository.Data;
using Exercises0.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Exercises0.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("AllowOrigin")]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly MyContext myContext;
        public EmployeesController(EmployeeRepository employeeRepository, MyContext myContext) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.myContext = myContext;
        }

        //[HttpPost("{Register}")]
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(Register register)
        {
            try
            {
                var result = employeeRepository.Register(register);
                if (result == 1)
                    return Ok(new { status = HttpStatusCode.BadRequest, result = result, message = "Phone and email already used" });
                else if (result == 2)
                    return Ok(new { status = HttpStatusCode.BadRequest, result = result, message = "Email already used" });
                else if (result == 3)
                    return Ok(new { status = HttpStatusCode.BadRequest, result = result, message = "Phone already used" });
                else
                    return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data inserted" });
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest });
            }
        }

        [HttpGet]
        [Route("RegisterVM")]
        public ActionResult<Register> GetRegisterVM()
        {
            try
            {
                var result = employeeRepository.GetRegisteredVM();
                //var result = employeeRepository.GetRegisteredDataEagerly();
                if (result.Count() <= 0)
                    return NotFound(result);
                else
                    return Ok(result);
                //return Ok(new ReturnMessage(HttpStatusCode.OK, result, "Data found"));
            }
            catch (Exception ex)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = ex.ToString() });
            }
        }

        [HttpGet]
        [Route("RegisterVM/{NIK}")]
        public ActionResult<Register> GetRegisteredData(string NIK)
        {
            try
            {
                var result = employeeRepository.GetRegisteredData(NIK);
                //var result = employeeRepository.GetRegisteredDataEagerly();
                if (result == null)
                    return NotFound(result);
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = ex.ToString() });
            }
        }

        //[HttpGet("{Register}")]
        //[Authorize(Roles ="Director, Manager")]
        [HttpGet]
        [Route("Register")]
        public ActionResult<Register> GetData()
        {
            try
            {
                var result = employeeRepository.GetRegisteredData();
                //var result = employeeRepository.GetRegisteredDataEagerly();
                if (result.Count() <= 0)
                    return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "No data found" });
                else
                    return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data found" });
            }
            catch (Exception ex)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = ex.ToString() });
            }
        }

        [HttpGet("Register/{NIK}")]
        //[Route("Register/NIK")]
        public ActionResult<Register> GetData(string NIK)
        {
            try
            {
                var result = employeeRepository.GetRegisteredData(NIK);
                //var result = employeeRepository.GetRegisteredDataEagerly();
                if (result == null)
                    return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "No data found" });
                else
                    return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data found" });
            }
            catch (Exception ex)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = ex.ToString() });
            }
        }

        [HttpPut]
        [Route("Register")]
        public ActionResult UpdateRegisteredData(Register register)
        {
            try
            {
                var result = employeeRepository.UpdateRegisteredData(register);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = ex.ToString() });
            }
        }

        [HttpDelete]
        [Route("DeleteRegisteredData/{NIK}")]
        public ActionResult DeleteRegisteredData(string NIK)
        {
            try
            {
                var findEmployee = employeeRepository.Get(NIK);
                if (findEmployee == null)
                {
                    return Ok($"Data with NIK {NIK} not found");
                }
                else
                {
                    employeeRepository.DeleteRegisteredData(NIK);
                    var result = employeeRepository.Delete(NIK);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = ex.ToString() });
            }
        }

        [HttpPost]
        [Route("only")]
        public ActionResult<Employee> InsertEmp(Employee employee)
        {
            try
            {
                var result = employeeRepository.InsertEmp(employee);
                //if (result == 1)
                //{
                //    return Ok(new { status = HttpStatusCode.OK, result = result, message = "Input success" });
                //}
                if (result == 1)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Email & phone already used" });
                }
                else if (result == 2)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Email already used" });
                }
                else if (result == 3)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Phone already used" });
                }
                else
                {
                    employeeRepository.Insert(employee);
                    return Ok(new { status = HttpStatusCode.OK, result = result, message = "Input success" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message =  "NIK already used" });
            }
        }

        [HttpPut]
        [Route("only")]
        public ActionResult UpdateEmp(Employee employee)
        {
            try
            {
                var result = employeeRepository.UpdateEmp(employee);
                if (result == 1)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Phone already used" });
                }
                else if (result == 2)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Email already used" });
                }
                else if (result == 3)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Phone & email already used" });
                }
                //else if (result == 3)
                //{
                //    return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "Data not found" });
                //}
                else
                {
                    employeeRepository.Update(employee);
                    return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data updated" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message =  ex.Message });
            }
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS berhasil");
        }
    }
}
