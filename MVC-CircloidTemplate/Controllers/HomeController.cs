using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        // GET: Home
      

        public HomeController()
        {
            ViewBag.MainPageSelected = "selected";
        }
      
        public ActionResult Index()
        {
        
            return View();
        }
        public ActionResult AssingCookie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AssingCookie(string CookieName, string CookieValue)
        {
            HttpCookie hc = new HttpCookie(CookieName);
            hc.Value = CookieValue;
            hc.Expires = DateTime.Now.AddDays(2);
            Response.Cookies.Add(hc);
            return View();
        }
    }
}