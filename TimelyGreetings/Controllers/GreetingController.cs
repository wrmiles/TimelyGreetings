using Microsoft.AspNet.Identity;
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

namespace TimelyGreetings.Controllers
{
    [Authorize]
    public class GreetingController : Controller
    {
        private TimelyGreetingsEntities db = new TimelyGreetingsEntities();

        // GET: Greeting
        public ActionResult Index()
        {
            var greetings = db.Greetings.Include(g => g.OccassionType).Include(g => g.User);
            return View(greetings.ToList());
        }

        // GET: Greeting/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Greeting greeting = db.Greetings.Find(id);
            if (greeting == null)
            {
                return HttpNotFound();
            }
            return View(greeting);
        }

        // GET: Greeting/Create
        public ActionResult Create()
        {
            ViewBag.OccassionTypeID = new SelectList(db.OccassionTypes, "OccassionTypeID", "OccassionTypeName");

            // LOOK UP CORRECT TimelyGreetingsUserID By User.Identity.GetUserName();
            
            UserDataAccess uda = new UserDataAccess();
            Int64 loggedInTGUserID = uda.GetLoggedInTGUserIDByUserName(User.Identity.GetUserName());

            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", loggedInTGUserID);
                        
            return View();
        }

        // POST: Greeting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GreetingID,OccassionTypeID,UserID,DateCreated,DateToSend,DateDelivered,DateOpened,Subject,BodyText")] Greeting greeting)
        {
            if (ModelState.IsValid)
            {
                greeting.DateCreated = DateTime.Now;
                                
                db.Greetings.Add(greeting);
                int newGreetingID = db.SaveChanges();
                //return RedirectToAction("Index");

                // TODO:  GET GreetingID JUST CREATED

                return RedirectToAction("Create","Recipient", new { gid = newGreetingID });

            }

            ViewBag.OccassionTypeID = new SelectList(db.OccassionTypes, "OccassionTypeID", "OccassionTypeName", greeting.OccassionTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", greeting.UserID);
            return View(greeting);
        }

        // GET: Greeting/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Greeting greeting = db.Greetings.Find(id);
            if (greeting == null)
            {
                return HttpNotFound();
            }
            ViewBag.OccassionTypeID = new SelectList(db.OccassionTypes, "OccassionTypeID", "OccassionTypeName", greeting.OccassionTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", greeting.UserID);
            return View(greeting);
        }

        // POST: Greeting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GreetingID,OccassionTypeID,UserID,DateCreated,DateSent,DateDelivered,DateOpened,Subject,BodyText")] Greeting greeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(greeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OccassionTypeID = new SelectList(db.OccassionTypes, "OccassionTypeID", "OccassionTypeName", greeting.OccassionTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", greeting.UserID);
            return View(greeting);
        }

        // GET: Greeting/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Greeting greeting = db.Greetings.Find(id);
            if (greeting == null)
            {
                return HttpNotFound();
            }
            return View(greeting);
        }

        // POST: Greeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Greeting greeting = db.Greetings.Find(id);
            db.Greetings.Remove(greeting);
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
