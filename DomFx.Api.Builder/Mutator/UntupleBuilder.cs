using System;

namespace DomFx.Api.Builder.Mutator
{
    public class UntupleBuilder<TSource1, TSource2> : IBuilder<Tuple<TSource1, TSource2>>
    {
        readonly IBuilder<TSource1, TSource2> builder;

        public UntupleBuilder(IBuilder<TSource1, TSource2> builder)
        {
            this.builder = builder;
        }

        public void Build(Tuple<TSource1, TSource2> source)
        {
            builder.Build(source.Item1, source.Item2);
        }
    }

    public class UntupleBuilder<TSource1, TSource2, TSource3> : IBuilder<Tuple<TSource1, TSource2, TSource3>>
    {
        readonly IBuilder<TSource1, TSource2, TSource3> builder;

        public UntupleBuilder(IBuilder<TSource1, TSource2, TSource3> builder)
        {
            this.builder = builder;
        }

        public void Build(Tuple<TSource1, TSource2, TSource3> source)
        {
            builder.Build(source.Item1, source.Item2, source.Item3);
        }
    }
}