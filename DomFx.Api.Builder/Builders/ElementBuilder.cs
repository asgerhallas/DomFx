using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using DomFx.Api.Builder.Styles;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Builders
{
    public abstract class ElementBuilder<TSource> : IBuilder<TSource, Element>
    {
        readonly UnitOfMeasure unitOfMeasure;

        protected ElementBuilder(UnitOfMeasure unitOfMeasure)
        {
            this.unitOfMeasure = unitOfMeasure;
        }

        public abstract IEnumerable<Element> Build(TSource source);

        protected Element Box(string name, IStyle style, params Element[] children)
        {
            return Box(name, style, (IEnumerable<Element>)children);
        }

        protected Element Box(IStyle style, params Element[] children)
        {
            return Box(null, style, (IEnumerable<Element>)children);
        }

        protected Element Box(
            string name = null,
            IStyle style = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            Borders borders = null,
            FlowStyle? flow = null,
            IFont font = null,
            IEnumerable<Element> children = null)
        {
            style = new CascadeStyle(
                style ?? new NullStyle(),
                MakeStyle(height, width, margins, borders, flow, font));

            return Box(
                name, style,
                children ?? Enumerable.Empty<Element>());
        }

        protected Element Box(string name, IStyle style, IEnumerable<Element> children)
        {
            return Create(elements => new Box(name, elements), style, children);
        }

        protected Element Text(string name, string text, IStyle style, params Element[] children)
        {
            return Text(name, text, style, (IEnumerable<Element>)children);
        }

        protected Element Text(string text, IStyle style, params Element[] children)
        {
            return Text(null, text, style, (IEnumerable<Element>)children);
        }

        protected Element Text(
            string name = null,
            string text = null,
            IStyle style = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            Borders borders = null,
            FlowStyle? flow = null,
            IFont font = null,
            IEnumerable<Element> children = null)
        {
            style = new CascadeStyle(
                style ?? new NullStyle(),
                MakeStyle(height, width, margins, borders, flow, font));

            return Text(
                name, text ?? "", style,
                children ?? Enumerable.Empty<Element>());
        }

        protected Element Text(string name, string text, IStyle style, IEnumerable<Element> children)
        {
            return Create(elements => new Text(name, text, elements), style, children);
        }

        protected Element Image(string name, IImageSource source, IStyle style, params Element[] children)
        {
            return Image(name, source, style, (IEnumerable<Element>)children);
        }

        protected Element Image(IImageSource image, IStyle style, params Element[] children)
        {
            return Image(null, image, style, (IEnumerable<Element>)children);
        }

        protected Element Image(
            string name = null,
            IImageSource source = null,
            IStyle style = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            Borders borders = null,
            FlowStyle? flow = null,
            IFont font = null,
            IEnumerable<Element> children = null)
        {
            style = new CascadeStyle(
                style ?? new NullStyle(),
                MakeStyle(height, width, margins, borders, flow, font));

            return Image(
                name, source, style,
                children ?? Enumerable.Empty<Element>());
        }

        protected Element Image(string name, IImageSource source, IStyle style, IEnumerable<Element> children)
        {
            return Create(elements => new Image(name, source, elements), style, children);
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

        protected IEnumerable<Element> Yield(params Element[] children)
        {
            return children;
        }

        protected IEnumerable<Element> Yield(IBuilder<TSource, Element> builder, TSource source)
        {
            return builder.Build(source);
        }

        Element Create<T>(Func<IEnumerable<IElement>, T> factory, IStyle style, IEnumerable<Element> children) where T : Box
        {
            return ambientStyle =>
            {
                var effectiveStyle = new CascadeStyle(ambientStyle, style);
                var box = factory(children.Select(generator => generator(effectiveStyle)));
                effectiveStyle.Apply(new StyleApplicator(box));
                return box;
            };             
        }

        IStyle MakeStyle(
            double? height,
            double? width,
            Margins margins,
            Borders borders,
            FlowStyle? flow,
            IFont font)
        {
            return new InlineStyle(style =>
            {
                if (flow != null)
                    style.Flow((FlowStyle) flow);
                
                if (width != null)
                    style.Width(Unit.From(unitOfMeasure, (double) width));

                if (height != null)
                    style.Height(Unit.From(unitOfMeasure, (double) height));

                if (margins != null)
                    style.Margins(margins);
                
                if (borders != null)
                    style.Borders(borders);

                if (font != null)
                    style.Font(font);
            });
        }
    }
}