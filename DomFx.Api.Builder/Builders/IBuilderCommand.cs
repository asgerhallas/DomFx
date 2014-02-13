namespace DomFx.Api.Builder.Builders
{
    public interface IBuilderCommand<TSource>
    {
        void Build(TSource input);
    }
}