using DomFx.Layouters;
using Xunit;

namespace DomFx.Tests.Units.Layouters.Paging
{
    public class children : paging_tests
    {
        [Fact]
        public void unbreakable_element_with_breakable_child_will_not_break_but_crop()
        {
            Element(breakable: false,
                    children: new[]
                    {
                        ChildElement(innerHeight: 20.cm(), breakable: true),
                    });

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(2).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
        }

        [Fact]
        public void unbreakable_element_with_breakable_child_will_not_break_but_move()
        {
            Element(top: 10.cm(), breakable: false,
                    children: new[]
                    {
                        ChildElement(innerHeight: 10.cm(), breakable: true),
                    });

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 10.cm());
            Element(2).ShouldBeAt(page: 2, top: OfPage, height: 10.cm());
        }

        [Fact]
        public void breakable_element_with_unbreakable_child_will_move_child_and_split_parent()
        {
            Element(top: 10.cm(), breakable: true,
                    children: new[]
                    {
                        ChildElement(innerHeight: 10.cm(), breakable: false),
                    });

            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: 10.cm(), height: 5.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 10.cm());
            Element(2).ShouldBeAt(page: 2, top: OfPage, height: 10.cm());
        }

        [Fact]
        public void breakable_element_with_two_unbreakable_children_where_last_does_not_fit_splits_between_children()
        {
            Element(breakable: true,
                    children: new[]
                    {
                        ChildElement(innerHeight: 8.cm(), breakable: false),
                        ChildElement(top: 8.cm(), innerHeight: 12.cm(), breakable: false)
                    });

            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 12.cm());
            Element(2).ShouldBeAt(page: 1, top: OfPage, height: 8.cm());
            Element(3).ShouldBeAt(page: 2, top: OfPage, height: 12.cm());
        }

        [Fact]
        public void breakable_element_with_two_unbreakable_children_where_last_is_on_next_page_splits_between_children()
        {
            Element(breakable: true,
                    children: new[]
                    {
                        ChildElement(innerHeight: 15.cm(), breakable: false),
                        ChildElement(top: 15.cm(), innerHeight: 12.cm(), breakable: false)
                    });

            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 12.cm());
            Element(2).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(3).ShouldBeAt(page: 2, top: OfPage, height: 12.cm());
        }

        [Fact]
        public void if_child_element_is_larger_after_split_parent_is_resized()
        {
            Element(breakable: true,
                    children: new[]
                    {
                        ChildElement(innerHeight: 20.cm(), border: 1.cm(), breakable: true),
                    });

            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 9.cm());
            Element(2, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(2, 2).ShouldBeAt(page: 2, top: OfPage, height: 9.cm());
        }

        [Fact]
        public void if_child_element_is_larger_after_split_following_lines_are_moved_down()
        {
            Element(breakable: true, children: new[]
            {
                ChildElement(innerHeight: 20.cm(), border: 1.cm(), breakable: true),
                ChildElement(top: 22.cm(), innerHeight: 5.cm())
            });

            LayoutWithPageHeight(15.cm());

            Element(2, 1).ShouldBeAt(page: 1, top: OfPage, height: 15.cm());
            Element(2, 2).ShouldBeAt(page: 2, top: OfPage, height: 9.cm());
            Element(3).ShouldBeAt(page: 2, top: 9.cm(), height: 5.cm());
        }

        [Fact]
        public void two_children_with_keeptogether_where_last_is_on_second_page_are_moved_down_together()
        {
            Element(top: 10.cm(), breakable: true, children: new[]
            {
                ChildElement(innerHeight: 5.cm(), keepWithNextLine: true),
                ChildElement(top: 5.cm(), innerHeight: 5.cm())
            });

            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: 10.cm(), height: 5.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 10.cm());
            Element(2).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
            Element(3).ShouldBeAt(page: 2, top: 5.cm(), height: 5.cm());
        }

        [Fact]
        public void two_children_with_keeptogether_where_last_does_not_fit_page_are_moved_down_together()
        {
            Element(top: 5.cm(), breakable: true, children: new[]
            {
                ChildElement(innerHeight: 5.cm(), keepWithNextLine: true),
                ChildElement(top: 5.cm(), innerHeight: 10.cm())
            });

            LayoutWithPageHeight(15.cm());

            Element(1, 1).ShouldBeAt(page: 1, top: 5.cm(), height: 10.cm());
            Element(1, 2).ShouldBeAt(page: 2, top: OfPage, height: 15.cm());
            Element(2).ShouldBeAt(page: 2, top: OfPage, height: 5.cm());
            Element(3).ShouldBeAt(page: 2, top: 5.cm(), height: 10.cm());
        }

        [Fact]
        public void children_with_keep_together_will_rather_split_last_breakable_child_than_between_children()
        {
            Element(breakable: true, children: new[]
            {
                ChildElement(innerHeight: 5.cm(), breakable: true, keepWithNextLine: true),
                ChildElement(top: 5.cm(), innerHeight: 5.cm(), breakable: true, keepWithNextLine: true),
                ChildElement(top: 10.cm(), innerHeight: 7.cm(), breakable: false),
            });

            LayoutWithPageHeight(15.cm());

            Element(2).ShouldBeAt(page: 1, top: OfPage, height: 5.cm());
            Element(3, 1).ShouldBeAt(page: 1, top: 5.cm(), height: 3.cm());
            Element(3, 2).ShouldBeAt(page: 2, top: OfPage, height: 2.cm());
            Element(4).ShouldBeAt(page: 2, top: 2.cm(), height: 7.cm());
        }

        [Fact]
        public void children_with_keep_together_will_not_make_widows_or_orphans_when_choosing_which_element_to_split()
        {
            Element(breakable: true, children: new[]
            {
                ChildElement(innerHeight: 10.cm(), breakable: true, keepWithNextLine: true),
                ChildElement(top: 10.cm(), innerHeight: 2.cm(), breakable: true, keepWithNextLine: true),
                ChildElement(top: 12.cm(), innerHeight: 5.cm(), breakable: false),
            });

            LayoutWithPageHeight(15.cm());

            Element(2, 1).ShouldBeAt(page: 1, top: OfPage, height: 8.cm());
            Element(2, 2).ShouldBeAt(page: 2, top: OfPage, height: 2.cm());
            Element(3).ShouldBeAt(page: 2, top: 2.cm(), height: 2.cm());
            Element(4).ShouldBeAt(page: 2, top: 4.cm(), height: 5.cm());
        }

        [Fact]
        public void parents_with_borders_move_children_accordingly()
        {
            Element(border: 2.cm(), children: new[]
            {
                ChildElement(innerHeight: 5.cm()),
            });

            LayoutWithPageHeight(15.cm());

            Element(2).ShouldBeAt(page: 1, top: 2.cm(), height: 5.cm());
        }


        [Fact]
        public void parents_with_borders_leave_less_space_for_children()
        {
            Element(breakable: true, border: 2.cm(), children: new[]
            {
                ChildElement(innerHeight: 7.cm()),
                ChildElement(top: 7.cm(), innerHeight: 7.cm()),
            });

            LayoutWithPageHeight(15.cm());

            Element(2).ShouldBeAt(page: 1, top: 2.cm(), height: 7.cm());
            Element(3).ShouldBeAt(page: 2, top: 2.cm(), height: 7.cm());
        }

        [Fact]
        public void parents_with_borders_leave_less_space_for_grand_children()
        {
            Element(breakable: true, border: 2.cm(), children: new[]
            {
                ChildElement(breakable: true, border: 2.cm(), children: new[]
                {
                    ChildElement(innerHeight: 3.cm()),
                    ChildElement(top: 3.cm(), innerHeight: 5.cm()),
                })
            });

            LayoutWithPageHeight(15.cm());

            Element(3).ShouldBeAt(page: 1, top: 4.cm(), height: 3.cm());
            Element(4).ShouldBeAt(page: 2, top: 4.cm(), height: 5.cm());
        }

        [Fact]
        public void unbreakable_child_that_does_not_fit_any_page_element_with_top_offset_is_first_moved_then_cropped()
        {
            Element(top: 5.cm(), breakable: true, children: new[]
            {
                ChildElement(innerHeight: 20.cm())
            });
            
            LayoutWithPageHeight(15.cm());

            Element(2).ShouldBeAt(page: 2, top: OfPage, height: 15.cm());
        }

        [Fact]
        public void unbreakable_grand_child_that_does_not_fit_any_page_in_breakable_child_in_breakable_element_with_top_offset_is_first_moved_then_cropped()
        {
            Element(top: 5.cm(), breakable: true, children: new[]
            {
                ChildElement(children: new[]
                {
                    ChildElement(innerHeight: 20.cm())
                })
            });
            
            LayoutWithPageHeight(15.cm());

            Element(3).ShouldBeAt(page: 2, top: OfPage, height: 15.cm());
        }

        [Fact]
        public void moving_child_element_that_aligns_with_parent_element_top_will_move_parent_too()
        {
            Element(top: 5.cm(), breakable: true, children: new[]
            {
                ChildElement(innerHeight: 12.cm(), breakable: false)
            });

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 12.cm());
            Element(2).ShouldBeAt(page: 2, top: OfPage, height: 12.cm());
        }

        [Fact]
        public void moving_child_element_that_aligns_with_parent_element_top_will_move_parent_too_2()
        {
            Element(top: 5.cm(), breakable: true, children: new[]
            {
                ChildElement(innerHeight: 6.cm(), breakable: false, keepWithNextLine: true),
                ChildElement(innerHeight: 6.cm(), breakable: false),
            });

            LayoutWithPageHeight(15.cm());

            Element(1).ShouldBeAt(page: 2, top: OfPage, height: 12.cm());
            Element(2).ShouldBeAt(page: 2, top: OfPage, height: 6.cm());
            Element(3).ShouldBeAt(page: 2, top: 6.cm(), height: 6.cm());
        }
    }
}