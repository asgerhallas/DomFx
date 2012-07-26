using DomFx.Api;
using DomFx.Layouters.Specification;
using Shouldly;
using Xunit;

namespace DomFx.Tests.Units.Api.Fluent
{
    public class simple : content_tests
    {
        [Fact]
        public void can_end_element()
        {
            Setup(() =>
            {
                Box();
                Text();
                End<Box>();
            });
            Build();
            Should.NotThrow(Evaluate);
        }

        [Fact]
        public void can_end_element2()
        {
            Setup(() =>
            {
                Text().Name("A").Text("Abe").Margins(0.5, 1, 0, 2.5);
                Text().Name("B").Text("Bbe");
            });
            Build();
        }
    }
}