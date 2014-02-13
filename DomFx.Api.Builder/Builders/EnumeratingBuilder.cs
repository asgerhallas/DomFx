using System.Collections.Generic;
using System.Linq;

namespace DomFx.Api.Builder.Builders
{
    public class EnumeratingBuilder<TSource, TCollection, TResult> : IBuilder<TSource, TResult>
    {
        readonly IProjection<TSource, IEnumerable<TCollection>> projection;
        readonly IBuilder<TCollection, TResult> builder;

        public EnumeratingBuilder(IProjection<TSource, IEnumerable<TCollection>> projection, IBuilder<TCollection, TResult> builder)
        {
            this.projection = projection;
            this.builder = builder;
        }

        public IEnumerable<TResult> Build(TSource input)
        {
            return from element in projection.Build(input)
                from result in builder.Build(element)
                select result;
        }
    }
}