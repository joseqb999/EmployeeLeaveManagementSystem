using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeLeave.DomainModels;
using EmployeeLeave.DomainModels.Context;

namespace EmployeeLeave.Repositories
{
    public interface IEmployeesRepository
    {
        void InsertEmployee(Employee e);
        void UpdateEmployeeDetails(Employee e);
        void UpdateEmployeePassword(Employee e);
        void DeleteEmployee(int eid);
        List<Employee> GetEmployeesByEmailAndPassword(string Email, string Password);
        List<Employee> GetEmployees();
        List<Employee> GetAllEmployees();
        List<Employee> GetEmployeesByEmpId(int EmpId);
        List<Employee> GetEmployeesByEmail(string Email);
        int GetLatestEmployeeId();
    }
    public class EmployeeRepository : IEmployeesRepository
    {
        LeaveManagementContext db;

        public EmployeeRepository()
        {
            db = new LeaveManagementContext();
        }

        public void InsertEmployee(Employee e)
        {
            db.Employees.Add(e);
            db.SaveChanges();
        }

        public void UpdateEmployeeDetails(Employee e)
        {
            Employee es = db.Employees.Where(temp => temp.EmpId == e.EmpId).FirstOrDefault();
            if (es != null)
            {
                es.EmpId = e.EmpId;
                es.EmpName = e.EmpName;
                es.Email = e.Email;
                es.Mobile = e.Mobile;
                db.SaveChanges();
            }
        }

        public void UpdateEmployeePassword(Employee e)
        {
            Employee es = db.Employees.Where(temp => temp.EmpId == e.EmpId).FirstOrDefault();
            if (es != null)
            {
                es.PasswordHash = e.PasswordHash;
                db.SaveChanges();
            }
        }

        public void DeleteEmployee(int eid)
        {
            Employee es = db.Employees.Where(temp => temp.EmpId == eid).FirstOrDefault();
            if (es != null)
            {
                db.Employees.Remove(es);
                db.SaveChanges();
            }
        }

        public List<Employee> GetEmployeesByEmailAndPassword(string Email, string PasswordHash)
        {
            List<Employee> es = db.Employees.Where(temp => temp.Email == Email && temp.PasswordHash == PasswordHash).ToList();
            return es;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> es = db.Employees.Where(temp => temp.IsHR == false && temp.IsManager == false).OrderBy(temp => temp.EmpName).ToList();
            return es;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> es = db.Employees.ToList();
            return es;
        }

        public List<Employee> GetEmployeesByEmpId(int EmpId)
        {
            List<Employee> es = db.Employees.Where(temp => temp.EmpId == EmpId).ToList();
            return es;
        }

        public List<Employee> GetEmployeesByEmail(string Email)
        {
            List<Employee> es = db.Employees.Where(temp => temp.Email == Email).ToList();
            return es;
        }

        public int GetLatestEmployeeId()
        {
            int eid = db.Employees.Select(temp => temp.EmpId).Max();
            return eid;
        }
    }
}