using System.Collections.Generic;
using DomFx.Api.Builder;
using DomFx.Api.Builder.Builders;
using DomFx.Api.Builder.Styles;
using DomFx.Layouters;
using DomFx.Layouters.Specification.DocumentStructure;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Tests.Api.Builder
{
    public class Header : ContentBuilder<int>
    {
        public Header(IBuilder<int, Element> builder) 
            : base(new Margins(1.cm(), 2.cm(), 3.cm(), 4.cm()), builder) {}
    }

    public class MainContent : ElementBuilder<int>
    {
        readonly IBuilder<int, Element> builder;
        
        public MainContent(IBuilder<int, Element> builder) : base(UnitOfMeasure.Centimeter)
        {
            this.builder = builder;
        }

        public override IEnumerable<Element> Build(int source)
        {
            yield return Box(null, children: Box());
        }
    }

    public class MainContentString : ElementBuilder<string>
    {
        public MainContentString() : base(UnitOfMeasure.Centimeter) { }

        public override IEnumerable<Element> Build(string source)
        {
            yield return Box(null, children: Box());
        }
    }

    public class MyBoxStyle : IStyle
    {
        public void Apply(IStyleApplicator applicator)
        {
            applicator.Float();
        }
    }

    public class Toc : Composer<int>
    {
        public override IBuilder<int, Document> Compose()
        {
            return Document(
                Section(
                    content: Content(new MainContent(
                        With(new StringFromSource(), Compose(
                            new MainContentString()))))
                    ));
        }
    }

    public class StringFromSource : IProjection<int, string> {
        public string Project(int source)
        {
            return source.ToString();
        }
    }
}