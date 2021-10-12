using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeave.ViewModels
{
    public class ApplyLeaveViewModel
    {
        public int LeaveId { get; set; }
        public int EmpId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string status { get; set; }
      

        [Required]
        public string Description { get; set; }

    }
}
