using DomFx.Layouters.Specification;

namespace DomFx.Layouters
{
    public class FixedElement
    {
        public FixedElement(LayoutedElement element, Unit left, Unit down)
        {
            Name = element.Name;
            Specification = element.Specification;
            MarginBox = new FixedBox(left, down, element.OuterWidth, element.OuterHeight);
            BorderBox = new FixedBox(left, down, element.OuterWidth, element.OuterHeight, element.Specification.Margins);
            InnerBox = new FixedBox(left, down, element.OuterWidth, element.OuterHeight, element.Specification.Edge);
            InnerBoxBeforeSplitOrCrop = new FixedBox(InnerBox.Left, InnerBox.Top, element.InnerWidthBeforeSplitOrCrop, element.InnerHeightBeforeSplitOrCrop);
            VisiblePartOfSpecification = new FixedBox(0.cm(), element.ViewportTop, InnerBox.Width, InnerBox.Height);
        }

        FixedElement(FixedElement element, Unit left, Unit down)
        {
            Name = element.Name;
            Specification = element.Specification;
            MarginBox = element.MarginBox.Move(left, down);
            BorderBox = element.BorderBox.Move(left, down);
            InnerBox = element.InnerBox.Move(left, down);
            InnerBoxBeforeSplitOrCrop = element.InnerBoxBeforeSplitOrCrop.Move(left, down);
            VisiblePartOfSpecification = element.VisiblePartOfSpecification;
        }

        public string Name { get; private set; }
        public ElementSpecification Specification { get; private set; }

        public FixedBox VisiblePartOfSpecification { get; private set; }
        public FixedBox InnerBoxBeforeSplitOrCrop { get; private set; }
        public FixedBox MarginBox { get; private set; }
        public FixedBox BorderBox { get; private set; }
        public FixedBox InnerBox { get; private set; }

        public FixedElement Move(Unit left, Unit down)
        {
            return new FixedElement(this, left, down);
        }

        public class FixedBox
        {
            public FixedBox(Unit left, Unit top, Unit width, Unit height)
            {
                Left = left;
                Top = top;
                Width = width;
                Height = height;
                Right = Left + Width;
                Bottom = Top + Height;
            }

            public FixedBox(Unit left, Unit top, Unit width, Unit height, Edge edge)
            {
                Left = left + edge.Left;
                Top = top + edge.Top;
                Width = width - edge.TotalHorizontal;
                Height = height - edge.TotalVertical;
                Right = Left + Width;
                Bottom = Top + Height;
            }

            public Unit Left { get; private set; }
            public Unit Top { get; private set; }
            public Unit Right { get; private set; }
            public Unit Bottom { get; private set; }
            public Unit Width { get; private set; }
            public Unit Height { get; private set; }

            public FixedBox Move(Unit left, Unit down)
            {
                return new FixedBox(Left + left, Top + down, Width, Height);
            }
        }
    }
}