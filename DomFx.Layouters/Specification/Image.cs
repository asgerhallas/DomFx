using System;
using System.Runtime.Serialization;

namespace DomFx.Layouters.Specification
{
    public class Image : ElementSpecificationAbstract, Imaged
    {
        public Image()
        {
            Behavior = new ImageBehavior(this);
        }

        public ImageSource Source { get; set; }

        public void SizeBySource()
        {
            if (Source == null)
                throw new SpecificationException("Source must be set before call to SizeBySource()");

            InnerWidth = Source.Width;
            InnerHeight = Source.Height;
        }
    }

    [Serializable]
    public class SpecificationException : Exception
    {
        public SpecificationException(string message) : base(message)
        {
        }

        public SpecificationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SpecificationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}