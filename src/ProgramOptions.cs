using MagnetArgs;
using MagnetArgs.Parsers;

namespace FileMatch
{
    [Magnetizable]
    internal class ProgramOptions
    {
        [Argument("path", Alias = "p")]
        public string Path { get; set; }

        [Argument("limit", Alias = "l"), Default(0)]
        public int Limit { get; set; }

        [Argument("mode", Alias = "m"), Parser(typeof(ModeParser)), Default("standard")]
        public SearchMode Mode { get; set; }

        [Argument("verbose", Alias = "v"), IfPresent]
        public bool Verbose { get; set; }
    }

    internal class ModeParser : IParser
    {
        public object Parse(string value)
        {
            switch (value)
            {
                case "basic":
                    return SearchMode.Basic;
                case "advanced":
                    return SearchMode.Advanced;
                case "strict":
                    return SearchMode.Strict;
                case "standard":
                default:
                    return SearchMode.Standard;
            }
        }
    }
}
