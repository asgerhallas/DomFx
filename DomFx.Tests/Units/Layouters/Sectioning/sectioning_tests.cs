using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Tests.Fakes;
using Shouldly;

namespace DomFx.Tests.Units.Layouters.Sectioning
{
    public class sectioning_tests
    {
        readonly List<Section> sections = new List<Section>();
        int elementNumber = 1;
        List<Page> pages;

        protected Section Section(Content header = null,
                                  Content footer = null,
                                  Content content = null)
        {
            var section = new Section(header ?? DomFx.Layouters.Specification.Content.Empty,
                                      content ?? DomFx.Layouters.Specification.Content.Empty, footer ?? DomFx.Layouters.Specification.Content.Empty);

            sections.Add(section);
            return section;
        }

        protected Content Content(Unit border = default(Unit),
                                  IEnumerable<ElementSpecification> elements = null)
        {
            var content = new Content(new Margins { Bottom = border, Left = border, Right = border, Top = border }, elements);
            return content;
        }

        protected ElementSpecification Element(Unit innerWidth = default(Unit),
                                               Unit innerHeight = default(Unit),
                                               bool breakable = false,
                                               bool followLineHeight = false,
                                               FlowStyle flow = FlowStyle.Float,
                                               Unit border = default(Unit),
                                               bool keepWithNextLine = false,
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
                Margins = new Margins { Bottom = border, Left = border, Right = border, Top = border }
            };

            return element;
        }

        void NameElements(IEnumerable<ElementSpecification> lines)
        {
            foreach (var element in lines)
            {
                (element).Name = "Element#" + (elementNumber++);
                NameElements(element.Children);
            }
        }

        protected IEnumerable<Page> LayoutWithPage(Unit width, Unit height)
        {
            NameElements(sections.SelectMany(x => x.Header.Elements));
            NameElements(sections.SelectMany(x => x.Content.Elements));
            NameElements(sections.SelectMany(x => x.Footer.Elements));
            pages = new Layouter(new Document(sections), width, height).Layout().ToList();
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

            public void ShouldBeAt(int page, Unit left, Unit top, Unit height)
            {
                pageNumber.ShouldBe(page);
                element.MarginBox.Left.ShouldBe(left);
                element.MarginBox.Top.ShouldBe(top);
                element.MarginBox.Height.ShouldBe(height);
            }
        }
    }
}