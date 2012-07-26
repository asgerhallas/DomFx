using System;
using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters.Specification;

namespace DomFx.Layouters
{
    /// <summary>
    ///   Orchestrates the layouting from document model to fully layouted document
    ///   Applies horizontal and vertical constraints and includes headers and footers
    /// </summary>
    public class Layouter
    {
        readonly Document document;
        readonly Unit pageHeight;
        readonly Unit pageWidth;

        public Layouter(Document document, Unit pageWidth, Unit pageHeight)
        {
            this.document = document;
            this.pageWidth = pageWidth;
            this.pageHeight = pageHeight;
        }

        public IEnumerable<Page> Layout()
        {
            return (from section in document.Sections
                    let headerInnerWidth = pageWidth - section.Header.Margins.TotalHorizontal
                    let header = ElementsLayouter.Layout(LiningLayouter.Layout(section.Header.Elements, headerInnerWidth).AsLines())
                    let footerInnerWidth = pageWidth - section.Footer.Margins.TotalHorizontal
                    let footer = ElementsLayouter.Layout(LiningLayouter.Layout(section.Footer.Elements, footerInnerWidth).AsLines())
                    let headerHeight = header.OuterHeight + section.Header.Margins.TotalVertical
                    let footerHeight = footer.OuterHeight + section.Footer.Margins.TotalVertical
                    let pageInnerWidth = pageWidth - section.Content.Margins.TotalHorizontal
                    let pageInnerHeight = pageHeight - section.Content.Margins.TotalVertical - headerHeight - footerHeight
                    from page in PagingLayouter.Layout(pageInnerHeight, LiningLayouter.Layout(section.Content.Elements, pageInnerWidth).AsLines())
                    select page.Move(section.Content.Margins.Left, section.Content.Margins.Top + headerHeight)
                        .OverlayWith(header, section.Header.Margins.Left, section.Header.Margins.Top)
                        .OverlayWith(footer, section.Footer.Margins.Left, pageHeight - footerHeight));
        }
    }
}