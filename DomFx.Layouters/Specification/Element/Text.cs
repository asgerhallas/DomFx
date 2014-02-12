using System;
using System.Collections.Generic;
using System.Windows.Media;
using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Layouters.Specification.Element
{
    public class Text : Box
    {
        public const string PageNumber = "%PageNumber%";

        public Text(string name, string text, IEnumerable<IElement> children)
            : base(name, children)
        {
            if (text == null)
                throw new ArgumentNullException("Text must be set");

            Content = text;
            BackgroundColor = Colors.Transparent;
            TextColor = Colors.Black;
            HorizontalAlignment = HorizontalAlignment.Left;
            Behavior = new TextBehavior(this);
        }

        public string Content { get; private set; }

        public IFont Font { get; set; }
        public Color TextColor { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public string Leader { get; set; }
    }
}