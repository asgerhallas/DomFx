using System.IO;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using Image = iTextSharp.text.Image;

namespace DomFx.Renderers.iTextSharp
{
    public class iTextSharpVectorImage : ImageSource
    {
        readonly byte[] source;

        iTextSharpVectorImage(byte[] source)
        {
            this.source = source;
            var image = GetImage(PdfWriter.GetInstance(new Document(), new MemoryStream()));
            Width = image.Width.points();
            Height = image.Height.points();
        }

        public Image GetImage(PdfWriter writer)
        {
            var reader = new PdfReader(source);
            var pdfImportedPage = writer.GetImportedPage(reader, 1);
            return Image.GetInstance(pdfImportedPage);
        }

        public static iTextSharpVectorImage FromPdf(Stream stream)
        {
            return new iTextSharpVectorImage(ReadFully(stream));
        }

        static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
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

        public Unit Width { get; private set; }
        public Unit Height { get; private set; }
    }
}