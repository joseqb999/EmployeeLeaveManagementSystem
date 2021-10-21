using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeave.DomainModels
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Mobile { get; set; }
        public int RoleId { get; set; }
        public int PMId { get; set; }
        public bool IsHR { get; set; }
        public bool IsManager { get; set; }
        public bool IsSpecialPermission { get; set; }


        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [ForeignKey("PMId")]
        public virtual Project Project { get; set; }
    }
}