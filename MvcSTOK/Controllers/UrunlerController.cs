using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSTOK.Models.Entity; //models klasörü içerisindeki entityi kullan

namespace MvcSTOK.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: Urunler
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Urunler()
        {
            var degerler = db.Tbl_URUNLER.ToList();
            return View(degerler);
        }

        #region Ürün Ekleme
        [HttpGet]
        public ActionResult YeniUrun()
        {
            //LİNQ: SORGUSULINQ, koleksiyonlar, ADO.Net DataSet, XML, SQL Server, 
            //Entity Framework ve diğer veritabanları gibi farklı veri kaynağı 
            //türlerinden veri almak için oluşturulmuş bir sorgu söz dizimidir. 

            //dropdownliste öğe çekiyor
            List<SelectListItem> degerler = (from i in db.Tbl_KATEGORILER.ToList() //TBL_KATEGORİLERİN listesini i değişkenine atadık
                                             select new SelectListItem //yeni listeyi seç
                                             {
                                                 Text = i.KATEGORIAD, //seçmiş olduğum listin textine i den gelen kategoriadı yazdır
                                                 Value = i.KATEGORIID.ToString() //arkaplanda dönecek değer
                                             }).ToList();

            ViewBag.dgr = degerler; //burdaki linq sorgusunu diğer sayfaya taşıyacak komut!!!!

            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(Tbl_URUNLER p3)
        {
            var ktg = db.Tbl_KATEGORILER.Where(m => m.KATEGORIID == p3.Tbl_KATEGORILER.KATEGORIID).FirstOrDefault(); //seçilen ilk değeri getir
            p3.Tbl_KATEGORILER = ktg; //yukardaki value değerine ulaşmak için eşleştirme işlemi yaptık

            db.Tbl_URUNLER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Urunler"); //ürünler sayfasına yönlendir
        }
        #endregion

        #region Ürün Silme
        public ActionResult SIL(int id)
        {
            var urun = db.Tbl_URUNLER.Find(id);
            db.Tbl_URUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Urunler");
        }
        #endregion

        #region URUN GETİR(GÜNCELLE)
        public ActionResult UrunGetir(int id)
        {
            //DROPDOWNLİSTE VERİYİ TAŞIMAK İÇİN
            List<SelectListItem> degerler = (from i in db.Tbl_KATEGORILER.ToList() //TBL_KATEGORİLERİN listesini i değişkenine atadık
                                             select new SelectListItem //yeni listeyi seç
                                             {
                                                 Text = i.KATEGORIAD, //seçmiş olduğum listin textine i den gelen kategoriadı yazdır
                                                 Value = i.KATEGORIID.ToString() //arkaplanda dönecek değer
                                             }).ToList();

            ViewBag.dgr = degerler; //burdaki linq sorgusunu diğer sayfaya taşıyacak komut!!!!

            var deger = db.Tbl_URUNLER.Find(id);
            return View("UrunGetir", deger);
        }

        public ActionResult UrunGüncelle(Tbl_URUNLER urunler)
        {
            var deger = db.Tbl_URUNLER.Find(urunler.URUNID);
            deger.URUNAD = urunler.URUNAD;
            deger.MARKA = urunler.MARKA;
            deger.STOK = urunler.STOK;
            deger.FIYAT = urunler.FIYAT;
           
            //deger.URUNKATEGORI = urunler.URUNKATEGORI;
            var ktg = db.Tbl_KATEGORILER.Where(m => m.KATEGORIID == urunler.Tbl_KATEGORILER.KATEGORIID).FirstOrDefault(); //seçilen ilk değeri getir
            deger.URUNKATEGORI = ktg.KATEGORIID;

            db.SaveChanges();

            return RedirectToAction("Urunler");
        }
        #endregion
    }
}