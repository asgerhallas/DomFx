using DomFx.Layouters;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Generator
{
    public interface IStyleBuilder
    {
        
    }

    public abstract class ElementStyleBuilder : IStyleBuilder
    {
        readonly IElement element;

        protected ElementStyleBuilder(IElement element)
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

        public void Height(Unit width)
        {
            element.InnerWidth = width;
        }
   }

    public class BoxStyleBuilder : ElementStyleBuilder
    {
        readonly Box box;

        public BoxStyleBuilder(Box box) : base(box)
        {
            this.box = box;
        }
    }

    public interface IStyle<in T>
    {
        void Apply(T style);
    }
}