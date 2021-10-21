using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeLeave.ViewModels;
using EmployeeLeave.ServiceLayers;
using EmployeeLeave.CustomFilters;
using System.Net;
using System.Net.Mail;

namespace EmployeeLeave.Controllers
{
    public class AccountController : Controller
    {
        IEmployeesService es;

        public AccountController(IEmployeesService es)
        {
            this.es = es;

        }
        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                EmployeeViewModel evm = this.es.GetEmployeesByEmailAndPassword(lvm.Email, lvm.Password);
                if (evm != null)
                {
                    Session["CurrentEmpId"] = evm.EmpId;
                    Session["CurrentEmployeename"] = evm.EmpName;
                    Session["CurrentEmployeeEmail"] = evm.Email;
                    Session["CurrentPassword"] = evm.Password;
                    Session["CurrentEmployeeMobile"] = evm.Mobile;
                    Session["CurrentEmployeeRoleId"] = evm.RoleId;
                    Session["CurrentEmployeePMId"] = evm.PMId;
                    Session["CurrentIsHR"] = evm.IsHR;
                    Session["CurrentIsManager"] = evm.IsManager;
                    Session["CurrentEmployeeIsSpecialPermission"] = evm.IsSpecialPermission;

                    if (evm.IsHR || evm.IsManager)
                    {
                        return RedirectToRoute(new { controller = "Home", action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email / Password");
                    return View(lvm);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(lvm);
            }
        }
        [EmployeeAuthorizationFilter]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [EmployeeAuthorizationFilter]
        public ActionResult EditProfile()
        {
            int EmpId = Convert.ToInt32(Session["CurrentEmpId"]);
            EmployeeViewModel evm = this.es.GetEmployeesByEmpId(EmpId);
            EditEmployeeDetailsViewModel eedvm = new EditEmployeeDetailsViewModel() { EmpId = evm.EmpId, EmpName = evm.EmpName, Email = evm.Email, Mobile = evm.Mobile };
            return View(eedvm);
        }

        [HttpPost]
        [EmployeeAuthorizationFilter]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditEmployeeDetailsViewModel eedvm)
        {
            if (ModelState.IsValid)
            {
                eedvm.EmpId = Convert.ToInt32(Session["CurrentEmpId"]);
                this.es.UpdateEmployeeDetails(eedvm);
                Session["CurrentEmployeename"] = eedvm.EmpName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eedvm);
            }
        }

        [EmployeeAuthorizationFilter]
        public ActionResult EditPassword()
        {
            int EmpId = Convert.ToInt32(Session["CurrentEmpId"]);
            EmployeeViewModel evm = this.es.GetEmployeesByEmpId(EmpId);
            EditEmployeePasswordViewModel eepvm = new EditEmployeePasswordViewModel() { Email = evm.Email, Password = "", ConfirmPassword = "", EmpId = evm.EmpId };
            return View(eepvm);
        }

        [HttpPost]
        [EmployeeAuthorizationFilter]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword(EditEmployeePasswordViewModel eepvm)
        {
            if (ModelState.IsValid)
            {
                eepvm.EmpId = Convert.ToInt32(Session["CurrentEmpId"]);
                this.es.UpdateEmployeePassword(eepvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eepvm);
            }
        }
        [EmployeeAuthorizationFilter]
        public ActionResult Myprofile()
        {
            int EmpId = Convert.ToInt32(Session["CurrentEmpId"]);
            EmployeeViewModel evm = this.es.GetEmployeesByEmpId(EmpId);
            Session["CurrentEmpId"] = evm.EmpId;
            Session["CurrentEmployeename"] = evm.EmpName;
            Session["CurrentEmployeeEmail"] = evm.Email;
            Session["CurrentPassword"] = evm.Password;
            Session["CurrentEmployeeMobile"] = evm.Mobile;
            Session["CurrentEmployeeRoleId"] = evm.RoleId;
            Session["CurrentEmployeePMId"] = evm.PMId;
            Session["CurrentIsHR"] = evm.IsHR;
            Session["CurrentIsManager"] = evm.IsManager;

            return View();


        }
        [HRAuthorizationFilter]
        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        [HRAuthorizationFilter]
        public ActionResult AddEmployee(AddEmployeeViewModel aevm)
        {

            if (ModelState.IsValid)
            {
                int EmpId = this.es.InsertEmployee(aevm);
                Session["CurrentEmpId"] = EmpId;
                Session["CurrentEmployeename"] = aevm.EmpName;
                Session["CurrentEmployeeEmail"] = aevm.Email;
                Session["CurrentEmployeePassword"] = aevm.Password;
                Session["CurrentEmployeeMobile"] = aevm.Mobile;
                Session["CurrentEmployeeRoleId"] = aevm.RoleId;
                Session["CurrentEmployeePMId"] = aevm.PMId;
                Session["CurrentIsHR"] = false;
                Session["CurrentIsManager"] = false;


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
        public ActionResult Employees()
        {
            List<EmployeeViewModel> employees = this.es.GetEmployees();
            return View(employees);
        }

        [HttpPatch]
        public ActionResult Employees(EmployeeViewModel employeeViewModel)
        {
            List<EmployeeViewModel> employees = this.es.GetEmployees();
            return View(employees);
        }
        [HRAuthorizationFilter]
        public ActionResult ChangeProfile(int id)
        {


            EmployeeViewModel employeeViewModel = this.es.GetEmployeesByEmpId(id);
            EditEmployeeDetailsViewModel EditEmpDetail = new EditEmployeeDetailsViewModel() { EmpId = employeeViewModel.EmpId, EmpName = employeeViewModel.EmpName, Email = employeeViewModel.Email, Mobile = employeeViewModel.Mobile};
            return View(EditEmpDetail);
        }


        [HttpPost]
        [HRAuthorizationFilter]
        public ActionResult ChangeProfile(EditEmployeeDetailsViewModel EditEmpDetail)
        {

            if (ModelState.IsValid)
            {

                this.es.UpdateEmployeeDetails(EditEmpDetail);
                return RedirectToAction("Employees", "Account");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(EditEmpDetail);
            }
        }
        [HRAuthorizationFilter]
        public ActionResult DeleteEmployee(int id)
        {

            this.es.DeleteEmployee(id);
            return RedirectToAction("Employees", "Account");
        }

        public ActionResult ViewProfile(int id)
        {
            EmployeeViewModel employeeViewModel = this.es.GetEmployeesByEmpId(id);
            return View(employeeViewModel);
        }
        [HRAuthorizationFilter]
        public ActionResult Search(string str, int RoleId)
        {
            if (RoleId == 1)
            {
                List<EmployeeViewModel> employees = this.es.GetAllEmployees().Where(temp => temp.EmpName.ToLower().Contains(str.ToLower()) && (temp.RoleId == 1)).ToList();
                ViewBag.str = str;
                return View(employees);
            }

            else if (RoleId == 2)
            {
                List<EmployeeViewModel> employees = this.es.GetAllEmployees().Where(temp => temp.EmpName.ToLower().Contains(str.ToLower()) && (temp.RoleId == 2)).ToList();
                ViewBag.str = str;
                return View(employees);
            }
            else
            {
                List<EmployeeViewModel> employees = this.es.GetAllEmployees().Where(temp => temp.EmpName.ToLower().Contains(str.ToLower()) && (temp.RoleId == 3)).ToList();
                ViewBag.str = str;
                return View(employees);
            }

        }
    }
}