using System;
using System.Collections.Generic;

namespace DomFx.Api.Builder.Mutator
{
    public abstract class Composer
    {
        public static CompositeBuilder<TSource> Compose<TSource>(
            params IBuilder<TSource>[] builders)
        {
            return new CompositeBuilder<TSource>(builders);
        }

        public static IBuilder<TSource> Compose<TSource, TResult>(
            IProjection<TSource, TResult> projection,
            params IBuilder<TResult>[] builders)
        {
            return new TransformationBuilder<TSource, TResult>(projection, 
                new CompositeBuilder<TResult>(builders));
        }

        public static ConditionalBuilder<TSource> When<TSource>(
            ISpecification<TSource> specification, IBuilder<TSource> then)
        {
            return new ConditionalBuilder<TSource>(specification, then);
        }


        public static IBuilder<TSource> Enumerate<TSource, TCollection>(
            IProjection<TSource, IEnumerable<TCollection>> source,
            IBuilder<TCollection> builder)
        {
            return new EnumeratingBuilder<TSource, TCollection>(source, builder);
        }
    }

    public abstract class Composer<TSource1> : Composer
    {
        public abstract IBuilder<TSource1> Compose();
    }
}
