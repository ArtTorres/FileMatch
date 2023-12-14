namespace FileMatch
{
    internal enum SearchMode
    {
        Basic,      // Filename
        Standard,   // Filename, Size
        Advanced,   // Filename, Size, CreationDate
        Strict      // Filename, Size, ModifiedDate
    }
}
