namespace DomFx.Api.Builder.Mutator
{
    public class TransformationBuilder<TSource, TResult> : IBuilder<TSource>
    {
        readonly IProjection<TSource, TResult> projection;
        readonly IBuilder<TResult> builder;

        public TransformationBuilder(IProjection<TSource, TResult> projection, IBuilder<TResult> builder)
        {
            this.projection = projection;
            this.builder = builder;
        }

        public void Build(TSource input)
        {
            builder.Build(projection.Build(input));
        }
    }
}