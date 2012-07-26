using DomFx.Layouters;
using DomFx.Layouters.Specification;

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

        Backgrounded BackgroundedElementSpecificationBuilder<BoxSpecificationBuilder>.Element
        {
            get { return element; }
        }

        ElementSpecification ElementSpecificationBuilder<BoxSpecificationBuilder>.Element
        {
            get { return element; }
        }

        public UnitType StandardUnit
        {
            get { return standardUnit; }
        }
    }
}