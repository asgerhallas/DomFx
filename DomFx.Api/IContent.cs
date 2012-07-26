using System.Collections.Generic;
using DomFx.Layouters;

namespace DomFx.Api
{
    public interface IContent<TReportData> : IContent
    {
        TReportData ReportData { get; }
    }

    public interface IContent
    {
        IEnumerable<Style> Styles { get; }
        ContentContext Context { get; set; }
        UnitType StandardUnit { get; }
        void Render();
        bool RenderIf();
    }
}