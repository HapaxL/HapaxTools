using System;
using System.Collections.Generic;
using System.Text;

namespace HapaxTools
{
    public static class IEnumerableExtensions
    {
        /* Taken from this StackOverflow thread https://stackoverflow.com/a/5058721 */
        /* Thanks to user xanatos https://stackoverflow.com/users/613130/xanatos */
        /// <summary>
        /// Removes given elements from an enumerable (unlike IEnumerable.Except, does not remove duplicates).
        /// </summary>
        /// <typeparam name="T">The type of the enumerables' elements.</typeparam>
        /// <param name="minuend">The current enumerable.</param>
        /// <param name="subtrahend">The enumerable whose elements will be subtracted from the minuend.</param>
        /// <returns>An enumerable of all the items in the minuend that were not subtracted.</returns>
        public static IEnumerable<T> SubtractRange<T>(this IEnumerable<T> minuend, IEnumerable<T> subtrahend)
        {
            Dictionary<T, int> elements = new Dictionary<T, int>();

            // count how many of each element there are in subtrahend
            foreach (var el in subtrahend)
            {
                int num = 0;
                elements.TryGetValue(el, out num);
                elements[el] = num + 1;
            }

            // go through all elements in minuend, remove 1 from the corresponding element count in the subtrahend
            // or yield return the element if that element's count in the subtrahend is zero
            foreach (var el in minuend)
            {
                int num = 0;
                if (elements.TryGetValue(el, out num) && num > 0)
                {
                    elements[el] = num - 1;
                }
                else
                {
                    yield return el;
                }
            }
        }

        /// <summary>
        /// Returns elements in common between two enumerables (unlike IEnumerable.Intersect, does not remove duplicates).
        /// </summary>
        /// <typeparam name="T">The type of the enumerables' elements.</typeparam>
        /// <param name="first">The current enumerable.</param>
        /// <param name="second">The enumerable to be intersected with the current enumerable.</param>
        /// <returns>An enumerable of all the elements contained in both enumerables.</returns>
        public static IEnumerable<T> IntersectRange<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            Dictionary<T, int> elements = new Dictionary<T, int>();

            // count how many of each element there are in the second enumerable
            foreach (var el in second)
            {
                int num = 0;
                elements.TryGetValue(el, out num);
                elements[el] = num + 1;
            }

            // go through all elements in the first enumerable, remove 1 from the corresponding element count in the second
            // yield return the element if that element's count in the second enumerable isn't zero
            foreach (var el in first)
            {
                int num = 0;
                if (elements.TryGetValue(el, out num) && num > 0)
                {
                    elements[el] = num - 1;
                    yield return el;
                }
            }
        }
    }
}
