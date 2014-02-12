using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
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
        readonly UnitOfMeasure unitOfMeasure;

        protected ElementBuilder(UnitOfMeasure unitOfMeasure)
        {
            this.unitOfMeasure = unitOfMeasure;
        }

        public abstract IEnumerable<IElement> Build(TSource source);

        protected Box Box(string name, IStyle<BoxStyleBuilder> style, params IElement[] children)
        {
            return Box(name, style, (IEnumerable<IElement>)children);
        }

        protected Box Box(IStyle<BoxStyleBuilder> style, params IElement[] children)
        {
            return Box(null, style, (IEnumerable<IElement>)children);
        }

        protected Box Box(
            string name = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            FlowStyle? flow = null,
            IEnumerable<IElement> children = null)
        {
            return Box(
                name,
                new InlineStyle<BoxStyleBuilder>(style =>
                {
                    if (flow == FlowStyle.Float)
                        style.Float();

                    style.Width(width != null ? Unit.From(unitOfMeasure, (double) width) : Unit.Undefined);
                    style.Height(height != null ? Unit.From(unitOfMeasure, (double) height) : Unit.Undefined);
                    style.Margins(margins ?? Layouters.Specification.Style.Margins.None());
                }),
                children ?? Enumerable.Empty<IElement>());
        }

        protected Box Box(string name, IStyle<BoxStyleBuilder> style, IEnumerable<IElement> children)
        {
            var box = new Box(name, children);
            style.Apply(new BoxStyleBuilder(box));
            return box;
        }

        protected Text Text(string name, string text, IStyle<BoxStyleBuilder> style, params IElement[] children)
        {
            return Text(name, text, style, (IEnumerable<IElement>)children);
        }

        protected Text Text(string text, IStyle<BoxStyleBuilder> style, params IElement[] children)
        {
            return Text(null, text, style, (IEnumerable<IElement>)children);
        }

        protected Box Text(
            string name = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            FlowStyle? flow = null,
            string text = null,
            IEnumerable<IElement> children = null)
        {
            return Text(
                name,
                text ?? "",
                new InlineStyle<BoxStyleBuilder>(style =>
                {
                    if (flow == FlowStyle.Float)
                        style.Float();

                    style.Width(width != null ? Unit.From(unitOfMeasure, (double)width) : Unit.Undefined);
                    style.Height(height != null ? Unit.From(unitOfMeasure, (double)height) : Unit.Undefined);
                    style.Margins(margins ?? Layouters.Specification.Style.Margins.None());
                }),
                children ?? Enumerable.Empty<IElement>());
        }


        protected Text Text(string name, string text, IStyle<BoxStyleBuilder> style, IEnumerable<IElement> children)
        {
            var box = new Text(name, children);
            box.TextContent = text;
            style.Apply(new BoxStyleBuilder(box));
            return box;
        }

        protected Image Image(string name, IImageSource image, IStyle<BoxStyleBuilder> style, params IElement[] children)
        {
            return Image(name, image, style, (IEnumerable<IElement>)children);
        }

        protected Image Image(IImageSource image, IStyle<BoxStyleBuilder> style, params IElement[] children)
        {
            return Image(null, image, style, (IEnumerable<IElement>)children);
        }

        protected Image Image(
            string name = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            FlowStyle? flow = null,
            IImageSource image = null,
            IEnumerable<IElement> children = null)
        {
            return Image(
                name,
                image,
                new InlineStyle<BoxStyleBuilder>(style =>
                {
                    if (flow == FlowStyle.Float)
                        style.Float();

                    style.Width(width != null ? Unit.From(unitOfMeasure, (double)width) : Unit.Undefined);
                    style.Height(height != null ? Unit.From(unitOfMeasure, (double)height) : Unit.Undefined);
                    style.Margins(margins ?? Layouters.Specification.Style.Margins.None());
                }),
                children ?? Enumerable.Empty<IElement>());
        }

        protected Image Image(string name, IImageSource image, IStyle<BoxStyleBuilder> style, IEnumerable<IElement> children)
        {
            var box = new Image(name, children);
            box.Source = image;
            style.Apply(new BoxStyleBuilder(box));
            return box;
        }

        protected Margins Margins(double top, double right, double bottom, double left)
        {
            return new Margins(
                Unit.From(unitOfMeasure, top),
                Unit.From(unitOfMeasure, right),
                Unit.From(unitOfMeasure, bottom),
                Unit.From(unitOfMeasure, left));
        }

        protected IEnumerable<IElement> Yield(params IElement[] children)
        {
            return children;
        }

        protected IElement Yield(IBuilder<TSource, IElement> builder, TSource source)
        {
            return null; //builder.Build(source);
        }
    }

    public class InlineStyle<T> : IStyle<T> where T : IStyleBuilder
    {
        readonly Action<T> styler;

        public InlineStyle(Action<T> styler)
        {
            this.styler = styler;
        }

        public void Apply(T style)
        {
            styler(style);
        }
    }

    public class NullStyle<T> : IStyle<T> where T : IStyleBuilder
    {
        public void Apply(T style)
        {
        }
    }
}