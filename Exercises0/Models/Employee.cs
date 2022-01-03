using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;        // used to set primary key with [Key]
using System.ComponentModel.DataAnnotations.Schema; // used to set table name
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Models
{
    [Table("TB_M_Employees")]                       // table name: name must be same with name in context
                                                    // place outside the class
    public class Employee
    {
        [Key]                                       // set primary key, manually
        public string NIK { get; set; }             // the primary key
        
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Must be at least 1 characters long.")]
        public string FirstName { get; set; }
        
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Must be at least 1 characters long.")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Phone is required")]
        [MinLength(12, ErrorMessage = "Must be at least 12 characters long")]
        [Phone]
        public string Phone { get; set; }

        [Range(typeof(DateTime), "1/1/1995", "12/31/2003", ErrorMessage = "Year of birth must be in 1995-2003 & age must be 18 in 2021")]
        public DateTime BirthDate { get; set; }

        [Range(4500000,50000000, ErrorMessage = "Salary must be at least 4,500,000 - 50,000,000")]
        public int Salary { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        
        public Gender Gender { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
    }

    public enum Gender
    { 
        Male,
        Female
    }
}
