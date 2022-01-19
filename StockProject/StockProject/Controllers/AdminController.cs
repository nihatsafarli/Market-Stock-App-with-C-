using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockProject.Models.Entity;
namespace StockProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DbStockProjectEntities1 db = new DbStockProjectEntities1();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(tbladmin p)
        {
            db.tbladmin.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}