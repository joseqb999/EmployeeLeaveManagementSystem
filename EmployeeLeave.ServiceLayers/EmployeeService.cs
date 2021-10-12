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
    public interface IEmployeesService
    {
        int InsertEmployee(AddEmployeeViewModel evm);
        void UpdateEmployeeDetails(EditEmployeeDetailsViewModel eedvm);
        void UpdateEmployeePassword(EditEmployeePasswordViewModel evm);
        void DeleteEmployee(int eid);
        EmployeeViewModel GetEmployeesByEmailAndPassword(string Email, string Password);
        List<EmployeeViewModel> GetEmployees();
        List<EmployeeViewModel> GetAllEmployees();
        EmployeeViewModel GetEmployeesByEmpId(int EmpId);
        EmployeeViewModel GetEmployeesByEmail(string Email);

    }
    public class EmployeeService : IEmployeesService
    {
        IEmployeesRepository er;

        public EmployeeService()
        {
            er = new EmployeeRepository();
        }

        public int InsertEmployee(AddEmployeeViewModel aevm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<AddEmployeeViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<AddEmployeeViewModel, Employee>(aevm);
            e.PasswordHash = SHA256HashGenerator.GenerateHash(aevm.Password);
            er.InsertEmployee(e);
            int eid = er.GetLatestEmployeeId();
            return eid;
        }

        public void UpdateEmployeeDetails(EditEmployeeDetailsViewModel eedvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditEmployeeDetailsViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<EditEmployeeDetailsViewModel, Employee>(eedvm);
            er.UpdateEmployeeDetails(e);


        }

        public void UpdateEmployeePassword(EditEmployeePasswordViewModel evm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditEmployeePasswordViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<EditEmployeePasswordViewModel, Employee>(evm);
            e.PasswordHash = SHA256HashGenerator.GenerateHash(evm.Password);
            er.UpdateEmployeePassword(e);
        }

        public void DeleteEmployee(int eid)
        {
            er.DeleteEmployee(eid);
        }

        public EmployeeViewModel GetEmployeesByEmailAndPassword(string Email, string Password)
        {
            Employee e = er.GetEmployeesByEmailAndPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            EmployeeViewModel evm = null;
            if (e != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                evm = mapper.Map<Employee, EmployeeViewModel>(e);
            }
            return evm;
        }

        public List<EmployeeViewModel> GetEmployees()
        {
            List<Employee> e = er.GetEmployees();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<EmployeeViewModel> evm = mapper.Map<List<Employee>, List<EmployeeViewModel>>(e);
            return evm;
        }

        public List<EmployeeViewModel> GetAllEmployees()
        {
            List<Employee> e = er.GetAllEmployees();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<EmployeeViewModel> evm = mapper.Map<List<Employee>, List<EmployeeViewModel>>(e);
            return evm;
        }

        public EmployeeViewModel GetEmployeesByEmpId(int EmpId)
        {
            Employee e = er.GetEmployeesByEmpId(EmpId).FirstOrDefault();
            EmployeeViewModel evm = null;
            if (e != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                evm = mapper.Map<Employee, EmployeeViewModel>(e);
            }
            return evm;
        }

        public EmployeeViewModel GetEmployeesByEmail(string Email)
        {
            Employee e = er.GetEmployeesByEmail(Email).FirstOrDefault();
            EmployeeViewModel evm = null;
            if (e != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                evm = mapper.Map<Employee, EmployeeViewModel>(e);
            }
            return evm;
        }
    }
}
