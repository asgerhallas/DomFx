using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using DomFx.Api.Builder.Generator;
using DomFx.Layouters;
using DomFx.Layouters.Specification.DocumentStructure;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Tests.Api.Builder
{
    public class Header : ContentBuilder<int>
    {
        public Header(IBuilder<int, IElement> builder) 
            : base(new Margins(1.cm(), 2.cm(), 3.cm(), 4.cm()), builder) {}
    }

    

    public class MainContent : ElementBuilder<int>
    {
        readonly IBuilder<int, IElement> builder;
        
        public MainContent(IBuilder<int, IElement> builder) : base(UnitOfMeasure.Centimeter)
        {
            this.builder = builder;
        }

        public override IEnumerable<IElement> Build(int source)
        {
            yield return Box(
                flow: FlowStyle.Float,
                children: new[]
                {
                    Yield(builder, source),
                    Box()
                });
        }
    }

    public class MyBoxStyle : IStyle<StyleBuilder>
    {
        public void Apply(StyleBuilder style)
        {
            style.Float();
        }
    }


    public class Toc : Composer<int>
    {
        public Toc() : base(UnitOfMeasure.Centimeter) { }

        public override IBuilder<int, Document> Compose()
        {
            return Document(
                Section(
                    header: Content(builders: new NullBuilder<int, IElement>()),
                    content: Content(Margins.None(), 
                        new MainContent(new NullBuilder<int, IElement>())))
                );
        }
    }
}