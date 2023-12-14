namespace FileMatch.Events
{
    internal class LimitReachedEvent : EventArgs
    {
        public long Limit { get; private set; }

        public LimitReachedEvent(long limit)
        {
            Limit = limit;
        }
    }
}
