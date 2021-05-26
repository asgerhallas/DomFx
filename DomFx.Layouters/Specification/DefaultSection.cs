using System.Windows.Media;

namespace DomFx.Layouters.Specification
{
    public class Section
    {
        public Section(Content content, Content header, Content footer, Color backgroundColor)
        {
            Content = content;
            Header = header;
            Footer = footer;
            BackgroundColor = backgroundColor;
        }

        public string Name { get; set; }
        public Color BackgroundColor { get; }
        public Content Header { get; private set; }
        public Content Content { get; private set; }
        public Content Footer { get; private set; }
    }
}