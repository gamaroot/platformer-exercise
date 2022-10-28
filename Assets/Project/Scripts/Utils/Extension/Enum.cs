using System;

namespace GamaPlatform
{
    public class Enum<T> where T : struct, IConvertible
    {
        public static int Length()
        {
            return Enum.GetValues(typeof(T)).Length;
        }
    }
}