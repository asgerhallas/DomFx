using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.DocumentStructure;

namespace DomFx.Api.Builder.Generator
{
    public class SectionBuilder<TSource> : IBuilder<TSource, Section>
    {
        readonly IBuilder<TSource, Content> header;
        readonly IBuilder<TSource, Content> content;
        readonly IBuilder<TSource, Content> footer;

        public SectionBuilder(
            IBuilder<TSource, Content> header, 
            IBuilder<TSource, Content> content,
            IBuilder<TSource, Content> footer)
        {
            this.header = header;
            this.content = content;
            this.footer = footer;
        }

        public IEnumerable<Section> Build(TSource input)
        {
            yield return new Section(
                header.Build(input).Single(), 
                content.Build(input).Single(), 
                footer.Build(input).Single());
        }
    }
}