using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Layouters.Specification.Element
{
    public class Box : IElement
    {
        readonly string name;
        readonly List<IElement> children;

        public Box() : this(null, Enumerable.Empty<IElement>()) { }

        public Box(string name, IEnumerable<IElement> children)
        {
            if (name != null && name.Contains('-'))
                throw new ArgumentException("Name must not contain special character - (dash)");

            if (children == null)
                throw new ArgumentNullException("Children must not be null");

            this.name = name;
            this.children = children.ToList();

            BackgroundColor = Colors.Transparent;
            Borders = new Borders();
            Margins = new Margins();
            KeepWithNextLine = false;
            Flow = FlowStyle.Float;
            Breakable = true;
            OrphanHeight = 2.cm();
            WidowHeight = 2.cm();
            FollowLineHeight = false;
            Behavior = new NullBehavior();
        }

        public string Name
        {
            get { return name; }
        }

        public IEnumerable<IElement> Children
        {
            get { return children; }
        }

        public Color BackgroundColor { get; set; }
        public Borders Borders { get; set; }
        public Margins Margins { get; set; }
        public bool KeepWithNextLine { get; set; }
        public FlowStyle Flow { get; set; }
        public bool Breakable { get; set; }
        public Unit OrphanHeight { get; set; }
        public Unit WidowHeight { get; set; }
        public bool FollowLineHeight { get; set; }
        public Unit InnerHeight { get; set; }
        public Unit InnerWidth { get; set; }
        public CompositeBehavior Behavior { get; set; }

        public Edge Edge
        {
            get
            {
                return new Edge
                {
                    Top = Borders.Top + Margins.Top,
                    Bottom = Borders.Bottom + Margins.Bottom,
                    Left = Borders.Left + Margins.Left,
                    Right = Borders.Right + Margins.Right,
                };
            }
        }
    }
}