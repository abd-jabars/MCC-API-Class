using Client.Base;
using Client.Repositories.Data;
using Exercises0.Models;
using Exercises0.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        private readonly UniversityRepository universityRepository;
        public UniversitiesController(UniversityRepository repository) : base(repository)
        {
            this.universityRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
