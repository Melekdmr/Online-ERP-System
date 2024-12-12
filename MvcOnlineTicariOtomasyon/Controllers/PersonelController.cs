using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;


namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        Context c = new Context();
        // GET: Personel
        public ActionResult Index()
        {
            var degerler = c.Personels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
			List<SelectListItem> deger1 = (from x in c.Departmans.ToList() //Kategori verilerini bir dropdown list’te göstermek amacıyla gerekli 
										   select new SelectListItem      //SelectListItem formatına dönüştürür.
										   {                              //Böylece her bir kategori dropdown'da hem kullanıcıya anlamlı bir ad (Text),
											   Text = x.Departmanad,       //hem de arka planda kullanılabilecek bir kimlik(Value) sağlar.
											   Value = x.DepartmanId.ToString()


										   }).ToList();
			ViewBag.dgr1 = deger1;
			return View();

        }
        [HttpPost]
		public ActionResult PersonelEkle(Personel p)
		{
            if(Request.Files.Count > 0) //istekler arasında bir dosya mevcut ise
            {
                string dosyaadi= Path.GetFileName(Request.Files[0].FileName);
				//p.PersonelAd
				
				string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "/Image/" + dosyaadi + uzanti;

            }
           c.Personels.Add(p);
            c.SaveChanges();
			return RedirectToAction("Index");


		}

        public ActionResult PersonelGetir (int id)

        {
			List<SelectListItem> deger1 = (from x in c.Departmans.ToList() //Kategori verilerini bir dropdown list’te göstermek amacıyla gerekli 
										   select new SelectListItem      //SelectListItem formatına dönüştürür.
										   {                              //Böylece her bir kategori dropdown'da hem kullanıcıya anlamlı bir ad (Text),
											   Text = x.Departmanad,       //hem de arka planda kullanılabilecek bir kimlik(Value) sağlar.
											   Value = x.DepartmanId.ToString()


										   }).ToList();
			ViewBag.dgr1 = deger1;
			var per = c.Personels.Find(id);
            return View("PersonelGetir", per);
        }
        public ActionResult PersonelGuncelle(Personel p)
        {
            if(Request.Files.Count > 0) //istekler arasında bir dosya mevcut ise
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "/Image/" + dosyaadi + uzanti;

            }
            var per = c.Personels.Find(p.PersonelId);
            per.PersonelAd = p.PersonelAd;
            per.PersonelSoyad = p.PersonelSoyad;
            per.PersonelGorsel = p.PersonelGorsel;
            per.DepartmanId = p.DepartmanId;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //Personel Tema sayfasına geçiş
        public ActionResult PersonelListe()
        {
            var sorgu = c.Personels.ToList();
            return View(sorgu);
        }
	}
}