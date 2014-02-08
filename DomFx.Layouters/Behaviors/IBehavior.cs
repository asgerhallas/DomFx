namespace DomFx.Layouters.Behaviors
{
    public interface IBehavior
    {
    }

    public interface IBehavior<in TElement> : IBehavior
    {
        void Behave(TElement element);
    }
}