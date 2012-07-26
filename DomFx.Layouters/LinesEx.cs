using System.Collections.Generic;

namespace DomFx.Layouters
{
    public static class LinesEx
    {
        public static Lines AsLines(this IEnumerable<Line> lines)
        {
            return new Lines(lines);
        }
    }
}