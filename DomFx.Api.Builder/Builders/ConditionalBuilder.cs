using System.Collections.Generic;
using System.Linq;

namespace DomFx.Api.Builder.Builders
{
    public class ConditionalBuilder<TSource, TResult> : IBuilder<TSource, TResult>
    {
        readonly IProjection<TSource, bool> specification;
        readonly IBuilder<TSource, TResult> then;

        public ConditionalBuilder(IProjection<TSource, bool> specification, IBuilder<TSource, TResult> then)
        {
            this.specification = specification;
            this.then = then;
        }

        public IEnumerable<TResult> Build(TSource input)
        {
            return specification.Project(input) 
                ? then.Build(input) 
                : Enumerable.Empty<TResult>();
        }
    }
}