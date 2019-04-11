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
    }
}