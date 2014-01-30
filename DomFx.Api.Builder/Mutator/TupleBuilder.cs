using System;

namespace DomFx.Api.Builder.Mutator
{
    public class TupleBuilder<TSource1, TSource2> : IBuilder<TSource1, TSource2>
    {
        readonly IBuilder<Tuple<TSource1, TSource2>> builder;

        public TupleBuilder(IBuilder<Tuple<TSource1, TSource2>> builder)
        {
            this.builder = builder;
        }

        public void Build(TSource1 source1, TSource2 source2)
        {
            builder.Build(Tuple.Create(source1, source2));
        }
    }

    public class TupleBuilder<TSource1, TSource2, TSource3> : IBuilder<TSource1, TSource2, TSource3>
    {
        readonly IBuilder<Tuple<TSource1, TSource2, TSource3>> builder;

        public TupleBuilder(IBuilder<Tuple<TSource1, TSource2, TSource3>> builder)
        {
            this.builder = builder;
        }

        public void Build(TSource1 source1, TSource2 source2, TSource3 source3)
        {
            builder.Build(Tuple.Create(source1, source2, source3));
        }
    }
}