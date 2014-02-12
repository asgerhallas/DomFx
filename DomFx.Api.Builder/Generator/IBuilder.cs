using System.Collections.Generic;

namespace DomFx.Api.Builder.Generator
{
    public interface IBuilder<in TSource, out TResult>
    {
        IEnumerable<TResult> Build(TSource source);
    }
}