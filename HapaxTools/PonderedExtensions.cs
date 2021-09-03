using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HapaxTools
{
    public static class PonderedExtensions
    {
        /// <summary>
        /// Returns a random element from a list pondered with ints.
        /// </summary>
        /// <param name="pondered">The given Pondered instance.</param>
        /// <param name="rng">The RNG provider.</param>
        public static T FetchRandomInt<T>(this Pondered<T, int> pondered, Random rng)
        {
            return pondered.Fetch(rng.Next(pondered.Size()));
        }

        /// <summary>
        /// Returns a random element from a list pondered with longs.
        /// </summary>
        /// <param name="pondered">The given Pondered instance.</param>
        /// <param name="rng">The RNG provider.</param>
        public static T FetchRandomLong<T>(this Pondered<T, long> pondered, Random rng)
        {
            return pondered.Fetch(rng.NextLong(pondered.Size()));
        }

        /// <summary>
        /// Returns a random element from a list pondered with doubles.
        /// </summary>
        /// <param name="pondered">The given Pondered instance.</param>
        /// <param name="rng">The RNG provider.</param>
        public static T FetchRandomDouble<T>(this Pondered<T, double> pondered, Random rng)
        {
            return pondered.Fetch(pondered.Size() * rng.NextDouble());
        }
    }
}
