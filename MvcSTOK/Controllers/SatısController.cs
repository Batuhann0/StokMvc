using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSTOK.Models.Entity;

namespace MvcSTOK.Controllers
{
    public class SatısController : Controller
    {
        // GET: Satıs
        public ActionResult Satıs()
        {
            return View();
        }

        MvcDbStokEntities db = new MvcDbStokEntities();

        #region Satış Ekleme
        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(Tbl_SATISLAR satıslar)
        {
            db.Tbl_SATISLAR.Add(satıslar);
            db.SaveChanges();
            return View("Satıs");
        } 
        #endregion
    }
}