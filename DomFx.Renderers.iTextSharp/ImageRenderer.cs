using System;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using iTextSharp.text.pdf;
using Image = iTextSharp.text.Image;

namespace DomFx.Renderers.iTextSharp
{
    public class ImageRenderer
    {
        public void Render(PdfWriter writer, FixedElement element)
        {
            var spec = (IImaged) element.Specification;

            var image = GetImage(writer, spec);

            image.ScaleAbsolute((float)element.InnerBoxBeforeSplitOrCrop.Width.Points, (float)element.InnerBoxBeforeSplitOrCrop.Height.Points);
                
            var directContent = writer.DirectContent;
            var template = directContent.CreateTemplate((float)element.InnerBox.Width.Points, (float)element.InnerBox.Height.Points);
            image.SetAbsolutePosition(0, (float)(-image.ScaledHeight + element.InnerBox.Height.Points + element.VisiblePartOfSpecification.Top.Points));
            template.AddImage(image);
            directContent.AddTemplate(template, (float)element.InnerBox.Left.Points, (float)(writer.PageSize.Height - element.InnerBox.Bottom.Points));
        }

        static Image GetImage(PdfWriter writer, IImaged spec)
        {
            if (spec.Source is iTextSharpImage)
            {
                return ((iTextSharpImage) spec.Source).Source;
            }
            
            if (spec.Source is iTextSharpVectorImage)
            {
                return ((iTextSharpVectorImage) spec.Source).GetImage(writer);
            }

            throw new ArgumentOutOfRangeException("Specification does not hold an image");
        }
    }
}