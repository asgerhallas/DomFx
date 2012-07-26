namespace DomFx.Layouters.Actions
{
    public class Move<T> : Changed<T>
    {
        public Move(params T[] changes) : base(changes)
        {
        }
    }
}