using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DomFx.Layouters.Actions
{
    public abstract class Change<T> : IEnumerable<T>, Change
    {
        readonly List<T> changes;

        protected Change(params T[] changes)
        {
            this.changes = changes.ToList();
        }

        public T this[int index]
        {
            get { return changes[index]; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return changes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public interface Change
    {
    }
}