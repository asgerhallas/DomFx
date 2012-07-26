using System.Linq;
using DomFx.Api;
using DomFx.Layouters;
using Shouldly;
using Xunit;

namespace DomFx.Tests.Units.Api.Fluent
{
    public class section : table_of_content_tests
    {
        [Fact]
        public void content_header_and_footer_has_correct_margins()
        {
            Section()
                .Content<ContentContent>()
                .Header<HeaderContent>().Margin(1, 1, 1, 1)
                .Footer<FooterContent>().Margin(2, 2, 2, 2);

            var doc = Build();
            
            var section = doc.Sections.First();
            section.Content.Margins.ShouldBe(Edge.None());
            section.Header.Margins.ShouldBe(new Edge(1.cm(), 1.cm(), 1.cm(), 1.cm()));
            section.Footer.Margins.ShouldBe(new Edge(2.cm(), 2.cm(), 2.cm(), 2.cm()));
        }

        [Fact]
        public void content_header_and_footer_has_correct_elements()
        {
            Section()
                .Content<ContentContent>()
                .Header<HeaderContent>()
                .Footer<FooterContent>();

            var doc = Build();
            
            var section = doc.Sections.First();
            section.Content.Elements.Single().Name.ShouldBe("C");
            section.Header.Elements.Single().Name.ShouldBe("A");
            section.Footer.Elements.Single().Name.ShouldBe("B");
        }

        [Fact]
        public void can_build_multiple_sections()
        {
            Section().Content<ContentContent>();
            Section().Content<FooterContent>();
            Section().Content<HeaderContent>();

            var doc = Build();
            
            var sections = doc.Sections.ToList();
            sections.Count.ShouldBe(3);
            sections[0].Content.Elements.Single().Name.ShouldBe("C");
            sections[1].Content.Elements.Single().Name.ShouldBe("B");
            sections[2].Content.Elements.Single().Name.ShouldBe("A");
        }

        class HeaderContent : ContentBase<int>
        {
            public override void Render()
            {
                Box().Name("A");
            }
        }

        class FooterContent : ContentBase<int>
        {
            public override void Render()
            {
                Box().Name("B");
            }
        }
        
        class ContentContent : ContentBase<int>
        {
            public override void Render()
            {
                Box().Name("C");
            }
        }
    }
}