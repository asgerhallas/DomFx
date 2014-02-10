using System;

namespace DomFx.Tests
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string name)
            : base(String.Format("{0} not found", name))
        {
        }
    }
}