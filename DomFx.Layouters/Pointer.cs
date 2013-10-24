using System;

namespace DomFx.Layouters
{
    public class Pointer
    {
        readonly int pageNumber;
        readonly Unit pageHeight;
        readonly Line previousLine;
        readonly Unit spaceToEndOfPage;

        Pointer(int pageNumber, Unit pageHeight, Unit spaceToEndOfPage, Unit current, Unit nextPage, Line previousLine)
        {
            this.pageNumber = pageNumber;
            this.pageHeight = pageHeight;
            this.spaceToEndOfPage = spaceToEndOfPage;
            this.previousLine = previousLine;
            Current = current;
            NextPage = nextPage;
        }

        public Unit PageHeight
        {
            get { return pageHeight; }
        }

        public Line PreviousLine
        {
            get { return previousLine; }
        }

        public Unit Current { get; private set; }
        public Unit NextPage { get; private set; }

        public static Pointer Origo(Unit pageHeight, Unit spaceToEndOfPage)
        {
            return new Pointer(0, pageHeight, spaceToEndOfPage, 0.cm(), spaceToEndOfPage, null);
        }

        public Pointer GotoAfter(Line line)
        {
            return GotoAfter(line, line.Bottom);
        }

        public Pointer GotoAfter(Line line, Unit position)
        {
            var nextPageNumber = (position >= NextPage) ? pageNumber + 1 : pageNumber;
            var nextPage = spaceToEndOfPage + nextPageNumber * pageHeight;
            return new Pointer(nextPageNumber, pageHeight, spaceToEndOfPage, position, nextPage, line);
        }

        public Pointer ChangePageIfNeccessary(Unit position)
        {
            return position >= NextPage ? GotoAfter(previousLine, position) : this;
        }
    }
}