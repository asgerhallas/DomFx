namespace DomFx.Layouters.Actions
{
    public class KeepTogether<T> : Change<T>
    {
        public static implicit operator StopOr<Change<T>>(KeepTogether<T> current)
        {
            return new StopOr<Change<T>>(current);
        }
    }
}