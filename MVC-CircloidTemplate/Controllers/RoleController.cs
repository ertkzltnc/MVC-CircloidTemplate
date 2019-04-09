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
        public ActionResult AddRole(string message=null)
        {
            ViewBag.Message = message;
            return View();
        }
       
        [HttpPost]
        [ActionName("AddRole")]
        public ActionResult AddRolePost(string RoleName)
        {
            if (string.IsNullOrWhiteSpace(RoleName))
            {
                return RedirectToAction("AddRole", new { message = "Boş olamaz" });
            }
            if (Roles.RoleExists(RoleName))
            {
                return RedirectToAction("AddRole", new { message = "Rol Mevcut" });
            }
            Roles.CreateRole(RoleName);
            return RedirectToAction("AddRole", new { message = "Basarılı" });
        }

       
        [HttpPost]
        public void DeleteRole(string RoleName)
        {
            Roles.DeleteRole(RoleName);
        }
    }
}