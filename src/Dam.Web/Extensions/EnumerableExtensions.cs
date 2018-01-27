using System;
using System.Collections.Generic;

namespace Dam.Web.Extensions
{
    public static class EnumerableExtensions
    {
        public static T MaxBy<T, TProperty>(
            this IEnumerable<T> items,
            Func<T, TProperty> selector)
        {
            var result = MaxByInternal(items, selector, out var maxFound);

            if (!maxFound)
            {
                throw new InvalidOperationException("Enumerable does not have any items.");
            }
            return result;
        }

        private static T MaxByInternal<T, TProperty>(
            IEnumerable<T> items, 
            Func<T, TProperty> selector, 
            out bool maxFound)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            maxFound = false;
            var currentMax = default(TProperty);
            var currentItem = default(T);
            var comparer = Comparer<TProperty>.Default;
            foreach (var item in items)
            {
                var value = selector(item);
                if (!maxFound)
                {
                    maxFound = true;
                    currentMax = value;
                    currentItem = item;
                }
                else
                {
                    if (comparer.Compare(value, currentMax) > 0)
                    {
                        currentMax = value;
                        currentItem = item;
                    }
                }
            }
            return currentItem;
        }
    }
}