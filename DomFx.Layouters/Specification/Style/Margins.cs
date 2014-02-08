namespace DomFx.Layouters.Specification.Style
{
    public class Margins : Edge
    {
        public Margins()
        {
        }

        public Margins(Unit top, Unit right, Unit bottom, Unit left) : base(top, right, bottom, left)
        {
        }

        public Margins(Margins edge) : base(edge)
        {
        }

        public new static Margins None()
        {
            return new Margins();
        }
    }
}