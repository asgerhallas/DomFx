using System;

namespace DomFx.Api.Builder.Generator
{
    public class InlineStyle : IStyle
    {
        readonly Action<StyleBuilder> styler;

        public InlineStyle(Action<StyleBuilder> styler)
        {
            this.styler = styler;
        }

        public void Apply(StyleBuilder style)
        {
            styler(style);
        }
    }
}