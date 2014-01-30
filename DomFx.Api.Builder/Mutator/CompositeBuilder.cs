namespace DomFx.Api.Builder.Mutator
{
    public class CompositeBuilder<TSource> : IBuilder<TSource>
    {
        readonly IBuilder<TSource>[] builders;

        public CompositeBuilder(params IBuilder<TSource>[] builders)
        {
            this.builders = builders;
        }

        public void Build(TSource input)
        {
            foreach (var builder in builders)
            {
                builder.Build(input);
            }
        }
    }
}