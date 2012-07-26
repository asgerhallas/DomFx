using System.Collections.Generic;

namespace DomFx.Layouters.Specification
{
    public class Document
    {
        readonly IEnumerable<Section> sections;

        public Document(IEnumerable<Section> sections)
        {
            this.sections = sections;
        }
        
        public IEnumerable<Section> Sections
        {
            get { return sections; }
        }
    }
}