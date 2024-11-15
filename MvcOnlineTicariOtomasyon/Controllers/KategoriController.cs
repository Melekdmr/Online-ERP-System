using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;


namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Kategoris.ToList();
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


	}
}