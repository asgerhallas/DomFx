using System;

namespace DomFx.Tests.Units
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string name)
            : base(String.Format("{0} not found", name))
        {
        }
    }
}