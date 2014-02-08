namespace DomFx.Layouters.Behaviors
{
    public abstract class StandardHeightBehavior : IHeightBehavior
    {
        public void Behave(LayoutedElement element)
        {
            if (element.ForcedInnerHeight.IsDefined)
                return;

            element.ForcedInnerHeight = Unit.Max(CalculateHeight(element), element.Children.OuterHeight);
        }

        protected abstract Unit CalculateHeight(LayoutedElement element);
    }
}