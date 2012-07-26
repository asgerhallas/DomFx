using System.Collections.Generic;
using DomFx.Layouters.Specification;

namespace DomFx.Tests.Fakes
{
    public class TestSpecification : ElementSpecificationAbstract
    {
        public TestSpecification(IEnumerable<ElementSpecification> children)
            : base(children)
        {
        }

        public TestSpecification()
            : this(new List<ElementSpecification>())
        {
        }
    }
}