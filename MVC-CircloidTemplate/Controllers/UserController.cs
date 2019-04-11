using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_CircloidTemplate.Add_Classes;

namespace MVC_CircloidTemplate.Controllers
{
    [Authorize(Roles="admin")]
    public class UserController : Controller
    {
        public UserController()
        {
            ViewBag.UserSelected = "selected";
        }
        // GET: User
        public ActionResult Index()
        {
            // databasedde ki userları cekip user isimli collectionda toplayacak
            MembershipUserCollection users = Membership.GetAllUsers();
            return View(users);

        }
        public ActionResult AddUser()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult AddUser(User u)
        {
            Membership.CreateUser(u.UserName, u.Password, u.Email, u.PasswordQuestion, u.PasswordAnswer, true, out MembershipCreateStatus status);

            string mesaj = "";
            switch (status)
            {
                case MembershipCreateStatus.InvalidUserName:
                    mesaj = "Geçersiz kullanıcı adı";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj = "Geçersiz parola";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj = "Geçersiz gizli soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj = "Geçersiz gizli cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj = "Geçersiz email adresi";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj = "Kullanılmış kullanıcı adı";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj = "Kullanılmış email adresi girildi";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj = "Kullanıcı engel hatası";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj = "Geçersiz kullanıcı key hatası";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj = " Kullanılmış kullanıcı key hatası";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj = "Üye yönetimi sağlayıcı hatası";
                    break;
                case MembershipCreateStatus.Success:
                    break;
                default:
                    break;
            }
            ViewBag.Mesaj = mesaj;

            if (status == MembershipCreateStatus.Success)
                return RedirectToAction("Index");
            else
                return View();
        }
        [HttpPost]
        public void DeleteUser(string id)
        {
            Membership.DeleteUser(id, deleteAllRelatedData: true);
        }

        public ActionResult AssingRole(string userName,string message= null)
        {
            /* Parametre olarak ıd yazmak zorundayız. sebebi projein App_start  klasörünün altında
             Route.Config dosyasında "{controller}/{action}/{id}" bu parametre adının default adı id olduğu için parametre adıda 
             id olması gerekiyor.
             
             
             kullanıcı Rol Ata ya tıkladığında  kullanıcı adını parametre olarak alıyoruz. burada da kullanıcının adını View'e gönderiyoruz.
              Amacımız parametre bilgisinin View'e taşımak .View Tarafında ekle butonuna basılınca  tekrar kullanıcı adının alıp ve rol adının
              View'den Post tarafına taşımak*/

            if (string.IsNullOrWhiteSpace(userName))
            {
                return RedirectToAction("Index");

            }
            MembershipUser user = Membership.GetUser(userName);
            if (user==null)
            {
                return HttpNotFound();
            }
            string[] userRoles = Roles.GetRolesForUser(userName);
            string[] allRoles = Roles.GetAllRoles();
            List<string> availableRoles = new List<string>();
            foreach (string role in allRoles)
            {
                if (!userRoles.Contains(role))
                {
                    availableRoles.Add(role);
                }
            }

            ViewBag.AvailableRoles = availableRoles;
            ViewBag.UserRoles = userRoles;
            ViewBag.UserName = userName;
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public ActionResult AssingRole(string userName, List<string> addedRoles)
          {
            if (addedRoles==null)
            {
                return RedirectToAction("AssingRole", new { userName = userName, message = "Once Rol Seciniz" });
            }
            if (addedRoles.Count < 1)
            {
                return RedirectToAction("AssingRole", new { userName = userName, message = "Hata" });
            }
            Roles.AddUserToRoles(userName, addedRoles.ToArray());
            return RedirectToAction("AssingRole", new { userName = userName, message = "Basarılı" });
        }

        [HttpPost]
        public string DeleteRole(string userName, string  removedRoles)
        {
            string[] removedRolesArray = removedRoles.Split(',');
            if (removedRolesArray.Length<1 || string.IsNullOrWhiteSpace(removedRolesArray[0]))
            {
                return "Hata";
            }

            Roles.RemoveUserFromRoles(userName, removedRolesArray);
            return "Basarılı";
        }


        [HttpPost]
        public string UserRole(string userName)
        {
            List<string> userRol = Roles.GetRolesForUser(userName).ToList();
            string rol = "";
            foreach (string r in userRol)
            {
                rol += r + ",";
            }
            if (rol.Length > 1)
                rol = rol.Remove(rol.Length - 1, 1);
            return rol;
        }
    }
}