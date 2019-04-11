using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CircloidTemplate.Models;

namespace MVC_CircloidTemplate.Controllers
{
    [Authorize(Roles ="uye")]
    public class SupplierController : Controller
    {
        NorthwindEntities1 db = new NorthwindEntities1();

        // GET: Supplier

        public SupplierController()
        {
            ViewBag.SupplierSelected = "selected";
        }
        public ActionResult Index()
        {
            List<Supplier> sup = db.Suppliers.ToList();
            return View(sup);
        }
        public ActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSupplier(Supplier sup)
        {
            db.Suppliers.Add(sup);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult UpdateSupplier(int id)
        {
          

            Supplier sup = db.Suppliers.Find(id);

            

            return View(sup);
        }

        [HttpPost]
        public ActionResult UpdateSupplier(Supplier s)
        {
            Supplier sup = db.Suppliers.Find(s.SupplierID);

           

            sup.CompanyName = s.CompanyName;
            sup.ContactName = s.ContactName;
            sup.ContactTitle = s.ContactTitle;

            db.Entry(s).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //public ActionResult DeleteSupplier(int supID)
        //{
        //    Supplier sup = db.Suppliers.Find(supID);
        //    return View(sup);

        //} 
        //[HttpPost]
        //public ActionResult DeleteSupplier(Supplier s)
        //{
        //    Supplier sup = db.Suppliers.Find(s.SupplierID);
        //    db.Suppliers.Remove(sup);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //bu methodun içinde olusan hata ajaxi etkilemez. ajax icin success ajaxin dogru bir sekilde actiona ualsmis olmasiyla ilgilidir. bu methodda veritabinindaki iliskilerden dolayi kayit silinemez ve benzeri hatalar ajaxi ilgilendirmez. bu yüzden bu method icinde olusan hatalarla iölgili ajax tarafıına bilgi göndermeliyiz.

       public string DeleteSupplier(int id)
        {
            try
            {
                Supplier s = db.Suppliers.Find(id);
                db.Suppliers.Remove(s);
                db.SaveChanges();

                return "OK";

            }
            catch (Exception)
            {

                return "ERROR";
            }
        }

    }
}