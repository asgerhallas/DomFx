namespace DomFx.Api.Builder.Mutator
{
    public class NullBuilder<TSource> : IBuilder<TSource>
    {
        public void Build(TSource source)
        {
        }
    }
}