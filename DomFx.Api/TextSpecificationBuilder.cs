using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Api
{
    public class TextSpecificationBuilder : ElementSpecificationBuilder<TextSpecificationBuilder>,
                                            BackgroundedElementSpecificationBuilder<TextSpecificationBuilder>,
                                            TextedElementSpecificationBuilder<TextSpecificationBuilder>

    {
        readonly Text element;
        readonly UnitType standardUnit;

        public TextSpecificationBuilder(Text element, UnitType standardUnit)
        {
            this.element = element;
            this.standardUnit = standardUnit;
        }

        IElement ElementSpecificationBuilder<TextSpecificationBuilder>.Element
        {
            get { return element; }
        }

        ITexted TextedElementSpecificationBuilder<TextSpecificationBuilder>.Element
        {
            get { return element; }
        }

        IBackgrounded BackgroundedElementSpecificationBuilder<TextSpecificationBuilder>.Element
        {
            get { return element; }
        }

        public UnitType StandardUnit
        {
            get { return standardUnit; }
        }
    }
}