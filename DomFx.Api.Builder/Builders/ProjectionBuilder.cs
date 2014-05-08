using System.Collections.Generic;

namespace DomFx.Api.Builder.Builders
{
    public class ProjectionBuilder<TSource, TMiddle, TResult> : IBuilder<TSource, TResult>
    {
        readonly IProjection<TSource, TMiddle> projection;
        readonly IBuilder<TMiddle, TResult> builder;

        public ProjectionBuilder(
            IProjection<TSource, TMiddle> projection, 
            IBuilder<TMiddle, TResult> builder)
        {
            this.projection = projection;
            this.builder = builder;
        }

        public IEnumerable<TResult> Build(TSource input)
        {
            return builder.Build(projection.Project(input));
        }
    }
}