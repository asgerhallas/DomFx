namespace DomFx.Api.Builder
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T input);
    }
}