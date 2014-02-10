using DomFx.Layouters;
using DomFx.Tests.Fakes;
using Xunit;

namespace DomFx.Tests.Layouters.Paging
{
    public class ElementHeightTests : PagingTestsBase
    {
        [Fact]
        public void element_with_behavior_and_no_children_gets_height_from_special_behavior()
        {
            Element(behavior: new TestBehaviors(new TestHeightBehavior()));

            LayoutWithPageHeight(10.cm());

            Element(1).ShouldBeAt(page: 1, top: 0.cm(), height: 5.cm());
        }

        [Fact]
        public void element_with_behavior_and_higher_children_gets_height_from_highest()
        {
            Element(behavior: new TestBehaviors(new TestHeightBehavior()), children: new[]
            {
                ChildElement(innerHeight: 6.cm())
            });

            LayoutWithPageHeight(10.cm());

            Element(1).ShouldBeAt(page: 1, top: 0.cm(), height: 6.cm());
        }

        [Fact]
        public void element_with_behavior_and_smaller_children_gets_height_from_highest()
        {
            Element(behavior: new TestBehaviors(new TestHeightBehavior()), children: new[]
            {
                ChildElement(innerHeight: 4.cm())
            });

            LayoutWithPageHeight(10.cm());

            Element(1).ShouldBeAt(page: 1, top: 0.cm(), height: 5.cm());
        }

        [Fact]
        public void element_with_no_children_gets_no_height()
        {
            Element(innerHeight: 10.cm(), children: new[]
            {
                ChildElement()
            });

            LayoutWithPageHeight(10.cm());

            Element(2).ShouldBeAt(page: 1, top: 0.cm(), height: 0.cm());
        }


        [Fact]
        public void element_with_children_gets_height_from_children()
        {
            Element(innerHeight: 10.cm(), children: new[]
            {
                ChildElement(children: new[]
                {
                    ChildElement(innerHeight: 5.cm())
                })
            });

            LayoutWithPageHeight(10.cm());

            Element(2).ShouldBeAt(page: 1, top: 0.cm(), height: 5.cm());
        }


        [Fact]
        public void element_with_children_and_height_gets_forced_height()
        {
            Element(innerHeight: 10.cm(), children: new[]
            {
                ChildElement(innerHeight: 2.cm(), children: new[]
                {
                    ChildElement(innerHeight: 5.cm())
                })
            });

            LayoutWithPageHeight(10.cm());

            Element(2).ShouldBeAt(page: 1, top: 0.cm(), height: 2.cm());
        }
         
    }
}