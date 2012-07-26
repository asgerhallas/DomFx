using System.Windows.Media;
using DomFx.Layouters.Behaviors;

namespace DomFx.Layouters.Specification
{
    public class Text : ElementSpecificationAbstract, Texted, Backgrounded
    {
        public Text()
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
        public Color BackgroundColor { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public string Leader { get; set; }

        public const string PageNumber = "%PageNumber%";
    }
}