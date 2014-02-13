using System.Collections.Generic;
using System.Linq;
using DomFx.Api.Builder.Styles;
using DomFx.Layouters.Specification.DocumentStructure;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Builders
{
    public class ContentBuilder<TSource> : IBuilder<TSource, Content>
    {
        readonly IBuilder<TSource, Element> builder;
        readonly Margins margins;

        public ContentBuilder(Margins margins, IBuilder<TSource, Element> builder)
        {
            this.margins = margins;
            this.builder = builder;
        }

        public IEnumerable<Content> Build(TSource source)
        {
            var elements = from generator in builder.Build(source) select generator(new NullStyle());
            yield return new Content(margins, elements);
        }
    }
}