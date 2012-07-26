using System;

namespace DomFx.Layouters
{
    public class Pointer
    {
        readonly Unit pageHeight;
        readonly Line previousLine;
        readonly Unit spaceToEndOfPage;

        Pointer(Unit pageHeight, Unit spaceToEndOfPage, Unit current, Unit nextPage, Line previousLine)
        {
            this.pageHeight = pageHeight;
            this.spaceToEndOfPage = spaceToEndOfPage;
            this.previousLine = previousLine;
            Current = current;
            NextPage = nextPage;
        }

        Pointer(Pointer position, Unit current, Unit nextPage, Line previousLine)
            : this(position.pageHeight, position.spaceToEndOfPage, current, nextPage, previousLine)
        {
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
            return new Pointer(pageHeight, spaceToEndOfPage, 0.cm(), spaceToEndOfPage, null);
        }

        public Pointer GotoAfter(Line line)
        {
            return GotoAfter(line, line.Bottom);
        }

        public Pointer GotoAfter(Line line, Unit position)
        {
            var numberOfPageBreaksUntilPosition = Math.Floor((position - spaceToEndOfPage)/pageHeight) + 1;
            var nextPage = spaceToEndOfPage + numberOfPageBreaksUntilPosition*pageHeight;
            return new Pointer(this, position, nextPage, line);
        }

        public Pointer ChangePageIfNeccessary(Unit position)
        {
            return position >= NextPage ? GotoAfter(previousLine, position) : this;
        }
    }
}