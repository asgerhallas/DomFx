using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Generator
{
    public interface IStyleBuilder
    {
        
    }

    public abstract class ElementStyleBuilder
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
   }

    public class BoxStyleBuilder : IStyleBuilder
    {
        readonly Box box;

        public BoxStyleBuilder(Box box)
        {
            this.box = box;
        }

        public Box Float()
        {
            box.Flow = FlowStyle.Float;
            return box;
        }
    }

    public interface IStyle<T>
    {
        void Apply(T style);
    }
}