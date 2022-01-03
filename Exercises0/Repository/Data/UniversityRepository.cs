using Exercises0.Context;
using Exercises0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        public UniversityRepository(MyContext myContext) : base(myContext)
        { 
            
        }
    }
}
