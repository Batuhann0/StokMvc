using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSTOK.Models.Entity;
namespace MvcSTOK.Controllers
{
    public class MusterilerController : Controller
    {
        // GET: Musteriler
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Musteri(string p)
        {
            //var degerler = db.Tbl_MUSTERILER.ToList();
            //return View(degerler);

            var degerler = from d in db.Tbl_MUSTERILER
                           select d;
            if (!string.IsNullOrEmpty(p)) //parametre boş değilse
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }

            return View(degerler.ToList());
        }

        #region Müşteri Ekleme
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(Tbl_MUSTERILER p2)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.Tbl_MUSTERILER.Add(p2);
            db.SaveChanges();
            return RedirectToAction("Musteri");
        }
        #endregion

        #region Müşteri Silme
        public ActionResult SIL(int id)
        {
            var müşteri = db.Tbl_MUSTERILER.Find(id);
            db.Tbl_MUSTERILER.Remove(müşteri);
            db.SaveChanges();
            return RedirectToAction("Musteri");
        }
        #endregion

        #region Müşteri Getir(GÜNCELLEME)

        public ActionResult MusteriGetir(int id)
        {
            var deger = db.Tbl_MUSTERILER.Find(id);
            return View("MusteriGetir", deger);
        }

        //MÜŞTERİ GÜNCELLEME
        public ActionResult MusteriGuncelle(Tbl_MUSTERILER musteri)
        {
            var deger = db.Tbl_MUSTERILER.Find(musteri.MUSTERIID);
            deger.MUSTERIAD = musteri.MUSTERIAD;
            deger.MUSTERISOYAD = musteri.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Musteri");
        }
        #endregion

    }
}