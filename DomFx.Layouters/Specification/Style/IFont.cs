namespace DomFx.Layouters.Specification.Style
{
    public interface IFont
    {
        string Family { get; }
        double Size { get; }
        Unit CalculateTextHeight(string text, Unit width);
        Unit CalculateLineHeight();
    }
}