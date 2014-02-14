using System;

namespace DomFx.Api.Builder.Styles
{
    public class InlineStyle : IStyle
    {
        readonly Action<IStyleApplicator> styler;

        public InlineStyle(Action<IStyleApplicator> styler)
        {
            this.styler = styler;
        }

        public void Apply(IStyleApplicator applicator)
        {
            styler(applicator);
        }
    }
}