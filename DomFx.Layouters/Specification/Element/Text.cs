using System.Collections.Generic;
using System.Windows.Media;
using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Layouters.Specification.Element
{
    public class Text : Box
    {
        public Text(string name, IEnumerable<IElement> children) : base(name, children)
        {
            BackgroundColor = Colors.Transparent;
            TextColor = Colors.Black;
            TextContent = "";
            HorizontalAlignment = HorizontalAlignment.Left;
            Behavior = new TextBehavior(this);
        }

        public Font Font { get; set; }
        public string TextContent { get; set; }

        public Color TextColor { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public string Leader { get; set; }

        public const string PageNumber = "%PageNumber%";
    }
}