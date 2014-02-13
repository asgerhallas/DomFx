namespace DomFx.Api.Builder.Generator
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

        public void Apply(StyleBuilder style)
        {
            ambient.Apply(style);
            overrides.Apply(style);
        }
    }
}