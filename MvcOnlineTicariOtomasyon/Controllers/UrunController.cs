using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using Newtonsoft.Json.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
	public class UrunController : Controller
	{
		// GET: Urun
		Context c = new Context();
		public ActionResult Index()
		{
			var urunler = c.Uruns.Where(x => x.Durum == true).ToList();
			return View(urunler);
		}
		[HttpGet]
		public ActionResult YeniUrun()
		{
			List<SelectListItem> deger1 = (from x in c.Kategoris.ToList() //Kategori verilerini bir dropdown list’te göstermek amacıyla gerekli 
										   select new SelectListItem      //SelectListItem formatına dönüştürür.
										   {                              //Böylece her bir kategori dropdown'da hem kullanıcıya anlamlı bir ad (Text),
											   Text = x.KategoriAd,       //hem de arka planda kullanılabilecek bir kimlik(Value) sağlar.
											   Value = x.KategoriId.ToString()


										   }).ToList();
			ViewBag.dgr1 = deger1;
			return View();
		}
		[HttpPost]
		public ActionResult YeniUrun(Urun p)
		{
			c.Uruns.Add(p);
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult UrunSil(int id)
		{
			var urun = c.Uruns.Find(id);
			urun.Durum = false;
			c.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult UrunGetir(int id)
		{
			// Kategorileri veritabanından çekiyoruz ve dropdown listesi için gerekli formata dönüştürüyoruz
			List<SelectListItem> deger1 = (from x in c.Kategoris.ToList() //Kategori verilerini bir dropdown list’te göstermek amacıyla gerekli 
										   select new SelectListItem      //SelectListItem formatına dönüştürür.
										   {                              //Böylece her bir kategori dropdown'da hem kullanıcıya anlamlı bir ad (Text),
											   Text = x.KategoriAd,       //hem de arka planda kullanılabilecek bir kimlik(Value) sağlar.
											   Value = x.KategoriId.ToString()


										   }).ToList();
		ViewBag.dgr1 = deger1;
		
		
			var urundeger = c.Uruns.Find(id);
			return View("UrunGetir", urundeger);
		}

		public ActionResult UrunGuncelle(Urun p)
		{
			var urun = c.Uruns.Find(p.UrunId);
			urun.AlisFiyat = p.AlisFiyat;
			urun.Durum = p.Durum;
			urun.SAtisFiyat = p.SAtisFiyat;
			urun.KategoriID = p.KategoriID;
			urun.Marka = p.Marka;
			urun.Stok = p.Stok;
			urun.UrunAd = p.UrunAd;
			urun.UrunGorsel = p.UrunGorsel;
			c.SaveChanges();
			return RedirectToAction("Index");
		}
		
		public ActionResult UrunListesi()
		{
			var degerler = c.Uruns.ToList();
			return View(degerler);
		}
	}
}