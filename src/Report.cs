using FileMatch.Events;
using FileMatch.Util;

namespace FileMatch
{
    internal class Report
    {
        private SearchMode _mode;
        private IFileAnalyzer _analyzer;
        private List<Exception> _errors;

        public Report(IFileAnalyzer fileAnalyzer)
        {
            _analyzer = fileAnalyzer;
            _mode = fileAnalyzer.Mode;
            _errors = new List<Exception>();

            _analyzer.DuplicateFound+=FileAnalyzer_DuplicationFound;
            _analyzer.LimitReached+=FileAnalyzer_LimitReached;
            _analyzer.AccessDenied+=FileAnalyzer_AccessDenied;
        }

        public void Generate(string path)
        {
            _analyzer.Analyze(path);
        }

        public void DisplayResume()
        {
            Console.WriteLine(".:");
            Console.WriteLine($"Files processed [{_analyzer.FilesProcessed}]");
            Console.WriteLine($"Matches [{_analyzer.Duplicates}]");
        }

        public void DisplayErrors()
        {
            Console.WriteLine($"Identified errors [{_errors.Count}]");

            foreach (Exception e in _errors)
            {
                Console.WriteLine($"{e.Message}");
            }
        }

        #region Events

        private void FileAnalyzer_DuplicationFound(object sender, Events.DuplicateFileEvent e)
        {
            Console.WriteLine($":: {e.A.Filename}");

            switch (_mode)
            {
                case SearchMode.Basic:
                case SearchMode.Standard:
                    Console.WriteLine($"[{SizeFormat.Compact(e.A.FileSize)}]\t{e.A.Fullname}");
                    Console.WriteLine($"[{SizeFormat.Compact(e.B.FileSize)}]\t{e.B.Fullname}");
                    break;
                case SearchMode.Advanced:
                    Console.WriteLine($"[CT {e.A.CreationTime}]\t{e.A.Fullname}");
                    Console.WriteLine($"[CT {e.B.CreationTime}]\t{e.B.Fullname}");
                    break;
                case SearchMode.Strict:
                    Console.WriteLine($"[LT {e.A.LastWriteTime}]\t{e.A.Fullname}");
                    Console.WriteLine($"[LT {e.B.LastWriteTime}]\t{e.B.Fullname}");
                    break;
            }
        }
        private void FileAnalyzer_LimitReached(object sender, Events.LimitReachedEvent e)
        {
            Console.WriteLine("[- Limit reached at {0} -]", e.Limit);
        }
        private void FileAnalyzer_AccessDenied(object sender, AccessDeniedEvent e)
        {
            _errors.Add(e.Exception);
        }

        #endregion
    }
}
