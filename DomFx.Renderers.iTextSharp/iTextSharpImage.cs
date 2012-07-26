using System.IO;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using iTextSharp.text.pdf;
using Image = iTextSharp.text.Image;

namespace DomFx.Renderers.iTextSharp
{
    public class iTextSharpImage : ImageSource
    {
        readonly Image source;

        iTextSharpImage(Image source)
        {
            this.source = source;
        }

        public Image Source
        {
            get { return source; }
        }

        public Unit Width
        {
            get { return source.Width.points(); }
        }

        public Unit Height
        {
            get { return source.Height.points(); }
        }

        public static iTextSharpImage FromBitmap(byte[] source)
        {
            // If the stream is closed before rendering the XImage will not work
            // With current impl. of MemoryStream this will not result in a memory leak
            // Read here for reference: 
            // http://stackoverflow.com/questions/234059/is-a-memory-leak-created-if-a-memorystream-in-net-is-not-closed/234257#234257
            return FromBitmap(new MemoryStream(source));
        }

        public static iTextSharpImage FromBitmap(Stream source)
        {
            return new iTextSharpImage(Image.GetInstance(source));
        }

        public static iTextSharpImage FromPdf(byte[] source)
        {
            // See comment on FromBitmap(byte[] source)
            return FromPdf(new MemoryStream(source));
        }

        public static iTextSharpImage FromPdf(Stream stream)
        {
            var reader = new PdfReader(ReadFully(stream));
            var writer = new PdfStamper(reader, new MemoryStream());
            var pdfImportedPage = writer.GetImportedPage(reader, 1);
            var image = Image.GetInstance(pdfImportedPage);
            var height = image.Height;
            return new iTextSharpImage(image);
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}