using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimelyGreetingsLite.Models;
using TimelyGreetingsLite.Repository;
using TimelyGreetingsLite.Helpers;

namespace TimelyGreetingsLite.Controllers
{
    [Authorize]
    public class RecipientController : Controller
    {
        RecipientRepository recipRepo = new RecipientRepository();
        ContextHelper cntxtHlpr = new ContextHelper();

        // GET: Recipient
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RecipientsByGreeting(Int64 greetId)
        {
            // GET CURRENT LOGGED IN USER ID BASED ON CURRENT CONTEXT USER
            ViewBag.GreetingID = greetId;
            if (greetId > 0)
            {
                return View(recipRepo.GetRecipientsByGreetingID(greetId));
            }
            else
            {
                throw new Exception("Unable to list Recipients. ");
            }

        }

        //// GET: Recipient/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View(recipRepo.GetRecipientByID(id));
        //}

        // GET: Recipient/Create
        public ActionResult Create(Int64 greetId)
        {
            Recipient objRecip = new Recipient();
            objRecip.GreetingID = greetId;
            return View(objRecip);
        }

        //// GET: Reccipient/Create
        //public ActionResult Create(Int64 greetId)
        //{            
        //    return View();
        //}

        // POST: Recipient/Create
        [HttpPost]
        public ActionResult Create(Recipient recip, Int64 greetId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Int64 userID = cntxtHlpr.GetCurrentUserID(User.Identity.Name);
                    recip.CreatedByUserID = userID;
                    recip.GreetingID = greetId;
                    recipRepo.AddRecipient(recip);

                    //ViewBag.Message = "Records added successfully.";
                }

                return RedirectToAction("RecipientsByGreeting", new { greetId = greetId });
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to create recipient. " + ex.Message, ex.InnerException);
            }
        }

        // GET: Recipient/Edit/5
        public ActionResult Edit(int id)
        {
            Recipient objRecip = recipRepo.GetRecipientByID(id);
            
            return View(objRecip);
        }

        // POST: Recipient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Recipient objRecip)
        {
            try
            {
                objRecip.RecipientID = id;
                recipRepo.UpdateRecipient(objRecip);

                return RedirectToAction("RecipientsByGreeting");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to update Recipient. " + ex.Message, ex.InnerException);
            }
        }

        // GET: Recipient/Delete/5
        public ActionResult Delete(int id)
        {
            Recipient objRecip = recipRepo.GetRecipientByID(id);
            

            return View(objRecip);
        }

        // POST: Recipient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Recipient objRecip)
        {
            try
            {
                objRecip = recipRepo.GetRecipientByID(id);

                recipRepo.DeleteRecipient(id);

                return RedirectToAction("RecipientsByGreeting", new { greetId = objRecip.GreetingID });
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to delete Recipient. " + ex.Message, ex.InnerException);
            }
        }
    }
}
