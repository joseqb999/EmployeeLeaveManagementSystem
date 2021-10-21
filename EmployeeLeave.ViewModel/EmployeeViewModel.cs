using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeLeave.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Mobile { get; set; }
        public int RoleId { get; set; }
        public int PMId { get; set; }
        public bool IsHR { get; set; }
        public bool IsManager { get; set; }
        public bool IsSpecialPermission { get; set; }

    }
}
