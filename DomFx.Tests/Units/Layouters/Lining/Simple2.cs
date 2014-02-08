using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Style;
using Xunit;
using Shouldly;

namespace DomFx.Tests.Units.Layouters.Lining
{
    public class Simple2 : LiningTests
    {
        [Fact]
        public void first_element_is_placed_in_top_left_corner()
        {
            Element(innerWidth: 5.cm(), innerHeight: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 5.cm(), height: 5.cm());
        }

        [Fact]
        public void floating_elements_break_to_next_line_when_needed()
        {
            Element(innerWidth: 7.cm(), innerHeight: 5.cm());
            Element(innerWidth: 7.cm(), innerHeight: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 7.cm(), height: 5.cm());
            Element(2).ShouldBeAt(top: 5.cm(), left: 0.cm(), width: 7.cm(), height: 5.cm());
        }

        [Fact]
        public void more_floating_elements_breaks_to_next_line_when_needed()
        {
            Element(innerWidth: 7.cm(), innerHeight: 5.cm());
            Element(innerWidth: 7.cm(), innerHeight: 5.cm());
            Element(innerWidth: 7.cm(), innerHeight: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 7.cm(), height: 5.cm());
            Element(2).ShouldBeAt(top: 5.cm(), left: 0.cm(), width: 7.cm(), height: 5.cm());
            Element(3).ShouldBeAt(top: 10.cm(), left: 0.cm(), width: 7.cm(), height: 5.cm());
        }

        [Fact]
        public void floating_elements_stays_on_same_line_when_possible()
        {
            Element(innerWidth: 5.cm(), innerHeight: 5.cm());
            Element(innerWidth: 5.cm(), innerHeight: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 5.cm(), height: 5.cm());
            Element(2).ShouldBeAt(top: 0.cm(), left: 5.cm(), width: 5.cm(), height: 5.cm());
        }

        [Fact]
        public void clearing_elements_breaks_to_next_line()
        {
            Element(innerWidth: 5.cm(), innerHeight: 5.cm());
            Element(innerWidth: 5.cm(), innerHeight: 5.cm(), flow: FlowStyle.Clear);

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 5.cm(), height: 5.cm());
            Element(2).ShouldBeAt(top: 5.cm(), left: 0.cm(), width: 5.cm(), height: 5.cm());
        }

        [Fact]
        public void an_element_too_wide_for_page_is_croped_to_page_width()
        {
            Element(innerWidth: 50.cm(), innerHeight: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 10.cm(), height: 5.cm(), originalInnerWidth: 50.cm());
        }

        [Fact]
        public void a_floating_element_too_wide_for_page_after_other_element_is_moved_to_next_line_and_cropped()
        {
            Element(innerWidth: 5.cm(), innerHeight: 5.cm());
            Element(innerWidth: 50.cm(), innerHeight: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 5.cm(), left: 0.cm(), width: 10.cm(), height: 5.cm(), originalInnerWidth: 50.cm());
        }

        [Fact]
        public void floating_elements_with_border_are_positioned_with_respect_to_border_when_on_same_line()
        {
            Element(innerWidth: 2.cm(), innerHeight: 5.cm(), border: 1.cm());
            Element(innerWidth: 2.cm(), innerHeight: 5.cm(), border: 0.5.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 4.cm(), height: 7.cm());
            Element(2).ShouldBeAt(top: 0.cm(), left: 4.cm(), width: 3.cm(), height: 6.cm());
        }

        [Fact]
        public void floating_elements_with_border_are_positioned_with_respect_to_border_when_there_is_a_break()
        {
            Element(innerWidth: 4.cm(), innerHeight: 5.cm(), border: 1.cm());
            Element(innerWidth: 2.cm(), innerHeight: 5.cm(), border: 2.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 6.cm(), height: 7.cm());
            Element(2).ShouldBeAt(top: 7.cm(), left: 0.cm(), width: 6.cm(), height: 9.cm());
        }

        [Fact]
        public void second_element_with_follow_lineheight_takes_height_from_first_element()
        {
            Element(innerWidth: 2.cm(), innerHeight: 5.cm());
            Element(innerWidth: 2.cm(), followLineHeight: true);

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 2.cm(), height: 5.cm());
            Element(2).ShouldBeAt(top: 0.cm(), left: 2.cm(), width: 2.cm(), height: 5.cm());
        }

        [Fact]
        public void first_element_with_follow_lineheight_takes_height_from_second_element()
        {
            Element(innerWidth: 2.cm(), followLineHeight: true);
            Element(innerWidth: 2.cm(), innerHeight: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 2.cm(), height: 5.cm());
            Element(2).ShouldBeAt(top: 0.cm(), left: 2.cm(), width: 2.cm(), height: 5.cm());
        }

        [Fact]
        public void element_with_follow_line_height_takes_height_from_highest_element()
        {
            Element(innerWidth: 2.cm(), innerHeight: 5.cm());
            Element(innerWidth: 2.cm(), innerHeight: 3.cm(), followLineHeight: true);
            Element(innerWidth: 2.cm(), innerHeight: 8.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 2.cm(), height: 5.cm());
            Element(2).ShouldBeAt(top: 0.cm(), left: 2.cm(), width: 2.cm(), height: 8.cm());
            Element(3).ShouldBeAt(top: 0.cm(), left: 4.cm(), width: 2.cm(), height: 8.cm());
        }

        [Fact]
        public void two_elements_with_follow_line_height_takes_height_from_highest_element()
        {
            Element(innerWidth: 2.cm(), innerHeight: 5.cm(), followLineHeight: true);
            Element(innerWidth: 2.cm(), innerHeight: 8.cm());
            Element(innerWidth: 2.cm(), innerHeight: 3.cm(), followLineHeight: true);

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 2.cm(), height: 8.cm());
            Element(2).ShouldBeAt(top: 0.cm(), left: 2.cm(), width: 2.cm(), height: 8.cm());
            Element(3).ShouldBeAt(top: 0.cm(), left: 4.cm(), width: 2.cm(), height: 8.cm());
        }

        [Fact]
        public void single_element_following_line_height_is_present_as_line_with_zero_height()
        {
            Element(innerWidth: 2.cm(), followLineHeight: true);

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 2.cm(), height: 0.cm());
        }

        [Fact]
        public void element_with_follow_line_height_takes_height_from_highest_innerheight()
        {
            Element(innerWidth: 2.cm(), innerHeight: 3.cm(), followLineHeight: true);
            Element(innerWidth: 2.cm(), innerHeight: 6.cm(), border: 1.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 2.cm(), height: 6.cm());
            Element(2).ShouldBeAt(top: 0.cm(), left: 2.cm(), width: 4.cm(), height: 8.cm());
        }

        [Fact]
        public void element_with_border_and_with_follow_line_height_takes_innerheight_from_highest_innerheight()
        {
            Element(innerWidth: 2.cm(), innerHeight: 3.cm(), border: 1.cm(), followLineHeight: true);
            Element(innerWidth: 2.cm(), innerHeight: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 4.cm(), height: 7.cm());
            Element(2).ShouldBeAt(top: 0.cm(), left: 4.cm(), width: 2.cm(), height: 5.cm());
        }

        [Fact]
        public void element_with_no_width_gets_page_width()
        {
            Element();

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 10.cm(), height: 0.cm());
        }  

        [Fact]
        public void element_with_no_width_after_element_with_width_gets_remaining_width()
        {
            Element(innerWidth: 5.cm());
            Element();

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 0.cm(), left: 5.cm(), width: 5.cm(), height: 0.cm());
        }  

        [Fact]
        public void element_with_no_width_before_element_with_width_gets_remaining_width()
        {
            Element();
            Element(innerWidth: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 5.cm(), height: 0.cm());
        }  

        [Fact]
        public void two_elements_with_no_width_after_element_with_width_will_give_remaining_space_to_first_element()
        {
            Element(innerWidth: 5.cm(), innerHeight: 2.cm());
            Element();
            Element();

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 0.cm(), left: 5.cm(), width: 5.cm(), height: 0.cm());
            Element(3).ShouldBeAt(top: 2.cm(), left: 0.cm(), width: 10.cm(), height: 0.cm());
        }  

        [Fact]
        public void two_elements_with_no_width_before_element_with_width_will_give_remaining_space_to_first_element()
        {
            Element(innerHeight: 2.cm());
            Element();
            Element(innerWidth: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 10.cm(), height: 2.cm());
            Element(2).ShouldBeAt(top: 2.cm(), left: 0.cm(), width: 5.cm(), height: 0.cm());
            Element(3).ShouldBeAt(top: 2.cm(), left: 5.cm(), width: 5.cm(), height: 0.cm());
        }  

        [Fact]
        public void element_with_no_width_after_two_elements_with_width_gets_remaining_width()
        {
            Element(innerWidth: 2.cm());
            Element(innerWidth: 5.cm());
            Element();

            LayoutWithPageWidth(10.cm());

            Element(3).ShouldBeAt(top: 0.cm(), left: 7.cm(), width: 3.cm(), height: 0.cm());
        }  

        [Fact]
        public void element_with_no_width_before_two_elements_with_width_gets_remaining_width()
        {
            Element();
            Element(innerWidth: 2.cm());
            Element(innerWidth: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 3.cm(), height: 0.cm());
        }

        [Fact]
        public void element_with_no_width_after_two_elements_that_are_page_wide_will_break()
        {
            Element(innerWidth: 5.cm(), innerHeight: 2.cm());
            Element(innerWidth: 5.cm(), innerHeight: 2.cm());
            Element();

            LayoutWithPageWidth(10.cm());

            Element(3).ShouldBeAt(top: 2.cm(), left: 0.cm(), width: 10.cm(), height: 0.cm());
        }  

        [Fact]
        public void element_with_no_width_after_two_elements_that_are_page_wide_will_break_and_get_remaining()
        {
            Element(innerWidth: 5.cm(), innerHeight: 2.cm());
            Element(innerWidth: 5.cm(), innerHeight: 2.cm());
            Element();
            Element(innerWidth: 5.cm());

            LayoutWithPageWidth(10.cm());

            Element(3).ShouldBeAt(top: 2.cm(), left: 0.cm(), width: 5.cm(), height: 0.cm());
        }

        [Fact]
        public void sets_keep_with_next_line_on_clearing_lines()
        {
            Element(innerHeight: 5.cm(), flow: FlowStyle.Clear);
            Element(innerHeight: 5.cm(), flow: FlowStyle.Clear, keepWithNextLine: true);
            Element(innerHeight: 5.cm(), flow: FlowStyle.Clear);

            LayoutWithPageWidth(29.7.cm());

            Line(1).KeepWithNextLine.ShouldBe(false);
            Line(2).KeepWithNextLine.ShouldBe(true);
            Line(3).KeepWithNextLine.ShouldBe(false);
        }

        [Fact]
        public void sets_keep_with_next_line_if_one_element_in_line_has_keep_with_next()
        {
            Element(innerHeight: 5.cm(), innerWidth: 5.cm());
            Element(innerHeight: 5.cm(), innerWidth: 5.cm(), keepWithNextLine: true);
            Element(innerHeight: 5.cm(), innerWidth: 5.cm());

            LayoutWithPageWidth(20.cm());

            Line(1).KeepWithNextLine.ShouldBe(true);
        }
    }
}