using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;
using Xunit;
using Shouldly;

namespace DomFx.Tests.Integration
{
    public class InfiniteTests
    {
        IEnumerable<IElement> NeverEndingStory()
        {
            while (true)
                yield return new Box
                {
                    InnerWidth = 5.cm(),
                    InnerHeight = 5.cm(),
                    Flow = FlowStyle.Float
                };
        }

        [Fact()]
        public void Test()
        {
            var linedDocument = LiningLayouter.Layout(NeverEndingStory(), 10.cm()).Take(5);
            linedDocument.Count().ShouldBe(5);
        }
    }
}