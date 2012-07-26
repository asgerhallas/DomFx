using System.Collections.Generic;
using System.Linq;

namespace DomFx.Layouters.Specification
{
    public class Content
    {
        public Margins Margins { get; set; }
        readonly IEnumerable<ElementSpecification> elements;

        public Content(IEnumerable<ElementSpecification> elements, Margins margins)
        {
            Margins = margins;
            this.elements = elements;
        }

        public static Content Empty
        {
            get
            {
                return new Content(Enumerable.Empty<ElementSpecification>(), Margins.None());
            }
        }

        public IEnumerable<ElementSpecification> Elements
        {
            get { return elements; }
        }
    }
}