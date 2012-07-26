using System.Collections.Generic;
using System.Xml.Linq;
using DomFx.Layouters;
using DomFx.Layouters.Specification;

namespace DomFx.Renderers.Html
{
    public class BoxRenderer
    {
        public void Render(XElement canvas, FixedElement element)
        {
            var spec = (Backgrounded)element.Specification;

            var styles = new Dictionary<string, string>
            {
                { "position", "absolute" },
                { "left", string.Format("{0}px", element.InnerBox.Left) },
                { "top", string.Format("{0}px", element.InnerBox.Top) },
                { "width", string.Format("{0}px", element.InnerBox.Width) },
                { "height", string.Format("{0}px", element.InnerBox.Height) },
                { "background-color", string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", spec.BackgroundColor.A, spec.BackgroundColor.R, spec.BackgroundColor.G, spec.BackgroundColor.B)},
            };

            var div = new XElement("div");
            
            div.SetAttributeValue("style", "");
        }
    }
}