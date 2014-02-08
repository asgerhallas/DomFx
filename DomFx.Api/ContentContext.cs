using System;
using System.Collections.Generic;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Api
{
    public class ContentContext
    {
        readonly Dictionary<Type, List<Type>> contextCompatibility;
        readonly Stack<IElement> stack = new Stack<IElement>();
        readonly List<IElement> rootElements = new List<IElement>();

        public IEnumerable<IElement> RootElements
        {
            get { return rootElements; }
        }

        public ContentContext()
        {
            contextCompatibility = new Dictionary<Type, List<Type>>()
            {
                { typeof(Box), new List<Type> { typeof (Box), typeof (Image) } },
                { typeof(Image), new List<Type> { typeof (Box), typeof (Image) } },
                { typeof(Text), new List<Type> { typeof (Box), typeof (Image) } }
            };
        }

        public T Begin<T>() where T : IElement, new()
        {
            var element = PushNew<T>();
            return element;
        }

        public void End<T>() where T : IElement
        {
            PopNext<T>();
        }

        T PushNew<T>() where T : IElement, new()
        {
            var context = PopUntilContextFor<T>();
            var element = new T();
            
            if (context == null)
            {
                rootElements.Add(element);
            }
            else
            {
                context.AddChild(element);
            }
            
            stack.Push(element);
            return element;
        }

        IElement PopUntilContextFor<T>()
        {
            List<Type> compatibleContexts;
            if (!contextCompatibility.TryGetValue(typeof (T), out compatibleContexts))
                compatibleContexts = new List<Type>();

            while (true)
            {
                if (stack.Count == 0)
                    return null;

                if (compatibleContexts.Contains(stack.Peek().GetType()))
                {
                    return stack.Peek();
                }

                stack.Pop();
            }
        }

        IElement PopNext<T>()
        {
            while (true)
            {
                if (stack.Count == 0)
                    throw new NotFoundOnStackException<T>();

                var dom = stack.Pop();
                if (dom.GetType() == typeof (T))
                {
                    return dom;
                }
            }
        }
    }
}