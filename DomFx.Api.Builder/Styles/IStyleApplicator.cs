using System.Windows.Media;
using DomFx.Layouters;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Styles
{
    public interface IStyleApplicator 
    {
        void Width(Unit width);
        void Height(Unit height);
        void Margins(Margins margins);
        void Borders(Borders borders);
        void Flow(FlowStyle flow);
        void Float();
        void Clear();
        void FollowLineHeight();
        void KeepWithNextLine();
        void Color(Color color);
        void HorizontalAlignment(HorizontalAlignment alignment);
        void Font(IFont font);
    }
}