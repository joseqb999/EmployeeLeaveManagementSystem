using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeLeave.DomainModels;
using EmployeeLeave.DomainModels.Context;

namespace EmployeeLeave.Repositories
{
    public interface ILeavesRepository
    {
        void ApplyLeave(Leave l);
        List<Leave> GetLeavesByLeaveId(int LeaveId);
        List<Leave> ViewLeaveStatus(int EmpId);
        List<Leave> GetLeavesByEmpId(int EmpId);
        List<Leave> GetLeaves();
        Employee UpdateLeaveStatusByLeaveID(Leave leaveReq);


    }
    public class LeaveRepository : ILeavesRepository
    {
        LeaveManagementContext db;

        public LeaveRepository()
        {
            db = new LeaveManagementContext();
        }


        public void ApplyLeave(Leave l)
        {
            db.Leaves.Add(l);
            db.SaveChanges();
        }

        public List<Leave> GetLeavesByLeaveId(int LeaveId)
        {
            List<Leave> ans = db.Leaves.Where(temp => temp.LeaveId == LeaveId).ToList();
            return ans;
        }

        public List<Leave> ViewLeaveStatus(int EmpId)
        {
            List<Leave> ans = db.Leaves.Where(temp => temp.EmpId == EmpId).ToList();
            return ans;
        }


        public List<Leave> GetLeavesByEmpId(int EmpId)
        {
            List<Leave> leave = db.Leaves.Where(temp => temp.EmpId == EmpId).ToList();
            return leave;
        }

        public List<Leave> GetLeaves()
        {
            List<Leave> leave = db.Leaves.OrderByDescending(temp => temp.StartDate).ToList();
            return leave;
        }

        public Employee UpdateLeaveStatusByLeaveID(Leave leaveReq)
        {
            Leave leave = db.Leaves.Where(temp => temp.LeaveId == leaveReq.LeaveId).FirstOrDefault();

            if (leave != null)
            {
                leave.status = leaveReq.status;
                db.SaveChanges();
            }

            Employee emp = db.Employees.Where(x => x.EmpId == leave.EmpId).ToList().FirstOrDefault();
            return emp;

        }
    }
}

