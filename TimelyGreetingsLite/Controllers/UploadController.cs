using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using TimelyGreetingsLite.Models;
using TimelyGreetingsLite.Repository;
using TimelyGreetingsLite.Helpers;

namespace TimelyGreetingsLite.Controllers
{
    public class UploadController : Controller
    {
        AttachmentRepository attchRepo = new AttachmentRepository();
        ContextHelper cntxtHlpr = new ContextHelper();

        // GET: Upload
        public ActionResult Index(Int64 greetId)
        {
            ViewBag.GreetingID = greetId;
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile, Int64 greetId)
        {
            Attachment attachment = new Attachment();
            attachment.GreetingID = greetId;

            if (postedFile != null)
            {
                Int64 dtStamp = DateTime.Now.ToFileTimeUtc();
                string uniqueFileName = attachment.GreetingID.ToString() + "_" + dtStamp.ToString() + "_" + postedFile.FileName;

                attachment.AttachmentName = uniqueFileName;
                attachment.AttachmentSize = postedFile.ContentLength.ToString();
                attachment.AttachmentType = postedFile.ContentType;
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                postedFile.SaveAs(path + Path.GetFileName(uniqueFileName));
                attachment.AttachmentURL = path + Path.GetFileName(uniqueFileName);

                // TODO: SAVE AttachmentData (varbinary);
                
                attchRepo.AddAttachment(attachment);

                ViewBag.Message = "File uploaded successfully.";
            }

            return RedirectToAction("AttachmentsByGreeting", "Attachment", new { greetId = greetId });
        }
    }
}