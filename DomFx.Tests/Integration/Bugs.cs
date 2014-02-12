using DomFx.Layouters;
using DomFx.Tests.Api.Builder;
using DomFx.Tests.Fakes;
using iTextSharp.text;
using Shouldly;
using Xunit;

namespace DomFx.Tests.Integration
{
    public class Bugs : IntegrationTestsBase
    {
        public Bugs() : base(UnitOfMeasure.Centimeter) { }

        [Fact]
        public void texts_with_no_width_set_is_layouted_on_the_same_position()
        {
            Setup(() => Yield(
                Text("A", "A", font: new TestFont()),
                Text("B", "B", font: new TestFont()),
                Text("C", "C", font: new TestFont())
            ));


            Layout();

            Element("A").BorderBox.Top.ShouldBe(0.cm());
            Element("B").BorderBox.Top.ShouldBe(1.cm());
            Element("C").BorderBox.Top.ShouldBe(2.cm());
        }

        [Fact]
        public void boxes_on_same_line_with_margins_is_ordered_wrong()
        {
            Setup(() => Yield(
                Box(name: "A",
                    height: 5,
                    width: 5,
                    margins: Margins(0, 0, 0, 5)),
                Box(name: "B",
                    height: 5,
                    width: 5)
                ));

            //Box().Name("A").Height(5).Width(5).Margins(0, 0, 0, 5);
            //End<Box>();
            //Box().Name("B").Height(5).Width(5);

            //Box()
            //    .Name("B")
            //    .Height(5)
            //    .Width(5)
            //    .Box()
            //    .Box()
            //        .Height(2)
            //        .WIdth(2)
            //        .Box()

            Layout();

            Element("A").BorderBox.Top.ShouldBe(0.cm());
            Element("A").BorderBox.Left.ShouldBe(5.cm());
            Element("B").BorderBox.Top.ShouldBe(0.cm());
            Element("B").BorderBox.Left.ShouldBe(10.cm());
        }

        //////[Fact]
        //////public void boxes_with_children_on_same_line_with_margins_misses_margins()
        //////{
        //////    Setup(() =>
        //////    {
        //////        Box()
        //////            .Margins(0, 0, 0, 2.5);
        //////        Text().Name("TextA")
        //////            .Text("1.")
        //////            .Font(new TestFont())
        //////            .Width(2);
        //////        End<Box>();

        //////        Box().Width(16.5);
        //////        Text().Name("TextB")
        //////            .Text("Test")
        //////            .Font(new TestFont())
        //////            .Width(16.5);
        //////        End<Box>();
        //////    });

        //////    Layout();

        //////    Element("TextA").BorderBox.Top.ShouldBe(0.cm());
        //////    Element("TextA").BorderBox.Left.ShouldBe(2.5.cm());
        //////    Element("TextB").BorderBox.Top.ShouldBe(0.cm());
        //////    Element("TextB").BorderBox.Left.ShouldBe(4.5.cm());
        //////}

        //////[Fact(Timeout = 1000)]
        //////public void text_with_forced_width_less_than_calculated_width_loops_endlessly()
        //////{
        //////    //Is due to calculation of lines when width is less then or equal width of a char

        //////    Setup(() => Text().Name("TextA")
        //////                    .Text("1.")
        //////                    .Font(new TestFont())
        //////                    .Width(1));

        //////    Layout();
        //////    Should.NotThrow(Evaluate);
        //////}

        //////[Fact]
        //////public void child_box_with_text_in_box_with_margin_gets_double_margin()
        //////{
        //////    Setup(() =>
        //////    {
        //////        Box().Name("Box1")
        //////            .Width(18.5)
        //////            .Margins(0.5, 0, 0, 2.5);

        //////        Box().Name("Box2")
        //////            .FollowLineHeight()
        //////            .Width(12.8);
        //////        Text()
        //////            .Name("TextA")
        //////            .Text("test")
        //////            .Width(12.1)
        //////            .Font(new TestFont())
        //////            .Margins(0.35, 0.35, 0, 0.35);
        //////        End<Box>();

        //////        End<Box>();
        //////    });

        //////    Layout();

        //////    Element("Box1").BorderBox.Top.ShouldBe(0.5.cm());
        //////    Element("Box1").BorderBox.Left.ShouldBe(2.5.cm());

        //////    Element("Box2").BorderBox.Top.ShouldBe(0.5.cm());
        //////    Element("Box2").BorderBox.Left.ShouldBe(2.5.cm());
        //////    Element("Box2").BorderBox.Width.ShouldBe(12.8.cm());

        //////    Element("TextA").BorderBox.Top.ShouldBe(0.85.cm());
        //////    Element("TextA").BorderBox.Left.ShouldBe(2.85.cm());
        //////    Element("TextA").BorderBox.Width.ShouldBe(12.1.cm());
        //////}

        //////[Fact]
        //////public void child_box_with_text_in_box_with_margin_gets_additional_margin()
        //////{
        //////    Setup(() =>
        //////    {
        //////        Text()
        //////            .Text("header")
        //////            .Font(new TestPdfSharpFont());

        //////        Box()
        //////            .Name("Box1")
        //////            .Width(1)
        //////            .Clear()
        //////            .Margins(0, 0, 0, 2.5);
        //////        Text()
        //////            .Name("Text1")
        //////            .Font(new TestPdfSharpFont())
        //////            .Text("1.");
        //////        End<Box>();

        //////        Box();
        //////        Text().Text("Test")
        //////            .Font(new TestPdfSharpFont())
        //////            .Width(16.5);
        //////        End<Box>();
        //////    });

        //////    Layout();

        //////    Element("Box1").BorderBox.Left.ShouldBe(2.5.cm());
        //////    Element("Text1").BorderBox.Left.ShouldBe(2.5.cm());
        //////}

        //////[Fact]
        //////public void child_do_not_care_about_right_margins_of_parent()
        //////{
        //////    Setup(() =>
        //////    {
        //////        Box()
        //////            .Margins(5, 5, 5, 5);

        //////        Box()
        //////            .Name("ChildL")
        //////            .Height(5);
        //////    });

        //////    Layout();

        //////    Element("ChildL").BorderBox.Top.ShouldBe(5.cm());
        //////    Element("ChildL").BorderBox.Left.ShouldBe(5.cm());
        //////    Element("ChildL").MarginBox.Width.ShouldBe(11.cm());
        //////}

        //////[Fact]
        //////public void child_without_width_and_with_neightbour_should_get_remaining_width()
        //////{
        //////    Setup(() =>
        //////    {
        //////        Box()
        //////            .Margins(5, 5, 5, 5);

        //////        Box()
        //////            .Name("ChildL")
        //////            .Width(5)
        //////            .Height(5);
        //////        End<Box>();

        //////        Box()
        //////            .Name("ChildR")
        //////            .Height(5);
        //////    });

        //////    Layout();

        //////    Element("ChildL").MarginBox.Width.ShouldBe(5.cm());
        //////    Element("ChildR").MarginBox.Width.ShouldBe(6.cm());
        //////}

        //////[Fact]
        //////public void keep_with_next_stopped_working()
        //////{
        //////    Setup(() =>
        //////    {
        //////        Box().Height(20.7);
        //////        End<Box>();

        //////        Box().Height(5).KeepWithNextLine().Unbreakable().Name("A");
        //////        End<Box>();

        //////        Box().Height(5).Unbreakable().Name("B");
        //////        End<Box>();
        //////    });

        //////    Layout();

        //////    Element("A").BorderBox.Top.ShouldBe(0.cm());
        //////    Element("B").BorderBox.Top.ShouldBe(5.cm());
        //////}

        //////[Fact]
        //////public void box_width_margin_gets_wrong_height_and_width_is_wrong()
        //////{
        //////    Setup(() =>
        //////    {
        //////        Box()
        //////            .Name("A")
        //////            .BackgroundColor(Colors.Gold)
        //////            .Height(2)
        //////            .Width(2);
        //////        End<Box>();
        //////        Box()
        //////            .Name("B")
        //////            .Margins(0, 0, 0, 1.5)
        //////            .BackgroundColor(Colors.Gold)
        //////            .Height(2)
        //////            .Width(7.2);
        //////    });

        //////    Layout();

        //////    Element("B").BorderBox.Top.ShouldBe(0.cm());
        //////    Element("B").BorderBox.Left.ShouldBe(3.5.cm());
        //////    Element("B").BorderBox.Width.ShouldBe(7.2.cm());
        //////    Element("B").BorderBox.Height.ShouldBe(2.cm());
        //////}

        //////[Fact]
        //////public void three_elements_with_keep_together_allow_strange_split()
        //////{
        //////    Setup(() =>
        //////    {
        //////        Box()
        //////            .Name("A")
        //////            .BackgroundColor(Colors.Gold)
        //////            .Height(25.7);
        //////        End<Box>();
        //////        Box()
        //////            .Name("B")
        //////            .KeepWithNextLine()
        //////            .BackgroundColor(Colors.Green)
        //////            .Height(3);
        //////        End<Box>();
        //////        Box()
        //////            .Name("C")
        //////            .KeepWithNextLine()
        //////            .BackgroundColor(Colors.Red)
        //////            .Height(4);
        //////        End<Box>();
        //////        Box()
        //////            .Name("D")
        //////            .BackgroundColor(Colors.Firebrick)
        //////            .Height(4);
        //////        End<Box>();
        //////    });

        //////    Layout();

        //////    Element("B").BorderBox.Top.ShouldBe(0.cm());
        //////    Element("B").BorderBox.Height.ShouldBe(3.cm());
        //////    Element("C").BorderBox.Top.ShouldBe(3.cm());
        //////    Element("C").BorderBox.Height.ShouldBe(4.cm());
        //////    Element("D").BorderBox.Top.ShouldBe(7.cm());
        //////    Element("D").BorderBox.Height.ShouldBe(4.cm());
        //////}

        //////[Fact]
        //////public void giving_remaining_width_to_element_does_not_account_for_clearing_elements()
        //////{
        //////    Setup(() =>
        //////    {
        //////        Box().Name("A").Height(10).BackgroundColor(Colors.HotPink);
        //////        End<Box>();

        //////        Box().Width(8.5).Margins(0, 0, 0, 2.5).BackgroundColor(Colors.ForestGreen).Clear();
        //////        End<Box>();
        //////    });

        //////    Layout();

        //////    Element("A").BorderBox.Width.ShouldBe(21.cm());
        //////}
    }
}