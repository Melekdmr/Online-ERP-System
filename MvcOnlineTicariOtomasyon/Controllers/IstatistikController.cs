using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;


namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        Context c = new Context();
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count().ToString();
            ViewBag.d1 = deger1;

			var deger2 = c.Uruns.Count().ToString();
			ViewBag.d2 = deger2;

			var deger3 = c.Personels.Count().ToString();
			ViewBag.d3 = deger3;

			var deger4 = c.Kategoris.Count().ToString();
			ViewBag.d4 = deger4;

            var deger5 = c.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.d5 = deger5;

            var deger6 = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = deger6;

          


			var deger7 = c.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.d7 = deger7;

            var deger8 = (from x in c.Uruns orderby x.SAtisFiyat descending select x.UrunAd).FirstOrDefault();
            ViewBag.d8 = deger8;

            var deger9 = (from x in c.Uruns orderby x.SAtisFiyat ascending select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = deger9;

            var deger10 = c.Uruns.Count(x => x.UrunAd == "Buzdolabı").ToString();
            ViewBag.d10 = deger10;

			var deger11 = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d11 = deger11;

            var deger14 = c.SatisHarekets.Sum(x => x.Toplamtutar).ToString();
            ViewBag.d14 = deger14;

            DateTime bugun = DateTime.Today;
            var deger15 = c.SatisHarekets.Count(x => x.Tarih == bugun).ToString("N2");
            ViewBag.d15 = deger15;

			/*   var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y => y.Toplamtutar).ToString();
			   ViewBag.d16 = deger16;*/
			
			var deger16 = c.SatisHarekets
						   .Where(x => x.Tarih == bugun)
						   .Sum(y => (decimal?)y.Toplamtutar) ?? 0; // Null kontrolü ve sıfır varsayılan değer
			ViewBag.d16 = deger16.ToString("N2"); // 2 basamaklı formatlama



			var deger12 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d12 = deger12;

            var deger13 = c.Uruns.Where(u=>u.UrunId==c.SatisHarekets.GroupBy(x => x.UrunId).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault()).Select(k=>k.UrunAd).FirstOrDefault();
            ViewBag.d13 = deger13;

            return View();
        }
        public ActionResult KolayTablolar()
        {
            var sorgu =from x in c.Carilers group x by x.CariSehir into g  /* Bu satırda, Carilers tablosundaki her kaydı, CariSehir (şehir) alanına göre gruplar. Yani, aynı şehirdeki müşteriler bir araya getirilir. */
					   select new SinifGrup /* Bu, her grup için yeni bir SinifGrup nesnesi oluşturur. SinifGrup modeli, her şehir için iki özellik içerir: */
					   {
                           Sehir=g.Key,   /* Sehir: Gruplamanın anahtar değeri, yani şehir adı.*/
						   /* Burada Carilers tablosundan veri çekiliyor, ancak veri çekme işlemi 
						    * sadece şehre göre gruplama ve sayma işlemleriyle özelleştirilmiş.
						    * Yani veriyi doğrudan almak yerine, tablodaki her kaydın şehir (CariSehir) 
						    * sütununa göre gruplandığı bir işlem gerçekleştiriliyor. Bu, daha anlamlı ve
						    * düzenli bir veri sunumu sağlamak amacıyla yapılır.*/
						   sayi = g.Count()  /* sayi: Bu şehirdeki toplam kayıt sayısını gösteren değer.*/

                       };
            return View(sorgu.ToList()); /* Son olarak, sorgu.ToList() ile verileri listeye dönüştürüp, View'a gönderir.  */

		}

        public PartialViewResult Partial1()
        {   //her departmanda kaç adet personel olduğunu sorguluyoruz
            var sorgu2 = from x in c.Personels
                         group x by x.Departman.Departmanad into g
                         select new SinifGrup2
                         {
                             Departman = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu2.ToList());
        }
        public PartialViewResult Partial2()
        {

            var sorgu1 = c.Carilers.ToList();
            return PartialView(sorgu1);      
        }
        public PartialViewResult Partial3()
        {
            var sorgu = c.Uruns.ToList();
            return PartialView(sorgu);

        }
        public PartialViewResult Partial4()
		{
			var sorgu = from x in c.Uruns
						 group x by x.Marka into g
						 select new SinifGrup3
						 {
							marka = g.Key,
							 sayi = g.Count()
						 };
			return PartialView(sorgu.ToList());

		}
    
    }

}