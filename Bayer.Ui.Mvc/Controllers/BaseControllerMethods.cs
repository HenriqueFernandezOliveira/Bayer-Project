using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Bayer.Ui.Mvc.Controllers
{
    public static class BaseControllerMethods
    {
        public static string RenderPartialViewToString(Controller thisController, string viewName, object model, ViewDataDictionary ViewData = null)
        {
            // assign the model of the controller from which this method was called to the instance of the passed controller (a new instance, by the way)
            thisController.ViewData.Model = model;

            // initialize a string builder
            using (StringWriter sw = new StringWriter())
            {
                // find and load the view or partial view, pass it through the controller factory
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(thisController.ControllerContext, viewName);
                ViewContext viewContext = null;
                if (ViewData == null)
                {
                    viewContext = new ViewContext(thisController.ControllerContext, viewResult.View, thisController.ViewData, thisController.TempData, sw);
                }
                else
                {
                    ViewData.Model = model;
                    viewContext = new ViewContext(thisController.ControllerContext, viewResult.View, ViewData, thisController.TempData, sw);
                }
                // render it
                viewResult.View.Render(viewContext, sw);

                //return the razorized view/partial-view as a string
                return sw.ToString();
            }
        }


        public static void SendEmail(SmtpClient client, MailAddress from, List<MailAddress> to, string emailBody, string subject, List<string> CC, List<Attachment> filesToAttach, bool isbodyHtml = true)
        {
            MailMessage message = new MailMessage();
            message.Subject = subject;

            foreach (var email in to)
            {
                message.To.Add(email);
            }

            if (filesToAttach != null)
            {
                foreach (var file in filesToAttach)
                {
                    message.Attachments.Add(file);
                }
            }

            message.From = from;
            if (CC != null && CC.Count > 0)
            {
                message.CC.Add(CC.Aggregate((x, y) => x + "," + y));
            }

            message.Body = emailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.SubjectEncoding = Encoding.UTF8;
            message.IsBodyHtml = isbodyHtml;

            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
                throw e;
            }

            message.Dispose();
            client.Dispose();
        }
    }
}