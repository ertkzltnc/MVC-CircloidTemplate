using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_CircloidTemplate.Add_Classes;

namespace MVC_CircloidTemplate.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult MemberLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MemberLogin(User user,string RememberMe )
        {
            bool validationResult=Membership.ValidateUser(user.UserName, user.Password);
            if (validationResult)
            {
                //Girilen kullanıcı ve sişfre bilgileri dogru ise kullanıcın web sitesine giris yapması gerekir.
                //    bunun için öncelikler web.config dosyasında authorization ayarlarının yapılması gerekir. Ayarlar yapıldıktan sonra
                //     FormsAuthentication.RedirectFromLoginPage() metodu cağırılacaktır.
                if (RememberMe == "on")
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, true);
                else
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, false);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Kullanıcı adı veya Parola Hatalı";
               
            }
            return View();

        }

        public ActionResult CreateNewAccount()
        {
            return View();
        }

        public ActionResult ForgotMyPassword()
        {
            return View();
        }

    }
}