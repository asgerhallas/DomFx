using DomFx.Layouters;
using DomFx.Layouters.Specification;

namespace DomFx.Api
{
    public class ImageSpecificationBuilder : ElementSpecificationBuilder<ImageSpecificationBuilder>,
                                             ImagedElementSpecificationBuilder<ImageSpecificationBuilder>

    {
        readonly Image element;
        readonly UnitType standardUnit;

        public ImageSpecificationBuilder(Image element, UnitType standardUnit)
        {
            this.element = element;
            this.standardUnit = standardUnit;
        }

        ElementSpecification ElementSpecificationBuilder<ImageSpecificationBuilder>.Element
        {
            get { return element; }
        }

        Imaged ImagedElementSpecificationBuilder<ImageSpecificationBuilder>.Element
        {
            get { return element; }
        }

        public UnitType StandardUnit
        {
            get { return standardUnit; }
        }
    }
}