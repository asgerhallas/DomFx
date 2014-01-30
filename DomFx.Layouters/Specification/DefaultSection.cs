namespace DomFx.Layouters.Specification
{
    public class Section
    {
        public Section(Content header, Content content, Content footer)
        {
            Content = content;
            Header = header;
            Footer = footer;
        }

        public Content Header { get; private set; }
        public Content Content { get; private set; }
        public Content Footer { get; private set; }
    }
}