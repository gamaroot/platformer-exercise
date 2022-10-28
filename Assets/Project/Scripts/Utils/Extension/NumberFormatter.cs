namespace GamaPlatform
{
    public static class NumberFormatter
    {
        public static string ToMeters(this int number)
        {
            return number < 1000 ? $"{number}m" : $"{number / 1000}km";
        }
    }
}