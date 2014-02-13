using System.Collections.Generic;
using DomFx.Api.Builder.Styles;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Api.Builder.Builders
{
    public delegate IEnumerable<IElement> Element(IStyle style);
}