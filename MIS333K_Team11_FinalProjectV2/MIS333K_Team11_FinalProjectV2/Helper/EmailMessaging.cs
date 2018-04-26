using System;
using System.Net.Mail;
using System.Net;

namespace MIS333K_Team11_FinalProjectV2
{
    public class EmailMessaging
    {
        private const string _emailServiceLogin = "Mis2018Team11@gmail.com";
        private const string _emailServicePassword = "mis333kteam11";
        private const string _senderEmail = "noreplay@team11movietheater.com";
        public static void SendEmail(String toEmail, String emailSubject, String emailBody)
        {
            try
            {
                //Create an email client to send the emails
                var client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailServiceLogin, _emailServicePassword);

                var finalMessage = emailBody + "<br/><br/>Thank you!<br/><br/>Team 11 Movie Theater Spring 2018";

                //Create an email address object for the sender address
                var senderEmail = new MailAddress(_senderEmail, "Team 11 Spring 2018");

                var mm = new MailMessage();

                mm.Subject = emailSubject;

                mm.Sender = senderEmail;

                mm.From = senderEmail;

                mm.To.Add(new MailAddress(toEmail));

                mm.Body = finalMessage; //adds a footer

                mm.IsBodyHtml = true;

                client.Send(mm);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}