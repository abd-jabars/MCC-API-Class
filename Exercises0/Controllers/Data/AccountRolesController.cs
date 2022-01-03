using Exercises0.Models;
using Exercises0.Repository.Data;
using Microsoft.AspNetCore.Authorization;
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
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository accountRoleRepository;
        public AccountRolesController(AccountRoleRepository accountRoleRepository) : base(accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
        }

        [Authorize(Roles ="Director")]
        [HttpPost]
        [Route("SignManager")]
        public ActionResult SignManager(AccountRole accountRole)
        {
            var result = accountRoleRepository.SignManager(accountRole);
            if (result == 1)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = $"Employee with NIK {accountRole.AccountNIK} is already a manager" });
            }
            else if (result == 2)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = $"Employee with NIK {accountRole.AccountNIK} become a manager"});
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "Data not found"});
        }
    }
}
