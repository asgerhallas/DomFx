namespace DomFx.Layouters.Specification
{
    public interface Font
    {
        string Family { get; }
        double Size { get; }
        Unit CalculateTextHeight(string text, Unit width);
        Unit CalculateLineHeight();
    }
}