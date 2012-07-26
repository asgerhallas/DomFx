using System.Collections.Generic;
using System.Linq;

namespace DomFx.Layouters
{
    public class Page
    {
        Page(IEnumerable<FixedElement> elements)
        {
            Elements = elements.ToList();
        }

        public List<FixedElement> Elements { get; private set; }

        public Unit OuterHeight
        {
            get { return Elements.Select(x => x.MarginBox.Bottom).DefaultIfEmpty().Max(); }
        }

        public void Add(Line line, Unit topOfPage)
        {
            AddInternal(line, 0.cm(), topOfPage, 0.cm());
        }

        void AddInternal(Line line, Unit parentLeft, Unit topOfPage, Unit topOfParentLine)
        {
            var topOfLine = topOfParentLine + line.Top;
            foreach (var element in line.Elements)
            {
                var elementLeft = parentLeft + element.Left;
                Elements.Add(new FixedElement(element, elementLeft, topOfLine - topOfPage));

                var elementTopBorder = element.Edge.Top;
                var elementLeftBorder = element.Edge.Left;
                foreach (var child in element.Children)
                {
                    AddInternal(child, elementLeft + elementLeftBorder, topOfPage, topOfLine + elementTopBorder);
                }
            }
        }

        public Page Move(Unit offsetLeft, Unit offsetTop)
        {
            return new Page(Elements.Select(x => x.Move(offsetLeft, offsetTop)));
        }

        public Page OverlayWith(Page page, Unit offsetLeft, Unit offsetTop)
        {
            return new Page(Elements.Concat(page.Move(offsetLeft, offsetTop).Elements));
        }

        public Page Next()
        {
            return new Page(Enumerable.Empty<FixedElement>());
        }

        public static Page First()
        {
            return new Page(Enumerable.Empty<FixedElement>());
        }
    }
}