namespace DomFx.Layouters.Specification
{
    public interface Imaged : ElementSpecification
    {
        ImageSource Source { get; set; }
        void SizeBySource();
    }
}