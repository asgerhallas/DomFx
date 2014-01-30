using System.Collections.Generic;
using System.Linq;

namespace DomFx.Api.Builder.Generator
{
    public class ConditionalBuilder<TSource, TResult> : IBuilder<TSource, TResult>
    {
        readonly ISpecification<TSource> specification;
        readonly IBuilder<TSource, TResult> then;

        public ConditionalBuilder(ISpecification<TSource> specification, IBuilder<TSource, TResult> then)
        {
            this.specification = specification;
            this.then = then;
        }

        public IEnumerable<TResult> Build(TSource input)
        {
            if (specification.IsSatisfiedBy(input))
                return then.Build(input);

            return Enumerable.Empty<TResult>();
        }
    }
}