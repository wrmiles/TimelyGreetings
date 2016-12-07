using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimelyGreetings.Repository;
using TimelyGreetings.DataAccess;
using Microsoft.AspNet.Identity;

namespace TimelyGreetings.Controllers
{
    [Authorize]
    public class RecipientController : Controller
    {
        private TimelyGreetingsEntities db = new TimelyGreetingsEntities();

        // GET: Recipient
        public ActionResult Index()
        {
            var recipients = db.Recipients.Include(r => r.User).Include(r => r.Greeting);
            return View(recipients.ToList());
        }

        // GET: Recipient/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipient recipient = db.Recipients.Find(id);
            if (recipient == null)
            {
                return HttpNotFound();
            }
            return View(recipient);
        }

        // GET: Recipient/Create
        public ActionResult Create()
        {
            ViewBag.CreatedByUserID = new SelectList(db.Users, "UserID", "UserName");
            ViewBag.GreetingID = new SelectList(db.Greetings, "GreetingID", "Subject");
            
            // TODO:  GET THE LATEST GREETING ID WHICH HAS NOT ALREADY BEEN SENT OUT FOR THIS USER
            // TODO:  PASS THIS GID TO THE CREATE VIEW
            
            return View();
        }

        // POST: Recipient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipientID,FirstName,LastName,EmailAddress,DateCreated,CreatedByUserID,GreetingID")] Recipient recipient, int gid)
        {
            if (ModelState.IsValid)
            {
                recipient.DateCreated = DateTime.Now;
                UserDataAccess uda = new UserDataAccess();
                Int64 loggedInTGUserID = uda.GetLoggedInTGUserIDByUserName(User.Identity.GetUserName());
                recipient.CreatedByUserID = loggedInTGUserID;
                recipient.GreetingID = gid;
                db.Recipients.Add(recipient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedByUserID = new SelectList(db.Users, "UserID", "UserName", recipient.CreatedByUserID);
            ViewBag.GreetingID = new SelectList(db.Greetings, "GreetingID", "Subject", recipient.GreetingID);
            return View(recipient);
        }

        // GET: Recipient/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipient recipient = db.Recipients.Find(id);
            if (recipient == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedByUserID = new SelectList(db.Users, "UserID", "UserName", recipient.CreatedByUserID);
            ViewBag.GreetingID = new SelectList(db.Greetings, "GreetingID", "Subject", recipient.GreetingID);
            return View(recipient);
        }

        // POST: Recipient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipientID,FirstName,LastName,EmailAddress,DateCreated,CreatedByUserID,GreetingID")] Recipient recipient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedByUserID = new SelectList(db.Users, "UserID", "UserName", recipient.CreatedByUserID);
            ViewBag.GreetingID = new SelectList(db.Greetings, "GreetingID", "Subject", recipient.GreetingID);
            return View(recipient);
        }

        // GET: Recipient/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipient recipient = db.Recipients.Find(id);
            if (recipient == null)
            {
                return HttpNotFound();
            }
            return View(recipient);
        }

        // POST: Recipient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Recipient recipient = db.Recipients.Find(id);
            db.Recipients.Remove(recipient);
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
