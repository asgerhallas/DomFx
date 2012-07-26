using System.Collections.Generic;
using System.Linq;
using DomFx.Layouters.Actions;

namespace DomFx.Layouters
{
    public class KeepTogetherBatch
    {
        readonly List<Line> originalLines;
        readonly Pointer originalPosition;

        KeepTogetherBatch(Pointer originalPosition, List<Line> originalLines)
        {
            this.originalPosition = originalPosition;
            this.originalLines = originalLines;
            CurrentNumberOfLinesBeingBacktracked = 0;
            AllowKeepTogether = this.originalLines.Count > 1;
            Results = new List<Line>();
            WorkingQueue = new Queue<Line>(this.originalLines);
        }

        KeepTogetherBatch(KeepTogetherBatch batch, IEnumerable<Line> workingQueue, bool resetCanNotStayPut)
            : this(batch.originalPosition, batch.originalLines)
        {
            WorkingQueue = resetCanNotStayPut 
                ? new Queue<Line>(workingQueue.Select(x => x.StayPut())) 
                : new Queue<Line>(workingQueue);
        }

        public bool AllowKeepTogether { get; private set; }
        public Queue<Line> WorkingQueue { get; private set; }
        public List<Line> Results { get; private set; }
        int CurrentNumberOfLinesBeingBacktracked { get; set; }

        public static KeepTogetherBatch Batch(Pointer topOfBatch, IEnumerable<Line> linesToKeepTogether)
        {
            return new KeepTogetherBatch(topOfBatch, linesToKeepTogether.Where(x => x.OuterHeight > 0.cm()).ToList());
        }

        public KeepTogetherBatch RetryByBacktracking(Line currentLine)
        {
            var lines = Results.Concat(currentLine).Concat(WorkingQueue).ToList();

            var numberOfLinesToBacktrack = CurrentNumberOfLinesBeingBacktracked + 1;
            if (numberOfLinesToBacktrack < lines.Count) 
            {
                var numberOfLinesInResultToLeaveAsIs = lines.Count - (numberOfLinesToBacktrack + 1);
                var keepAsResult = lines.Take(numberOfLinesInResultToLeaveAsIs).ToList();
                var retry = lines.Skip(numberOfLinesInResultToLeaveAsIs).Select((x, i) => i == 0 ? x.DoNotStayPut() : x).ToList();

                return new KeepTogetherBatch(this, retry, resetCanNotStayPut: false)
                {
                    CurrentNumberOfLinesBeingBacktracked = numberOfLinesToBacktrack,
                    Results = keepAsResult,
                };
            }

            return new KeepTogetherBatch(this, lines, resetCanNotStayPut: true)
            {
                AllowKeepTogether = false
            };
        }

        public Pointer GetPositionAfterBatch()
        {
            var previousLine = Results.LastOrDefault();
            return previousLine != null ? originalPosition.GotoAfter(previousLine) : originalPosition;
        }

        public KeepTogetherBatch RetryWithChange(StopOr<Change<Line>> signal)
        {
            return new KeepTogetherBatch(this, signal.Result.Concat(WorkingQueue), resetCanNotStayPut: true)
            {
                AllowKeepTogether = AllowKeepTogether,
                Results = Results,
                CurrentNumberOfLinesBeingBacktracked = signal.Result is Move<Line> ? CurrentNumberOfLinesBeingBacktracked : 0,
            };
        }
    }
}