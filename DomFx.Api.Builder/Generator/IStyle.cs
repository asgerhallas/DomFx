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
        readonly UnitOfMeasure unitOfMeasure;
        readonly IElement element;

        public StyleBuilder(UnitOfMeasure unitOfMeasure, IElement element)
        {
            this.unitOfMeasure = unitOfMeasure;
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

        public void Font(IFont font)
        {
            var text = element as Text;
            if (text != null)
            {
                text.Font = font;
            }
        }
    }

    public interface IStyle
    {
        void Apply(StyleBuilder style);
    }

    class SomStyle : IStyle
    {
        public void Apply(StyleBuilder style)
        {
        }
    }
}