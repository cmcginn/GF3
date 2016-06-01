using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Greenflag.Services
{
    public class EmailService
    {
        public string GetToddress()
        {
            var result = System.Configuration.ConfigurationManager.AppSettings["contactSubmissionRecipient"];
            return result;
        }

        public List<string> GetBccList()
        {
            var result = System.Configuration.ConfigurationManager.AppSettings["contactSubmissionBccList"].Split(',').ToList();
            return result;
        }

        public async Task<bool> SendContactSubmission(string emailAddress, string name, string comments, string phone)
        {
            var result = false;
            try
            {
                var fromAddress = "greenflagsoftware@gmail.com";
                var message = new MailMessage();
               // message.From = new MailAddress(fromAddress);
                message.To.Add(new MailAddress(GetToddress()));
                GetBccList().ForEach(x => message.Bcc.Add(new MailAddress(x)));
                message.Subject = "Webform inquiry from Green Flag";
                var body = String.Format("Name:{0}{1}", name, System.Environment.NewLine);
                body += String.Format("Email:{0}{1}", emailAddress, System.Environment.NewLine);
                body += String.Format("Phone:{0}{1}", phone, System.Environment.NewLine);
                body += comments;
               
                message.Body = body;
                var client = new SmtpClient();
                await client.SendMailAsync(message);
                
                result = true;
            }
            catch
            {
                //swallow
            }
            return result;
        }
    }
}