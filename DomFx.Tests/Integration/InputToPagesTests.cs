using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using DomFx.Layouters;
using DomFx.Renderers.iTextSharp;
using DomFx.Renderers.PdfSharp.WPF;

namespace DomFx.Tests.Integration
{
    public class InputToPagesTests : IntegrationTestsBase
    {
        public InputToPagesTests(UnitOfMeasure unitOfMeasure) : base(unitOfMeasure) {}

        protected void ShowWithPdfSharp()
        {
            var pdfDocument = new PdfSharpRenderer().Render(pages, 21.cm(), 29.7.cm());
            pdfDocument.Save("test_pdfsharp_wpf.pdf");
            Process.Start("test_pdfsharp_wpf.pdf");
        }

        protected void ShowWithiTextSharp()
        {
            while (true)
            {
                var processes = Process.GetProcessesByName("AcroRd32").ToList();
                if (!processes.Any())
                    break;

                foreach (var process in processes)
                {
                    process.CloseMainWindow();
                    Thread.Sleep(100);
                }
            }

            var memoryStream = new iTextSharpRenderer().Render(pages, 21.cm(), 29.7.cm());
            File.WriteAllBytes("test_itextsharp.pdf", memoryStream.ToArray());

            Process.Start("test_itextsharp.pdf");
        }
    }
}