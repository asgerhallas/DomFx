using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Generator
{
    public abstract class ElementBuilder<TSource> : IBuilder<TSource, IElement>
    {
        readonly UnitOfMeasure unitOfMeasure;

        protected ElementBuilder(UnitOfMeasure unitOfMeasure)
        {
            this.unitOfMeasure = unitOfMeasure;
        }

        public abstract IEnumerable<IElement> Build(TSource source);

        protected Box Box(string name, IStyle<StyleBuilder> style, params IElement[] children)
        {
            return Box(name, style, (IEnumerable<IElement>)children);
        }

        protected Box Box(IStyle<StyleBuilder> style, params IElement[] children)
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
                new InlineStyle<StyleBuilder>(style =>
                {
                    if (flow == FlowStyle.Float)
                        style.Float();

                    style.Width(width != null ? Unit.From(unitOfMeasure, (double) width) : Unit.Undefined);
                    style.Height(height != null ? Unit.From(unitOfMeasure, (double) height) : Unit.Undefined);
                    style.Margins(margins ?? Layouters.Specification.Style.Margins.None());
                }),
                children ?? Enumerable.Empty<IElement>());
        }

        protected Box Box(string name, IStyle style, IEnumerable<IElement> children)
        {
            var box = new Box(name, children);
            style.Apply(new StyleBuilder(unitOfMeasure, box));
            return box;
        }

        protected Text Text(string name, string text, IStyle<StyleBuilder> style, params IElement[] children)
        {
            return Text(name, text, style, (IEnumerable<IElement>)children);
        }

        protected Text Text(string text, IStyle<StyleBuilder> style, params IElement[] children)
        {
            return Text(null, text, style, (IEnumerable<IElement>)children);
        }

        protected Box Text(
            string name = null,
            string text = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            FlowStyle? flow = null,
            IFont font = null,
            IEnumerable<IElement> children = null)
        {
            return Text(
                name,
                text ?? "",
                new InlineStyle<StyleBuilder>(style =>
                {
                    style.Flow(flow ?? FlowStyle.Clear);
                    style.Width(width != null ? Unit.From(unitOfMeasure, (double)width) : Unit.Undefined);
                    style.Height(height != null ? Unit.From(unitOfMeasure, (double)height) : Unit.Undefined);
                    style.Margins(margins ?? Layouters.Specification.Style.Margins.None());
                    style.Font(font ?? new NullFont());
                }),
                children ?? Enumerable.Empty<IElement>());
        }

        protected Text Text(string name, string text, IStyle<StyleBuilder> style, IEnumerable<IElement> children)
        {
            var box = new Text(name, text, children);
            style.Apply(new StyleBuilder(box));
            return box;
        }

        protected Image Image(string name, IImageSource source, IStyle<StyleBuilder> style, params IElement[] children)
        {
            return Image(name, source, style, (IEnumerable<IElement>)children);
        }

        protected Image Image(IImageSource image, IStyle<StyleBuilder> style, params IElement[] children)
        {
            return Image(null, image, style, (IEnumerable<IElement>)children);
        }

        protected Image Image(
            string name = null,
            IImageSource source = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            FlowStyle? flow = null,
            IEnumerable<IElement> children = null)
        {
            return Image(
                name,
                source,
                new InlineStyle<StyleBuilder>(style =>
                {
                    if (flow == FlowStyle.Float)
                        style.Float();

                    style.Width(width != null ? Unit.From(unitOfMeasure, (double)width) : Unit.Undefined);
                    style.Height(height != null ? Unit.From(unitOfMeasure, (double)height) : Unit.Undefined);
                    style.Margins(margins ?? Layouters.Specification.Style.Margins.None());
                }),
                children ?? Enumerable.Empty<IElement>());
        }

        protected Image Image(string name, IImageSource source, IStyle<StyleBuilder> style, IEnumerable<IElement> children)
        {
            var box = new Image(name, source, children);
            style.Apply(new StyleBuilder(box));
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
}