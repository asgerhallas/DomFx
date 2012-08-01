using System.Windows.Media;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using ImageSource = DomFx.Layouters.Specification.ImageSource;

namespace DomFx.Api
{
    public static class BuilderEx
    {
        public static T Name<T>(this T builder, string name) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.Name = name;
            return builder;
        }

        public static T Float<T>(this T builder) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.Flow = FlowStyle.Float;
            return builder;
        }

        public static T Clear<T>(this T builder) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.Flow = FlowStyle.Clear;
            return builder;
        }

        public static T Breakable<T>(this T builder) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.Breakable = true;
            return builder;
        }

        public static T Unbreakable<T>(this T builder) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.Breakable = false;
            return builder;
        }

        public static T KeepWithNextLine<T>(this T builder) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.KeepWithNextLine = true;
            return builder;
        }

        public static T FollowLineHeight<T>(this T builder) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.FollowLineHeight = true;
            return builder;
        }

        public static T Width<T>(this T builder, double width) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.InnerWidth = Unit.From(builder.StandardUnit, width);
            return builder;
        }

        public static T Height<T>(this T builder, double height) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.InnerHeight = Unit.From(builder.StandardUnit, height);
            return builder;
        }

        public static T Size<T>(this T builder, double width, double height) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.InnerWidth = Unit.From(builder.StandardUnit, width);
            builder.Element.InnerHeight = Unit.From(builder.StandardUnit, height);
            return builder;
        }

        public static T Style<T>(this T builder, Style<T> style) where T : ElementSpecificationBuilder<T>
        {
            style.Apply(builder);
            return builder;
        }

        public static T Borders<T>(this T builder, double top, double right, double bottom, double left, Color color) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.Borders = new Borders(Unit.From(builder.StandardUnit, top),
                                                  Unit.From(builder.StandardUnit, right),
                                                  Unit.From(builder.StandardUnit, bottom),
                                                  Unit.From(builder.StandardUnit, left),
                                                  color);
            return builder;
        }

        public static T Margins<T>(this T builder, double top, double right, double bottom, double left) where T : ElementSpecificationBuilder<T>
        {
            builder.Element.Margins = new Margins(Unit.From(builder.StandardUnit, top),
                                                  Unit.From(builder.StandardUnit, right),
                                                  Unit.From(builder.StandardUnit, bottom),
                                                  Unit.From(builder.StandardUnit, left));
            return builder;
        }

        public static T BackgroundColor<T>(this T builder, Color color) where T : BackgroundedElementSpecificationBuilder<T>
        {
            builder.Element.BackgroundColor = color;
            return builder;
        }

        public static T Source<T>(this T builder, ImageSource source) where T : ImagedElementSpecificationBuilder<T>
        {
            builder.Element.Source = source;
            return builder;
        }

        public static T SizeBySource<T>(this T builder) where T : ImagedElementSpecificationBuilder<T>
        {
            builder.Element.SizeBySource();
            return builder;
        }

        public static T Font<T>(this T builder, Font font) where T : TextedElementSpecificationBuilder<T>
        {
            builder.Element.Font = font;
            return builder;
        }

        public static T TextColor<T>(this T builder, Color color) where T : TextedElementSpecificationBuilder<T>
        {
            builder.Element.TextColor = color;
            return builder;
        }

        public static T Text<T>(this T builder, string text) where T : TextedElementSpecificationBuilder<T>
        {
            builder.Element.TextContent = text ?? "";
            return builder;
        }

        public static T Align<T>(this T builder, HorizontalAlignment alignment) where T : TextedElementSpecificationBuilder<T>
        {
            builder.Element.HorizontalAlignment = alignment;
            return builder;
        }

        public static T Align<T>(this T builder, VerticalAlignment alignment) where T : TextedElementSpecificationBuilder<T>
        {
            builder.Element.VerticalAlignment = alignment;
            return builder;
        }  
        
        public static T Leader<T>(this T builder, string filler) where T : TextedElementSpecificationBuilder<T>
        {
            builder.Element.Leader = filler;
            return builder;
        }
    }
}