using System;
using System.Collections.Generic;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Api
{
    public abstract class ContentBase<TReportData> : IContent<TReportData>, IContentSetter<TReportData>
    {
        ContentContext context;
        TReportData reportData;
        UnitType standardUnit;
        IEnumerable<Style> styles;

        public UnitType StandardUnit
        {
            get { return standardUnit; }
        }

        public ContentContext Context
        {
            get { return context; }
            set { context = value; }
        }

        public IEnumerable<Style> Styles
        {
            get { return styles; }
        }

        public TReportData ReportData
        {
            get { return reportData; }
        }

        public abstract void Render();

        public virtual bool RenderIf()
        {
            return true;
        }

        UnitType IContentSetter<TReportData>.StandardUnit
        {
            set { standardUnit = value; }
        }

        ContentContext IContentSetter<TReportData>.Context
        {
            set { context = value; }
        }

        IEnumerable<Style> IContentSetter<TReportData>.Styles
        {
            set { styles = value; }
        }

        TReportData IContentSetter<TReportData>.ReportData
        {
            set { reportData = value; }
        }

        protected void End<T>() where T : IElement
        {
            Context.End<T>();
        }

        protected void Content<T>() where T : IContent<TReportData>
        {
            var content = (IContent<TReportData>) Activator.CreateInstance(typeof (T));

            var contentSetter = content as IContentSetter<TReportData>;
            if (contentSetter != null)
            {
                contentSetter.Context = Context;
                contentSetter.Styles = Styles;
                contentSetter.StandardUnit = StandardUnit;
                contentSetter.ReportData = ReportData;
            }

            if (content.RenderIf())
                content.Render();
        }

        protected ImageSpecificationBuilder Image()
        {
            var image = Context.Begin<Image>();
            return ImageInitializer(new ImageSpecificationBuilder(image, StandardUnit));
        }

        protected virtual ImageSpecificationBuilder ImageInitializer(ImageSpecificationBuilder element)
        {
            return element;
        }

        protected TextSpecificationBuilder Text()
        {
            var fixedHeightRectangle = Context.Begin<Text>();
            return TextInitializer(new TextSpecificationBuilder(fixedHeightRectangle, StandardUnit));
        }

        protected virtual TextSpecificationBuilder TextInitializer(TextSpecificationBuilder element)
        {
            return element;
        }

        protected BoxSpecificationBuilder Box()
        {
            var rectangle = Context.Begin<Box>();
            return BoxInitializer(new BoxSpecificationBuilder(rectangle, StandardUnit));
        }

        protected virtual BoxSpecificationBuilder BoxInitializer(BoxSpecificationBuilder builder)
        {
            return builder;
        }
    }
}