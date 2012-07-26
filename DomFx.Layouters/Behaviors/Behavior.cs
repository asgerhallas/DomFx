namespace DomFx.Layouters.Behaviors
{
    public interface Behavior
    {
    }

    public interface Behavior<TElement> : Behavior
    {
        void Behave(TElement element);
    }
}