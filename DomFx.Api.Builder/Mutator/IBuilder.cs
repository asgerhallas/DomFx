namespace DomFx.Api.Builder.Mutator
{
    public interface IBuilder<in TSource>
    {
        void Build(TSource source);
    }

    public interface IBuilder<in TSource1, in TSource2>
    {
        void Build(TSource1 source1, TSource2 source2);
    }

    public interface IBuilder<in TSource1, in TSource2, in TSource3>
    {
        void Build(TSource1 source1, TSource2 source2, TSource3 source3);
    }
}