using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using DomFx.Layouters;
using DomFx.Renderers;
using DomFx.Renderers.PdfSharp.WPF;
using DomFx.Renderers.iTextSharp;
using DomFx.Tests.Units;
using DomFx.Tests.Units.Api;
using DomFx.Tests.Units.Api.Fluent;

namespace DomFx.Tests.Integration
{
    public class input_to_pages_tests : content_tests
    {
        List<Page> pages;

        protected IEnumerable<Page> Layout()
        {
            Build();
            var lines = LiningLayouter.Layout(document.Sections.First().Content.Elements, 21.cm()).ToList();
            pages = new Layouter(document, 21.cm(), 29.7.cm()).Layout().ToList();
            return pages;
        }

        protected FixedElement Element(string name)
        {
            var element = pages.SelectMany(x => x.Elements).SingleOrDefault(x => x.Name == name);
            if (element == null)
                throw new ElementNotFoundException(name);
            return element;
        }

        protected int PageOfElement(string name)
        {
            var page = pages.SingleOrDefault(x => x.Elements.Any(e => e.Name == name));
            if (page == null)
                throw new ElementNotFoundException(name);
            return pages.IndexOf(page) + 1;
        }

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