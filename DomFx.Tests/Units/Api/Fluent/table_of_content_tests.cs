using DomFx.Api;
using DomFx.Layouters;

namespace DomFx.Tests.Units.Api.Fluent
{
    public class table_of_content_tests : TableOfContentsBase<int>
    {
        public table_of_content_tests()
            : base(42, UnitType.Centimeter)
        {
        }

        public override void Init()
        {
        }
    }
}