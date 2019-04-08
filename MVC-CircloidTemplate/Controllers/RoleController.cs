using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_CircloidTemplate.Controllers
{
    public class RoleController : Controller
    {
        public RoleController()
        {
            ViewBag.RoleSelected = "selected";
        }
        // GET: Role
        public ActionResult Index()
        {
            List<string> rolesList= Roles.GetAllRoles().ToList();
            return View(rolesList);
        }
        public ActionResult AddRole()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult AddRole(string RoleName)
        {
            Roles.CreateRole(RoleName);
            return RedirectToAction("Index");
        }

       
        [HttpPost]
        public void DeleteRole(string RoleName)
        {
            Roles.DeleteRole(RoleName);
        }
    }
}