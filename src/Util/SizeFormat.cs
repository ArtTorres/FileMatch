namespace FileMatch.Util
{
    internal static class SizeFormat
    {
        private static string[] _units = new string[] { "B", "kB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string Compact(long size)
        {
            int c = 0;
            decimal s = Shrink(size, ref c);

            return $"{s:0.00} {_units[c]}";

            decimal Shrink(decimal value, ref int cycles)
            {
                if (value >= 1000)
                {
                    cycles++;

                    return Shrink(value / 1024m, ref cycles);
                }
                else
                {
                    return value;
                }
            }
        }
    }
}
