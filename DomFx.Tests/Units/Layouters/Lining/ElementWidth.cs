using DomFx.Layouters;
using Xunit;

namespace DomFx.Tests.Units.Layouters.Lining
{
    public class ElementWidth : LiningTests
    {
        [Fact]
        public void child_with_no_children_gets_width_from_parent()
        {
            Element(innerWidth: 2.cm(), innerHeight: 3.cm(), children: new[]
            {
                ChildElement()
            });

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 2.cm(), height: 0.cm());
        }

        [Fact]
        public void child_with_children_gets_width_from_children()
        {
            Element(innerWidth: 10.cm(), innerHeight: 3.cm(), children: new[]
            {
                ChildElement(children: new[]
                {
                    ChildElement(innerWidth: 5.cm())
                })
            });

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 5.cm(), height: 0.cm());
        }

        [Fact]
        public void child_with_children_and_width_gets_forced_width()
        {
            Element(innerWidth: 10.cm(), innerHeight: 3.cm(), children: new[]
            {
                ChildElement(innerWidth: 2.cm(), children: new[]
                {
                    ChildElement(innerWidth: 5.cm())
                })
            });

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 2.cm(), height: 0.cm());
        }
    }
}