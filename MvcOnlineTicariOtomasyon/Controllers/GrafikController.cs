using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            var grafikciz = new Chart(600, 600);
            grafikciz.AddTitle("Kategori - Ürün Stok Sayısı ").AddLegend("Stok").AddSeries("Değerler",
                xValue: new[] { "Mobilya", "Telefon ve Aksesuar", "Ofis Eşyaları" },
                yValues: new[] { 85, 66, 98 }).Write();
            return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");
        }

        Context c = new Context();
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var sonuclar = c.Uruns.ToList();
            sonuclar.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            sonuclar.ToList().ForEach(y => yvalue.Add(y.Stok));
            var grafik = new Chart(width: 1000, height: 1000)
                .AddTitle("Stoklar")
                .AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult Index4()
        {
            return View();
        }
        public ActionResult VisualizeUrunResult()
        {
            return Json(Urunlistesi(), JsonRequestBehavior.AllowGet); //google chartlarda ki görsellere ulaşmak için gerekli komutlar

		}
        public List<sinif1> Urunlistesi()
        {
            List<sinif1> snf = new List<sinif1>();
            snf.Add(new sinif1()
            {
                urunad = "Bilgisayar",
                stok=120
            });
            snf.Add(new sinif1
            {
                urunad = "Beyaz Eşya",
                stok = 150
            });
			snf.Add(new sinif1
			{
				urunad = "Aydınlatıcı",
				stok = 250

			});
			snf.Add(new sinif1
			{
				urunad = "Ofis EŞyalar",
				stok = 100

			});
			snf.Add(new sinif1
			{
				urunad = "Kulaklık",
				stok =320

			});
            return snf;

		}
        public ActionResult Index5()
        {
            return View();
        }
		public ActionResult VisualizeUrunResult2()  /* Amacı, istemciye JSON formatında veri göndermektir.*/
        {
            return Json(UrunListesi2(), JsonRequestBehavior.AllowGet); /*UrunListesi2'den dönen liste JSON formatına dönüştürülür. */
			//JsonRequestBehavior.AllowGet: Bu parametre, tarayıcıdan GET yöntemi ile JSON verisi alınmasına izin verir. 


		}
		public List<Sinif2> UrunListesi2()
        {
            List<Sinif2> snf = new List<Sinif2>();
            using (var c = new Context()) /*using bloğu, işlem tamamlandıktan sonra bağlamın otomatik olarak kapatılmasını sağlar*/

			{
                snf = c.Uruns.Select(x => new Sinif2
                {
                    urn = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }
            return snf;
        }
        public ActionResult Index6()
        {
            return View();
        }
		public ActionResult Index7()
		{
			return View();
		}

	}
}