using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Models
{
    [Table("TB_M_Roles")]
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
