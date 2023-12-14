namespace FileMatch.Events
{
    internal class DuplicateFileEvent : EventArgs
    {
        public Metadata A { get; private set; }
        public Metadata B { get; private set; }


        public DuplicateFileEvent(Metadata a, Metadata b)
        {
            A = a;
            B = b;
        }
    }
}
