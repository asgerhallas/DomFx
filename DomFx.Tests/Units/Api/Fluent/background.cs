using System.Windows.Media;
using DomFx.Api;
using DomFx.Layouters.Specification;
using Shouldly;
using Xunit;

namespace DomFx.Tests.Units.Api.Fluent
{
    public class background : content_tests
    {
        [Fact]
        public void can_set_background_color()
        {
            Setup(() => Box().Name("A").BackgroundColor(Colors.Green));
            Build();
            Specification<Box>("A").BackgroundColor.ShouldBe(Colors.Green);
        }
    }
}