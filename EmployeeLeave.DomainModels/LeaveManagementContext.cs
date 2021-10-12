using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EmployeeLeave.DomainModels;

namespace EmployeeLeave.DomainModels.Context
{
    public class LeaveManagementContext : DbContext
    {
        public LeaveManagementContext() : base("LeaveManagementContextDb")
        { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}