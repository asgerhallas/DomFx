using System.Windows.Media;
using DomFx.Layouters;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Styles
{
    public class StyleApplicator : IStyleApplicator
    {
        readonly IElement element;

        public StyleApplicator(IElement element)
        {
            this.element = element;
        }

        public void Width(Unit width)
        {
            element.InnerWidth = width;
        }

        public void Height(Unit height)
        {
            element.InnerHeight = height;
        }

        public void Margins(Margins margins)
        {
            element.Margins = margins;
        }

        public void Borders(Borders borders)
        {
            element.Borders = borders;
        }

        public void Flow(FlowStyle flow)
        {
            element.Flow = flow;
        }

        public void Float()
        {
            Flow(FlowStyle.Float);
        }

        public void Clear()
        {
            Flow(FlowStyle.Clear);
        }

        public void FollowLineHeight()
        {
            element.FollowLineHeight = true;
        }

        public void KeepWithNextLine()
        {
            element.KeepWithNextLine = true;
        }

        public void Color(Color color)
        {
            var text = element as Text;
            if (text != null)
            {
                text.TextColor = color;
            }
        }

        public void HorizontalAlignment(HorizontalAlignment alignment)
        {
            var text = element as Text;
            if (text != null)
            {
                text.HorizontalAlignment = alignment;
            }
        }

        public void Font(IFont font)
        {
            var text = element as Text;
            if (text != null)
            {
                text.Font = font;
            }
        }
    }
}