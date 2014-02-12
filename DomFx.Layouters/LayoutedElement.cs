using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DomFx.Layouters.Actions;
using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Layouters
{
    /// <summary>
    ///   Representation of smallest unit in a document
    /// </summary>
    [DebuggerDisplay("{Name}, ViewportTop {ViewportTop}, Left {Left}")]
    public class LayoutedElement
    {
        readonly Children children;
        readonly IElement specification;

        public LayoutedElement(IElement specification, Children children)
        {
            Name = specification.Name;
            this.specification = specification;
            this.children = children;
        }

        LayoutedElement(IElement specification, IEnumerable<Line> children)
            : this(specification, new Children(children))
        {
        }

        public Children Children
        {
            get { return children; }
        }

        public Unit ViewportTop { get; set; }

        public Unit InnerHeight
        {
            get
            {
                Specification.Behavior.ApplyBeforePaging(this);
                return ForcedInnerHeight.IsDefined ? ForcedInnerHeight : children.OuterHeight;
            }
        }

        public Unit InnerWidth
        {
            get { return ForcedInnerWidth; }
        }

        public Unit OuterHeight
        {
            get
            {
                if (ForcedOuterHeight.IsDefined)
                    return ForcedOuterHeight;

                return InnerHeight + Edge.TotalVertical;
            }
        }

        public Unit OuterWidth
        {
            get
            {
                if (ForcedOuterWidth.IsDefined)
                    return ForcedOuterWidth;
                
                return InnerWidth + Edge.TotalHorizontal;
            }
        }

        public Unit Left { get; set; }

        public Unit Right
        {
            get { return Left + OuterWidth; }
        }

        public IEnumerable<LayoutedElement> AllDecendants
        {
            get { return children.SelectMany(x => x.AllDecendants); }
        }

        bool IsParted
        {
            get { return Name.Split('-').Length == 2; }
        }

        public Edge Edge
        {
            get { return Specification.Edge; }
        }

        public IElement Specification
        {
            get { return specification; }
        }

        public string Name { get; set; }

        public bool Breakable
        {
            get { return Specification.Breakable; }
        }

        public Unit OrphanHeight
        {
            get { return Specification.OrphanHeight; }
        }

        public Unit WidowHeight
        {
            get { return Specification.WidowHeight; }
        }

        public bool FollowLineHeight
        {
            get { return Specification.FollowLineHeight; }
        }

        public Unit ForcedInnerHeight { get; set; }
        public Unit ForcedOuterHeight { get; set; }

        public Unit ForcedInnerWidth { get; set; }
        public Unit ForcedOuterWidth { get; set; }

        public Unit InnerWidthBeforeSplitOrCrop { get; set; }
        public Unit InnerHeightBeforeSplitOrCrop { get; set; }

        public LayoutedElement Crop(Unit maximumHeight)
        {
            return new LayoutedElement(Specification, from child in children select child.Crop(maximumHeight))
            {
                Name = Name,
                Left = Left,
                ForcedOuterWidth = ForcedOuterWidth,
                ForcedOuterHeight = Unit.Min(OuterHeight, maximumHeight)
            };
        }

        public StopOr<Change<LayoutedElement>> Split(Unit pageHeight, Unit spaceLeftBeforeSplit)
        {
            if (!Breakable)
                return new Stop<Change<LayoutedElement>>();

            var innerSpaceLeftBeforeSplit = spaceLeftBeforeSplit - Edge.TotalVertical;

            innerSpaceLeftBeforeSplit = TryCorrectSpaceLeftBeforeSplit(innerSpaceLeftBeforeSplit);

            var part2InnerHeightAfterSplit = InnerHeight - innerSpaceLeftBeforeSplit;
            if (part2InnerHeightAfterSplit < WidowHeight)
            {
                innerSpaceLeftBeforeSplit = innerSpaceLeftBeforeSplit - (WidowHeight - part2InnerHeightAfterSplit);
                innerSpaceLeftBeforeSplit = TryCorrectSpaceLeftBeforeSplit(innerSpaceLeftBeforeSplit);
                part2InnerHeightAfterSplit = InnerHeight - innerSpaceLeftBeforeSplit;
            }

            if (innerSpaceLeftBeforeSplit < OrphanHeight)
                return new Stop<Change<LayoutedElement>>();

            //TODO: Fejl her... children skal også splittes med horeunger for øje
            var signal = children.Split(pageHeight - Edge.TotalVertical, innerSpaceLeftBeforeSplit);
            if (signal.OperationWasNotAllowed)
                return new Stop<Change<LayoutedElement>>();

            var upper = new LayoutedElement(Specification, signal.Result[0])
            {
                Name = GenerateNextPartName(1),
                Left = Left,
                ForcedInnerWidth = ForcedInnerWidth,
                ForcedOuterWidth = ForcedOuterWidth,
                ForcedInnerHeight = innerSpaceLeftBeforeSplit,
                InnerHeightBeforeSplitOrCrop = InnerHeight,
                ViewportTop = ViewportTop,
            };

            var lower = new LayoutedElement(Specification, signal.Result[1])
            {
                Name = GenerateNextPartName(2),
                Left = Left,
                InnerHeightBeforeSplitOrCrop = InnerHeight,
                ForcedInnerWidth = ForcedInnerWidth,
                ForcedOuterWidth = ForcedOuterWidth,
                ForcedInnerHeight = signal.Result[1].Any() ? Unit.Undefined : part2InnerHeightAfterSplit,
                ViewportTop = ViewportTop + innerSpaceLeftBeforeSplit
            };

            return new Changed<LayoutedElement>(upper, lower);
        }

        // A text can only be split in parts equal to line height. 
        // Correct the space so only a whole number of lines are left before the split
        Unit TryCorrectSpaceLeftBeforeSplit(Unit innerSpaceLeftBeforeSplit)
        {
            var text = Specification as Text;
            if (text != null)
            {
                var lineHeight = text.Font.CalculateLineHeight();
                return innerSpaceLeftBeforeSplit - innerSpaceLeftBeforeSplit%lineHeight;
            }
            return innerSpaceLeftBeforeSplit;
        }

        string GenerateNextPartName(int part)
        {
            if (Name == null)
                return "";

            var nameParts = Name.Split('-');
            var baseName = nameParts[0];
            var nextPartNumber = IsParted ? Convert.ToInt32(nameParts[1]) + part - 1 : part;

            return string.Format("{0}-{1}", baseName, nextPartNumber);
        }
    }
}