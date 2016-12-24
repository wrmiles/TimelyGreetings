using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimelyGreetingsLite.Models;
using TimelyGreetingsLite.Repository;
using TimelyGreetingsLite.Helpers;
using System.IO;

namespace TimelyGreetingsLite.Controllers
{
    public class AttachmentController : Controller
    {
        AttachmentRepository attchRepo = new AttachmentRepository();
        ContextHelper cntxtHlpr = new ContextHelper();

        // GET: Attachment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AttachmentsByGreeting(Int64 greetId)
        {
            // GET CURRENT LOGGED IN USER ID BASED ON CURRENT CONTEXT USER
            ViewBag.GreetingID = greetId;
            if (greetId > 0)
            {
                return View(attchRepo.GetAttachmentsByGreetingID(greetId));
            }
            else
            {
                throw new Exception("Unable to list Attachments For This Greeting. ");
            }

        }

        //// GET: Attachment/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Attachment/Create
        public ActionResult Create(Int64 greetId)
        {
            Attachment objAttch = new Attachment();
            objAttch.GreetingID = greetId;
            //return View(objAttch);
            return RedirectToAction("Index", "Upload", new { greetId = greetId });
        }
            
        
        //// POST: Attachment/Create
        //[HttpPost]
        //public ActionResult Create(HttpPostedFileBase fileUpload1)
        //{
        //    //HttpPostedFileBase postedFile = Request.Files["fileUpload"];

        //    try
        //    {
               


        //        //if (ModelState.IsValid)
        //        //{
        //        //    Int64 userID = cntxtHlpr.GetCurrentUserID(User.Identity.Name);
                    
        //        //    objAttch.GreetingID = greetId;
        //        //    attchRepo.AddAttachment(objAttch);

        //        //    //ViewBag.Message = "Records added successfully.";
        //        //}

        //        return RedirectToAction("AttachmentsByGreeting", new { greetId = 0 });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Unable to create recipient. " + ex.Message, ex.InnerException);
        //    }
        //}

        // GET: Attachment/Edit/5
        public ActionResult Edit(Int64 id)
        {
            Attachment objAttch = attchRepo.GetAttachmentByID(id);

            return View(objAttch);
        }

        // POST: Attachment/Edit/5
        [HttpPost]
        public ActionResult Edit(Int64 id, Attachment objAttch)
        {
            try
            {
                objAttch.AttachmentID = id;
                attchRepo.UpdateAttachment(objAttch);

                return RedirectToAction("AttachmentsByGreeting");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to update Attachment. " + ex.Message, ex.InnerException);
            }
        }

        // GET: Attachment/Delete/5
        public ActionResult Delete(Int64 id)
        {
            Attachment objAttch = attchRepo.GetAttachmentByID(id);
            return View(objAttch);
        }

        // POST: Attachment/Delete/5
        [HttpPost]
        public ActionResult Delete(Int64 id, Attachment objAttch)
        {
            try
            {
                objAttch = attchRepo.GetAttachmentByID(id);

                attchRepo.DeleteAttachment(id);

                return RedirectToAction("AttachmentsByGreeting", new { greetId = objAttch.GreetingID });
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to delete Attachment. " + ex.Message, ex.InnerException);
            }
        }


        
    }
}
