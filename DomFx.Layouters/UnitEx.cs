using System;
using System.Collections.Generic;
using System.Linq;

namespace DomFx.Layouters
{
    public static class UnitEx
    {
        // ReSharper disable InconsistentNaming
        public static Unit cm(this double number)
        {
            return Unit.FromCentimeters(number);
        }

        public static Unit cm(this int number)
        {
            return Unit.FromCentimeters(number);
        }

        public static Unit points(this double number)
        {
            return Unit.FromPoints(number);
        }

        public static Unit points(this float number)
        {
            return Unit.FromPoints(number);
        }

        public static Unit points(this int number)
        {
            return Unit.FromPoints(number);
        }
        // ReSharper enable InconsistentNaming

        public static Unit Sum<T>(this IEnumerable<T> units, Func<T, Unit> selector)
        {
            return units.Aggregate(Unit.Zero, (aggr, next) => aggr + selector(next));
        }
    }
}