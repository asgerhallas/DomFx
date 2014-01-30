namespace DomFx.Api.Builder.Generator
{
    public interface IProjection<in TSource, out TResult>
    {
        TResult Build(TSource source);
    }
}