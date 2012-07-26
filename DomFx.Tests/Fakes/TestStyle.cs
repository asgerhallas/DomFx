using System.Windows.Media;
using DomFx.Api;

namespace DomFx.Tests.Fakes
{
    public class TestStyle : Style<BoxSpecificationBuilder>
    {
        public void Apply(BoxSpecificationBuilder builder)
        {
            builder.BackgroundColor(Colors.GhostWhite);
        }
    }
}