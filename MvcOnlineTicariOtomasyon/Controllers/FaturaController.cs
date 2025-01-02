using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var list = c.Faturalars.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
		[HttpPost]
		public ActionResult FaturaEkle(Faturalar f)
		{
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult FaturaGetir(int id)
        {
            var fatura = c.Faturalars.Find(id);
            return View("FaturaGetir",fatura);

        }
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var fatura = c.Faturalars.Find(f.FaturaId);
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.FaturaSıraNo = f.FaturaSıraNo;
            fatura.VargiDairesi = f.VargiDairesi;
            fatura.Tarih = f.Tarih;
            fatura.Saat = f.Saat;
            fatura.TeslimEden = f.TeslimEden;
            fatura.TeslimAlan = f.TeslimAlan;
            c.SaveChanges();
            return RedirectToAction("Index");
                
        }
        public ActionResult FaturaDetay(int id)
        {
			var degerler = c.FaturaKalems.Where(x => x.FaturaId == id).ToList();
			//FaturaKalems tablosundaki FaturaId değeri id ile eşleşen tüm kayıtları filtreleyerek bir listeye dönüştürür(ToList()).
			var dpt = c.Faturalars.Where(x => x.FaturaId== id).Select(y => y.FaturaSıraNo).FirstOrDefault();
			ViewBag.d = dpt;

			return View(degerler);
		}
        [HttpGet]
        public ActionResult YeniKalem() {


            return View();

        }

		[HttpPost]
		public ActionResult YeniKalem(FaturaKalem p)
		{
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
		}

        public ActionResult Dinamik()
        {
            Class4 cs = new Class4();
            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();
            return View(cs);
        }

        public ActionResult FaturaKaydet(string FaturaSeriNo,string FaturaSıraNo, DateTime Tarih,string VargiDairesi,string Saat,
            string TeslimEden,string TeslimAlan,string ToplamTutar, FaturaKalem[] Kalemler)
            
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VargiDairesi = VargiDairesi;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            f.Saat = Saat;
            f.ToplamTutar =decimal.Parse( ToplamTutar);
            c.Faturalars.Add(f);
            foreach(var x in Kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;
                fk.BirimFiyat = x.BirimFiyat;
                fk.FaturaId = x.FaturaKalemId;
                fk.miktar = x.miktar;
                fk.Tutar = x.Tutar;

                c.FaturaKalems.Add(fk);

            }
            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
	}
}