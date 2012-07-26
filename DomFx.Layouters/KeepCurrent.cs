using DomFx.Layouters.Actions;

namespace DomFx.Layouters
{
    public class KeepCurrent<T> : Change<T>
    {
        public static implicit operator StopOr<Change<T>>(KeepCurrent<T> current)
        {
            return new StopOr<Change<T>>(current);
        }
    }
}