using Exercises0.Context;
using Exercises0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, int>
    {
        //private readonly MyContext myContext;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            //this.myContext = myContext;
        }
    }
}
