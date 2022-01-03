using Exercises0.Context;
using Exercises0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Repository.Data
{
    public class ProfilingRepository : GeneralRepository<MyContext, Profiling, string>
    {
        public ProfilingRepository(MyContext myContext) : base(myContext)
        { 
            
        }
    }
}
