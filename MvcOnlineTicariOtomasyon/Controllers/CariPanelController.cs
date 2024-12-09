using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}