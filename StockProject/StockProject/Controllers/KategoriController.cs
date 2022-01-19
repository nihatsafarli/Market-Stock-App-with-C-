using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockProject.Models.Entity;
namespace StockProject.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DbStockProjectEntities1 db = new DbStockProjectEntities1();
        [Authorize]
        public ActionResult Index(string k)
        {
            //var urunler = db.tblurunler.Where(x=>x.durum==true).ToList();
            var kategoriler = db.tblkategoriler.Where(x => x.durum == true);
            if (!string.IsNullOrEmpty(k))
            {
                kategoriler = kategoriler.Where(x => x.ad.Contains(k) && x.durum == true);
            }
            return View(kategoriler.ToList());
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(tblkategoriler p)
        {
            p.durum = true;
            db.tblkategoriler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(tblurunler k1)
        {
            var katbul = db.tblkategoriler.Find(k1.id);
            katbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.tblkategoriler.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult KategoriGuncelle(tblkategoriler k)
        {
            var ktg = db.tblkategoriler.Find(k.id);
            ktg.ad = k.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}