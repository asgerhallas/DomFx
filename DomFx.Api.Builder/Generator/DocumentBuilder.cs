using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters.Specification;

namespace DomFx.Api.Builder.Generator
{
    public class DocumentBuilder<TSource> : IBuilder<TSource, Document>
    {
        readonly IBuilder<TSource, Section> builder;

        public DocumentBuilder(IBuilder<TSource, Section> builder)
        {
            this.builder = builder;
        }

        public IEnumerable<Document> Build(TSource input)
        {
            yield return new Document(builder.Build(input));
        }
    }

    public class ContentBuilder<TSource> : IBuilder<TSource, Content>
    {
        readonly IBuilder<TSource, ElementSpecification> builder;
        readonly Margins margins;

        public ContentBuilder(Margins margins, IBuilder<TSource, ElementSpecification> builder)
        {
            this.margins = margins;
            this.builder = builder;
        }

        public IEnumerable<Content> Build(TSource source)
        {
            yield return new Content(margins, builder.Build(source));
        }
    }

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