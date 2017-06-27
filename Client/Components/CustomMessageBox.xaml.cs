using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IronPdf;
using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using MahApps.Metro.Controls.Dialogs;

namespace Client.Components
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : MetroWindow
    {

        static string from = "projet-gen.exia@outlook.fr";
        static string password = "Exiaprojet";
        static string recipient = "florian.bellotti@viacesi.fr";

        private string emailBody;
        private string fileText;
        private string pdfContent;
        

        public CustomMessageBox()
        {
            InitializeComponent();
        }

        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(from, password);
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                var mail = new MailMessage(from.Trim(), recipient.Trim());
                mail.Subject = "Project Gen - Decoded Message";
                mail.Body = this.emailBody;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private void btnExportText_Click(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path += @"/test.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(this.fileText);
                }
            }
        }

        private void btnExportPDF_Click(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
            HtmlToPdf.RenderHtmlAsPdf(this.pdfContent).SaveAs(@"" + path + "/exportedFile.pdf");
        }

        public void setEmailBody(string emailBody)
        {
            this.emailBody = emailBody;
        }

        public void setExportText(string text)
        {
            this.fileText = text;
        }

        public void setPdfContent(string pdfContent)
        {
            this.pdfContent = pdfContent;
        }
    }
}
