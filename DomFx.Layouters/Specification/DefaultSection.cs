namespace DomFx.Layouters.Specification
{
    public class Section
    {
        public Section(Content content, Content header, Content footer)
        {
            Content = content;
            Header = header;
            Footer = footer;
        }

        public string Name { get; set; }
        public Content Header { get; private set; }
        public Content Content { get; private set; }
        public Content Footer { get; private set; }
    }
}