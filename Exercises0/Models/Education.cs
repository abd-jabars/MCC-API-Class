using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Models
{
    [Table("TB_M_Educations")]
    public class Education
    {
        public int EducationId { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
        [JsonIgnore]
        public virtual University University { get; set; }
        public int UniversityId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Profiling> Profilings { get; set; }
    }
}
