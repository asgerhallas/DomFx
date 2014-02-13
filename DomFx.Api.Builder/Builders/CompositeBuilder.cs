using System.Collections.Generic;
using System.Linq;

namespace DomFx.Api.Builder.Builders
{
    public class CompositeBuilder<TSource, TResult> : IBuilder<TSource, TResult>
    {
        readonly IBuilder<TSource, TResult>[] builders;

        public CompositeBuilder(params IBuilder<TSource, TResult>[] builders)
        {
            this.builders = builders;
        }

        public IEnumerable<TResult> Build(TSource input)
        {
            return from builder in builders
                from result in builder.Build(input)
                select result;
        }
    }
}