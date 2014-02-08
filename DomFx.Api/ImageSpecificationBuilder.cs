using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

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

        IElement ElementSpecificationBuilder<ImageSpecificationBuilder>.Element
        {
            get { return element; }
        }

        IImaged ImagedElementSpecificationBuilder<ImageSpecificationBuilder>.Element
        {
            get { return element; }
        }

        public UnitType StandardUnit
        {
            get { return standardUnit; }
        }
    }
}