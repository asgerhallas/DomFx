using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using DomFx.Layouters;

namespace DomFx.Api
{
    public abstract class TableOfContentsBase<TReportData>
    {
        readonly List<SectionBuilder<TReportData>> sectionBuilders;
        readonly TReportData reportData;
        readonly UnitType standardUnit;

        protected TableOfContentsBase(TReportData reportData, UnitType standardUnit)
        {
            this.standardUnit = standardUnit;
            this.reportData = reportData;
            sectionBuilders = new List<SectionBuilder<TReportData>>();
        }

        public TReportData ReportData
        {
            get { return reportData; }
        }

        public UnitType StandardUnit
        {
            get { return standardUnit; }
        }

        public Layouters.Specification.Document Build()
        {
            Init();
            return new Layouters.Specification.Document(sectionBuilders.Select(x => x.Render()));
        }

        public abstract void Init();

        protected IBuildWithContents<TReportData> Section(Color? backgroundColor = default)
        {
            var sectionBuilder = new SectionBuilder<TReportData>(reportData, standardUnit, backgroundColor ?? Colors.Transparent);
            sectionBuilders.Add(sectionBuilder);
            return sectionBuilder;
        }
    }
}