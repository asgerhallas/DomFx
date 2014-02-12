using DomFx.Layouters;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Generator
{
    public interface IStyleBuilder
    {
        
    }

    public class StyleBuilder : IStyleBuilder
    {
        readonly IElement element;

        public StyleBuilder(IElement element)
        {
            this.element = element;
        }

        public void Flow(FlowStyle flow)
        {
            element.Flow = flow;
        }

        public void Float()
        {
            Flow(FlowStyle.Float);
        }

        public void Clear()
        {
            Flow(FlowStyle.Clear);
        }

        public void Width(Unit width)
        {
            element.InnerWidth = width;
        }

        public void Height(Unit height)
        {
            element.InnerHeight = height;
        }

        public void Margins(Margins margins)
        {
            element.Margins = margins;
        }
   }

    public interface IStyle<in T>
    {
        void Apply(T style);
    }
}