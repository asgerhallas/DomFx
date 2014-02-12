using System;

namespace DomFx.Api.Builder.Generator
{
    public class InlineStyle<T> : IStyle<T> where T : IStyleBuilder
    {
        readonly Action<T> styler;

        public InlineStyle(Action<T> styler)
        {
            this.styler = styler;
        }

        public void Apply(T style)
        {
            styler(style);
        }
    }
}