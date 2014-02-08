﻿using System;
using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters;
using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;
using DomFx.Tests.Fakes;
using Shouldly;

namespace DomFx.Tests.Units.Layouters.Lining
{
    public class LiningTests
    {
        readonly List<IElement> elements = new List<IElement>();
        int cardinality = 1;
        List<Line> lines;
        Page page;

        protected IElement Element(
            Unit innerWidth = default(Unit),
            Unit innerHeight = default(Unit),
            bool breakable = false,
            bool followLineHeight = false,
            FlowStyle flow = FlowStyle.Float,
            Unit border = default(Unit),
            bool keepWithNextLine = false,
            CompositeBehavior behavior = null,
            IEnumerable<Func<int, IElement>> children = null)
        {
            var generator = ChildElement(
                innerWidth, innerHeight, breakable,
                followLineHeight, flow, border,
                keepWithNextLine, behavior, children);
            
            var line = generator(cardinality++);
            elements.Add(line);
            return line;
        }

        protected Func<int, IElement> ChildElement(
            Unit innerWidth = default(Unit),
            Unit innerHeight = default(Unit),
            bool breakable = false,
            bool followLineHeight = false,
            FlowStyle flow = FlowStyle.Float,
            Unit border = default(Unit),
            bool keepWithNextLine = false,
            CompositeBehavior behavior = null,
            IEnumerable<Func<int, IElement>> children = null)
        {
            children = (children ?? Enumerable.Empty<Func<int, IElement>>());

            return cardinality => new TestSpecification(
                "Element#" + cardinality,
                children.Select((x, i) => x.Invoke(cardinality + i + 1)))
                {
                    InnerHeight = innerHeight,
                    InnerWidth = innerWidth,
                    FollowLineHeight = followLineHeight,
                    Flow = flow,
                    KeepWithNextLine = keepWithNextLine,
                    Breakable = breakable,
                    Behavior = behavior ?? new NullBehavior(),
                    Margins = new Margins {Bottom = border, Left = border, Right = border, Top = border}
                };
        }

        protected Page LayoutWithPageWidth(Unit pageWidth)
        {
            lines = LiningLayouter.Layout(elements, pageWidth).ToList();
            page = ElementsLayouter.Layout(lines.AsLines());
            return page;
        }

        protected Line Line(int i)
        {
            return lines[i - 1];
        }

        protected ElementAssertionWrapper Element(int i)
        {
            var name = string.Format("Element#{0}", i);
            return new ElementAssertionWrapper(Element(name));
        }

        FixedElement Element(string name)
        {
            var element = page.Elements.SingleOrDefault(x => x.Name == name);
            if (element == null)
                throw new ElementNotFoundException(name);
            return element;
        }

        protected class ElementAssertionWrapper
        {
            readonly FixedElement element;

            public ElementAssertionWrapper(FixedElement element)
            {
                this.element = element;
            }

            public void ShouldBeAt(Unit left, Unit top, Unit width, Unit height, Unit originalInnerWidth = default(Unit), Unit originalInnerHeight = default(Unit))
            {
                element.MarginBox.Top.ShouldBe(top);
                element.MarginBox.Left.ShouldBe(left);
                element.MarginBox.Width.ShouldBe(width);
                element.MarginBox.Height.ShouldBe(height);

                if (!originalInnerHeight.IsDefined)
                    originalInnerHeight = height - element.Specification.Edge.TotalVertical;

                element.InnerBoxBeforeSplitOrCrop.Height.ShouldBe(originalInnerHeight);

                if (!originalInnerWidth.IsDefined)
                    originalInnerWidth = width - element.Specification.Edge.TotalHorizontal;

                element.InnerBoxBeforeSplitOrCrop.Width.ShouldBe(originalInnerWidth);
            }
        }
    }
}