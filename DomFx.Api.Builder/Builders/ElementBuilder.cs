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
            return Box(name, style, Yield(children));
        }

        protected Element Box(IStyle style, params Element[] children)
        {
            return Box(null, style, Yield(children));
        }

        protected Element Box(
            string name = null,
            IStyle style = null,
            double? height = null,
            double? width = null,
            Margins margins = null,
            Borders borders = null,
            FlowStyle? flow = null,
            bool? breakable = null,
            bool? followLineHeight = null,
            bool? keepWithNextLine = null,
            Color? backgroundColor = null,
            Color? color = null,
            HorizontalAlignment? horizontalAlignment = null,
            IFont font = null,
            Element children = null)
        {
            style = new CompositeStyle(
                style ?? new NullStyle(),
                MakeStyle(height, width, margins, borders, flow, breakable, followLineHeight, keepWithNextLine, backgroundColor, color, horizontalAlignment, font, null));

            return Box(name, style, children ?? Nothing());
        }

        protected Element Box(string name, IStyle style, Element children)
        {
            return Create(elements => new Box(name, elements), style, children);
        }

        protected Element Text(string name, string text, IStyle style, params Element[] children)
        {
            return Text(name, text, style, Yield(children));
        }

        protected Element Text(string text, IStyle style, params Element[] children)
        {
            return Text(null, text, style, Yield(children));
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
            bool? breakable = null,
            bool? followLineHeight = null,
            bool? keepWithNextLine = null,
            Color? backgroundColor = null,
            Color? color = null,
            HorizontalAlignment? horizontalAlignment = null,
            IFont font = null,
            string tail = null,
            Element children = null)
        {
            style = new CompositeStyle(
                style ?? new NullStyle(),
                MakeStyle(height, width, margins, borders, flow, breakable, followLineHeight, keepWithNextLine, backgroundColor, color, horizontalAlignment, font, tail));

            return Text(name, text ?? "", style, children ?? Nothing());
        }

        protected Element Text(string name, string text, IStyle style, Element children)
        {
            return Create(elements => new Text(name, text, elements), style, children);
        }

        protected Element Image(string name, IImageSource source, IStyle style, params Element[] children)
        {
            return Image(name, source, style, Yield(children));
        }

        protected Element Image(IImageSource image, IStyle style, params Element[] children)
        {
            return Image(null, image, style, Yield(children));
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
            bool? breakable = null,
            bool? followLineHeight = null,
            bool? keepWithNextLine = null,
            Color? backgroundColor = null,
            Color? color = null,
            HorizontalAlignment? horizontalAlignment = null,
            IFont font = null,
            Element children = null)
        {
            style = new CompositeStyle(
                style ?? new NullStyle(),
                MakeStyle(height, width, margins, borders, flow, breakable, followLineHeight, keepWithNextLine, backgroundColor, color, horizontalAlignment, font, null));

            return Image(name, source, style, children ?? Nothing());
        }

        protected Element Image(string name, IImageSource source, IStyle style, Element children)
        {
            return Create(elements => new Image(name, source, elements), style, children);
        }

        protected Element Nothing()
        {
            return style => Enumerable.Empty<IElement>();
        }

        protected Element Yield(IBuilder<TSource, Element> builder, TSource source)
        {
            return style => from generator in builder.Build(source)
                            from element in generator(style)
                            select element;
        }

        protected Element Yield(params Element[] children)
        {
            return Yield((IEnumerable<Element>) children);
        }

        protected Element Yield(IEnumerable<Element> children)
        {
            return style => from generator in children
                            from element in generator(style)
                            select element;
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

        Element Create<T>(Func<IEnumerable<IElement>, T> factory, IStyle style, Element children) where T : Box
        {
            return ambientStyle =>
            {
                var effectiveStyle = new CascadeStyle(ambientStyle, style);
                var box = factory(children(style));
                effectiveStyle.Apply(new BoxStyleApplicator(box));
                return new[] { box };
            };             
        }

        IStyle MakeStyle(
            double? height, 
            double? width, 
            Margins margins, 
            Borders borders, 
            FlowStyle? flow, 
            bool? breakable, 
            bool? followLineHeight, 
            bool? keepWithNextLine, 
            Color? backgroundColor, 
            Color? color, 
            HorizontalAlignment? horizontalAlignment, 
            IFont font,
            string tail)
        {
            return new InlineStyle(style =>
            {
                if (width != null)
                    style.Width(Unit.From(unitOfMeasure, (double) width));

                if (height != null)
                    style.Height(Unit.From(unitOfMeasure, (double) height));

                if (margins != null)
                    style.Margins(margins);
                
                if (borders != null)
                    style.Borders(borders);

                if (flow != null)
                    style.Flow(flow.Value);

                if (breakable != null)
                    style.Breakable(breakable.Value);

                if (followLineHeight != null)
                    style.FollowLineHeight(followLineHeight.Value);

                if (keepWithNextLine != null)
                    style.KeepWithNextLine(keepWithNextLine.Value);

                if (backgroundColor != null)
                    style.BackgroundColor(backgroundColor.Value);

                if (color != null)
                    style.Color(color.Value);

                if (horizontalAlignment != null)
                    style.HorizontalAlignment(horizontalAlignment.Value);

                if (font != null)
                    style.Font(font);

                if (tail != null)
                    style.Tail(tail);
            });
        }
    }
}