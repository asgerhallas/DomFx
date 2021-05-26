using System.Drawing;

namespace DomFx.Renderers.iTextSharp
{
    public static class iTextSharpEx
    {
        public static global::iTextSharp.text.Color ToiTextSharpColor(this System.Windows.Media.Color color) => 
            new global::iTextSharp.text.Color(Color.FromArgb(color.A, color.R, color.G, color.B));
    }
}