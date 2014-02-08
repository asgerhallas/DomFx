using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Api
{
    public class BoxSpecificationBuilder : ElementSpecificationBuilder<BoxSpecificationBuilder>, BackgroundedElementSpecificationBuilder<BoxSpecificationBuilder>
    {
        readonly Box element;
        readonly UnitType standardUnit;

        public BoxSpecificationBuilder(Box element, UnitType standardUnit)
        {
            this.element = element;
            this.standardUnit = standardUnit;
        }

        IBackgrounded BackgroundedElementSpecificationBuilder<BoxSpecificationBuilder>.Element
        {
            get { return element; }
        }

        IElement ElementSpecificationBuilder<BoxSpecificationBuilder>.Element
        {
            get { return element; }
        }

        public UnitType StandardUnit
        {
            get { return standardUnit; }
        }
    }
}