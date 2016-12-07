using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimelyGreetings.Repository;

namespace TimelyGreetings.Controllers
{
    [Authorize]
    public class OccassionTypeController : Controller
    {
        private TimelyGreetingsEntities db = new TimelyGreetingsEntities();

        // GET: OccassionType
        public ActionResult Index()
        {
            return View(db.OccassionTypes.ToList());
        }

        // GET: OccassionType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OccassionType occassionType = db.OccassionTypes.Find(id);
            if (occassionType == null)
            {
                return HttpNotFound();
            }
            return View(occassionType);
        }

        // GET: OccassionType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OccassionType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OccassionTypeID,OccassionTypeName")] OccassionType occassionType)
        {
            if (ModelState.IsValid)
            {
                db.OccassionTypes.Add(occassionType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(occassionType);
        }

        // GET: OccassionType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OccassionType occassionType = db.OccassionTypes.Find(id);
            if (occassionType == null)
            {
                return HttpNotFound();
            }
            return View(occassionType);
        }

        // POST: OccassionType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OccassionTypeID,OccassionTypeName")] OccassionType occassionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(occassionType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(occassionType);
        }

        // GET: OccassionType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OccassionType occassionType = db.OccassionTypes.Find(id);
            if (occassionType == null)
            {
                return HttpNotFound();
            }
            return View(occassionType);
        }

        // POST: OccassionType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OccassionType occassionType = db.OccassionTypes.Find(id);
            db.OccassionTypes.Remove(occassionType);
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
