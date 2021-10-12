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
    public class LeaveController : Controller
    {
        // GET: Leave
        ILeavesService ls;
        IEmployeesService es;
        IProjectsService ps;

        public LeaveController(ILeavesService ls, IEmployeesService es, IProjectsService ps)
        {
            this.ls = ls;
            this.es = es;
            this.ps = ps;
        }
        [EmployeeAuthorizationFilter]
        public ActionResult LeaveRequest()
        {

            ApplyLeaveViewModel leaveReq = new ApplyLeaveViewModel();
            return View(leaveReq);
        }


        [HttpPost]
        [EmployeeAuthorizationFilter]
        public ActionResult LeaveRequest(ApplyLeaveViewModel leaveReq)
        {
            leaveReq.EmpId = Convert.ToInt32(Session["CurrentEmpId"]);
            leaveReq.EmpName = Convert.ToString(Session["CurrentEmployeename"]);
            leaveReq.status = "Pending";

            this.ls.ApplyLeave(leaveReq);
            return RedirectToAction("Index", "Home");

        }
        [EmployeeAuthorizationFilter]
        public ActionResult LeaveStatus()
        {
            int EmpId = Convert.ToInt32(Session["CurrentEmpId"]);
            List<LeaveViewModel> leaves = this.ls.GetLeavesByEmpId(EmpId);
            return View(leaves);
        }


        [HRandPMAuthorizationFilter]
        public ActionResult LeaveUpdation()
        {
            List<LeaveViewModel> leaves = this.ls.GetLeaves();
            return View(leaves);
        }

        [HttpPost]
        [HRandPMAuthorizationFilter]
        public ActionResult LeaveUpdation(LeaveViewModel updateLeave)
        {

            MailViewModel MailViewModel = this.ls.UpdateLeaveStatusByLeaveID(updateLeave);
            try
            {
                var senderEmail = new MailAddress("mvcp990@gmail.com", "mvc");
                var receiverEmail = new MailAddress(MailViewModel.Email, "Receiver");
                var password = "mvcp1234";
                var sub = MailViewModel.status + " your leave request";
                var body = MailViewModel.EmpName + ", your leave request has been " + MailViewModel.status;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return RedirectToAction("LeaveUpdation", "Leave");


        }
    }
}