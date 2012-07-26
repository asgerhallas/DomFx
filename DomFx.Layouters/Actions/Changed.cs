namespace DomFx.Layouters.Actions
{
    public class Changed<T> : Change<T>
    {
        public Changed(params T[] changes)
            : base(changes)
        {
        }

        public static implicit operator StopOr<Change<T>>(Changed<T> current)
        {
            return new StopOr<Change<T>>(current);
        }
    }
}