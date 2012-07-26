using DomFx.Layouters;
using Xunit;

namespace DomFx.Tests.Units.Layouters.Paging
{
    public class lines : paging_tests
    {
        [Fact]
        public void if_one_element_in_line_cannot_be_moved_or_split_line_is_not_moved()
        {
            Element(innerHeight: 20.cm());
            Element(innerHeight: 5.cm());
            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(2).ShouldBeAt(page: 1, top: OfPage, height: 5.cm());
        }

        [Fact]
        public void if_one_element_in_line_is_moved_all_line_is_moved()
        {
            Element(top: 10.cm(), innerHeight: 10.cm());
            Element(top: 10.cm(), innerHeight: 5.cm());
            
            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 10.cm());
            Element(2).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
        }

        [Fact]
        public void if_one_element_in_line_is_moved_all_line_is_moved_though_other_elements_could_be_split()
        {
            Element(top: 10.cm(), innerHeight: 10.cm(), breakable: true);
            Element(top: 10.cm(), innerHeight: 10.cm(), breakable: false);
            
            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 10.cm());
            Element(2).ShouldBeAt(page: 2, top: OfPage, height: 10.cm());
        }

        [Fact]
        public void if_one_element_in_line_cannot_be_moved_or_split_other_breakable_elements_are_still_split()
        {
            Element(innerHeight: 20.cm(), breakable: false);
            Element(innerHeight: 20.cm(), breakable: true);
            
            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(2, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(2, 2).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
        }

        [Fact]
        public void if_one_element_in_line_cannot_be_moved_or_split_other_breakable_elements_with_follow_line_height_are_still_split()
        {
            Element(innerHeight: 20.cm(), breakable: false);
            Element(innerHeight: 20.cm(), followLineHeight: true, breakable: true);
            
            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(2, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(2, 2).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
        }

        [Fact]
        public void if_one_element_in_line_is_larger_after_change_elements_with_follow_line_height_will_resize()
        {
            Element(innerHeight: 20.cm(), border: 2.cm(), breakable: true);
            Element(innerHeight: 22.cm(), followLineHeight: true, breakable: true);
            
            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 13.cm());
            Element(2, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(2, 2).ShouldBeAt(page: 2, top: OfPage, height: 9.cm());
        }
    }
}