using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace DomFx.Renderers.PdfSharp.WPF
{
    public class PdfSharpRenderer //: PdfRenderer<PdfDocument>
    {
        readonly BorderRenderer borderRenderer;
        readonly BackgroundRenderer backgroundRenderer;
        readonly ImageRenderer imageRenderer;
        readonly TextRenderer textRenderer;

        public PdfSharpRenderer()
        {
            borderRenderer = new BorderRenderer();
            backgroundRenderer = new BackgroundRenderer();
            imageRenderer = new ImageRenderer();
            textRenderer = new TextRenderer();
        }

        public PdfDocument Render(IEnumerable<Page> pages, Unit pageWidth, Unit pageHeight)
        {
            var currentPageNumber = 1;
            var totalPageNumber = pages.Count();
            var document = new PdfDocument();
            foreach (var page in pages)
            {
                var canvas = AddPageTo(document, pageWidth, pageHeight);
                foreach (var element in page.Elements)
                {
                    borderRenderer.Render(canvas, element);
                    backgroundRenderer.Render(canvas, element);

                    if (element.Specification is Image)
                        imageRenderer.Render(canvas, element);

                    if (element.Specification is Text)
                        textRenderer.Render(canvas, element);
                }
                currentPageNumber++;
            }

            return document;
        }

        static XGraphics AddPageTo(PdfDocument document, Unit width, Unit height)
        {
            PdfPage page = document.AddPage();
            page.Width = width.Points;
            page.Height = height.Points;

            return XGraphics.FromPdfPage(page);
        }
    }
}