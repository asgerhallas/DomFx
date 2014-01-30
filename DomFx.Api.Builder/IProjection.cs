namespace DomFx.Api.Builder
{
    public interface IProjection<in TSource, out TResult>
    {
        TResult Build(TSource source);
    }
}