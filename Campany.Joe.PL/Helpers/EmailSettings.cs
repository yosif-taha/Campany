using System.Net;
using System.Net.Mail;

namespace Campany.Joe.PL.Helpers
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            //to send email: you need two parts : 1- mail server(gmail)
            //2- protocol used to send email (SMTP)

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587); //SmtpClient:- its (protocol) built-in class in .net used to send email - "smtp.gmail.com", 587 :- from google search used this word (smtp gmail)
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("yousif.t.abdulwahab@gmail.com", "rgqcuxkyvpntmnzi");  //this step for sender : its account for your app - //rgqcuxkyvpntmnzi:- this password from your account of gmail search on(app password)
                client.Send("yousif.t.abdulwahab@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch (Exception e )
            {
                return false;
            }


        }
    }
}
