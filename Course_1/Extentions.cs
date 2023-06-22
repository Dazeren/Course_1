using System;

namespace Course_1
{
    public static class Extentions
    {
        public static bool IsArrayMinSize<T>(this T[] array, int minSize) => array.Length >= minSize;

        public static T RandomElement<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
                return default(T);

            Random rng = new Random();
            return array[rng.Next(0, array.Length)];
        }
    }
}
