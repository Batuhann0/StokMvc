using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSTOK.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcSTOK.Controllers
{
    public class KategoriController : Controller
    {
        //MVC (Model),(Controller),(View) baş harfleri mantığı veriyor, 
        //MODEL kısmı hangi nesneyi kullanacağımız,VİEW hangi sayfanın görüntüleneceği,
        //CONTROLLER ise action result metotları ile viewleri kontrol eden kısım

        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities(); //bu sınıf modelimizi tutuyor yani tabloları 

        #region LİSTELEME
        public ActionResult Index(int sayfa = 1)
        {
            // var degerler = db.Tbl_KATEGORILER.ToList(); //tbl_kategoriler tablom içerisinde bulunan değerleri listele(toListin görevi)
            var degerler = db.Tbl_KATEGORILER.ToList().ToPagedList(sayfa, 4); //burdaki 1:sayfa kaçtan başlasın bir sayfada 4 tane değer göster
            return View(degerler);
        }
        #endregion

        #region KATEGORİ EKLEME
        [HttpGet] //sayfa ilk yüklendiğinde çalışacak her yüklendiğinde kayıt eklemesin diye 
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost] //gönderme işlemi yaparsam bu çalışacak(ekle butonuna bastığım zaman devreye girecek)
        public ActionResult YeniKategori(Tbl_KATEGORILER p1)
        {
            if (!ModelState.IsValid) //doğrulanma işlemi yapılmadıysa ValidationMessageFor için
            {
                return View("YeniKategori");
            }

            db.Tbl_KATEGORILER.Add(p1);
            db.SaveChanges(); //değişiklikleri kaydet
            return View();
        }
        #endregion

        #region Kategori Silme
        public ActionResult SIL(int id)
        {
            var kategori = db.Tbl_KATEGORILER.Find(id);
            db.Tbl_KATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Kategori Getir(Güncelle)
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.Tbl_KATEGORILER.Find(id);
            return View("KategoriGetir", ktgr);
        }

        public ActionResult Guncelle(Tbl_KATEGORILER kategori)
        {
            var ktg = db.Tbl_KATEGORILER.Find(kategori.KATEGORIID);
            ktg.KATEGORIAD = kategori.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}