using Client.Base;
using Client.Repositories.Data;
using Exercises0.Models;
using Exercises0.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : BaseController<Account, LoginRepository, string>
    {
        private readonly LoginRepository loginRepository;
        public LoginController(LoginRepository repository) : base(repository)
        {
            this.loginRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(Login login)
        {
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.IdToken;

            if (token == null)
            {
                return RedirectToAction("index", "Login");
            }

            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("index", "SbAdmin");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Login");
        }

        [HttpPut]
        public JsonResult ForgotPassword(ForgotPassword forgotPassword)
        {
            var result = repository.ForgotPassword(forgotPassword);
            return Json(result);
        }

        [HttpPut]
        public JsonResult ChangePassword(ForgotPassword forgotPassword)
        {
            var result = repository.ChangePassword(forgotPassword);
            return Json(result);
        }

        //[HttpPost]
        //public JsonResult Login(Login login)
        //{
        //    var result = loginRepository.Auth(login);
        //    return Json(result);
        //}

    }
}
