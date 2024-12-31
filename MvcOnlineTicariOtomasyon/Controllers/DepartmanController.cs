using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
	[Authorize]
	public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();
		
		public ActionResult Index()
        {

            var degerler = c.Departmans.Where(x => x.Durum == true).ToList(); 
            return View(degerler);
        }
        [HttpGet]
      
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        [HttpPost]

		public ActionResult DepartmanEkle(Departman d)
		{
			d.Durum = true;
			c.Departmans.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");


        }
	
		public ActionResult DepartmanSil(int id)
        {
            var deger = c.Departmans.Find(id);
            deger.Durum = false;
           
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanGetir(int id)
        {
            var departman = c.Departmans.Find(id);
            return View("DepartmanGetir",departman);  //değiştir
        }
        public ActionResult DepartmanGuncelle(Departman d)
        {
            var departman = c.Departmans.Find(d.DepartmanId);
            departman.Departmanad = d.Departmanad;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
		public ActionResult DepartmanDetay(int? id )
		{
			var degerler = c.Personels.Where(x => x.DepartmanId == id).ToList();
            var dpt = c.Departmans.Where(x => x.DepartmanId == id).Select(y => y.Departmanad).FirstOrDefault();
            ViewBag.d = dpt;

			return View(degerler);
		}
        public ActionResult  DepartmanPersonelSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.PersonelId == id).ToList();
            var per = c.Personels.Where(x => x.PersonelId == id).Select(y => y.PersonelAd+" "+y.PersonelSoyad).FirstOrDefault();
            ViewBag.dpers = per;
            return View(degerler);
        }
	}
}