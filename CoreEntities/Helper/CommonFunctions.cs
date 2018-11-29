using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CoreEntities.Helper
{
    public class CommonFunctions
    {
        public static bool SendEmail(string emailTo, string emailSubject, string emailText, string Mailbcc = null)
        {
            bool status = false;

            string smtpAddress = ConfigurationManager.AppSettings["MailServer"];
            int portNumber = Convert.ToInt32(ConfigurationManager.AppSettings["MailServerPort"]);
            string emailFrom = ConfigurationManager.AppSettings["UserName"];
            string password = ConfigurationManager.AppSettings["FromAddresspassword"];
            string subject = emailSubject;
            string body = emailText;

            using (MailMessage mail = new MailMessage())
            
            {
                mail.From = new MailAddress(emailFrom);
                //mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                if (!String.IsNullOrEmpty(Mailbcc))
                {
                    mail.Bcc.Add(Mailbcc);
                }
             
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;                

                // Can set to false, if you are sending pure text.

                //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpAddress;
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = emailFrom;
                NetworkCred.Password = password;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = portNumber;
                smtp.Send(mail);
                //using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                //{
                //    smtp.UseDefaultCredentials = true;
                //    smtp.Credentials = new NetworkCredential(emailFrom, password);
                //    smtp.EnableSsl = true;                      
                //    smtp.Send(mail);
                //}
            }

            return status;
        }
    }
}
