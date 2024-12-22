using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
		// GET: CariPanel
	
		Context c = new Context();
		[Authorize] /*[Authorize] özniteliğinin yalnızca method veya class seviyesinde kullanılabilir.[Authorize]
		             *özniteliğinin, Context c = new Context(); gibi bir field tanımının üzerinde yer alması syntax hatasına neden olur.*/
		public ActionResult Index()
        {
			var mail = (string)Session["CariMail"]; //Amaç: Kullanıcının oturum bilgilerini almak.Session,
											//kullanıcının tarayıcı oturumu boyunca veri saklamak için kullanılır.
													//oturumdan alınan değeri açıkça bir string türüne dönüştürür.
			var degerler = c.Carilers.FirstOrDefault(x => x.CariMail == mail);
			ViewBag.m = mail;
            return View(degerler);
        }
		public ActionResult Siparislerim()
		{
			var mail = (string)Session["CariMail"];
			var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariId).FirstOrDefault();
			var degerler = c.SatisHarekets.Where(x => x.CariId == id).ToList();
			return View(degerler);
		}
		public ActionResult GelenMesajlar()
		{
			var mail = (string)Session["CariMail"];
			var mesajlar = c.Mesajlars.Where(x=>x.Gonderilen==mail).OrderByDescending(x=>x.MesajId)  .ToList();
			var gelensayisi = c.Mesajlars.Count(x => x.Gonderilen == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = c.Mesajlars.Count(x => x.Gonderen == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View(mesajlar);
			
		}
		public ActionResult GidenMesajlar()
		{
			var mail = (string)Session["CariMail"];
			var mesajlar = c.Mesajlars.Where(x => x.Gonderen == mail).OrderByDescending(z=> z.MesajId).ToList();
			var gelensayisi = c.Mesajlars.Count(x => x.Gonderilen == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = c.Mesajlars.Count(x => x.Gonderen == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View(mesajlar);
		}
		public ActionResult MesajDetay(int id)
		{
			var degerler = c.Mesajlars.Where(x => x.MesajId == id).ToList();
			var mail = (string)Session["CariMail"];
			var gelensayisi = c.Mesajlars.Count(x => x.Gonderilen == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = c.Mesajlars.Count(x => x.Gonderen == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View(degerler);
		}
		[HttpGet]
		public ActionResult YeniMesaj()
		{
			var mail = (string)Session["CariMail"];
			var gelensayisi = c.Mesajlars.Count(x => x.Gonderilen == mail).ToString();
			ViewBag.d1 = gelensayisi;
			var gidensayisi = c.Mesajlars.Count(x => x.Gonderen == mail).ToString();
			ViewBag.d2 = gidensayisi;
			return View();
		}

		[HttpPost]
		public ActionResult YeniMesaj(Mesajlar m)
		{
			var mail = (string)Session["CariMail"];
			m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
			m.Gonderen = mail;
			c.Mesajlars.Add(m);
			c.SaveChanges();
			
			return View();
		}
		public ActionResult KargoTakip(string p)
		{
			var k = from x in c.KargoDetays select x;
			if (!string.IsNullOrEmpty(p))
			{
				k = k.Where(y => y.TakipKodu.Contains(p));
			}
			return View(k.ToList());
		}
		public ActionResult CariKargoTakip(string id)
		{

			var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();


			return View(degerler);
		}
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Index","Login");
		}
	}
}