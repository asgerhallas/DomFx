using System.Collections.Generic;
using DomFx.Layouters.Specification.DocumentStructure;

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
}