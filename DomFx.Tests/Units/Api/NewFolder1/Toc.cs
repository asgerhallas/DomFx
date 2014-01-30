using System.Collections.Generic;
using DomFx.Api.Builder.Generator;
using DomFx.Layouters.Specification;

namespace DomFx.Tests.Units.Api.NewFolder1
{
    public class Header : ContentBuilder<int>
    {
        public Header(IBuilder<int, ElementSpecification> builder) 
            : base(new Margins(1, 2, 3, 4), builder) {}
    }

    public class Toc : Composer<int>
    {
        public override IBuilder<int, Document> Compose()
        {
            return Document(
                Section(
                    header: new Header())
                );
        }
    }
}