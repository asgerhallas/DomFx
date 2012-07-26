using DomFx.Layouters;
using DomFx.Layouters.Specification;

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

        ElementSpecification ElementSpecificationBuilder<TextSpecificationBuilder>.Element
        {
            get { return element; }
        }

        Texted TextedElementSpecificationBuilder<TextSpecificationBuilder>.Element
        {
            get { return element; }
        }

        Backgrounded BackgroundedElementSpecificationBuilder<TextSpecificationBuilder>.Element
        {
            get { return element; }
        }

        public UnitType StandardUnit
        {
            get { return standardUnit; }
        }
    }
}