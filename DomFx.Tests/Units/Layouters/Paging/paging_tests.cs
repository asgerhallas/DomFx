using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters;
using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification;
using DomFx.Tests.Fakes;
using Shouldly;

namespace DomFx.Tests.Units.Layouters.Paging
{
    public class paging_tests
    {
        readonly List<Line> lines = new List<Line>();
        int elementNumber = 1;
        List<Page> pages;

        protected Unit OfPage
        {
            get { return Unit.Zero; }
        }

        protected Line Element(Unit left = default(Unit),
                               Unit top = default(Unit),
                               Unit innerHeight = default(Unit),
                               bool breakable = false,
                               bool followLineHeight = false,
                               Unit border = default(Unit),
                               bool keepWithNextLine = false,
                               Behaviors behavior = null,
                               IEnumerable<Line> children = null)
        {
            // Top offsets are not allowed in real usage, so we fake one by adding an element
            if (lines.Count == 0 && top > 0.cm())
            {
                lines.Add(new Line(0.cm(), false, new LayoutedElement(new TestSpecification
                {
                    Breakable = true,
                    OrphanHeight = 0.cm(),
                    WidowHeight = 0.cm()
                }, Children.Empty)
                {
                    Name = "Spacer",
                    ForcedInnerHeight = top,
                }));
            }

            var line = ChildElement(left, top, innerHeight, breakable, followLineHeight, border, keepWithNextLine, behavior, children);
            lines.Add(line);
            return line;
        }

        protected Line ChildElement(Unit left = default(Unit),
                                    Unit top = default(Unit),
                                    Unit innerHeight = default(Unit),
                                    bool breakable = false,
                                    bool followLineHeight = false,
                                    Unit border = default(Unit),
                                    bool keepWithNextLine = false,
                                    Behaviors behavior = null,
                                    IEnumerable<Line> children = null)
        {
            LayoutedElement element;
            if (children == null)
            {
                element = new LayoutedElement(new TestSpecification(), Children.Empty);
            }
            else
            {
                element = new LayoutedElement(new TestSpecification(), new Children(children.GroupBy(x => x.Top).SelectMany(x => x)));
            }

            element.Left = left;
            element.ForcedInnerHeight = innerHeight;
            element.Specification.FollowLineHeight = followLineHeight;
            element.Specification.Breakable = breakable;
            element.Specification.Margins = new Margins { Bottom = border, Left = border, Right = border, Top = border };
            element.Specification.Behavior = behavior ?? new NullBehavior();

            return new Line(top, keepWithNextLine, element);
        }

        void NameElements(IEnumerable<Line> lines)
        {
            foreach (var element in lines.SelectMany(x => x.AllDecendants))
            {
                if (element.Name != "Spacer")
                    element.Name = "Element#" + (elementNumber++);
            }
        }

        protected IEnumerable<Page> LayoutWithPageHeight(Unit pageHeight)
        {
            NameElements(lines);

            var mergedLines = lines
                .GroupBy(x => x.Top)
                .Select(x => new Line(x.Key, x.Any(y => y.KeepWithNextLine), x.SelectMany(y => y.Elements)))
                .AsLines();

            pages = PagingLayouter.Layout(pageHeight, mergedLines).ToList();
            return pages;
        }

        protected ElementAssertionWrapper Element(int i)
        {
            var name = string.Format("Element#{0}", i);
            return new ElementAssertionWrapper(Element(name), PageOfElement(name));
        }

        protected ElementAssertionWrapper Element(int i, int part)
        {
            var name = string.Format("Element#{0}-{1}", i, part);
            return new ElementAssertionWrapper(Element(name), PageOfElement(name));
        }

        FixedElement Element(string name)
        {
            var element = pages.SelectMany(x => x.Elements).SingleOrDefault(x => x.Name == name);
            if (element == null)
                throw new ElementNotFoundException(name);
            return element;
        }

        protected int PageOfElement(string name)
        {
            var page = pages.SingleOrDefault(x => x.Elements.Any(e => e.Name == name));
            if (page == null)
                throw new ElementNotFoundException(name);
            return pages.IndexOf(page) + 1;
        }

        protected class ElementAssertionWrapper
        {
            readonly FixedElement element;
            readonly int pageNumber;

            public ElementAssertionWrapper(FixedElement element, int pageNumber)
            {
                this.element = element;
                this.pageNumber = pageNumber;
            }

            public void ShouldBeAt(int page, Unit top, Unit height)
            {
                pageNumber.ShouldBe(page);
                element.MarginBox.Top.ShouldBe(top);
                element.MarginBox.Height.ShouldBe(height);
            }
        }
    }
}