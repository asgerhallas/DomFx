namespace DomFx.Api.Builder.Mutator
{
    public class ConditionalBuilder<TSource> : IBuilder<TSource>
    {
        readonly ISpecification<TSource> specification;
        readonly IBuilder<TSource> then;

        public ConditionalBuilder(ISpecification<TSource> specification, IBuilder<TSource> then)
        {
            this.specification = specification;
            this.then = then;
        }

        public void Build(TSource input)
        {
            if (specification.IsSatisfiedBy(input))
                then.Build(input);
        }
    }
}