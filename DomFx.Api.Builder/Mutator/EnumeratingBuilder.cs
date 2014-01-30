using System.Collections.Generic;

namespace DomFx.Api.Builder.Mutator
{
    public class EnumeratingBuilder<TSource, TCollection> : IBuilder<TSource>
    {
        readonly IProjection<TSource, IEnumerable<TCollection>> projection;
        readonly IBuilder<TCollection> builder;

        public EnumeratingBuilder(IProjection<TSource, IEnumerable<TCollection>> projection, IBuilder<TCollection> builder)
        {
            this.projection = projection;
            this.builder = builder;
        }

        public void Build(TSource input)
        {
            foreach (var element in projection.Build(input))
            {
                builder.Build(element);
            }
        }
    }
}