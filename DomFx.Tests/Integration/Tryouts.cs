using System.IO;
using System.Windows.Media;
using DomFx.Layouters;
using DomFx.Renderers.iTextSharp;
using Xunit;
using iTextSharp.text.pdf;

namespace DomFx.Tests.Integration
{
    public class Tryouts : InputToPagesTests
    {
        public Tryouts() : base(UnitOfMeasure.Centimeter) {}

        [Fact(Skip="")]
        public void text_set_to_null_has_previously_caused_exception_in_text_render()
        {
            Setup(() =>
            {
                var manifestResourceStream = GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.DINOffc.ttf");
                var readFully = ReadFully(manifestResourceStream);
                var baseFont = BaseFont.CreateFont("DINOffc.ttf", BaseFont.CP1252, true, true, readFully, null);

                return Yield(
                    Box(width: 10, borders: Borders(1, 1, 1, 1, Colors.Aqua)),
                    Text(font: new iTextSharpFont(baseFont, 12, 12), text: null, width: 10));
            });

            Layout();

            ShowWithiTextSharp();
        }

        [Fact(Skip = "")]
        public void Trythis()
        {
            Setup(() =>
            {
                var manifestResourceStream = GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.DINOffc.ttf");
                var readFully = ReadFully(manifestResourceStream);
                var baseFont = BaseFont.CreateFont("DINOffc.ttf", BaseFont.CP1252, true, true, readFully, null);

                return Yield(
                    Text(font: new iTextSharpFont(baseFont, 12, 12), text: "hej", width: 10));
            });

            Layout();

            ShowWithiTextSharp();
        }

        [Fact]
        public void test_i_text_sharp()
        {
            Setup(() => 
                Image(source:iTextSharpVectorImage.FromPdf(GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.test.pdf"))));

            Layout();

            ShowWithiTextSharp();
        }


        [Fact]
        public void leading_tryouts()
        {
            Setup(() =>
                Box(height: 10, width: 10, backgroundColor: Colors.Aqua));

            Layout();

            ShowWithiTextSharp();
        }

        //[Fact]
        //public void end_of_text_is_missing_on_page()
        //{
        //    Setup(() =>
        //    {
        //        var manifestResourceStream = GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.DINOffc.ttf");
        //        var readFully = ReadFully(manifestResourceStream);
        //        var baseFont = BaseFont.CreateFont("DINOffc.ttf", BaseFont.CP1252, true, true, readFully, null);

        //        Text()
        //            .Font(new iTextSharpFont(baseFont, 12, 12))
        //            .Text(File.ReadAllText("Integration\\longtext1.txt")).Width(17);
        //    });

        //    Layout();

        //    ShowWithiTextSharp();
        //}

        //[Fact]
        //public void end_of_text_is_missing_on_page2()
        //{
        //    Setup(() =>
        //    {
        //        var manifestResourceStream = GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.DINOffc.ttf");
        //        var readFully = ReadFully(manifestResourceStream);
        //        var baseFont = BaseFont.CreateFont("DINOffc.ttf", BaseFont.CP1252, true, true, readFully, null);

        //        Box().BackgroundColor(Colors.Beige);
                
        //        Text()
        //            .Clear()
        //            .Margins(1, 1, 1, 1)
        //            .Font(new iTextSharpFont(baseFont, 12, 12))
        //            .Text(File.ReadAllText("Integration\\longtext3.txt"))
        //            .Width(15);
        //    });

        //    Layout();

        //    ShowWithiTextSharp();
        //}
        
        //[Fact]
        //public void end_of_text_is_missing_on_page3()
        //{
        //    Setup(() =>
        //    {
        //        var manifestResourceStream = GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.DINOffc.ttf");
        //        var readFully = ReadFully(manifestResourceStream);
        //        var baseFont = BaseFont.CreateFont("DINOffc.ttf", BaseFont.CP1252, true, true, readFully, null);

        //        var top = 1 + 2.9999996001228419;
        //        Box().BackgroundColor(Colors.Red).Borders(top, 0, 1.7527777777777774, 0, Colors.Brown);

        //        Box().BackgroundColor(Colors.Beige).Height(19.465832933456177-top);
        //        End<Box>();

        //        Text()
        //            .Clear()
        //            .Font(new iTextSharpFont(baseFont, 10, 13))
        //            .Text(File.ReadAllText("Integration\\longtext4.txt"))
        //            .Width(15);
        //    });

        //    Layout();

        //    ShowWithiTextSharp();
        //}
        
        
        //[Fact]
        //public void long_text_goes_beyond_borders_in_the_end()
        //{
        //    Setup(() =>
        //    {
        //        var manifestResourceStream = GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.DINOffc.ttf");
        //        var readFully = ReadFully(manifestResourceStream);
        //        var baseFont = BaseFont.CreateFont("DINOffc.ttf", BaseFont.CP1252, true, true, readFully, null);

        //        Text()
        //            .Width(14)
        //            .Font(new iTextSharpFont(baseFont, 12, 12))
        //            .Borders(11.3, 1.3, 11.3, 1.3, Colors.SaddleBrown)
        //            .Text(File.ReadAllText("Integration\\longtext2.txt"));
        //    });

        //    Layout();

        //    ShowWithiTextSharp();
        //}

        //[Fact]
        //public void keep_with_next_fails()
        //{
        //    Setup(() =>
        //    {
        //        Box().Margins(2, 2, 2, 2);

        //        Box().Width(10).Height(20).BackgroundColor(Colors.Aqua);
        //        End<Box>();
        //        Box().Width(10).Height(3).KeepWithNextLine().BackgroundColor(Colors.Red);
        //        End<Box>();
        //        Box().Borders(0.1, 0.1, 0.1, 0.1, Colors.Crimson).Unbreakable();
        //            Box().Width(10).Height(2).KeepWithNextLine().BackgroundColor(Colors.Gray);
        //            End<Box>();
        //            Box().Width(10).Height(20).BackgroundColor(Colors.Green);
        //            End<Box>();
        //        End<Box>();
        //    });

        //    Layout();

        //    ShowWithiTextSharp();
        //}


        //[Fact(Skip = "")]
        //public void test_i_text_sharp_text()
        //{
        //    Setup(() =>
        //    {
        //        var manifestResourceStream = GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.DINOffc.ttf");
        //        var readFully = ReadFully(manifestResourceStream);
        //        var baseFont = BaseFont.CreateFont("DINOffc.ttf", BaseFont.CP1252, true, true, readFully, null);

        //        Box().Width(10).Borders(1, 1, 1, 1, Colors.Aqua);
        //        Text().Font(new iTextSharpFont(baseFont, 12, 12)).Text("Hej\nMed\nDig").Width(10);
        //    });

        //    Layout();

        //    ShowWithiTextSharp();
        //}

        //[Fact(Skip = "")]
        //public void test_itextsharp()
        //{
        //    Setup(() => Image()
        //                    .Source(iTextSharpVectorImage.FromPdf(GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.test.pdf")))
        //                    .SizeBySource());

        //    Layout();

        //    ShowWithiTextSharp();
        //}

        public static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16*1024];
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