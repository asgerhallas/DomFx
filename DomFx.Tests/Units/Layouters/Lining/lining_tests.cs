using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters;
using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification;
using DomFx.Tests.Fakes;
using Shouldly;

namespace DomFx.Tests.Units.Layouters.Lining
{
    public class lining_tests
    {
        readonly List<ElementSpecification> elements = new List<ElementSpecification>();
        int elementNumber = 1;
        List<Line> lines;
        Page page;

        protected ElementSpecification Element(Unit innerWidth = default(Unit),
                                               Unit innerHeight = default(Unit),
                                               bool breakable = false,
                                               bool followLineHeight = false,
                                               FlowStyle flow = FlowStyle.Float,
                                               Unit border = default(Unit),
                                               bool keepWithNextLine = false,
                                               Behaviors behavior = null,
                                               IEnumerable<ElementSpecification> children = null)
        {
            var line = ChildElement(innerWidth, innerHeight, breakable, followLineHeight, flow, border, keepWithNextLine, behavior, children);
            elements.Add(line);
            return line;
        }

        protected ElementSpecification ChildElement(Unit innerWidth = default(Unit),
                                                    Unit innerHeight = default(Unit),
                                                    bool breakable = false,
                                                    bool followLineHeight = false,
                                                    FlowStyle flow = FlowStyle.Float,
                                                    Unit border = default(Unit),
                                                    bool keepWithNextLine = false,
                                                    Behaviors behavior = null,
                                                    IEnumerable<ElementSpecification> children = null)
        {
            var element = new TestSpecification(children != null ? children.ToList() : new List<ElementSpecification>())
            {
                InnerHeight = innerHeight,
                InnerWidth = innerWidth,
                FollowLineHeight = followLineHeight,
                Flow = flow,
                KeepWithNextLine = keepWithNextLine,
                Breakable = breakable,
                Behavior = behavior ?? new NullBehavior(),
                Margins = new Margins { Bottom = border, Left = border, Right = border, Top = border }
            };

            return element;
        }

        void NameElements(IEnumerable<ElementSpecification> lines)
        {
            foreach (var element in lines)
            {
                element.Name = "Element#" + (elementNumber++);
                NameElements(element.Children);
            }
        }

        protected Page LayoutWithPageWidth(Unit pageWidth)
        {
            NameElements(elements);

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