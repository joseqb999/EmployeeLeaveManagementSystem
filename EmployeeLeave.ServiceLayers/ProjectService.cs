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
    public interface IProjectsService
    {
        List<ViewLeaveRequestsViewModel> ViewLeaveRequest(int EmpId);
        void ApproveLeave(LeaveViewModel pvm);
        void RejectLeave(LeaveViewModel pvm);

    }
    public class ProjectService : IProjectsService
    {
        IProjectsRepository er;

        public ProjectService()
        {
            er = new ProjectRepository();
        }

        public List<ViewLeaveRequestsViewModel> ViewLeaveRequest(int EmpId)
        {
            List<Leave> l = er.ViewLeaveRequest(EmpId);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, ViewLeaveRequestsViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<ViewLeaveRequestsViewModel> pvm = mapper.Map<List<Leave>, List<ViewLeaveRequestsViewModel>>(l);
            return pvm;
        }

        public void ApproveLeave(LeaveViewModel pvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ApplyLeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave l = mapper.Map<LeaveViewModel, Leave>(pvm);
            er.ApproveLeave(l);
        }

        public void RejectLeave(LeaveViewModel pvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ApplyLeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave l = mapper.Map<LeaveViewModel, Leave>(pvm);
            er.RejectLeave(l);
        }
    }
}
