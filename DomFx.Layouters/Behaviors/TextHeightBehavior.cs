using System;
using System.Collections.Generic;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Layouters.Behaviors
{
    public class TextHeightBehavior : StandardHeightBehavior
    {
        readonly Text specification;

        public TextHeightBehavior(Text specification)
        {
            this.specification = specification;
        }

        protected override Unit CalculateHeight(LayoutedElement element)
        {
            return specification.Font.CalculateTextHeight(specification.Content, element.ForcedInnerWidth);
        }

        IEnumerable<string> GetParagraphs()
        {
            return specification.Content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        }
    }
}