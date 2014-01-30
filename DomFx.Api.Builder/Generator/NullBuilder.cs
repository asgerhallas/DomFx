using System.Collections.Generic;
using System.Linq;

namespace DomFx.Api.Builder.Generator
{
    public class NullBuilder<TSource, TResult> : IBuilder<TSource, TResult> 
    {
        public IEnumerable<TResult> Build(TSource source)
        {
            return Enumerable.Empty<TResult>();
        }
    }
}