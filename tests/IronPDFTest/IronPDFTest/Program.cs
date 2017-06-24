using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPdf;


namespace IronPDFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
            HtmlToPdf.RenderHtmlAsPdf("<p>hello world</p>").SaveAs(@"" + path + "/File.Pdf");
        }
    }
}
