using Exercises0.Models;
using Exercises0.Repository.Data;
using Exercises0.ViewModel;
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
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        private readonly UniversityRepository universityRepository;
        public UniversitiesController(UniversityRepository universityRepository) : base(universityRepository)
        {
            this.universityRepository = universityRepository;
        }

        [HttpGet]
        [Route("Count")]
        public ActionResult<Register> CountUniversity()
        {
            try
            {
                var result = universityRepository.CountUniversity();
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
    }
}
