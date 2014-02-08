using System.Linq;
using DomFx.Layouters;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;
using Shouldly;
using Xunit;

namespace DomFx.Tests.Units.Layouters.Paging
{
    public class LayoutedElementTests
    {
        [Fact]
        public void forced_outerheight_is_transferred_to_fixed_element()
        {
            var element = new LayoutedElement(new Box(), DomFx.Layouters.Children.Empty) {ForcedOuterHeight = 2.cm()};
            var page = Page.First();
            page.Add(new Line(0.cm(), false, element), 0.cm());
            page.Elements.Single().MarginBox.Height.ShouldBe(2.cm());
        }

        [Fact]
        public void forced_outerwidth_is_transferred_to_fixed_element()
        {
            var element = new LayoutedElement(new Box(), DomFx.Layouters.Children.Empty) {ForcedOuterWidth = 2.cm()};
            var page = Page.First();
            page.Add(new Line(0.cm(), false, element), 0.cm());
            page.Elements.Single().MarginBox.Width.ShouldBe(2.cm());
        }

        [Fact]
        public void forced_innerheight_is_transferred_to_fixed_element()
        {
            var element = new LayoutedElement(new Box(), DomFx.Layouters.Children.Empty) {ForcedInnerHeight = 2.cm()};
            var page = Page.First();
            page.Add(new Line(0.cm(), false, element), 0.cm());
            page.Elements.Single().InnerBox.Height.ShouldBe(2.cm());
        }

        [Fact]
        public void forced_innerwidth_is_transferred_to_fixed_element()
        {
            var element = new LayoutedElement(new Box(), DomFx.Layouters.Children.Empty) {ForcedInnerWidth = 2.cm()};
            var page = Page.First();
            page.Add(new Line(0.cm(), false, element), 0.cm());
            page.Elements.Single().InnerBox.Width.ShouldBe(2.cm());
        }

        [Fact]
        public void crop_forces_max_height()
        {
            var element = new LayoutedElement(new Box {Margins = new Margins(1.cm(), 1.cm(), 1.cm(), 1.cm())}, DomFx.Layouters.Children.Empty) {ForcedInnerHeight = 2.cm()};
            var croppedElement = element.Crop(2.cm());
            croppedElement.OuterHeight.ShouldBe(2.cm());
        }

        [Fact]
        public void crop_does_not_force_a_larger_height_than_current()
        {
            var element = new LayoutedElement(new Box(), DomFx.Layouters.Children.Empty) {ForcedInnerHeight = 2.cm()};
            var croppedElement = element.Crop(3.cm());
            croppedElement.OuterHeight.ShouldBe(2.cm());
        }

        [Fact]
        public void split_element_views_corrects_parts_of_element()
        {
            var element = new LayoutedElement(new Box(), DomFx.Layouters.Children.Empty) {ForcedInnerHeight = 6.cm()};
            var result = element.Split(15.cm(), 3.cm()).Result;

            result[0].ViewportTop.ShouldBe(0.cm());
            result[1].ViewportTop.ShouldBe(3.cm());
        }

        [Fact]
        public void element_splitted_twice_views_corrects_parts_of_element()
        {
            var element = new LayoutedElement(new Box(), DomFx.Layouters.Children.Empty) {ForcedInnerHeight = 40.cm()};
            var result1 = element.Split(15.cm(), 15.cm()).Result;
            var result2 = result1[1].Split(15.cm(), 15.cm()).Result;

            result1[0].ViewportTop.ShouldBe(0.cm());
            result1[1].ViewportTop.ShouldBe(15.cm());
            result2[0].ViewportTop.ShouldBe(result1[1].ViewportTop);
            result2[1].ViewportTop.ShouldBe(30.cm());
        }

        [Fact]
        public void cropped_element_is_viewing_top_of_element()
        {
            var element = new LayoutedElement(new Box(), DomFx.Layouters.Children.Empty) {ForcedInnerHeight = 4.cm()};
            var result = element.Crop(3.cm());
            result.ViewportTop.ShouldBe(0.cm());
        }

        [Fact]
        public void split_element_has_original_layouted_element_height_and_width_stored()
        {
            var element = new LayoutedElement(new Box(), DomFx.Layouters.Children.Empty) {ForcedInnerHeight = 6.cm(), ForcedInnerWidth = 6.cm()};
            var result = element.Split(15.cm(), 3.cm()).Result;

            //result[0].TotalInnerHeightAsIfItWasOneBigElement.ShouldBe(0.cm());
//            result[0].LayoutedInnerWidth.ShouldBe(0.cm());
            result[1].ViewportTop.ShouldBe(3.cm());
        }
    }
}