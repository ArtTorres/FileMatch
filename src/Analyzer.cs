using FileMatch.Events;

namespace FileMatch
{
    internal class Analyzer : IFileAnalyzer
    {
        public event EventHandler<DuplicateFileEvent> DuplicateFound;
        public void OnDuplicateFound(Metadata a, Metadata b)
        {
            DuplicateFound?.Invoke(this, new DuplicateFileEvent(a, b));
        }

        public event EventHandler<AccessDeniedEvent> AccessDenied;
        public void OnAccessDenied(Exception ex)
        {
            AccessDenied?.Invoke(this, new AccessDeniedEvent(ex));
        }

        public event EventHandler<LimitReachedEvent> LimitReached;
        public void OnLimitReached(int limit)
        {
            LimitReached?.Invoke(this, new LimitReachedEvent(limit));
        }

        private bool IsLimitReached { get { return _limit > 0 && _count >= _limit; } }
        private int _limit;

        public long FilesProcessed { get { return _count; } }
        private long _count;

        public long Duplicates { get { return _duplicates; } }
        private long _duplicates;

        public SearchMode Mode { get { return _mode; } }
        private SearchMode _mode;

        private Dictionary<int, Metadata> _hash = new Dictionary<int, Metadata>();

        public Analyzer(SearchMode mode = SearchMode.Standard, int limit = 5000)
        {
            _limit = limit;
            _mode = mode;
        }

        public void Analyze(string path)
        {
            var directory = new DirectoryInfo(path);

            if (directory.Exists)
            {
                Analyze(directory);
            }
            else
            {
                var drive = new DriveInfo(path);
                Analyze(drive);
            }
        }

        public void Analyze(DriveInfo directory)
        {
            try
            {
                Eval(directory.RootDirectory.GetDirectories());

                if (IsLimitReached) return;
                Eval(directory.RootDirectory.GetFiles());
            }
            catch (Exception ex)
            {
                OnAccessDenied(ex);
            }
        }

        public void Analyze(DirectoryInfo directory)
        {
            try
            {
                Eval(directory.GetDirectories());

                if (IsLimitReached) return;
                Eval(directory.GetFiles());
            }
            catch (Exception ex)
            {
                OnAccessDenied(ex);
            }
        }

        private void Eval(IEnumerable<DirectoryInfo> directories)
        {
            foreach (DirectoryInfo directory in directories)
            {
                if (IsLimitReached) break;

                try
                {
                    Eval(directory.GetDirectories());

                    if (IsLimitReached) break;

                    Eval(directory.GetFiles());
                }
                catch (Exception ex)
                {
                    OnAccessDenied(ex);
                }
            }
        }

        private void Eval(IEnumerable<FileInfo> files)
        {

            foreach (FileInfo file in files)
            {
                if (IsLimitReached)
                {
                    OnLimitReached(_limit);
                    break;
                }

                try
                {
                    var meta = new Metadata(file, _mode);
                    var key = meta.GetHashCode();

                    if (_hash.ContainsKey(key))
                    {
                        OnDuplicateFound(_hash[key], meta);
                        _duplicates++;
                    }
                    else
                    {
                        _hash.Add(key, meta);
                    }
                }
                catch (Exception ex)
                {
                    OnAccessDenied(ex);
                }
                finally
                {
                    _count++;
                }
            }
        }
    }
}
