using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Models
{
    [Table("TB_TR_AccountRoles")]
    public class AccountRole
    {
        public int Id { get; set; }
        
        [JsonIgnore]
        public virtual Account Account { get; set; }
        public string AccountNIK { get; set; }
        
        [JsonIgnore]
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
