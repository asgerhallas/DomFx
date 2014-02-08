using System.Windows.Media;
using DomFx.Api;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;
using DomFx.Tests.Fakes;
using Shouldly;
using Xunit;

namespace DomFx.Tests.Units.Api.Fluent
{
    public class element_specification : content_tests
    {
        [Fact]
        public void has_default_breakable()
        {
            Setup(() => Box().Name("A"));
            Build();
            Specification<Box>("A").Breakable.ShouldBe(true);
        }

        [Fact]
        public void can_set_breakable()
        {
            Setup(() => Box().Name("A").Unbreakable().Breakable());
            Build();
            Specification<Box>("A").Breakable.ShouldBe(true);
        }

        [Fact]
        public void can_set_unbreakable()
        {
            Setup(() => Box().Name("A").Unbreakable());
            Build();
            Specification<Box>("A").Breakable.ShouldBe(false);
        }

        [Fact]
        public void has_default_float()
        {
            Setup(() => Box().Name("A"));
            Build();
            Specification<Box>("A").Flow.ShouldBe(FlowStyle.Float);
        }

        [Fact]
        public void can_set_clear()
        {
            Setup(() => Box().Name("A").Clear());
            Build();
            Specification<Box>("A").Flow.ShouldBe(FlowStyle.Clear);
        }

        [Fact]
        public void can_set_float()
        {
            Setup(() => Box().Name("A").Clear().Float());
            Build();
            Specification<Box>("A").Flow.ShouldBe(FlowStyle.Float);
        }

        [Fact]
        public void can_set_margin()
        {
            Setup(() => Box().Name("A").Margins(1, 2, 3, 4));
            Build();
            Specification<Box>("A").Margins.ShouldBe(new Margins(1.cm(), 2.cm(), 3.cm(), 4.cm()));
        }

        [Fact]
        public void can_set_borders()
        {
            Setup(() => Box().Name("A").Borders(1, 2, 3, 4, Colors.Gold));
            Build();
            Specification<Box>("A").Borders.ShouldBe(new Borders(1.cm(), 2.cm(), 3.cm(), 4.cm(), Colors.Gold));
        }

        [Fact]
        public void has_default_no_keep_with_next_line()
        {
            Setup(() => Box().Name("A"));
            Build();
            Specification<Box>("A").KeepWithNextLine.ShouldBe(false);
        }

        [Fact]
        public void can_set_keep_with_next_line()
        {
            Setup(() => Box().Name("A").KeepWithNextLine());
            Build();
            Specification<Box>("A").KeepWithNextLine.ShouldBe(true);
        }

        [Fact]
        public void has_default_no_follow_line_height()
        {
            Setup(() => Box().Name("A"));
            Build();
            Specification<Box>("A").FollowLineHeight.ShouldBe(false);
        }

        [Fact]
        public void can_set_follow_line_height()
        {
            Setup(() => Box().Name("A").FollowLineHeight());
            Build();
            Specification<Box>("A").FollowLineHeight.ShouldBe(true);
        }

        [Fact]
        public void can_set_width()
        {
            Setup(() => Box().Name("A").Width(5));
            Build();
            Specification<Box>("A").InnerWidth.ShouldBe(5.cm());
        }

        [Fact]
        public void can_set_height()
        {
            Setup(() => Box().Name("A").Height(5));
            Build();
            Specification<Box>("A").InnerHeight.ShouldBe(5.cm());
        }

        [Fact]
        public void can_set_size()
        {
            Setup(() => Box().Name("A").Size(4, 5));
            Build();
            Specification<Box>("A").InnerHeight.ShouldBe(5.cm());
            Specification<Box>("A").InnerWidth.ShouldBe(4.cm());
        }

        [Fact]
        public void can_set_style()
        {
            Setup(() => Box().Name("A").Style(new TestStyle()));
            Build();
            Specification<Box>("A").BackgroundColor.ShouldBe(Colors.GhostWhite);
        }
    }
}