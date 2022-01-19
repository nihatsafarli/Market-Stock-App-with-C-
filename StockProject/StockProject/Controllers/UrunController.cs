using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockProject.Models.Entity;

namespace StockProject.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DbStockProjectEntities1 db = new DbStockProjectEntities1();
        [Authorize]
        public ActionResult Index(string p)
        {
            //var urunler = db.tblurunler.Where(x=>x.durum==true).ToList();
            var urunler = db.tblurunler.Where(x=>x.durum==true);
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ad.Contains(p) && x.durum==true);
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = (from x in db.tblkategoriler.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString(),

                                        }).ToList();
            ViewBag.drop = ktg;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(tblurunler u)
        {
            var ktgr = db.tblkategoriler.Where(x => x.id == u.tblkategoriler.id).FirstOrDefault();    
            u.tblkategoriler = ktgr;
            db.tblurunler.Add(u);
            u.durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kat = (from x in db.tblkategoriler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList();

            var ktgr = db.tblurunler.Find(id);
            ViewBag.urunkategori = kat;
            return View("UrunGetir", ktgr);
        }
        public ActionResult UrunGuncelle(tblurunler p)
        {
            var urun = db.tblurunler.Find(p.id);
            urun.ad = p.ad;
            urun.marka = p.marka;
            urun.stok = p.stok;
            urun.alisfiyat = p.alisfiyat;
            urun.satisfiyat = p.satisfiyat;
            var ktg = db.tblkategoriler.Where(x => x.id == p.tblkategoriler.id).FirstOrDefault();
            urun.kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(tblurunler p1)
        {
            var urunbul = db.tblurunler.Find(p1.id);
            urunbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}