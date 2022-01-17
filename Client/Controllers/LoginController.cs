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
            var token = HttpContext.Session.GetString("JWToken");

            if (token == null)
            {
                return View();
            }

            return RedirectToAction("index", "SbAdmin");
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

        [HttpPost("Accounts/Login")]
        public JsonResult Validation(Login login)
        {
            var result = loginRepository.Validation(login);
            return Json(result);
        }

    }
}
