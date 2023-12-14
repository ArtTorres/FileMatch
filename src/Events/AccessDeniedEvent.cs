namespace FileMatch.Events
{
    internal class AccessDeniedEvent : EventArgs
    {
        public Exception Exception { get; private set; }
        public AccessDeniedEvent(Exception ex)
        {
            Exception = ex;
        }
    }
}
