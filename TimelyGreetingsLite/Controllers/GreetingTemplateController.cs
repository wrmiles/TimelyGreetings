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
    public class GreetingTemplateController : Controller
    {
        GreetingTemplateRepository greetingTmpltRepo = new GreetingTemplateRepository();
        

        // GET: Greeting
        public ActionResult Index()
        {
            return View(greetingTmpltRepo.GetAllGreetingTemplates());            
        }        

        // GET: GreetingTemplate/Create
        public ActionResult Create()
        {            
            PopulateOccassionTypeDropDown(null);
            return View();
        }

        // POST: Greeting/Create
        [HttpPost]
        public ActionResult Create(GreetingTemplate greetingTmplt)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    greetingTmpltRepo.AddGreetingTemplate(greetingTmplt);

                    //ViewBag.Message = "Records added successfully.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to create greeting template. " + ex.Message, ex.InnerException);
            }
        }

        // GET: Greeting/Edit/5
        public ActionResult Edit(int id)
        {
            GreetingTemplate objGreetTmplt = greetingTmpltRepo.GetGreetingTemplateByID(id);
            PopulateOccassionTypeDropDown(objGreetTmplt.OccassionTypeID);
            return View(objGreetTmplt);
        }

        // POST: Greeting/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, GreetingTemplate objGreetTmplt)
        {
            try
            {
                greetingTmpltRepo.UpdateGreetingTemplate(objGreetTmplt);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to update Greeting Template. " + ex.Message, ex.InnerException);
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
                greetingTmpltRepo.DeleteGreetingTemplate(id);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to delete Greeting Templae. " + ex.Message, ex.InnerException);
            }
        }

        

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
