using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HapaxTools
{
    public static class ISetExtensions
    {
        /// <summary>
        /// Gets all possible subsets of a set.
        /// </summary>
        /// <typeparam name="T">The type of the set's elements.</typeparam>
        /// <param name="set">The current set.</param>
        /// <returns>An enumerable of all the possible subsets of the set.</returns>
        public static IEnumerable<ISet<T>> Powerset<T>(this ISet<T> set)
        {
            var subsets = new List<ISet<T>>();
            subsets.Add(new HashSet<T>()); // empty set

            if (set.Count == 0)
                return subsets;

            var item = set.First();
            var remaining = new HashSet<T>(set);
            remaining.Remove(item);
            var subSubsets = Powerset(remaining);

            subsets.AddRange(subSubsets);
            foreach (var subSubset in subSubsets)
            {
                var newSubset = new HashSet<T>(subSubset)
                {
                    item,
                };
                subsets.Add(newSubset);
            }

            return subsets;
        }
    }
}
