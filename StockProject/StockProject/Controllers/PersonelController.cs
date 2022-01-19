using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockProject.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace StockProject.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        DbStockProjectEntities1 db = new DbStockProjectEntities1();
        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            var personelliste = db.tblpersoneller.Where(x => x.durum == true).ToList().ToPagedList(sayfa, 5);
            return View(personelliste);            
        }
        [HttpGet]
        public ActionResult YeniPersonel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniPersonel(tblpersoneller p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniPersonel");
            }
            p.durum = true;
            db.tblpersoneller.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelSil(tblpersoneller p)
        {

            var perbul = db.tblpersoneller.Find(p.id);
            perbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            var mus = db.tblpersoneller.Find(id);
            return View("PersonelGetir", mus);
        }
        public ActionResult PersonelGuncelle(tblpersoneller t)
        {
            var per = db.tblpersoneller.Find(t.id);
            per.ad = t.ad;
            per.soyad = t.soyad;
            per.departman = t.departman;            
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}