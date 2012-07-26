using System.IO;
using System.Windows.Media.Imaging;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using PdfSharp.Drawing;

namespace DomFx.Renderers.PdfSharp.WPF
{
    public class PdfSharpImage : ImageSource
    {
        readonly XImage source;

        PdfSharpImage(XImage source)
        {
            this.source = source;
        }

        public XImage Source
        {
            get { return source; }
        }

        public Unit Width
        {
            get { return Unit.FromInches(source.PixelWidth/source.VerticalResolution); }
        }

        public Unit Height
        {
            get { return Unit.FromInches(source.PixelHeight/source.HorizontalResolution); }
        }

        public static PdfSharpImage FromBitmap(byte[] source)
        {
            // If the stream is closed before rendering the XImage will not work
            // With current impl. of MemoryStream this will not result in a memory leak
            // Read here for reference: 
            // http://stackoverflow.com/questions/234059/is-a-memory-leak-created-if-a-memorystream-in-net-is-not-closed/234257#234257
            return FromBitmap(new MemoryStream(source));
        }

        public static PdfSharpImage FromBitmap(Stream source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = source;
            bitmap.EndInit();
            var image = XImage.FromBitmapSource(bitmap);
            return new PdfSharpImage(image);
        }

        public static PdfSharpImage FromPdf(byte[] source)
        {
            // See comment on FromBitmap(byte[] source)
            return FromPdf(new MemoryStream(source));
        }

        public static PdfSharpImage FromPdf(Stream stream)
        {
            return new PdfSharpImage(XPdfForm.FromStream(stream));
        }
    }
}