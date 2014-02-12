using System.Collections.Generic;
using DomFx.Layouters;
using DomFx.Layouters.Specification.DocumentStructure;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Generator
{
    public abstract class Composer<T>
    {
        readonly UnitOfMeasure unitOfMeasure;

        protected Composer(UnitOfMeasure unitOfMeasure)
        {
            this.unitOfMeasure = unitOfMeasure;
        }

        public abstract IBuilder<T, Document> Compose();

        public static IBuilder<TSource, Document> Document<TSource>(params IBuilder<TSource, Section>[] builders)
        {
            return new DocumentBuilder<TSource>(
                new CompositeBuilder<TSource, Section>(builders));
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
            params IBuilder<TSource, IElement>[] builders)
        {
            return Content(Margins.None(), builders);
        }

        public static IBuilder<TSource, Content> Content<TSource>(
            Margins margins,
            params IBuilder<TSource, IElement>[] builders)
        {
            return new ContentBuilder<TSource>(
                margins, new CompositeBuilder<TSource, IElement>(builders));
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

        public static IBuilder<TSource, TResult> Null<TSource, TResult>()
        {
            return new NullBuilder<TSource, TResult>();
        }
    }
}