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
    public class MusteriController : Controller
    {
        // GET: Musteri
        DbStockProjectEntities1 db = new DbStockProjectEntities1();
        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            //var musteriliste = db.tblmusteriler.ToList();
            var musteriliste = db.tblmusteriler.Where(x => x.durum == true).ToList().ToPagedList(sayfa, 10);
            return View(musteriliste);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(tblmusteriler p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            p.durum = true;
            db.tblmusteriler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSil(tblmusteriler p)
        {

            var musteribul = db.tblmusteriler.Find(p.id);
            musteribul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mus = db.tblmusteriler.Find(id);
            return View("MusteriGetir", mus);
        }
        public ActionResult MusteriGuncelle(tblmusteriler t)
        {
            var mus = db.tblmusteriler.Find(t.id);
            mus.ad = t.ad;
            mus.soyad = t.soyad;
            mus.sehir = t.sehir;
            mus.bakiye = t.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}