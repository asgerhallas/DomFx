using DomFx.Api;
using DomFx.Layouters;
using DomFx.Layouters.Specification.Element;
using DomFx.Renderers.PdfSharp.WPF;
using DomFx.Tests.Fakes;
using Shouldly;
using Xunit;

namespace DomFx.Tests.Api.Fluent
{
    public class image : content_tests
    {
        [Fact]
        public void can_set_image_source()
        {
            var bitmap = PdfSharpImage.FromBitmap(GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.test.jpg"));
            Setup(() => Image().Name("A").Source(bitmap));
            Build();
            Specification<Image>("A").Source.ShouldBe(bitmap);
        }

        [Fact]
        public void can_set_image_size_by_source()
        {
            Setup(() => Image().Name("A").Source(new TestImageSource(width: 15.cm(), height: 10.cm())).SizeBySource());
            Build();
            Specification<Image>("A").InnerHeight.ShouldBe(10.cm());
            Specification<Image>("A").InnerWidth.ShouldBe(15.cm());
        }
    }
}