using System.Collections.Generic;
using System.Linq;

namespace DomFx.Api.Builder.Builders
{
    public class EnumerationBuilder<TSource, TCollection, TResult> : IBuilder<TSource, TResult>
    {
        readonly IProjection<TSource, IEnumerable<TCollection>> projection;
        readonly IBuilder<TCollection, TResult> builder;

        public EnumerationBuilder(
            IProjection<TSource, IEnumerable<TCollection>> projection, 
            IBuilder<TCollection, TResult> builder)
        {
            this.projection = projection;
            this.builder = builder;
        }

        public IEnumerable<TResult> Build(TSource input)
        {
            return from element in projection.Project(input)
                from result in builder.Build(element)
                select result;
        }
    }
}