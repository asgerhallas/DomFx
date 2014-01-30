namespace DomFx.Api.Builder.Generator
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T input);
    }
}