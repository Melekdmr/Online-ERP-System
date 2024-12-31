using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;


namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index(int sayfa=1)
        {
            var degerler = c.Kategoris.ToList().ToPagedList(sayfa,7);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
		public ActionResult KategoriEkle(Kategori k)
		{
            c.Kategoris.Add(k);
            c.SaveChanges();
			return RedirectToAction("Index");
		}
        public ActionResult KategoriSil(int id)
        {
            var ktg = c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

		[HttpGet]
		public ActionResult KategoriGuncelle(int id)  //getir
		{
			var kategori = c.Kategoris.Find(id);
			return View("KategoriGuncelle", kategori);
		}

		[HttpPost]
		public ActionResult KategoriGetir(Kategori k)  //guncelle
		{
			var ktgr = c.Kategoris.Find(k.KategoriId);
			ktgr.KategoriAd = k.KategoriAd;
			c.SaveChanges();
			return RedirectToAction("Index");
		}
        public ActionResult Deneme()
        {
            Class3 cs = new Class3();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriId", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "UrunId", "UrunAd");
            return View(cs);
        }
        public JsonResult UrunGetir (int p)
        {
            var urunlistesi = (from x in c.Uruns
                               join y in c.Kategoris
                               on x.Kategori.KategoriId equals y.KategoriId
                               where x.Kategori.KategoriId == p
                               select new
                               {
                                   Text = x.UrunAd,
                                   Value = x.UrunId.ToString()
                               }).ToList();
            return Json(urunlistesi, JsonRequestBehavior.AllowGet);
        }

	}
}