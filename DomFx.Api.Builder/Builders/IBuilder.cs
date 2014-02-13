using System.Collections.Generic;

namespace DomFx.Api.Builder.Builders
{
    public interface IBuilder<in TSource, out TResult>
    {
        IEnumerable<TResult> Build(TSource source);
    }
}