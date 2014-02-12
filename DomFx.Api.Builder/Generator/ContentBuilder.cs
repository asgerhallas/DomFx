using System.Collections.Generic;
using DomFx.Layouters.Specification.DocumentStructure;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Generator
{
    public class ContentBuilder<TSource> : IBuilder<TSource, Content>
    {
        readonly IBuilder<TSource, IElement> builder;
        readonly Margins margins;

        public ContentBuilder(Margins margins, IBuilder<TSource, IElement> builder)
        {
            this.margins = margins;
            this.builder = builder;
        }

        public IEnumerable<Content> Build(TSource source)
        {
            yield return new Content(margins, builder.Build(source));
        }
    }
}