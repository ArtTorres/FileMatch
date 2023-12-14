using FileMatch.Events;

namespace FileMatch
{
    internal interface IFileAnalyzer
    {
        event EventHandler<DuplicateFileEvent> DuplicateFound;

        event EventHandler<AccessDeniedEvent> AccessDenied;

        event EventHandler<LimitReachedEvent> LimitReached;

        SearchMode Mode { get; }
        long FilesProcessed { get; }
        long Duplicates {  get; }

        void Analyze(string path);
    }
}
