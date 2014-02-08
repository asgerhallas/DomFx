using System.Collections.Generic;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.DocumentStructure;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Generator
{
    public abstract class Composer<T>
    {
        public abstract IBuilder<T, Document> Compose();

        public static IBuilder<TSource, Document> Document<TSource>(IBuilder<TSource, Section> builder)
        {
            return new DocumentBuilder<TSource>(builder);
        }

        public static IBuilder<TSource, Section> Section<TSource>(
            IBuilder<TSource, Content> header = null,
            IBuilder<TSource, Content> content = null,
            IBuilder<TSource, Content> footer = null)
        {
            return new SectionBuilder<TSource>(
                header ?? Content<TSource>(),
                content ?? Content<TSource>(),
                footer ?? Content<TSource>());
        }

        public static IBuilder<TSource, Content> Content<TSource>(
            Margins margins = null,
            IBuilder<TSource, IElement> builder = null)
        {
            return new ContentBuilder<TSource>(
                margins ?? Margins.None(),
                builder ?? new NullBuilder<TSource, IElement>());
        }

        public static IBuilder<TSource, TResult> Compose<TSource, TResult>(
            params IBuilder<TSource, TResult>[] builders)
        {
            return new CompositeBuilder<TSource, TResult>(builders);
        }

        public static IBuilder<TSource, TResult> Conditional<TSource, TResult>(
            ISpecification<TSource> specification, IBuilder<TSource, TResult> then)
        {
            return new ConditionalBuilder<TSource, TResult>(specification, then);
        }

        public static IBuilder<TSource, TResult> Enumerate<TSource, TCollection, TResult>(
            IProjection<TSource, IEnumerable<TCollection>> source,
            IBuilder<TCollection, TResult> builder)
        {
            return new EnumeratingBuilder<TSource, TCollection, TResult>(source, builder);
        }

        public static IBuilder<TSource, TResult> Transform<TSource, TCollection, TResult>(
            IProjection<TSource, IEnumerable<TCollection>> projection,
            IBuilder<IEnumerable<TCollection>, TResult> builder)
        {
            return new TransformationBuilder<TSource, TCollection, TResult>(projection, builder);
        }
    }
}