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
}