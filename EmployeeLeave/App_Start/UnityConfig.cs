using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using EmployeeLeave.ServiceLayers;
using System.Web.Mvc;

namespace EmployeeLeave
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IEmployeesService, EmployeeService>();
            container.RegisterType<ILeavesService, LeaveService>();
            container.RegisterType<IProjectsService, ProjectService>();
            container.RegisterType<IRolesService, RoleService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}