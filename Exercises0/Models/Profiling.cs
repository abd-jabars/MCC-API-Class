using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Models
{
    [Table("TB_TR_Profilings")]
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        //[JsonIgnore]
        public virtual Account Account { get; set; }
        //[JsonIgnore]
        public virtual Education Education { get; set; }
        public int EducationId { get; set; }
    }
}
