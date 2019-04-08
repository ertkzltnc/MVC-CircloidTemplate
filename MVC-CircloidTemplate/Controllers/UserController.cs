using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_CircloidTemplate.Add_Classes;

namespace MVC_CircloidTemplate.Controllers
{
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
    }
}