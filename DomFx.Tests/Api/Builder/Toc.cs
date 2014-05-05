using System.Collections.Generic;
using DomFx.Api.Builder.Builders;
using DomFx.Api.Builder.Styles;
using DomFx.Layouters;
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
        readonly IBuilder<int, IElement> builder;
        
        public MainContent(IBuilder<int, IElement> builder) : base(UnitOfMeasure.Centimeter)
        {
            this.builder = builder;
        }

        public override IEnumerable<Element> Build(int source)
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
}