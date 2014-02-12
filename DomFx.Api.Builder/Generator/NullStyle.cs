namespace DomFx.Api.Builder.Generator
{
    public class NullStyle<T> : IStyle<T> where T : IStyleBuilder
    {
        public void Apply(T style)
        {
        }
    }
}