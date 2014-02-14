using System;
using System.Collections.Generic;
using System.Linq;

namespace DomFx.Layouters.Specification.Element
{
    public class Image : Box
    {
        public Image(IImageSource source) : this(null, source, Enumerable.Empty<IElement>()) {}

        public Image(string name, IImageSource source, IEnumerable<IElement> children) : base(name, children)
        {
            if (source == null)
                throw new ArgumentNullException("Image source must be set");

            Source = source;
            InnerWidth = Source.Width;
            InnerHeight = Source.Height;

            Behavior = new ImageBehavior(this);
        }

        public IImageSource Source { get; private set; }
    }
}