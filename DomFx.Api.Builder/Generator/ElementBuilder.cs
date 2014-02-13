using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
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

        protected Box Box(string name, IStyle style, params IElement[] children)
        {
            return Box(name, style, (IEnumerable<IElement>)children);
        }

        protected Box Box(IStyle style, params IElement[] children)
        {
            return Box(null, style, (IEnumerable<IElement>)children);
        }

        protected Box Box(
            string name = null,
            IStyle style = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            Borders borders = null,
            FlowStyle? flow = null,
            IFont font = null,
            IEnumerable<IElement> children = null)
        {
            style = new CascadeStyle(
                style ?? new NullStyle(),
                MakeStyle(height, width, margins, borders, flow, font));

            return Box(
                name, style,
                children ?? Enumerable.Empty<IElement>());
        }

        protected Box Box(string name, IStyle style, IEnumerable<IElement> children)
        {
            var box = new Box(name, children);
            style.Apply(new StyleBuilder(box));
            return box;
        }

        protected Text Text(string name, string text, IStyle style, params IElement[] children)
        {
            return Text(name, text, style, (IEnumerable<IElement>)children);
        }

        protected Text Text(string text, IStyle style, params IElement[] children)
        {
            return Text(null, text, style, (IEnumerable<IElement>)children);
        }

        protected Box Text(
            string name = null,
            string text = null,
            IStyle style = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            Borders borders = null,
            FlowStyle? flow = null,
            IFont font = null,
            IEnumerable<IElement> children = null)
        {
            style = new CascadeStyle(
                style ?? new NullStyle(),
                MakeStyle(height, width, margins, borders, flow, font));

            return Text(
                name, text ?? "", style,
                children ?? Enumerable.Empty<IElement>());
        }

        protected Text Text(string name, string text, IStyle style, IEnumerable<IElement> children)
        {
            var box = new Text(name, text, children);
            style.Apply(new StyleBuilder(box));
            return box;
        }

        protected Image Image(string name, IImageSource source, IStyle style, params IElement[] children)
        {
            return Image(name, source, style, (IEnumerable<IElement>)children);
        }

        protected Image Image(IImageSource image, IStyle style, params IElement[] children)
        {
            return Image(null, image, style, (IEnumerable<IElement>)children);
        }

        protected Image Image(
            string name = null,
            IImageSource source = null,
            IStyle style = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            Borders borders = null,
            FlowStyle? flow = null,
            IFont font = null,
            IEnumerable<IElement> children = null)
        {
            style = new CascadeStyle(
                style ?? new NullStyle(),
                MakeStyle(height, width, margins, borders, flow, font));

            return Image(
                name, source, style,
                children ?? Enumerable.Empty<IElement>());
        }

        protected Image Image(string name, IImageSource source, IStyle style, IEnumerable<IElement> children)
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

        protected Borders Borders(double top, double right, double bottom, double left, Color color)
        {
            return new Borders(
                Unit.From(unitOfMeasure, top),
                Unit.From(unitOfMeasure, right),
                Unit.From(unitOfMeasure, bottom),
                Unit.From(unitOfMeasure, left),
                color);
        }

        protected IEnumerable<IElement> Yield(params IElement[] children)
        {
            return children;
        }

        protected IElement Yield(IBuilder<TSource, IElement> builder, TSource source)
        {
            return null; //builder.Build(source);
        }

        IStyle MakeStyle(
            double? height = null,
            double? width = null,
            Margins margins = null,
            Borders borders = null,
            FlowStyle? flow = null,
            IFont font = null)
        {
            return new InlineStyle(style =>
            {
                style.Flow(flow ?? FlowStyle.Clear);
                style.Width(width != null ? Unit.From(unitOfMeasure, (double) width) : Unit.Undefined);
                style.Height(height != null ? Unit.From(unitOfMeasure, (double) height) : Unit.Undefined);
                style.Margins(margins ?? new Margins());
                style.Borders(borders ?? new Borders());
                style.Font(font ?? new NullFont());
            });
        }
    }
}