using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CircloidTemplate.Models;

namespace MVC_CircloidTemplate.Controllers
{
    [Authorize(Roles = "uye , admin")]
    public class ProductController : Controller
    {
        
        public ProductController()
        {
            ViewBag.ProductSelected = "selected";
        }
        NorthwindEntities1 db = new NorthwindEntities1();
        // GET: Product
        public ActionResult Index()
        {
            List<Product> prdList = db.Products.ToList();
            return View(prdList);
        }

        public ActionResult AddProduct()
        {
            ViewBag.catList = db.Categories.ToList();
            ViewBag.suplist = db.Suppliers.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /* Delete islemini 3 farklı yol ile yapacagız. Ilk cözümümüz sil butonuna basılınca yeni bir view acılacak yani kullanıcı yeni bir sayfaya 
         * yönlendirilecek ve onay verilirse silinecek.
         2. yol sil butonuna bsılınca yukrıda alert kutusu cıkacak ve kayıt silinsin mi diye soracak evet denirse silinecek bu yöntemin dezavantajı
         alert kutusu bir kaç kez görüntülendikten sonra browser otomatik olarak alert kutusu altına checkbox ekliyor ve bu mesajı tekrar gösterme seceneği 
         sunuyor. Eğer kullanıcı checkbox işarertlerse tekrar alert kutuusu gözükmeyeceği için silme işlemi gerçekleştirilemiyor(Ajax kodu yazılacak) 
         
         3. yol  sil butonuna basılınca küçük bir pencere acılacak yani başka sayfaya yönderilmeyecek evet seçilirse silme işlemi gerçekleşecek
         (Ajax Kodu yazılacak )*/
        public ActionResult DeleteProduct(int prdID)
        {
            Product prd = db.Products.FirstOrDefault(x=>x.ProductID==prdID);
            return View(prd);

        }

        [HttpPost]
        public ActionResult DeleteProduct(Product p)
        {
            Product prod = db.Products.FirstOrDefault(x => x.ProductID == p.ProductID);
            db.Products.Remove(prod);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateProduct(int id)
        {


            
            Product product = db.Products.Find(id);
            ViewBag.catList = db.Categories.ToList();
            ViewBag.supList = db.Suppliers.ToList();

            return View(product);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product prd)
        {


            
            Product product = db.Products.Find(prd.ProductID);
            product.ProductName = prd.ProductName;
            product.UnitPrice = prd.UnitPrice;
            product.UnitsInStock = prd.UnitsInStock;
            product.CategoryID = prd.CategoryID;
            product.SupplierID = prd.SupplierID;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult UpdateProduct2()
        {
            

            int proID = Convert.ToInt32(Request.QueryString["prdID"].ToString());
            string proName = Request.QueryString["prdName"].ToString();

            Product product = db.Products.FirstOrDefault(x => x.ProductID == proID);
            ViewBag.catList = db.Categories.ToList();
            ViewBag.supList = db.Suppliers.ToList();


            return View(product);
        }
        [HttpPost]
        public ActionResult UpdateProduct2(Product prd)
        {


            
            Product product = db.Products.Find(prd.ProductID);
            product.ProductName = prd.ProductName;
            product.UnitPrice = prd.UnitPrice;
            product.UnitsInStock = prd.UnitsInStock;
            product.CategoryID = prd.CategoryID;
            product.SupplierID = prd.SupplierID;

            db.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}