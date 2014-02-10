using DomFx.Layouters;
using Shouldly;
using Xunit;

namespace DomFx.Tests.Layouters.Paging
{
    public class Simple : PagingTestsBase
    {
        [Fact]
        public void unbreakable_element_that_fits_page_is_not_moved()
        {
            Element(innerHeight: 10.cm());
            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: OfPage, height: 10.cm());
        }

        [Fact]
        public void unbreakable_element_that_does_not_fit_rest_of_page_is_moved()
        {
            Element(top: 10.cm(), innerHeight: 10.cm());
            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 10.cm());
        }

        [Fact]
        public void topmost_unbreakable_element_that_does_not_fit_any_page_is_not_moved_but_cropped()
        {
            Element(innerHeight: 20.cm());
            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
        }

        [Fact]
        public void unbreakable_element_with_top_offset_that_does_not_fit_any_page_is_first_moved_then_cropped()
        {
            Element(top: 5.cm(), innerHeight: 20.cm());
            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 15.cm());
        }

        [Fact]
        public void element_with_no_height_is_not_added()
        {
            Element(top: 5.cm(), innerHeight: 0.cm());
            
            LayoutWithPageHeight(15.cm());

            Should.Throw<ElementNotFoundException>(() => Element(1));
        }
        
        [Fact]
        public void breakable_element_that_does_not_fit_page_is_split()
        {
            Element(innerHeight: 20.cm(), breakable: true);
            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
        }

        [Fact]
        public void breakable_element_that_does_not_fit_this_page_and_next_is_split_in_three()
        {
            Element(innerHeight: 40.cm(), breakable: true);
            
            LayoutWithPageHeight(15.cm());
            
            Element(1, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 15.cm());
            Element(1, 3).ShouldBeAt(page: 3, top: OfPage, height: 10.cm());
        }

        [Fact]
        public void breakable_element_that_would_leave_an_orphan_on_split_will_move()
        {
            Element(top: 14.cm(), innerHeight: 10.cm(), breakable: true);
            
            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 10.cm());
        }

        [Fact]
        public void breakable_element_that_would_make_a_widow_on_normal_split_will_split_earlier()
        {
            Element(top: 10.cm(), innerHeight: 6.cm(), breakable: true);

            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: 10.cm(), height: 4.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 2.cm());
        }

        [Fact]
        public void breakable_element_that_would_make_widow_but_is_too_small_to_not_leave_orphan_on_split_early_will_not_split()
        {
            Element(top: 13.cm(), innerHeight: 3.cm(), breakable: true);

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 3.cm());
        }

        [Fact]
        public void breakable_element_that_exactly_does_not_leave_orphan_or_makes_widow_will_split()
        {
            Element(top: 13.cm(), innerHeight: 4.cm(), breakable: true);

            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: 13.cm(), height: 2.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 2.cm());
        }

        [Fact]
        public void breakable_element_where_top_border_is_crossing_split_point_is_moved()
        {
            Element(top: 14.cm(), innerHeight: 10.cm(), border: 2.cm(), breakable: true);

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 14.cm());
        }

        [Fact]
        public void breakable_element_where_bottom_border_is_crossing_split_point_is_still_split()
        {
            Element(top: 2.cm(), innerHeight: 10.cm(), border: 2.cm(), breakable: true);

            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: 2.cm(), height: 12.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 6.cm());
        }

        [Fact]
        public void breakable_element_where_bottom_border_is_crossing_split_point_and_where_split_would_leave_an_orphan_will_move()
        {
            Element(top: 9.cm(), innerHeight: 3.cm(), border: 2.cm(), breakable: true);

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 7.cm());
        }

        [Fact]
        public void when_element_with_border_is_split_borders_remain_on_both_sides_of_split()
        {
            Element(innerHeight: 10.cm(), border: 2.cm(), breakable: true);

            LayoutWithPageHeight(10.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: OfPage, height: 10.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 8.cm());
        }

        [Fact]
        public void if_element_is_larger_after_split_following_lines_are_moved_down()
        {
            Element(innerHeight: 20.cm(), border: 1.cm(), breakable: true);
            Element(top: 22.cm(), innerHeight: 5.cm());

            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 9.cm());
            Element(2).ShouldBeAt(page: 2, top: 9.cm(), height: 5.cm());
        }

        [Fact]
        public void if_element_is_smaller_after_crop_following_lines_are_moved_up()
        {
            Element(innerHeight: 20.cm());
            Element(top: 20.cm(), innerHeight: 5.cm());

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(2).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
        }

        [Fact]
        public void two_elements_with_keeptogether_where_last_is_on_second_page_are_moved_down_together()
        {
            Element(top: 10.cm(), innerHeight: 5.cm(), keepWithNextLine: true);
            Element(top: 15.cm(), innerHeight: 5.cm());

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
            Element(2).ShouldBeAt(page: 2, top: 5.cm(), height: 5.cm());
        }

        [Fact]
        public void two_elements_with_keeptogether_where_last_does_not_fit_page_are_moved_down_together()
        {
            Element(top: 5.cm(), innerHeight: 5.cm(), keepWithNextLine: true);
            Element(top: 10.cm(), innerHeight: 10.cm());

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
            Element(2).ShouldBeAt(page: 2, top: 5.cm(), height: 10.cm());
        }

        [Fact]
        public void two_elements_with_keeptogether_where_first_is_not_moved_but_is_cropped_must_allow_elements_to_be_on_seperate_pages()
        {
            Element(innerHeight: 20.cm(), keepWithNextLine: true);
            Element(top: 20.cm(), innerHeight: 5.cm());

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(2).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
        }

        [Fact]
        public void two_elements_with_keeptogether_where_first_is_moved_and_then_cropped_must_allow_elements_to_be_on_seperate_pages()
        {
            Element(top: 5.cm(), innerHeight: 20.cm(), keepWithNextLine: true);
            Element(top: 25.cm(), innerHeight: 5.cm());

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 15.cm());
            Element(2).ShouldBeAt(page: 3, top: OfPage, height: 5.cm());
        }

        [Fact]
        public void two_elements_with_keeptogether_where_first_is_at_top_of_page_and_second_need_to_move_will_crop_the_second()
        {
            Element(innerHeight: 10.cm(), keepWithNextLine: true);
            Element(top: 10.cm(), innerHeight: 10.cm());

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: OfPage, height: 10.cm());
            Element(2).ShouldBeAt(page: 1, top: 10.cm(), height: 5.cm());
        }

        [Fact]
        public void three_elements_with_keeptogether_where_last_is_moved_will_move_all_elements()
        {
            Element(top: 2.cm(), innerHeight: 5.cm(), keepWithNextLine: true);
            Element(top: 7.cm(), innerHeight: 5.cm(), keepWithNextLine: true);
            Element(top: 12.cm(), innerHeight: 5.cm());

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
            Element(2).ShouldBeAt(page: 2, top: 5.cm(), height: 5.cm());
            Element(3).ShouldBeAt(page: 2, top: 10.cm(), height: 5.cm());
        }

        [Fact]
        public void three_elements_with_keeptogether_where_last_is_moved_but_still_too_large_will_move_all_elements_and_crop_the_last()
        {
            Element(top: 2.cm(), innerHeight: 5.cm(), keepWithNextLine: true);
            Element(top: 7.cm(), innerHeight: 5.cm(), keepWithNextLine: true);
            Element(top: 12.cm(), innerHeight: 10.cm());

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
            Element(2).ShouldBeAt(page: 2, top: 5.cm(), height: 5.cm());
            Element(3).ShouldBeAt(page: 2, top: 10.cm(), height: 5.cm());
        }

        [Fact]
        public void elements_with_keep_together_will_rather_split_last_breakable_element_than_between_elements()
        {
            Element(innerHeight: 5.cm(), breakable: true, keepWithNextLine: true);
            Element(top: 5.cm(), innerHeight: 5.cm(), breakable: true, keepWithNextLine: true);
            Element(top: 10.cm(), innerHeight: 7.cm(), breakable: false);

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: OfPage, height: 5.cm());
            Element(2, 1).ShouldBeAt(page: 1, top: 5.cm(), height: 3.cm());
            Element(2, 2).ShouldBeAt(page: 2, top: OfPage, height: 2.cm());
            Element(3).ShouldBeAt(page: 2, top: 2.cm(), height: 7.cm());
        }

        [Fact]
        public void elements_with_keep_together_will_rather_split_another_breakable_element_than_one_that_is_too_small()
        {
            Element(innerHeight: 10.cm(), breakable: true, keepWithNextLine: true);
            Element(top: 10.cm(), innerHeight: 2.cm(), breakable: true, keepWithNextLine: true);
            Element(top: 12.cm(), innerHeight: 5.cm(), breakable: false);

            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: OfPage, height: 8.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 2.cm());
            Element(2).ShouldBeAt(page: 2, top: 2.cm(), height: 2.cm());
            Element(3).ShouldBeAt(page: 2, top: 4.cm(), height: 5.cm());
        }

        [Fact]
        public void elements_with_keep_together_where_last_element_does_not_fit_because_of_border_and_can_not_be_split_because_of_height_will_move()
        {
            Element(top: 8.cm(), innerHeight: 3.cm(), breakable: true, keepWithNextLine: true);
            Element(top: 11.cm(), innerHeight: 3.cm(), border: 2.cm(), breakable: true);

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 3.cm());
            Element(2).ShouldBeAt(page: 2, top: 3.cm(), height: 7.cm());
        }

        [Fact]
        public void elements_with_keep_together_where_last_element_does_not_fit_because_of_border_and_can_be_split_will_split()
        {
            Element(top: 5.cm(), innerHeight: 3.cm(), breakable: true, keepWithNextLine: true);
            Element(top: 8.cm(), innerHeight: 4.cm(), border: 2.cm(), breakable: true);

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: 5.cm(), height: 3.cm());
            Element(2, 1).ShouldBeAt(page: 1, top: 8.cm(), height: 6.cm());
            Element(2, 2).ShouldBeAt(page: 2, top: OfPage, height: 6.cm());
        }

        [Fact]
        public void elements_with_keep_together_where_last_element_is_breakable_and_does_not_fit_will_split()
        {
            Element(top: 8.cm(), innerHeight: 3.cm(), breakable: true, keepWithNextLine: true);
            Element(top: 11.cm(), innerHeight: 6.cm(), breakable: true);

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: 8.cm(), height: 3.cm());
            Element(2, 1).ShouldBeAt(page: 1, top: 11.cm(), height: 4.cm());
            Element(2, 2).ShouldBeAt(page: 2, top: OfPage, height: 2.cm());
        }
    }
}