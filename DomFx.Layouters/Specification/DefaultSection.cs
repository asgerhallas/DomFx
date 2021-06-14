using System.Windows.Media;

namespace DomFx.Layouters.Specification
{
    public class Section
    {
        public Section(Content content, Content header, Content footer, Color backgroundColor, bool firstPageHeaderHasNoHeight)
        {
            Content = content;
            Header = header;
            Footer = footer;
            BackgroundColor = backgroundColor;
            FirstPageHeaderHasNoHeight = firstPageHeaderHasNoHeight;
        }

        public string Name { get; set; }
        public Color BackgroundColor { get; }
        public bool FirstPageHeaderHasNoHeight{ get; private set; }
        public Content Header { get; private set; }
        public Content Content { get; private set; }
        public Content Footer { get; private set; }
    }
}