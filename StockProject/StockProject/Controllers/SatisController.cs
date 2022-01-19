using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockProject.Models.Entity;
namespace StockProject.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        DbStockProjectEntities1 db = new DbStockProjectEntities1();
        [Authorize]
        public ActionResult Index()
        {
            var satislar = db.tblsatislar.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            //Ürünler
            List<SelectListItem> urun = (from x in db.tblurunler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString(),

                                         }).ToList();
            ViewBag.drop1 = urun;


            //Personeller
            List<SelectListItem> per = (from x in db.tblpersoneller.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad +" "+ x.soyad,
                                            Value = x.id.ToString(),

                                        }).ToList();
            ViewBag.drop2 = per;

            //Müşteriler

            List<SelectListItem> must = (from x in db.tblmusteriler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad +" "+ x.soyad,
                                             Value = x.id.ToString(),

                                         }).ToList();
            ViewBag.drop3 = must;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(tblsatislar p)
        {
            var urun = db.tblurunler.Where(x => x.id == p.tblurunler.id).FirstOrDefault();
            var personel = db.tblpersoneller.Where(x => x.id == p.tblpersoneller.id).FirstOrDefault();
            var musteri = db.tblmusteriler.Where(x => x.id == p.tblmusteriler.id).FirstOrDefault();
            p.tblurunler = urun;
            p.tblpersoneller = personel;
            p.tblmusteriler = musteri;
            p.tarih=DateTime.Parse(DateTime.Now.ToLongDateString());
            db.tblsatislar.Add(p);           
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}