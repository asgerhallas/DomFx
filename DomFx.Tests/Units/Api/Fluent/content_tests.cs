using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using DomFx.Api;
using DomFx.Layouters;
using DomFx.Layouters.Specification;

namespace DomFx.Tests.Units.Api.Fluent
{
    public class content_tests : ContentBase<int>
    {
        readonly TOC toc;
        protected Document document;
        Action setupContent;
        Action<TOC> setupToc;

        protected content_tests()
        {
            toc = new TOC(this);
        }

        protected void Build()
        {
            document = toc.Build();
        }

        protected void Setup(Action setup)
        {
            setupContent = setup;
        }

        protected void SetupTOC(Action<TOC> setup)
        {
            setup(toc);
        }

        public override void Render()
        {
            if (setupContent != null)
                setupContent();
        }

        protected TSpec Specification<TSpec>(string name) where TSpec : ElementSpecification
        {
            return (TSpec) Specification(name);
        }

        protected void Evaluate()
        {
            var noop = document.Sections.Single().Content.Elements.ToList();
        }

        protected ElementSpecification Specification(string name)
        {
            var element = Specification(document.Sections.Single().Content.Elements, name);
            if (element == null)
                throw new ElementNotFoundException(name);

            return element;
        }

        ElementSpecification Specification(IEnumerable<ElementSpecification> elements, string name)
        {
            foreach (var element in elements)
            {
                if (element.Name == name)
                    return element;

                Specification(element.Children, name);
            }

            return null;
        }

        protected class TOC : TableOfContentsBase<int>
        {
            readonly IContent<int> content;

            public TOC(IContent<int> content)
                : base(42, UnitType.Centimeter)
            {
                this.content = content;
            }

            public override void Init()
            {
                Section().Content(content);
                //Section(Colors.Yellow).Content(content);
                //Section(Colors.Crimson).Content(content);
                //Section(Colors.Khaki).Content(content);
            }
        }

        public class Header : ContentBase<int>
        {
            public override void Render()
            {
                Box().Height(10).Borders(0.1, 0.1, 0.1, 0.1, Color.FromRgb(200, 0, 0));
            }
        }
    }
}