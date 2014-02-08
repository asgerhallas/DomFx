using System;
using System.Collections.Generic;
using DomFx.Layouters.Specification.DocumentStructure;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Generator
{
    public class ContentBuilder<TSource> : IBuilder<TSource, Content>
    {
        readonly IBuilder<TSource, IElement> builder;
        readonly Margins margins;

        public ContentBuilder(Margins margins, IBuilder<TSource, IElement> builder)
        {
            this.margins = margins;
            this.builder = builder;
        }

        public IEnumerable<Content> Build(TSource source)
        {
            yield return new Content(margins, builder.Build(source));
        }
    }

    public abstract class ElementBuilder<TSource> : IBuilder<TSource, IElement>
    {
        public abstract IEnumerable<IElement> Build(TSource source);

        protected Box Box(params IElement[] children)
        {
            return null;
        }

        protected Box Box(IStyle<BoxStyleBuilder> style, params IElement[] children)
        {
            var box = new Box();
            var styleBuilder = new BoxStyleBuilder(box);
            style.Apply(styleBuilder);
            return box;
        }

        protected Box Box(FlowStyle flow = FlowStyle.Float, params IElement[] children)
        {
            return Box(new GenericStyle<BoxStyleBuilder>(style =>
            {
                if (flow == FlowStyle.Float)
                    style.Float();
            }));
        }

        protected IElement Yield(IBuilder<TSource, IElement> builder, TSource source)
        {
            return null; //builder.Build(source);
        }
    }

    public class GenericStyle<T> : IStyle<T> where T : IStyleBuilder
    {
        readonly Action<T> styler;

        public GenericStyle(Action<T> styler)
        {
            this.styler = styler;
        }

        public void Apply(T style)
        {
            styler(style);
        }
    }
}