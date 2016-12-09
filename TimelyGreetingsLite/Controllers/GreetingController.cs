using System;
using System.Collections;
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
    public class GreetingController : Controller
    {
        GreetingRepository greetingRepo = new GreetingRepository();
        ContextHelper cntxtHlpr = new ContextHelper();

        // GET: Greeting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Content(int id)
        {
            return View(greetingRepo.GetGreetingByID(id));
        }

        public ActionResult GreetingsByUser()
        {
            // GET CURRENT LOGGED IN USER ID BASED ON CURRENT CONTEXT USER
            Int64 userID = cntxtHlpr.GetCurrentUserID(User.Identity.Name);

            if (userID > 0)
            {
                return View(greetingRepo.GetGreetingsByUserID(userID));
            }
            else
            {
                return View();
            }
            
        }

        

        //// GET: Greeting/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View(greetingRepo.GetGreetingByID(id));
        //}

        // GET: Greeting/Create
        public ActionResult Create()
        {            
            PopulateOccassionTypeDropDown(null);
            return View();
        }

        // POST: Greeting/Create
        [HttpPost]
        public ActionResult Create(Greeting greeting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Int64 userID = cntxtHlpr.GetCurrentUserID(User.Identity.Name);
                    greeting.UserID = userID;
                    //greeting.DateCreated = DateTime.Now;                    
                    greetingRepo.AddGreeting(greeting);

                    //ViewBag.Message = "Records added successfully.";
                }

                return RedirectToAction("GreetingsByUser");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to create greeting. " + ex.Message, ex.InnerException);
            }
        }

        // GET: Greeting/Edit/5
        public ActionResult Edit(int id)
        {
            Greeting objGreet = greetingRepo.GetGreetingByID(id);
            PopulateOccassionTypeDropDown(objGreet.OccassionTypeID);
            return View(objGreet);
        }

        // POST: Greeting/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Greeting objGreet)
        {
            try
            {
                objGreet.GreetingID = id;
                greetingRepo.UpdateGreeting(objGreet);

                return RedirectToAction("GreetingsByUser");
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to update Greeting. " + ex.Message, ex.InnerException);
            }
        }

        //// GET: Greeting/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Greeting/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                greetingRepo.DeleteGreeting(id);

                return RedirectToAction("GreetingsByUser");
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to delete Greeting. " + ex.Message, ex.InnerException);
            }
        }

        //public void PopulateOccassionTypeSelectList(int? SelectedOccassionTypeID, out Greeting greet)
        //{
        //    // CREATE INSTANCE OF Greeting Class
        //    greet = new Greeting();

        //    // CREATE INSTANCE OF OccassionType REPOSITORY
        //    OccassionTypeRepository occRepo = new OccassionTypeRepository();

        //    // BIND PROPERTY
        //    //greet.OccassionTypeSelectList = new SelectList(occRepo.GetAllOccassionTypes() as IEnumerable(), "OccassionTypeID", "OccassionTypeName", SelectedOccassionTypeID);
        //    greet.OccassionTypes = occRepo.GetAllOccassionTypes();
        //    // PASS TO VIEWBAG THEN CALL VIEWBAG IN VIEW

        //    ViewBag.OccassionTypeSelectList = greet.OccassionTypes;

        //}

        private void PopulateOccassionTypeDropDown(int? selectedOccassionTypeID)
        {
            OccassionTypeRepository occRepo = new OccassionTypeRepository();           
            Greeting objGreet = new Greeting();
            objGreet.OccassionTypes = occRepo.GetAllOccassionTypes();
            if (selectedOccassionTypeID != null)
            {
                // EDIT MODE
                ViewBag.OccassionTypeID = new SelectList(objGreet.OccassionTypes, "OccassionTypeID", "OccassionTypeName", selectedOccassionTypeID);
            }
            else
            {
                ViewBag.OccassionTypeID = new SelectList(objGreet.OccassionTypes, "OccassionTypeID", "OccassionTypeName");
            }
            
        }


    }
}
