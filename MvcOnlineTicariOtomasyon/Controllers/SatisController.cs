using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;


namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        Context c = new Context();
        // GET: Satis
        public ActionResult Index()
        {
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            

			List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
										   select new SelectListItem  //yeni bir öge seçiyoruz
										   {
											   Text = x.UrunAd,
											   Value = x.UrunId.ToString()
										   }).ToList();


			ViewBag.dgr1 = deger1;

			List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
										   select new SelectListItem  //yeni bir öge seçiyoruz
										   {
											   Text = x.CariAd+" "+x.CariSoyad,
											   Value = x.CariId.ToString()
										   }).ToList();


			ViewBag.dgr2 = deger2;

			List<SelectListItem> deger3 = (from x in c.Personels.ToList()
										   select new SelectListItem  //yeni bir öge seçiyoruz
										   {
											   Text = x.PersonelAd+" "+x.PersonelSoyad,
											   Value = x.PersonelId.ToString()
										   }).ToList();


			ViewBag.dgr3 = deger3;
			return View();
        }
		[HttpPost]
		public ActionResult YeniSatis(SatisHareket s)
		{
			s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult SatisGetir(int id)
		{

			List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
										   select new SelectListItem  //yeni bir öge seçiyoruz
										   {
											   Text = x.UrunAd,
											   Value = x.UrunId.ToString()
										   }).ToList();


			ViewBag.dgr1 = deger1;

			List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
										   select new SelectListItem  //yeni bir öge seçiyoruz
										   {
											   Text = x.CariAd + " " + x.CariSoyad,
											   Value = x.CariId.ToString()
										   }).ToList();


			ViewBag.dgr2 = deger2;

			List<SelectListItem> deger3 = (from x in c.Personels.ToList()
										   select new SelectListItem  //yeni bir öge seçiyoruz
										   {
											   Text = x.PersonelAd + " " + x.PersonelSoyad,
											   Value = x.PersonelId.ToString()
										   }).ToList();


			ViewBag.dgr3 = deger3;
			

			var deger = c.SatisHarekets.Find(id);
			return View("SatisGetir", deger);

		}
		public ActionResult SatisGuncelle(SatisHareket s)
		{
			var satis = c.SatisHarekets.Find(s.SatisId);
			satis.Adet = s.Adet;
			satis.Fiyat = s.Fiyat;
			satis.CariId = s.CariId;
			satis.Toplamtutar = s.Toplamtutar;
			satis.PersonelId = s.PersonelId;
			satis.UrunId = s.UrunId;
			satis.Tarih = s.Tarih ;
			c.SaveChanges();
			return RedirectToAction("Index");


		}

		public ActionResult SatisDetay(int id)
		{
			var degerler = c.SatisHarekets.Where(x => x.SatisId == id).ToList();
			return View(degerler);
		}

	}
}