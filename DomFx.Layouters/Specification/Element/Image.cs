using System;
using System.Collections.Generic;

namespace DomFx.Layouters.Specification.Element
{
    public class Image : Box
    {
        public Image() { }

        public Image(string name, IEnumerable<IElement> children) : base(name, children)
        {
            Behavior = new ImageBehavior(this);
        }

        public IImageSource Source { get; set; }

        public void SizeBySource()
        {
            if (Source == null)
                throw new InvalidOperationException("Source must be set before call to SizeBySource()");

            InnerWidth = Source.Width;
            InnerHeight = Source.Height;
        }
    }
}