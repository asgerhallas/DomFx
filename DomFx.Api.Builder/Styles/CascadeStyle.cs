namespace DomFx.Api.Builder.Styles
{
    public class CascadeStyle : IStyle
    {
        readonly IStyle ambient;
        readonly IStyle overrides;

        public CascadeStyle(IStyle ambient, IStyle overrides)
        {
            this.ambient = ambient;
            this.overrides = overrides;
        }

        public void Apply(IStyleApplicator applicator)
        {
            ambient.Apply(new CascadeStyleApplicator(applicator));
            overrides.Apply(applicator);
        }
    }

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