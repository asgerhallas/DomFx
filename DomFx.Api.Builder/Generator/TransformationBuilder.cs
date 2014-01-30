using System.Collections.Generic;

namespace DomFx.Api.Builder.Generator
{
    public class TransformationBuilder<TSource, TCollection, TResult> : IBuilder<TSource, TResult>
    {
        readonly IProjection<TSource, IEnumerable<TCollection>> projection;
        readonly IBuilder<IEnumerable<TCollection>, TResult> builder;

        public TransformationBuilder(IProjection<TSource, IEnumerable<TCollection>> projection, IBuilder<IEnumerable<TCollection>, TResult> builder)
        {
            this.projection = projection;
            this.builder = builder;
        }

        public IEnumerable<TResult> Build(TSource input)
        {
            return builder.Build(
                projection.Build(input));
        }
    }
}