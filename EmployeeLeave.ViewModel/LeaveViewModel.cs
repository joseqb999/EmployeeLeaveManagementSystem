using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeave.ViewModels
{
    public class LeaveViewModel
    {
        public int LeaveId { get; set; }
        public int EmpId { get; set; }
        [Required]
        public string EmpName { get; set; }
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string status { get; set; }
        [Required]
        public string Description { get; set; }


    }
}
