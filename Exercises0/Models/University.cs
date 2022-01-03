using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Models
{
    [Table("TB_M_Universities")]
    public class University
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }

        //[JsonIgnore]
        public virtual ICollection<Education> Educations { get; set; }
    }

}
