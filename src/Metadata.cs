namespace FileMatch
{
    internal class Metadata
    {
        public string Filename { get; private set; }
        public string Fullname { get; private set; }
        public long FileSize { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime LastWriteTime { get; private set; }

        public SearchMode Mode { get; private set; }

        public Metadata(FileInfo file, SearchMode mode)
        {
            Filename = file.Name;
            Fullname = file.FullName;
            FileSize = file.Length;
            CreationTime = file.CreationTime;
            LastWriteTime = file.LastWriteTime;
            Mode = mode;
        }

        public override bool Equals(object obj)
        {
            if (obj is  Metadata)
            {
                var metadata = (Metadata)obj;
                return metadata.GetHashCode() == this.GetHashCode();
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            switch (Mode)
            {
                case SearchMode.Basic:
                    return Filename.GetHashCode();
                case SearchMode.Advanced:
                    return Filename.GetHashCode() ^ FileSize.GetHashCode() ^ CreationTime.GetHashCode();
                case SearchMode.Strict:
                    return Filename.GetHashCode() ^ FileSize.GetHashCode() ^ LastWriteTime.GetHashCode();
                case SearchMode.Standard:
                default:
                    return Filename.GetHashCode() ^ FileSize.GetHashCode();
            }
        }
    }
}
