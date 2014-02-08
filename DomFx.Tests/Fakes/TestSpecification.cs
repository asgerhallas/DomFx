using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Tests.Fakes
{
    public class TestSpecification : Box
    {
        public TestSpecification() { }
        public TestSpecification(string name) : this(name, Enumerable.Empty<IElement>()) { }
        public TestSpecification(IEnumerable<IElement> children) : base(null, children) { }
        public TestSpecification(string name, IEnumerable<IElement> children) : base(name, children) { }
    }
}