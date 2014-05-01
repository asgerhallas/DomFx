namespace DomFx.Api.Builder.Styles
{
    public class CompositeStyle : IStyle
    {
        readonly IStyle[] styles;

        public CompositeStyle(params IStyle[] styles)
        {
            this.styles = styles;
        }

        public void Apply(IStyleApplicator applicator)
        {
            foreach (var style in styles)
            {
                style.Apply(applicator);
            }
        }
    }
}