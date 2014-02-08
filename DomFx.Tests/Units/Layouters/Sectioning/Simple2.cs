using DomFx.Layouters;
using Xunit;

namespace DomFx.Tests.Units.Layouters.Sectioning
{
    public class Simple2 : SectioningTests
    {
        [Fact]
        public void single_element_is_layouted()
        {
            Section(content: Content(border: 1.cm(), elements: new[]
            {
                Element(innerWidth: 2.cm(), innerHeight: 2.cm())
            }));

            LayoutWithPage(10.cm(), 15.cm());

            Element(1).ShouldBeAt(page: 1, left: 1.cm(), top: 1.cm(), height: 2.cm());
        }

        [Fact]
        public void header_content_and_footer_are_all_layouted()
        {
            Section(header: Content(border: 1.cm(), elements: new[]
                    {
                        Element(innerWidth: 2.cm(), innerHeight: 2.cm())
                    }),
                    content: Content(border: 1.cm(), elements: new[]
                    {
                        Element(innerWidth: 2.cm(), innerHeight: 2.cm())
                    }),
                    footer: Content(border: 1.cm(), elements: new[]
                    {
                        Element(innerWidth: 2.cm(), innerHeight: 2.cm())
                    }));

            LayoutWithPage(10.cm(), 15.cm());

            Element(1).ShouldBeAt(page: 1, left: 1.cm(), top: 1.cm(), height: 2.cm());
            Element(2).ShouldBeAt(page: 1, left: 1.cm(), top: 5.cm(), height: 2.cm());
            Element(3).ShouldBeAt(page: 1, left: 1.cm(), top: 11.cm(), height: 2.cm());
        }

        [Fact]
        public void content_page_height_respects_header_and_footer()
        {
            Section(header: Content(border: 1.cm(), elements: new[]
                    {
                        Element(innerWidth: 2.cm(), innerHeight: 2.cm())
                    }),
                    content: Content(border: 1.cm(), elements: new[]
                    {
                        Element(innerWidth: 2.cm(), innerHeight: 10.cm(), breakable: true)
                    }),
                    footer: Content(border: 1.cm(), elements: new[]
                    {
                        Element(innerWidth: 2.cm(), innerHeight: 2.cm())
                    }));

            LayoutWithPage(10.cm(), 15.cm());

            Element(2, 1).ShouldBeAt(page: 1, left: 1.cm(), top: 5.cm(), height: 5.cm());
            Element(2, 2).ShouldBeAt(page: 2, left: 1.cm(), top: 5.cm(), height: 5.cm());
        }

        [Fact]
        public void section_begins_on_new_page()
        {
            Section(content: Content(border: 1.cm(), elements: new[]
                    {
                        Element(innerWidth: 2.cm(), innerHeight: 5.cm())
                    }));

            Section(content: Content(border: 1.cm(), elements: new[]
                    {
                        Element(innerWidth: 2.cm(), innerHeight: 5.cm())
                    }));

            LayoutWithPage(10.cm(), 15.cm());

            Element(1).ShouldBeAt(page: 1, left: 1.cm(), top: 1.cm(), height: 5.cm());
            Element(2).ShouldBeAt(page: 2, left: 1.cm(), top: 1.cm(), height: 5.cm());
        }
    
    }
}