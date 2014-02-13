using System;
using System.Collections.Generic;
using System.Linq;
using DomFx.Api.Builder.Builders;
using DomFx.Api.Builder.Styles;
using DomFx.Layouters;
using DomFx.Layouters.Specification.DocumentStructure;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Tests.Integration
{
    public class IntegrationTestsBase : ElementBuilder<int>
    {
        IEnumerable<Element> elements;
        List<Page> pages;

        public IntegrationTestsBase(UnitOfMeasure unitOfMeasure) : base(unitOfMeasure) {}

        public override IEnumerable<Element> Build(int source)
        {
            if (elements == null)
                return Enumerable.Empty<Element>();

            return elements;
        }

        public void Setup(Func<IEnumerable<Element>> setup)
        {
            elements = setup();
        }

        public void Setup(Func<Element> setup)
        {
            elements = new[] { setup() };
        }

        protected IEnumerable<Page> Layout()
        {
            var document = Composer<int>
                .Document(Composer<int>
                    .Section(content: Composer<int>
                        .Content(new Margins(), this))
                ).Build(42).Single();

            var lines = LiningLayouter.Layout(document.Sections.First().Content.Elements, 21.cm()).ToList();
            pages = new Layouter(document, 21.cm(), 29.7.cm()).Layout().ToList();
            return pages;
        }

        protected FixedElement Element(string name)
        {
            var element = pages.SelectMany(x => x.Elements).SingleOrDefault(x => x.Name == name);
            if (element == null)
                throw new ElementNotFoundException(name);
            return element;
        }
    }

    public class TestComposer : Composer<int>
    {
        public override IBuilder<int, Document> Compose()
        {
            return null;
        }
    }

}