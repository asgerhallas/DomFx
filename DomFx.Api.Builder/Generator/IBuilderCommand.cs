namespace DomFx.Api.Builder.Generator
{
    public interface IBuilderCommand<TSource>
    {
        void Build(TSource input);
    }
}