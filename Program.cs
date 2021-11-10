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
        static string emailFromAddress = "xxx@gmail.com,   xxx@yahoo.com"; //Sender Email Address  
        static string emailToAddress = "xxx@gmail.com"; //Receiver Email Address  

        static string subject = "Test Email";
        static string body = "Just testing out sending email";

        static void Main(string[] args)
        {
            SendEmail(emailFromAddress);
        }
        public static void SendEmail(string emailFromAddress)
        {
            emailFromAddress = RemoveWhiteSpace(emailFromAddress);
            var splittedEmailFromAddress = emailFromAddress.Split(',');

            foreach (var sender in splittedEmailFromAddress)
            {
                char[] spearator = { '@' };
                var splittedSender = sender.Split(spearator);

                if (splittedSender[1].ToUpper() == "GMAIL.COM")
                {
                    smtpAddress = gmailSmtpAddress;
                    portNumber = gmailPortNumber;
                    password = gmailPassword;
                }
                else if (splittedSender[1].ToUpper() == "YAHOO.COM")
                {
                    smtpAddress = yahooSmtpAddress;
                    portNumber = yahooPortNumber;
                    password = yahooPassword;
                }
                else
                {
                    Console.WriteLine("The sender is not a Gmail or Yahoo account");
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(sender);
                    mail.To.Add(emailToAddress);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(sender, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            
        }

        public static string RemoveWhiteSpace(string input)
        {
            return string.Join("", input.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
    }
}