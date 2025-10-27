using System.Collections.Generic;

namespace UnicoStudio.Extensions
{
    public static class EnumerableExtensions
    {
        private static System.Random _rng = new System.Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
