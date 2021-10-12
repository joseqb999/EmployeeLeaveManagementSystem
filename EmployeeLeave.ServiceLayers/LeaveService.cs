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
    public interface ILeavesService
    {
        void ApplyLeave(ApplyLeaveViewModel lvm);
        LeaveViewModel GetLeavesByLeaveId(int LeaveId);
        ViewLeaveStatusViewModel ViewLeaveStatus(int EmpId);
        List<LeaveViewModel> GetLeavesByEmpId(int EmpId);
        List<LeaveViewModel> GetLeaves();
        MailViewModel UpdateLeaveStatusByLeaveID(LeaveViewModel leaveRequest);

    }
    public class LeaveService : ILeavesService
    {
        ILeavesRepository er;

        public LeaveService()
        {
            er = new LeaveRepository();
        }

        public void ApplyLeave(ApplyLeaveViewModel lvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ApplyLeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave l = mapper.Map<ApplyLeaveViewModel, Leave>(lvm);
            er.ApplyLeave(l);
        }

        public LeaveViewModel GetLeavesByLeaveId(int LeaveId)
        {
            Leave l = er.GetLeavesByLeaveId(LeaveId).FirstOrDefault();
            LeaveViewModel lvm = null;
            if (l != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                lvm = mapper.Map<Leave, LeaveViewModel>(l);
            }
            return lvm;
        }

        public ViewLeaveStatusViewModel ViewLeaveStatus(int EmpId)
        {
            Leave l = er.ViewLeaveStatus(EmpId).FirstOrDefault();
            ViewLeaveStatusViewModel lvm = null;
            if (l != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, ViewLeaveStatusViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                lvm = mapper.Map<Leave, ViewLeaveStatusViewModel>(l);
            }
            return lvm;
        }


        public List<LeaveViewModel> GetLeavesByEmpId(int EmpId)
        {
            List<Leave> leave = er.GetLeavesByEmpId(EmpId);
            //LeaveViewModel leaveViewModel = null;

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<LeaveViewModel> leaveViewModels = mapper.Map<List<Leave>, List<LeaveViewModel>>(leave);
            return leaveViewModels;
            //if (leave != null)
            //{
            //var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            //    IMapper mapper = config.CreateMapper();
            //    leaveViewModel = mapper.Map<Leave, LeaveViewModel>(leave);
            ////}

            //return leaveViewModel ;
        }

        public List<LeaveViewModel> GetLeaves()
        {
            List<Leave> leave = er.GetLeaves();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<LeaveViewModel> leaveViewModels = mapper.Map<List<Leave>, List<LeaveViewModel>>(leave);
            return leaveViewModels;
        }

        public MailViewModel UpdateLeaveStatusByLeaveID(LeaveViewModel leaveRequest)
        {

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave leave = mapper.Map<LeaveViewModel, Leave>(leaveRequest);
            Employee emp = er.UpdateLeaveStatusByLeaveID(leave);

            var config1 = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, MailViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper1 = config1.CreateMapper();
            MailViewModel mvm = mapper1.Map<Employee, MailViewModel>(emp);

            mvm.status = leaveRequest.status;

            return mvm;


        }
    }
}

