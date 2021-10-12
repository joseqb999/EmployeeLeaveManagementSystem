using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeLeave.DomainModels;
using EmployeeLeave.DomainModels.Context;

namespace EmployeeLeave.Repositories
{
    public interface IProjectsRepository
    {
        List<Leave> ViewLeaveRequest(int EmpId);
        void ApproveLeave(Leave l);
        void RejectLeave(Leave l);

    }
    public class ProjectRepository : IProjectsRepository
    {
        LeaveManagementContext db;

        public ProjectRepository()
        {
            db = new LeaveManagementContext();
        }

        public List<Leave> ViewLeaveRequest(int EmpId)
        {
            List<Leave> lt = db.Leaves.Where(temp => temp.EmpId == EmpId).ToList();
            return lt;
        }

        public void ApproveLeave(Leave l)
        {
            Leave lt = db.Leaves.Where(temp => temp.LeaveId == l.LeaveId).FirstOrDefault();
            if (lt != null)
            {
                lt.EmpId = l.EmpId;
                lt.StartDate = l.StartDate;
                lt.EndDate = l.EndDate;
                lt.status = l.status;
                lt.Description = l.Description;
                db.SaveChanges();
            }
        }

        public void RejectLeave(Leave l)
        {
            Leave lt = db.Leaves.Where(temp => temp.LeaveId == l.LeaveId).FirstOrDefault();
            if (lt != null)
            {
                lt.EmpId = l.EmpId;
                lt.StartDate = l.StartDate;
                lt.EndDate = l.EndDate;
                lt.status = l.status;
                db.SaveChanges();
            }
        }
    }
}

