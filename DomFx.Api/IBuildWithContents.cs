using System;
using System.Linq;
using System.Windows.Media;
using DomFx.Layouters;
using DomFx.Layouters.Specification;

namespace DomFx.Api
{
    public interface IBuildWithContents<TReportData>
    {
        ContentBuilder<TContent, TReportData> Footer<TContent>() where TContent : class, IContent<TReportData>, new();
        ContentBuilder<TContent, TReportData> Header<TContent>() where TContent : class, IContent<TReportData>, new();
        ContentBuilder<TContent, TReportData> Content<TContent>() where TContent : class, IContent<TReportData>, new();
        ContentBuilder<TContent, TReportData> Content<TContent>(TContent content) where TContent : class, IContent<TReportData>;
    }
    
    public class SectionBuilder<TReportData> : IBuildWithContents<TReportData>
    {
        readonly TReportData reportData;
        readonly UnitType standardUnitType;
        readonly Color backgroundColor;

        ContentBuilder<TReportData> contentBuilder;
        ContentBuilder<TReportData> footerBuilder;
        ContentBuilder<TReportData> headerBuilder;

        public SectionBuilder(TReportData reportData, UnitType standardUnitType, Color backgroundColor)
        {
            this.reportData = reportData;
            this.standardUnitType = standardUnitType;
            this.backgroundColor = backgroundColor;
            contentBuilder = new NullContentBuilder<TReportData>();
            headerBuilder = new NullContentBuilder<TReportData>();
            footerBuilder = new NullContentBuilder<TReportData>();
        }

        public ContentBuilder<TContent, TReportData> Content<TContent>() where TContent : class, IContent<TReportData>, new()
        {
            contentBuilder = new ContentBuilder<TContent, TReportData>(this, null, reportData, standardUnitType);
            return (ContentBuilder<TContent, TReportData>) contentBuilder;
        }

        public ContentBuilder<TContent, TReportData> Content<TContent>(TContent content) where TContent : class, IContent<TReportData>
        {
            contentBuilder = new ContentBuilder<TContent, TReportData>(this, content, reportData, standardUnitType);
            return (ContentBuilder<TContent, TReportData>)contentBuilder;
        }

        public ContentBuilder<TContent, TReportData> Header<TContent>() where TContent : class, IContent<TReportData>, new()
        {
            headerBuilder = new ContentBuilder<TContent, TReportData>(this, null, reportData, standardUnitType);
            return (ContentBuilder<TContent, TReportData>) headerBuilder;
        }

        public ContentBuilder<TContent, TReportData> Footer<TContent>() where TContent : class, IContent<TReportData>, new()
        {
            footerBuilder = new ContentBuilder<TContent, TReportData>(this, null, reportData, standardUnitType);
            return (ContentBuilder<TContent, TReportData>) footerBuilder;
        }

        public Section Render()
        {
            return new Section(contentBuilder.Render(), headerBuilder.Render(), footerBuilder.Render(), backgroundColor);
        }
    }

    public interface ContentBuilder<TReportData>
    {
        Content Render();
    }

    public interface IBuildWithMargins<TReportData>
    {
        IBuildWithContents<TReportData> Margin(double top, double right, double bottom, double left);
    }

    public class NullContentBuilder<TReportData> : ContentBuilder<TReportData>
    {
        public Content Render()
        {
            return new Content(Enumerable.Empty<ElementSpecification>(), Margins.None());
        }
    }

    public class ContentBuilder<TContent, TReportData> : ContentBuilder<TReportData>, IBuildWithContents<TReportData>, IBuildWithMargins<TReportData> 
        where TContent : class, IContent<TReportData>
    {
        readonly IBuildWithContents<TReportData> section;
        TContent content;
        readonly TReportData reportData;
        readonly UnitType standardUnit;
        Margins margin;

        public ContentBuilder(IBuildWithContents<TReportData> section, TContent content, TReportData reportData, UnitType standardUnit)
        {
            this.section = section;
            this.content = content;
            this.reportData = reportData;
            this.standardUnit = standardUnit;
            margin = Margins.None();
        }

        public IBuildWithContents<TReportData> Margin(double top, double right, double bottom, double left)
        {
            margin = new Margins
            {
                Top = Unit.From(standardUnit, top),
                Right = Unit.From(standardUnit, right),
                Bottom = Unit.From(standardUnit, bottom),
                Left = Unit.From(standardUnit, left)
            };
            return section;
        }

        public ContentBuilder<TFooter, TReportData> Footer<TFooter>() where TFooter : class, IContent<TReportData>, new()
        {
            return section.Footer<TFooter>();
        }

        public ContentBuilder<THeader, TReportData> Header<THeader>() where THeader : class, IContent<TReportData>, new()
        {
            return section.Header<THeader>();
        }

        public ContentBuilder<TContentContent, TReportData> Content<TContentContent>() where TContentContent : class, IContent<TReportData>, new()
        {
            return section.Content<TContentContent>();
        }

        public ContentBuilder<TContent1, TReportData> Content<TContent1>(TContent1 content) where TContent1 : class, IContent<TReportData>
        {
            return section.Content(content);
        }

        public Content Render()
        {
            var contentContext = new ContentContext();

            if (content == null)
                content = (TContent) Activator.CreateInstance(typeof (TContent));

            var contentSetter = content as IContentSetter<TReportData>;
            if (contentSetter != null)
            {
                contentSetter.Context = contentContext;
                contentSetter.StandardUnit = standardUnit;
                contentSetter.ReportData = reportData;
            }

            if (content.RenderIf())
                content.Render();

            return new Content(contentContext.RootElements, margin);
        }
    }
}