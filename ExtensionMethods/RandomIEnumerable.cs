using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Gallery.ExtensionMethods
{
    public static class RandomIEnumerable
    {
        public static Random random = new Random();

        public static T Random<T>(this IEnumerable<T> list)
        {
            return list.ToArray().Random();
        }

        public static T Random<T>(this T[] array)
        {
            return array[random.Next(0, array.Length - 1)];
        }
    }
}
