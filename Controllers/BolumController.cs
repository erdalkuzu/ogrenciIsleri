using OgrenciIsleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OgrenciIsleri.ViewModel;


namespace OgrenciIsleri.Controllers
{
    [Authorize]
    public class BolumController : Controller
    {
        // GET: Bolum
        public ActionResult List()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
              
                return View(db.Bolumlers.Include(x=>x.Ogrencilers).ToList());
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(Bolumler model)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                if (!ModelState.IsValid)
                    return View(model);
                if (model.ID == 0)
                    db.Bolumlers.Add(model);
                else
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("List");
            }
        }
        public ActionResult Update(int ID)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var find = db.Bolumlers.Find(ID);
                if (find == null)
                    return HttpNotFound();
                return View("Create",find);
            }
        }
        public ActionResult Delete(int ID)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var sil = db.Bolumlers.Find(ID);
                if (sil == null)
                    return HttpNotFound();
                db.Bolumlers.Remove(sil);
                db.SaveChanges();
                return RedirectToAction("List");
            }
        }
    }
}