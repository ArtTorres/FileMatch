using MagnetArgs;

namespace FileMatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var options = Magnet.Attract<ProgramOptions>(args);

                if (string.IsNullOrEmpty(options.Path))
                {
                    options.Path = System.Environment.CurrentDirectory;
                }

                var analyzer = new Analyzer(options.Mode, options.Limit);
                var report = new Report(analyzer);

                report.Generate(options.Path);

                if (options.Verbose)
                {
                    report.DisplayResume();
                    report.DisplayErrors();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
