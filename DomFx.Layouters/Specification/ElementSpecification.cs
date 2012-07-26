using System;
using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters.Behaviors;

namespace DomFx.Layouters.Specification
{
    public interface ElementSpecification
    {
        IEnumerable<ElementSpecification> Children { get; }
        Borders Borders { get; set; }
        Margins Margins { get; set; }
        bool KeepWithNextLine { get; set; }
        FlowStyle Flow { get; set; }
        string Name { get; set; }
        bool Breakable { get; set; }
        Unit OrphanHeight { get; set; }
        Unit WidowHeight { get; set; }
        bool FollowLineHeight { get; set; }
        Unit InnerHeight { get; set; }
        Unit InnerWidth { get; set; }
        Behaviors.Behaviors Behavior { get; set; }
        Edge Edge { get; }
        void AddChild(ElementSpecification child);
    }

    public abstract class ElementSpecificationAbstract : ElementSpecification
    {
        readonly List<ElementSpecification> children;

        protected ElementSpecificationAbstract()
            : this(new List<ElementSpecification>())
        {
        }

        protected ElementSpecificationAbstract(IEnumerable<ElementSpecification> children)
        {
            this.children = children.ToList();
            Borders = new Borders();
            Margins = new Margins();
            WidowHeight = 2.cm();
            OrphanHeight = 2.cm();
            Breakable = true;
            Behavior = new NullBehavior();
        }

        public IEnumerable<ElementSpecification> Children
        {
            get { return children; }
        }

        public void AddChild(ElementSpecification child)
        {
            children.Add(child);
        }

        public Borders Borders { get; set; }
        public Margins Margins { get; set; }
        public bool KeepWithNextLine { get; set; }
        public FlowStyle Flow { get; set; }
        string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value.Contains('-'))
                    throw new ArgumentException("Name must not contain special character - (dash)");

                name = value;
            }
        }

        public bool Breakable { get; set; }
        public Unit OrphanHeight { get; set; }
        public Unit WidowHeight { get; set; }
        public bool FollowLineHeight { get; set; }
        public Unit InnerHeight { get; set; }
        public Unit InnerWidth { get; set; }
        public Behaviors.Behaviors Behavior { get; set; }

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