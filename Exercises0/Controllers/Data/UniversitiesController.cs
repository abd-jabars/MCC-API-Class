﻿using Exercises0.Models;
using Exercises0.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}