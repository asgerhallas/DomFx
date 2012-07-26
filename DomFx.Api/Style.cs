
namespace DomFx.Api
{
    public interface Style
    {
    }

    public interface Style<TBuilder> : Style
    {
        void Apply(TBuilder builder);
    }
}