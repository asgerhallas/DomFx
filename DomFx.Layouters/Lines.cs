using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DomFx.Layouters.Actions;

namespace DomFx.Layouters
{
    /// <summary>
    ///   Streaming representation of lines in a document
    /// </summary>
    public class Lines : IEnumerable<Line>
    {
        protected readonly IEnumerable<Line> lines;

        public Lines(IEnumerable<Line> lines)
        {
            this.lines = lines;
        }

        public IEnumerator<Line> GetEnumerator()
        {
            return lines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<Lines> FitTo(Unit pageHeight, Unit spaceToEndOfPage)
        {
            return FitTo(pageHeight, spaceToEndOfPage, forceIfNothingElseWorks: true);
        }

        protected IEnumerable<Lines> FitTo(Unit pageHeight, Unit spaceToEndOfPage, bool forceIfNothingElseWorks)
        {
            var resultingLines = new List<Line>();

            var position = Pointer.Origo(pageHeight, spaceToEndOfPage);
            foreach (var linesToKeepTogether in lines.BatchUntil(x => !x.KeepWithNextLine))
            {
                var safeguard = 0;

                var batch = KeepTogetherBatch.Batch(position, linesToKeepTogether);
                while (batch.WorkingQueue.Count > 0)
                {
                    if (safeguard > 995)
                        Debugger.Break();
                    
                    if (safeguard++ > 1000)
                        throw new Exception("It seems to loop forever");

                    var currentLine = batch.WorkingQueue.Dequeue();
                    position = position.ChangePageIfNeccessary(currentLine.Top);

                    var signal = currentLine.FitTo(position, batch.AllowKeepTogether, forceIfNothingElseWorks);

                    if (signal.OperationWasNotAllowed)
                        return Enumerable.Empty<Lines>();

                    if (signal.Result is KeepTogether<Line>)
                    {
                        batch = batch.RetryByBacktracking(currentLine);
                        position = batch.GetPositionAfterBatch();
                        continue;
                    }

                    if (signal.Result is Changed<Line>)
                    {
                        batch = batch.RetryWithChange(signal);
                        continue;
                    }

                    batch.Results.Add(currentLine);
                    position = position.GotoAfter(currentLine);
                }

                resultingLines.AddRange(batch.Results);
            }

            return new[] { new Lines(resultingLines) };
        }
    }
}