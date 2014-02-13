using DomFx.Layouters;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Styles
{
    public interface IStyleApplicator 
    {
        void Flow(FlowStyle flow);
        void Float();
        void Clear();
        void Width(Unit width);
        void Height(Unit height);
        void Margins(Margins margins);
        void Borders(Borders borders);
        void Font(IFont font);
    }
}