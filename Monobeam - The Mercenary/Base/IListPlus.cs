using System.Collections.Generic;

namespace TheMatiaz0_MonobeamTheMercenary
{
    public static class IListPlus
    {
        public static void Add<T>(this IList<T> list, params T[] objects)
        {
            foreach (T item in objects)
            {
                list.Add(item);
            }
        }
    }
}
