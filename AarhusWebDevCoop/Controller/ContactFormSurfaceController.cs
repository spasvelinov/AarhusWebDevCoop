using AarhusWebDevCoop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Models;


namespace AarhusWebDevCoop.Controller
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index() 
        {
           
            return PartialView("ContactForm", new ContactFormViewModel());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }
           

            using (SmtpClient smtp = new SmtpClient())
            {
               // success for sending email
                TempData["success"] = true;
                // create message
                IContent emailMessage = Services.ContentService.CreateContent(model.Subject,CurrentPage.Id,"Message");

                // set value to message properties
                emailMessage.SetValue("senderName", model.Name);
                emailMessage.SetValue("email", model.Email);
                emailMessage.SetValue("subject", model.Subject);
                emailMessage.SetValue("message", model.Message);

                //Save
                Services.ContentService.Save(emailMessage);
 
            }
            return RedirectToCurrentUmbracoPage();
        }


    }
}