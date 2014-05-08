namespace DomFx.Api.Builder
{
    public interface IProjection<in TSource, out TResult>
    {
        TResult Project(TSource source);
    }
}