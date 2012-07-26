using DomFx.Api;
using DomFx.Layouters.Specification;
using DomFx.Renderers.PdfSharp.WPF;
using Shouldly;
using Xunit;

namespace DomFx.Tests.Units.Api.Fluent
{
    public class text : content_tests
    {
        [Fact]
        public void can_set_text()
        {
            Setup(() => Text().Name("A").Text("Asger"));
            Build();
            Specification<Text>("A").TextContent.ShouldBe("Asger");
        }

        [Fact]
        public void can_set_font()
        {
            Setup(() => Text().Name("A").Font(new PdfSharpFont("Arial", 10)));
            Build();
            Specification<Text>("A").Font.Family.ShouldBe("Arial");
            Specification<Text>("A").Font.Size.ShouldBe(10);
        }
    }
}