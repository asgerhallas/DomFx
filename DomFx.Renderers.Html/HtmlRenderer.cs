using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DomFx.Layouters;
using DomFx.Renderers;

namespace DomFx.Renderer
{
    public class HtmlRenderer //: PdfRenderer<XElement>
    {
        public XElement Render(IEnumerable<Page> pages, Unit pageWidth, Unit pageHeight)
        {
            //var currentPageNumber = 1;
            //var totalPageNumber = pages.Count();
            //var document = new PdfDocument();
            //foreach (var page in pages)
            //{
            //    var canvas = "div";
            //    foreach (var element in page.Elements)
            //    {
            //    }
            //    currentPageNumber++;
            //}

            return new XElement("peter");
        }
    }
}