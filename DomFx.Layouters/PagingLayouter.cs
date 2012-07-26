using System;
using System.Collections.Generic;
using System.Linq;

namespace DomFx.Layouters
{
    /// <summary>
    ///   Distribute positioned elements on pages based on a page height constraint
    /// </summary>
    public class PagingLayouter
    {
        readonly Unit pageHeight;

        PagingLayouter(Unit pageHeight)
        {
            this.pageHeight = pageHeight;
        }

        Unit TopOfNextPage(int pageNumber)
        {
            return pageNumber * pageHeight;
        }

        Unit TopOfPage(int pageNumber)
        {
            return (pageNumber - 1) * pageHeight;
        }

        public static IEnumerable<Page> Layout(Unit pageHeight, Lines lines)
        {
            if (pageHeight <= 0.cm())
                throw new ArgumentException("Page height must be larger than zero");

            var pagingLayouter = new PagingLayouter(pageHeight);
            return pagingLayouter.Layout(lines);
        }

        IEnumerable<Page> Layout(Lines root)
        {
            var pagedDocument = new List<Page>();
            var currentPage = Page.First();
            var currentPageNumer = 1;
            foreach (var line in from lines in root.FitTo(pageHeight, pageHeight)
                                 from line in lines
                                 select line)
            {
                if (line.Top >= TopOfNextPage(currentPageNumer))
                {
                    pagedDocument.Add(currentPage);
                    currentPage = currentPage.Next();
                    currentPageNumer++;
                }

                currentPage.Add(line, TopOfPage(currentPageNumer));
            }

            pagedDocument.Add(currentPage);
            return pagedDocument;
        }
    }
}