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
        void Breakable(bool breakability);
        void Breakable();
        void Unbreakable();
        void FollowLineHeight(bool followLineHeight);
        void FollowLineHeight();
        void KeepWithNextLine(bool keepWithNextLine);
        void KeepWithNextLine();
        void BackgroundColor(Color color);
        void Color(Color color);
        void HorizontalAlignment(HorizontalAlignment alignment);
        void Font(IFont font);
    }
}