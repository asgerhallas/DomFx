using System.Collections.Generic;
using DomFx.Layouters;

namespace DomFx.Api
{
    internal interface IContentSetter<TReportData>
    {
        UnitType StandardUnit { set; }
        IEnumerable<Style> Styles { set; }
        ContentContext Context { set; }
        TReportData ReportData { set; }
    }
}