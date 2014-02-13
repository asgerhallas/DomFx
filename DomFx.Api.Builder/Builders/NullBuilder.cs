using System.Collections.Generic;
using System.Linq;

namespace DomFx.Api.Builder.Builders
{
    public class NullBuilder<TSource, TResult> : IBuilder<TSource, TResult> 
    {
        public IEnumerable<TResult> Build(TSource source)
        {
            return Enumerable.Empty<TResult>();
        }
    }
}