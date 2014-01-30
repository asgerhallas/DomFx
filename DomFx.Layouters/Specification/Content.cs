using System.Collections.Generic;
using System.Linq;

namespace DomFx.Layouters.Specification
{
    public class Content
    {
        public Margins Margins { get; set; }
        readonly IEnumerable<ElementSpecification> elements;

        public Content(Margins margins, IEnumerable<ElementSpecification> elements)
        {
            Margins = margins;
            this.elements = elements;
        }

        public static Content Empty
        {
            get { return new Content(Margins.None(), Enumerable.Empty<ElementSpecification>()); }
        }

        public IEnumerable<ElementSpecification> Elements
        {
            get { return elements; }
        }
    }
}