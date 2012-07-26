namespace DomFx.Layouters.Specification
{
    public class TextPlaceholderReplacement
    {
        public TextPlaceholder TextPlaceholder { get; private set; }
        public string Replacement { get; private set; }

        public TextPlaceholderReplacement(TextPlaceholder textPlaceholder, string replacement)
        {
            TextPlaceholder = textPlaceholder;
            Replacement = replacement;
        }
    }
}