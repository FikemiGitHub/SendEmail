using System.Net;
using System.Net.Mail;
namespace SendMail
{
    class Program
    {
        static int portNumber; static string smtpAddress; static string password;

        static string gmailSmtpAddress = "smtp.gmail.com";
        static int gmailPortNumber = 587;
        static string gmailPassword = "yrxpuxryxgjhzmmw"; //Sender Password 
        static string yahooSmtpAddress = "smtp.mail.yahoo.com";
        static int yahooPortNumber = 465;
        static string yahooPassword = "yrxpuxryxgjhzmmw"; //Sender Password 

        static bool enableSSL = true;
        static string emailFromAddress = "fikemiff@gmail.com"; //Sender Email Address  
        static string emailToAddress = "fikemiff@gmail.com"; //Receiver Email Address  

        static string subject = "Test Email";
        static string body = "Just testing out sending email";

        static void Main(string[] args)
        {
            SendEmail();
        }
        public static void SendEmail()
        {
            char[] spearator = {'@'};
            var splittedEmailFromAddress = emailFromAddress.Split(spearator);
            if (splittedEmailFromAddress[1].ToUpper() == "GMAIL.COM")
            {
                smtpAddress = gmailSmtpAddress;
                portNumber = gmailPortNumber;
                password = gmailPassword;
            }

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailToAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment
                
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
}