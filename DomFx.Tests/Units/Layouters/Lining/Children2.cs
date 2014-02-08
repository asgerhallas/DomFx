using DomFx.Layouters;
using DomFx.Layouters.Specification.Style;
using Xunit;

namespace DomFx.Tests.Units.Layouters.Lining
{
    public class Children2 : LiningTests
    {
        [Fact]
        public void child_higher_than_parent_will_crop()
        {
            Element(innerHeight: 10.cm(),
                    children: new[]
                    {
                        ChildElement(innerHeight: 12.cm())
                    });

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 10.cm(), height: 10.cm());
            Element(2).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 10.cm(), height: 10.cm(), originalInnerHeight: 12.cm());
        }

        [Fact]
        public void child_not_on_first_line_with_insufficient_parent_height_will_crop()
        {
            Element(innerHeight: 10.cm(),
                    children: new[]
                    {
                        ChildElement(innerHeight: 6.cm()),
                        ChildElement(innerHeight: 6.cm(), flow: FlowStyle.Clear),
                    });

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 10.cm(), height: 10.cm());
            Element(2).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 10.cm(), height: 6.cm());
            Element(3).ShouldBeAt(top: 6.cm(), left: 0.cm(), width: 10.cm(), height: 4.cm(), originalInnerHeight: 6.cm());
        }
        
        [Fact]
        public void first_child_is_placed_in_top_left_corner_of_parent()
        {
            Element(children: new[]
            {
                ChildElement(innerWidth: 5.cm(), innerHeight: 5.cm())
            });

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 5.cm(), height: 5.cm());
        }

        [Fact]
        public void children_on_line_determine_parent_height()
        {
            Element(innerWidth: 6.cm(), children: new[]
            {
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Float),
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Float)
            });

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 6.cm(), height: 5.cm());
        }

        [Fact]
        public void child_too_wide_for_parent_will_be_cropped_to_parent_width()
        {
            Element(innerWidth: 6.cm(), children: new[]
            {
                ChildElement(innerWidth: 7.cm())
            });

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 6.cm(), height: 0.cm(), originalInnerWidth: 7.cm());
        }
        
        [Fact]
        public void children_on_top_of_each_other_determine_parent_height()
        {
            Element(innerWidth: 6.cm(), children: new[]
            {
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Clear),
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Clear)
            });

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 6.cm(), height: 10.cm());
        }

        [Fact]
        public void clearing_children_that_fit_parent_are_placed_with_respect_to_parent_bounderies()
        {
            Element(innerWidth: 6.cm(), border: 1.cm(), children: new[]
            {
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Clear),
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Clear)
            });

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 8.cm(), height: 12.cm());
            Element(2).ShouldBeAt(top: 1.cm(), left: 1.cm(), width: 3.cm(), height: 5.cm());
            Element(3).ShouldBeAt(top: 6.cm(), left: 1.cm(), width: 3.cm(), height: 5.cm());
        }

        [Fact]
        public void floating_children_that_fit_parent_are_placed_with_respect_to_parent_bounderies()
        {
            Element(innerWidth: 6.cm(), border: 1.cm(), children: new[]
            {
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Float),
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Float)
            });

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 8.cm(), height: 7.cm());
            Element(2).ShouldBeAt(top: 1.cm(), left: 1.cm(), width: 3.cm(), height: 5.cm());
            Element(3).ShouldBeAt(top: 1.cm(), left: 4.cm(), width: 3.cm(), height: 5.cm());
        }      
        
        [Fact]
        public void floating_children_that_does_not_fit_parent_are_broken_with_respect_to_parent_bounderies()
        {
            Element(innerWidth: 5.cm(), border: 1.cm(), children: new[]
            {
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Float),
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Float)
            });

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 7.cm(), height: 12.cm());
            Element(2).ShouldBeAt(top: 1.cm(), left: 1.cm(), width: 3.cm(), height: 5.cm());
            Element(3).ShouldBeAt(top: 6.cm(), left: 1.cm(), width: 3.cm(), height: 5.cm());
        }
        
        [Fact]
        public void clearing_child_after_floating_child_that_fits_parent_are_placed_with_respect_to_parent_bounderies()
        {
            Element(innerWidth: 6.cm(), border: 1.cm(), children: new[]
            {
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Float),
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Clear)
            });

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 8.cm(), height: 12.cm());
            Element(2).ShouldBeAt(top: 1.cm(), left: 1.cm(), width: 3.cm(), height: 5.cm());
            Element(3).ShouldBeAt(top: 6.cm(), left: 1.cm(), width: 3.cm(), height: 5.cm());
        } 
        
        [Fact]
        public void floating_child_after_clearing_child_that_fits_parent_are_placed_with_respect_to_parent_bounderies()
        {
            Element(innerWidth: 6.cm(), border: 1.cm(), children: new[]
            {
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Clear),
                ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Float)
            });

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 8.cm(), height: 7.cm());
            Element(2).ShouldBeAt(top: 1.cm(), left: 1.cm(), width: 3.cm(), height: 5.cm());
            Element(3).ShouldBeAt(top: 1.cm(), left: 4.cm(), width: 3.cm(), height: 5.cm());
        }  
        
        [Fact]
        public void children_and_grand_children_that_fits_parents_are_positioned_with_respect_to_parent_boundaries()
        {
            Element(innerWidth: 7.cm(), border: 1.cm(), children: new[]
            {
                ChildElement(innerWidth: 5.cm(), border: 1.cm(), children: new[]
                {
                    ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm())
                }),
            });

            LayoutWithPageWidth(10.cm());

            Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 9.cm(), height: 9.cm());
            Element(2).ShouldBeAt(top: 1.cm(), left: 1.cm(), width: 7.cm(), height: 7.cm());
            Element(3).ShouldBeAt(top: 2.cm(), left: 2.cm(), width: 3.cm(), height: 5.cm());
        }

        //[Fact]
        //public void child_with_no_width_gets_parent_width()
        //{
        //    Element(innerWidth: 6.cm(), border: 1.cm(), Children: new[]
        //    {
        //        ChildElement(innerWidth: 3.cm(), innerHeight: 5.cm(), flow: FlowStyle.Clear)
        //    });

        //    LayoutWithPageWidth(10.cm());

        //    Element(1).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 8.cm(), height: 7.cm());
        //}  

    }
}