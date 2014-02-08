using System.Collections.Generic;
using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Layouters.Specification.Element
{
    public interface IElement
    {
        string Name { get; }
        IEnumerable<IElement> Children { get; }

        Borders Borders { get; set; }
        Margins Margins { get; set; }
        bool KeepWithNextLine { get; set; }
        FlowStyle Flow { get; set; }
        bool Breakable { get; set; }
        Unit OrphanHeight { get; set; }
        Unit WidowHeight { get; set; }
        bool FollowLineHeight { get; set; }
        Unit InnerHeight { get; set; }
        Unit InnerWidth { get; set; }
        CompositeBehavior Behavior { get; set; }

        Edge Edge { get; }
    }
}