using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters.Actions;

namespace DomFx.Layouters
{
    /// <summary>
    ///   Non-streaming collection of lines as children to an element
    /// </summary>
    public class Children : Lines
    {
        public Children(IEnumerable<Line> lines)
            : base(new List<Line>(lines))
        {
        }

        public static Children Empty
        {
            get { return new Children(Enumerable.Empty<Line>()); }
        }

        public Unit OuterHeight
        {
            get { return lines.Sum(x => x.OuterHeight); }
        }

        public Unit OuterWidth
        {
            get { return lines.Select(x => x.OuterWidth).DefaultIfEmpty().Max(); }
        }

        StopOr<Change<Children>> TryFitTo(Unit pageHeight, Unit spaceToEndOfPage)
        {
            var result = FitTo(pageHeight, spaceToEndOfPage, forceIfNothingElseWorks: false).ToList();
            if (result.Count == 0)
                return new Stop<Change<Children>>();

            return new Changed<Children>(new Children(from lines in result
                                                      from line in lines
                                                      select line));
        }

        public StopOr<Change<Children>> Split(Unit pageHeight, Unit spaceLeftToEndPage)
        {
            var signal = TryFitTo(pageHeight, spaceLeftToEndPage);

            if (signal.OperationWasNotAllowed)
                return new Stop<Change<Children>>();

            var upper = new Children(from change in signal.Result
                                     from line in change
                                     where line.Top < spaceLeftToEndPage
                                     select line);

            var lower = new Children(from change in signal.Result
                                     from line in change
                                     where line.Top >= spaceLeftToEndPage
                                     select line.MoveDown(-spaceLeftToEndPage));

            return new Changed<Children>(upper, lower);
            ;
        }
    }
}