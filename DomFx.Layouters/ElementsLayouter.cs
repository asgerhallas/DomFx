namespace DomFx.Layouters
{
    public class ElementsLayouter
    {
        public static Page Layout(Lines root)
        {
            var currentPage = Page.First();
            foreach (var line in root)
            {
                currentPage.Add(line, 0.cm());
            }

            return currentPage;            
        }
    }
}