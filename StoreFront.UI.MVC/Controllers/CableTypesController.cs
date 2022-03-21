using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;

namespace StoreFront.UI.MVC.Controllers
{
    public class CableTypesController : Controller
    {
        private BurntheMeterEntities db = new BurntheMeterEntities();

        // GET: CableTypes
        public ActionResult Index()
        {
            return View(db.CableTypes.ToList());
        }

        // GET: CableTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CableType cableType = db.CableTypes.Find(id);
            if (cableType == null)
            {
                return HttpNotFound();
            }
            return View(cableType);
        }

        // GET: CableTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CableTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CableTypeID,CableTypeName,Description")] CableType cableType)
        {
            if (ModelState.IsValid)
            {
                db.CableTypes.Add(cableType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cableType);
        }

        // GET: CableTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CableType cableType = db.CableTypes.Find(id);
            if (cableType == null)
            {
                return HttpNotFound();
            }
            return View(cableType);
        }

        // POST: CableTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CableTypeID,CableTypeName,Description")] CableType cableType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cableType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cableType);
        }

        // GET: CableTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CableType cableType = db.CableTypes.Find(id);
            if (cableType == null)
            {
                return HttpNotFound();
            }
            return View(cableType);
        }

        // POST: CableTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CableType cableType = db.CableTypes.Find(id);
            db.CableTypes.Remove(cableType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
