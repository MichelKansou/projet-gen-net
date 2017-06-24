using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailTest
{
    class Program
    {
        static string sender = "projet-gen.exia@outlook.fr";
        static string password = "Exiaprojet";
        static string recipient = "florian.bellotti@viacesi.fr";

        static void Main(string[] args)
        {
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(sender, password);
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                var mail = new MailMessage(sender.Trim(), recipient.Trim());
                mail.Subject = "Alors ca marche";
                mail.Body = "ce message est envoyé en C# quel merde";
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

    }
}
