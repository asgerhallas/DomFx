using System.Collections.Generic;
using System.IO;
using System.Linq;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;

namespace DomFx.Renderers.iTextSharp
{
    public class iTextSharpRenderer
    {
        readonly BorderRenderer borderRenderer;
        readonly BackgroundRenderer backgroundRenderer;
        readonly ImageRenderer imageRenderer;
        readonly TextRenderer textRenderer;

        public iTextSharpRenderer()
        {
            borderRenderer = new BorderRenderer();
            backgroundRenderer = new BackgroundRenderer();
            imageRenderer = new ImageRenderer();
            textRenderer = new TextRenderer();
        }

        public MemoryStream Render(IEnumerable<Page> pages, Unit pageWidth, Unit pageHeight)
        {
            var currentPageNumber = 1;

            var document = new Document(new Rectangle((float) pageWidth.Points, (float) pageHeight.Points));
            
            var memoryStream = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();
            foreach (var page in pages)
            {
                foreach (var element in page.Elements)
                {
                    borderRenderer.Render(writer, element);

                    if (element.Specification is IBackgrounded)
                        backgroundRenderer.Render(writer, element);

                    if (element.Specification is IImaged)
                        imageRenderer.Render(writer, element);

                    if (element.Specification is ITexted)
                        textRenderer.Render(writer, element);
                }
                currentPageNumber++;
                document.NewPage();
            }

            document.Close();
            return memoryStream;
        }
    }
}