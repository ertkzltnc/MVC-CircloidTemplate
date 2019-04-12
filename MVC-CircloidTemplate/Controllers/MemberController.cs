using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_CircloidTemplate.Add_Classes;

namespace MVC_CircloidTemplate.Controllers
{
    //[Authorize]
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
        public ActionResult ExitMember()
        {
            
            FormsAuthentication.SignOut();
            return RedirectToAction("MemberLogin", "Member");
        }

        public ActionResult CreateNewAccount()
        {
            return View();
        }

        [HttpPost]

        public ActionResult CreateNewAccount(User u)
        {
            Membership.CreateUser(u.UserName, u.Password,u.Email, u.PasswordQuestion, u.PasswordAnswer, true, out MembershipCreateStatus status);

            string message = "";
            switch (status)
            {
                case MembershipCreateStatus.InvalidUserName:
                    message = "Geçersiz kullanıcı adı";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    message = "Geçersiz parola";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    message = "Geçersiz gizli soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    message = "Geçersiz gizli cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    message = "Geçersiz email adresi";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    message = "Kullanılmış kullanıcı adı";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    message = "Kullanılmış email adresi girildi";
                    break;
                case MembershipCreateStatus.UserRejected:
                    message = "Kullanıcı engel hatası";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    message = "Geçersiz kullanıcı key hatası";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    message = " Kullanılmış kullanıcı key hatası";
                    break;
                case MembershipCreateStatus.ProviderError:
                    message = "Üye yönetimi sağlayıcı hatası";
                    break;
                case MembershipCreateStatus.Success:
                    message = "Basarılı";
                   
                    break;
                default:
                    break;
            }
            ViewBag.Message = message;

            if (status == MembershipCreateStatus.Success)
                //return RedirectToAction("MemberLogin", "Member");
                return View();
            else
                return View();
        }
    

     
        public ActionResult ForgotMyPassword()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult ForgotMyPassword(User u)
        {
            try
            {
                MembershipUser mu = Membership.GetUser(u.UserName);
                if (mu.PasswordQuestion == u.PasswordQuestion && mu.Email==u.Email)
                {
                    // Gizli soru cevabı kontrol ediyor
                    string pwd = mu.ResetPassword(u.PasswordAnswer);
                    mu.ChangePassword(pwd, u.Password);
                    return RedirectToAction("MemberLogin");
                }
                else
                {
                    ViewBag.Message = "Bilgilerinizi Kontrol Ediniz";
                    return View();

                }
            }
            catch (Exception)
            {

                ViewBag.Message = "Bilgilerinizi Kontrol Ediniz";
                return View();
            }
        }
    }
}