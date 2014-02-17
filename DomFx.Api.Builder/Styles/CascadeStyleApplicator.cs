using System.Windows.Media;
using DomFx.Layouters;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Styles
{
    public class CascadeStyleApplicator : IStyleApplicator 
    {
        readonly IStyleApplicator applicator;

        public CascadeStyleApplicator(IStyleApplicator applicator)
        {
            this.applicator = applicator;
        }

        public void Width(Unit width)
        {
        }

        public void Height(Unit height)
        {
        }

        public void Margins(Margins margins)
        {
        }

        public void Borders(Borders borders)
        {
        }

        public void Flow(FlowStyle flow)
        {
        }

        public void Float()
        {
        }

        public void Clear()
        {
        }

        public void FollowLineHeight()
        {
        }

        public void KeepWithNextLine()
        {
        }

        public void Color(Color color)
        {
            applicator.Color(color);
        }

        public void HorizontalAlignment(HorizontalAlignment alignment)
        {
            applicator.HorizontalAlignment(alignment);
        }

        public void Font(IFont font)
        {
            applicator.Font(font);
        }
    }
}