using System;

namespace DomFx.Layouters
{
    public struct Unit : IComparable<Unit>
    {
        public static readonly Unit Undefined = new Unit(0, UnitOfMeasure.Point, false);
        public static readonly Unit Zero = new Unit(0, UnitOfMeasure.Point, true);
        readonly bool defined;
        readonly double points;

        Unit(double value, UnitOfMeasure unitOfMeasure, bool defined)
        {
            this.defined = defined;
            switch (unitOfMeasure)
            {
                case UnitOfMeasure.Point:
                    points = value;
                    break;
                case UnitOfMeasure.Centimeter:
                    points = value*72/2.54;
                    break;
                case UnitOfMeasure.Inch:
                    points = value*72;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("unitOfMeasure");
            }
        }

        public double Points
        {
            get { return Math.Round(points, 2, MidpointRounding.AwayFromZero); }
        }

        public double Inches
        {
            get { return Math.Round(points/72, 2, MidpointRounding.AwayFromZero); }
        }

        public double Centimeters
        {
            get { return Math.Round(points*2.54/72, 2, MidpointRounding.AwayFromZero); }
        }

        public bool IsDefined
        {
            get { return defined; }
        }

        public int CompareTo(Unit other)
        {
            return points.CompareTo(other.points);
        }

        public static Unit FromCentimeters(double value)
        {
            return new Unit(value, UnitOfMeasure.Centimeter, true);
        }

        public static Unit FromInches(double value)
        {
            return new Unit(value, UnitOfMeasure.Inch, true);
        }

        public static Unit FromPoints(double value)
        {
            return new Unit(value, UnitOfMeasure.Point, true);
        }

        public static Unit From(UnitOfMeasure unitOfMeasure, double value)
        {
            return new Unit(value, unitOfMeasure, true);
        }

        public static Unit operator -(Unit left, Unit right)
        {
            return FromPoints(left.points - right.points);
        }

        public static Unit operator -(Unit unit)
        {
            return FromPoints(-unit.points);
        }

        public static Unit operator +(Unit left, Unit right)
        {
            return FromPoints(left.points + right.points);
        }

        public static Unit operator *(Unit left, Unit right)
        {
            return FromPoints(left.points * right.points);
        }

        public static Unit operator *(Unit left, int right)
        {
            return FromPoints(left.points * right);
        }

        public static Unit operator *(Unit left, double right)
        {
            return FromPoints(left.points * right);
        }

        public static Unit operator *(double left, Unit right)
        {
            return FromPoints(left * right.points);
        }

        public static Unit operator %(Unit left, Unit right)
        {
            return FromPoints(left.points % right.points);
        }

        public static double operator /(Unit left, Unit right)
        {
            return left.points / right.points;
        }

        public static Unit operator /(Unit left, int right)
        {
            return FromPoints(left.points / right);
        }

        public bool Equals(Unit other)
        {
            return !defined && !other.defined || other.Points.Equals(Points);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (obj.GetType() != typeof (Unit))
                return false;
            return Equals((Unit) obj);
        }

        public override int GetHashCode()
        {
            return points.GetHashCode();
        }

        public static bool operator ==(Unit left, Unit right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Unit left, Unit right)
        {
            return !left.Equals(right);
        }

        public static bool operator >(Unit left, Unit right)
        {
            return left.Points > right.Points;
        }

        public static bool operator <(Unit left, Unit right)
        {
            return left.Points < right.Points;
        }

        public static bool operator >=(Unit left, Unit right)
        {
            return left.Points >= right.Points;
        }

        public static bool operator <=(Unit left, Unit right)
        {
            return left.Points <= right.Points;
        }

        public override string ToString()
        {
            return Centimeters + " cm";
        }

        public static Unit Max(Unit left, Unit right)
        {
            return left > right ? left : right;
        }

        public static Unit Min(Unit left, Unit right)
        {
            return left < right ? left : right;
        }
    }
}