using Exercises0.Context;
using Exercises0.Models;
using Exercises0.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        private readonly MyContext myContext;
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public IEnumerable<Object> CountUniversity()
        {
            var list = from edu in myContext.Educations
                       join uni in myContext.Universities on edu.UniversityId equals uni.UniversityId
                       group uni by new { edu.UniversityId, uni.UniversityName } into Group
                       select new
                       {
                           UniversityId = Group.Key.UniversityId,
                           UniversityName = Group.Key.UniversityName,
                           UnivCount = Group.Count()
                       };
            return list.ToList();
        }

    }
}
