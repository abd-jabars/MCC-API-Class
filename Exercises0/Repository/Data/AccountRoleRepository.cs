using Exercises0.Context;
using Exercises0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext myContext;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int SignManager(AccountRole accountRole)
        {
            // var findAccountRole = myContext.AccountRoles.Where(acr => acr.AccountNIK == accountRole.AccountNIK).FirstOrDefault();
            var findEmployee = myContext.Employees.Find(accountRole.AccountNIK);
            if (findEmployee != null)
            {
                var findRole = myContext.AccountRoles.ToList().Where(acr => acr.AccountNIK == accountRole.AccountNIK);
                foreach (var item in findRole)
                {
                    if (item.RoleId == 2)
                    {
                        return 1; // already signed as a manager
                    }
                }
                
                var roleAccount = new AccountRole
                {
                    AccountNIK = accountRole.AccountNIK,
                    RoleId = 2
                };
                myContext.AccountRoles.Add(roleAccount);
                myContext.SaveChanges();
                return 2; // become a manager
            }
            return 0; // data not found
        }
    }
}
