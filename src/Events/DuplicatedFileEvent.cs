namespace FileMatch.Events
{
    internal class DuplicatedFileEvent : EventArgs
    {
        public Metadata A { get; private set; }
        public Metadata B { get; private set; }


        public DuplicatedFileEvent(Metadata a, Metadata b)
        {
            A = a;
            B = b;
        }
    }
}
