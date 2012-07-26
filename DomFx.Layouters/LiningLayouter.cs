using System;
using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification;

namespace DomFx.Layouters
{
    /// <summary>
    ///   Positions a section's document elements with respect to section width constraint
    /// </summary>
    public class LiningLayouter
    {
        readonly Unit containerInnerHeight;
        readonly Unit containerInnerWidth;

        LiningLayouter(Unit containerInnerWidth, Unit containerInnerHeight)
        {
            if (containerInnerWidth <= 0.cm())
                throw new ArgumentException("Inner width must be larger than zero");

            this.containerInnerWidth = containerInnerWidth;
            this.containerInnerHeight = containerInnerHeight;
        }

        public static IEnumerable<Line> Layout(IEnumerable<ElementSpecification> elements, Unit pageWidth)
        {
            return new LiningLayouter(pageWidth, 0.cm()).PositionElements(elements);
        }

        IEnumerable<Line> PositionElements(IEnumerable<ElementSpecification> elements)
        {
            var elementsInLine = new List<LayoutedElement>();
            var lineWidth = 0.cm();
            var lineTop = 0.cm();
            var keepWithNextLine = false;
            foreach (var element in elements)
            {
                var breakBeforeThisElement = false;
                element.Behavior.Apply<WidthBehavior>(element);

                var layoutedElement = LayoutElement(element);

                if (layoutedElement.OuterWidth > containerInnerWidth)
                    layoutedElement.ForcedOuterWidth = containerInnerWidth;

                if (element.InnerWidth == Unit.Undefined
                    && elementsInLine.Any(x => x.ForcedInnerWidth == Unit.Zero))
                    breakBeforeThisElement = true;

                var containerSpaceLeft = containerInnerWidth - lineWidth;
                if (containerSpaceLeft == Unit.Zero)
                    breakBeforeThisElement = true;

                if (element.Flow == FlowStyle.Clear
                    || lineWidth + layoutedElement.OuterWidth > containerInnerWidth)
                    breakBeforeThisElement = true;

                if (breakBeforeThisElement)
                {
                    if (elementsInLine.Any())
                    {
                        var line = GetLine(lineWidth, elementsInLine, lineTop, keepWithNextLine);
                        yield return line;
                        lineTop += line.OuterHeight;
                    }

                    ConstrainOuterHeightToContainerInnerHeight(layoutedElement, lineTop);

                    lineWidth = layoutedElement.OuterWidth;
                    layoutedElement.Left = 0.cm();
                    elementsInLine = new List<LayoutedElement> { layoutedElement };
                    keepWithNextLine = element.KeepWithNextLine;
                }
                else
                {
                    keepWithNextLine |= element.KeepWithNextLine;

                    layoutedElement.Left = lineWidth;

                    elementsInLine.Add(layoutedElement);
                    lineWidth += layoutedElement.OuterWidth;
                }
            }


            if (elementsInLine.Any())
                yield return GetLine(lineWidth, elementsInLine, lineTop, keepWithNextLine);
        }

        LayoutedElement LayoutElement(ElementSpecification element)
        {
            var hasDefinedWidth = element.InnerWidth != Unit.Undefined;
            var hasDefinedHeight = element.InnerHeight != Unit.Undefined;

            if (element.Children.Any())
            {
                var verticalSpaceForChildren = hasDefinedWidth ? element.InnerWidth : containerInnerWidth - element.Edge.TotalHorizontal;
                var heightForChildren = hasDefinedHeight ? element.InnerHeight : Unit.Max(containerInnerHeight - element.Edge.TotalVertical, Unit.Zero);
                var lines = new LiningLayouter(verticalSpaceForChildren, heightForChildren).PositionElements(element.Children);
                var children = new Children(lines);

                return new LayoutedElement(element, children)
                {
                    Name = element.Name,
                    ForcedInnerWidth = hasDefinedWidth ? element.InnerWidth : children.OuterWidth,
                    ForcedInnerHeight = element.InnerHeight,
                };
            }

            if (hasDefinedWidth)
            {
                return new LayoutedElement(element, Children.Empty)
                {
                    Name = element.Name,
                    ForcedInnerWidth = element.InnerWidth,
                    ForcedInnerHeight = element.InnerHeight,
                };
            }

            return new LayoutedElement(element, Children.Empty)
            {
                Name = element.Name,
                ForcedInnerWidth = Unit.Undefined,
                ForcedInnerHeight = element.InnerHeight
            };
        }

        void ConstrainOuterHeightToContainerInnerHeight(LayoutedElement positionedElement, Unit lineTop)
        {
            if (containerInnerHeight != 0.cm() && lineTop + positionedElement.OuterHeight > containerInnerHeight)
                positionedElement.ForcedOuterHeight = containerInnerHeight - lineTop;
        }

        Line GetLine(Unit lineWidth, List<LayoutedElement> elementsInLine, Unit lineTop, bool keepWithNextLine)
        {
            DistributeRemainingSpaceToElementsWithUndefinedWidth(lineWidth, elementsInLine);
            return new Line(lineTop, keepWithNextLine, elementsInLine);
        }

        void DistributeRemainingSpaceToElementsWithUndefinedWidth(Unit lineWidth, List<LayoutedElement> elementsInLine)
        {
            var elementWithUndefinedWidth = elementsInLine.SingleOrDefault(x => !x.ForcedInnerWidth.IsDefined);
            if (elementWithUndefinedWidth != null)
            {
                var containerSpaceLeft = containerInnerWidth - lineWidth;
                if (containerSpaceLeft == 0.cm())
                    containerSpaceLeft = containerInnerWidth;

                elementWithUndefinedWidth.ForcedInnerWidth = containerSpaceLeft;
            }

            var left = Unit.Zero;
            var top = Unit.Zero;
            foreach (var layoutedElement in elementsInLine)
            {
                layoutedElement.Left = left;
                left += layoutedElement.OuterWidth;

                if (containerInnerHeight != 0.cm() && top + layoutedElement.OuterHeight > containerInnerHeight)
                    layoutedElement.ForcedOuterHeight = containerInnerHeight - top;

                top += layoutedElement.OuterHeight;
            }
        }
    }
}