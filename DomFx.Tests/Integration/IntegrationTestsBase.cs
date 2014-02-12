using System;
using System.Collections.Generic;
using System.Linq;
using DomFx.Api.Builder.Generator;
using DomFx.Layouters;
using DomFx.Layouters.Specification.DocumentStructure;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Tests.Integration
{
    public class IntegrationTestsBase : ElementBuilder<int>
    {
        IEnumerable<IElement> elements;
        List<Page> pages;

        public IntegrationTestsBase(UnitOfMeasure unitOfMeasure) : base(unitOfMeasure) {}

        public override IEnumerable<IElement> Build(int source)
        {
            if (elements == null)
                return Enumerable.Empty<IElement>();

            return elements;
        }

        public void Setup(Func<IEnumerable<IElement>> setup)
        {
            elements = setup();
        }

        public void Setup(Func<IElement> setup)
        {
            elements = new[] { setup() };
        }

        protected IEnumerable<Page> Layout()
        {
            var document = Composer<int>
                .Document(Composer<int>
                    .Section(content: Composer<int>
                        .Content(DomFx.Layouters.Specification.Style.Margins.None(), this))
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
        public TestComposer(UnitOfMeasure unitOfMeasure) : base(unitOfMeasure) {}
        public override IBuilder<int, Document> Compose()
        {
            return null;
        }
    }

}