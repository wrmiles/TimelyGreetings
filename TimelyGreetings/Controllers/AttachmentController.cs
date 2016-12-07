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
    public class AttachmentController : Controller
    {
        private TimelyGreetingsEntities db = new TimelyGreetingsEntities();

        // GET: Attachment
        public ActionResult Index()
        {
            var attachments = db.Attachments.Include(a => a.Greeting);
            return View(attachments.ToList());
        }

        // GET: Attachment/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = db.Attachments.Find(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            return View(attachment);
        }

        // GET: Attachment/Create
        public ActionResult Create()
        {
            ViewBag.GreetingID = new SelectList(db.Greetings, "GreetingID", "Subject");
            return View();
        }

        // POST: Attachment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttachmentID,AttachmentType,AttachmentSize,AttachmentData,AttachmentURL,DateCreated,MessageID,GreetingID")] Attachment attachment)
        {
            if (ModelState.IsValid)
            {
                db.Attachments.Add(attachment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GreetingID = new SelectList(db.Greetings, "GreetingID", "Subject", attachment.GreetingID);
            return View(attachment);
        }

        // GET: Attachment/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = db.Attachments.Find(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            ViewBag.GreetingID = new SelectList(db.Greetings, "GreetingID", "Subject", attachment.GreetingID);
            return View(attachment);
        }

        // POST: Attachment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttachmentID,AttachmentType,AttachmentSize,AttachmentData,AttachmentURL,DateCreated,MessageID,GreetingID")] Attachment attachment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attachment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GreetingID = new SelectList(db.Greetings, "GreetingID", "Subject", attachment.GreetingID);
            return View(attachment);
        }

        // GET: Attachment/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = db.Attachments.Find(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            return View(attachment);
        }

        // POST: Attachment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Attachment attachment = db.Attachments.Find(id);
            db.Attachments.Remove(attachment);
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
