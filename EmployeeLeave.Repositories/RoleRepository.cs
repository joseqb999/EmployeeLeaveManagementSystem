using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeLeave.DomainModels;
using EmployeeLeave.DomainModels.Context;

namespace EmployeeLeave.Repositories
{
    public interface IRolesRepository
    {
        List<Role> GetRolesByRoleId(int RoleId);

    }
    public class RoleRepository : IRolesRepository
    {
        LeaveManagementContext db;

        public RoleRepository()
        {
            db = new LeaveManagementContext();
        }

        public List<Role> GetRolesByRoleId(int RoleId)
        {
            List<Role> rt = db.Roles.Where(temp => temp.RoleId == RoleId).ToList();
            return rt;
        }


    }
}

