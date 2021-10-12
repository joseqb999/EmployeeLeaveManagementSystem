using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeLeave.DomainModels;
using EmployeeLeave.ViewModels;
using EmployeeLeave.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace EmployeeLeave.ServiceLayers
{
    public interface IRolesService
    {
        RoleViewModel GetRolesByRoleId(int RoleId);

    }
    public class RoleService : IRolesService
    {
        IRolesRepository er;

        public RoleService()
        {
            er = new RoleRepository();
        }

        public RoleViewModel GetRolesByRoleId(int RoleId)
        {
            Role r = er.GetRolesByRoleId(RoleId).FirstOrDefault();
            RoleViewModel rvm = null;
            if (r != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Role, RoleViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                rvm = mapper.Map<Role, RoleViewModel>(r);
            }
            return rvm;
        }
    }
}