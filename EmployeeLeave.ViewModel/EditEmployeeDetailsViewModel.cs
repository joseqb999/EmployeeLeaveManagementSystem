using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeave.ViewModels
{
    public class EditEmployeeDetailsViewModel
    {
        [Required]
        public int EmpId { get; set; }

        [Required]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string EmpName { get; set; }

        [Required]
        public int Mobile { get; set; }


    }
}
