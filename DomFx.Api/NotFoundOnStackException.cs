using System;

namespace DomFx.Api
{
    internal class NotFoundOnStackException<T> : Exception
    {
        public NotFoundOnStackException() : base(string.Format("Type {0} was not found on the stack", typeof(T)))
        {
            
        }
    }
}