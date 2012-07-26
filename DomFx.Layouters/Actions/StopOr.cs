namespace DomFx.Layouters.Actions
{
    public class StopOr<T> where T : Change
    {
        readonly T result;

        protected StopOr() : this(default(T))
        {
        }

        public StopOr(T result)
        {
            this.result = result;
        }

        public bool OperationWasNotAllowed
        {
            get { return Equals(result, default(T)); }
        }

        public T Result { get { return result; }}
    }
}