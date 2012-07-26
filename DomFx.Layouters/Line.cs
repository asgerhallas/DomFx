using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters.Actions;

namespace DomFx.Layouters
{
    /// <summary>
    ///   Non-streaming collection of elements that all have the same top offset
    /// </summary>
    public class Line
    {
        readonly List<LayoutedElement> elements;
        readonly Unit offsetTop;
        bool allowStayPut;

        public Line(Unit top, bool keepWithNextLine, params LayoutedElement[] elements)
            : this(top, keepWithNextLine, elements.AsEnumerable())
        {
        }

        public Line(Unit top, bool keepWithNextLine, IEnumerable<LayoutedElement> elements)
        {
            KeepWithNextLine = keepWithNextLine;
            offsetTop = top;
            allowStayPut = true;

            this.elements = new List<LayoutedElement>(elements);

            var lineHeight = this.elements.Max(x => x.InnerHeight);
            foreach (var element in this.elements)
            {
                if (element.FollowLineHeight && element.InnerHeight < lineHeight)
                {
                    element.ForcedInnerHeight = lineHeight;
                }

                // set the final original inner sizes
                if (!element.InnerHeightBeforeSplitOrCrop.IsDefined)
                    element.InnerHeightBeforeSplitOrCrop = element.InnerHeight;

                if (!element.InnerWidthBeforeSplitOrCrop.IsDefined)
                    element.InnerWidthBeforeSplitOrCrop = element.InnerWidth;
            }
        }

        public IEnumerable<LayoutedElement> Elements
        {
            get { return elements; }
        }

        public IEnumerable<LayoutedElement> AllDecendants
        {
            get { return elements.SelectMany(x => x.Concat(x.AllDecendants)); }
        }

        public Unit Top
        {
            get { return offsetTop; }
        }

        public Unit Bottom
        {
            get { return Top + OuterHeight; }
        }

        public bool KeepWithNextLine { get; set; }

        public Unit OuterHeight
        {
            get { return elements.Select(x => x.OuterHeight).DefaultIfEmpty().Max(); }
        }

        public Unit OuterWidth
        {
            get
            {
                var left = elements.Select(x => x.Left).DefaultIfEmpty().Min();
                var right = elements.Select(x => x.Right).DefaultIfEmpty().Max();
                return right - left;
            }
        }

        public Line DoNotStayPut()
        {
            return new Line(Top, KeepWithNextLine, elements) { allowStayPut = false };
        }

        public Line StayPut()
        {
            return new Line(Top, KeepWithNextLine, elements) { allowStayPut = true };
        }

        public Line Crop(Unit spaceLeftBeforeSplit)
        {
            return new Line(Top, KeepWithNextLine, from element in elements select element.Crop(spaceLeftBeforeSplit));
        }

        public Line MoveDown(Unit offset)
        {
            return new Line(Top + offset, KeepWithNextLine, elements) { allowStayPut = allowStayPut };
        }

        Line MoveTo(Unit newTop)
        {
            return MoveDown(newTop - Top);
        }

        public StopOr<Change<Line>> FitTo(Pointer position, bool mayTryKeepTogether, bool forceIfNothingElseWorks)
        {
            if (position.Current != Top)
                return new Move<Line>(MoveTo(position.Current));

            var result = TryToStayPut(position, mayTryKeepTogether);

            if (result.OperationWasNotAllowed)
                result = TrySplit(position, fallBackToCrop: false);

            if (result.OperationWasNotAllowed)
                result = TryMove(position);

            if (result.OperationWasNotAllowed)
                result = TryKeepTogether(position, mayTryKeepTogether);

            if (result.OperationWasNotAllowed && forceIfNothingElseWorks)
                result = TrySplit(position, fallBackToCrop: true);

            return result;
        }

        StopOr<Change<Line>> TryToStayPut(Pointer position, bool mayTryKeepTogether)
        {
            if (!allowStayPut)
                return new Stop<Change<Line>>();

            if (position.PreviousLine != null && position.PreviousLine.KeepWithNextLine && mayTryKeepTogether)
            {
                if (position.NextPage - position.PageHeight > position.PreviousLine.Top
                    && Top >= position.PreviousLine.Bottom /*TODO er dette check nødvendigt, hvornår kan previousline ende efter toppen på currentline*/)
                    return new Stop<Change<Line>>();
            }

            if (LineIsNotCrossingTheSplitPoint(position.NextPage))
                return new KeepCurrent<Line>();

            return new Stop<Change<Line>>();
        }

        StopOr<Change<Line>> TrySplit(Pointer position, bool fallBackToCrop)
        {
            var upperElements = new List<LayoutedElement>();
            var lowerElements = new List<LayoutedElement>();

            var spaceLeftBeforeSplit = position.NextPage - position.Current;

            foreach (var element in elements)
            {
                var signal = element.Split(position.PageHeight, spaceLeftBeforeSplit);
                if (signal.OperationWasNotAllowed)
                {
                    if (!fallBackToCrop)
                        return new Stop<Change<Line>>();

                    if (!allowStayPut)
                        return new KeepTogether<Line>();

                    upperElements.Add(element.Crop(spaceLeftBeforeSplit));
                }
                else if (signal.Result is Changed<LayoutedElement>)
                {
                    upperElements.Add(signal.Result[0]);
                    lowerElements.Add(signal.Result[1]);
                }
                else
                {
                    upperElements.Add(element);
                }
            }

            if (lowerElements.Count > 0)
            {
                return new Changed<Line>(new Line(Top, false, upperElements),
                                         new Line(spaceLeftBeforeSplit, KeepWithNextLine, lowerElements));
            }

            return new Changed<Line>(new Line(Top, false, upperElements));
        }

        StopOr<Change<Line>> TryMove(Pointer position)
        {
            if (position.PreviousLine != null && position.PreviousLine.KeepWithNextLine)
                return new Stop<Change<Line>>();

            if (IsAtTopOfAnyPage(position))
                return new Stop<Change<Line>>();

            return new Move<Line>(MoveTo(position.NextPage));
        }

        StopOr<Change<Line>> TryKeepTogether(Pointer position, bool mayTryKeepTogether)
        {
            if (position.PreviousLine != null && position.PreviousLine.KeepWithNextLine && mayTryKeepTogether)
                return new KeepTogether<Line>();

            return new Stop<Change<Line>>();
        }

        bool IsAtTopOfAnyPage(Pointer position)
        {
            var spaceToEndOfPage = position.NextPage%position.PageHeight;

            if ((Top + (position.PageHeight - spaceToEndOfPage))%position.PageHeight == 0.cm())
            {
                return true;
            }

            return false;
        }

        bool LineIsNotCrossingTheSplitPoint(Unit topOfNextPage)
        {
            return LineEndsBeforeSplit(topOfNextPage) || LineBeginsAfterSplit(topOfNextPage);
        }

        bool LineBeginsAfterSplit(Unit topOfNextPage)
        {
            return Top >= topOfNextPage;
        }

        bool LineEndsBeforeSplit(Unit topOfNextPage)
        {
            return Bottom <= topOfNextPage;
        }
    }
}