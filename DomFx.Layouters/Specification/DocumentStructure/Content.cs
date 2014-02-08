using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Layouters.Specification.DocumentStructure
{
    public class Content
    {
        public Margins Margins { get; set; }
        readonly IEnumerable<IElement> elements;

        public Content(Margins margins, IEnumerable<IElement> elements)
        {
            Margins = margins;
            this.elements = elements;
        }

        public static Content Empty
        {
            get { return new Content(Margins.None(), Enumerable.Empty<IElement>()); }
        }

        public IEnumerable<IElement> Elements
        {
            get { return elements; }
        }
    }
}