using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HapaxTools
{
    
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a string array that contains the substrings in this instance that are delimited
        /// by elements of a specified Unicode character array, but considering everything within
        /// a specified pair of enclosing characters to be a single subtring. Parameters specify the
        /// maximum number of substrings to return and whether to return empty array elements.
        /// </summary>
        /// <param name="value">The given String instance.</param>
        /// <param name="separator">An array of Unicode characters that delimit the substrings
        /// in this instance, an empty array that contains no delimiters, or null.</param>
        /// <param name="delimiter">The opening and closing string.</param>
        public static IEnumerable<string> SplitWithEnclosingCharacters(
            this string value,
            char[] separator,
            string delimiter)
        {
            return SplitWithEnclosingCharactersInternal(value, separator, delimiter, delimiter, int.MaxValue, StringSplitOptions.None);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this instance that are delimited
        /// by elements of a specified Unicode character array, but considering everything within
        /// a specified pair of enclosing characters to be a single subtring. Parameters specify the
        /// maximum number of substrings to return and whether to return empty array elements.
        /// </summary>
        /// <param name="value">The given String instance.</param>
        /// <param name="separator">An array of Unicode characters that delimit the substrings
        /// in this instance, an empty array that contains no delimiters, or null.</param>
        /// <param name="delimiter">The opening and closing string.</param>
        /// <param name="options">System.StringSplitOptions.RemoveEmptyEntries to omit empty array
        /// elements from the array returned; or System.StringSplitOptions.None to include empty
        /// array elements in the array returned.</param>
        public static IEnumerable<string> SplitWithEnclosingCharacters(
            this string value,
            char[] separator,
            string delimiter,
            StringSplitOptions options)
        {
            return SplitWithEnclosingCharactersInternal(value, separator, delimiter, delimiter, int.MaxValue, options);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this instance that are delimited
        /// by elements of a specified Unicode character array, but considering everything within
        /// a specified pair of enclosing characters to be a single subtring. Parameters specify the
        /// maximum number of substrings to return and whether to return empty array elements.
        /// </summary>
        /// <param name="value">The given String instance.</param>
        /// <param name="separator">An array of Unicode characters that delimit the substrings
        /// in this instance, an empty array that contains no delimiters, or null.</param>
        /// <param name="delimiter">The opening and closing string.</param>
        /// <param name="count">The maximum number of substrings to return.</param>
        public static IEnumerable<string> SplitWithEnclosingCharacters(
            this string value,
            char[] separator,
            string delimiter,
            int count)
        {
            return SplitWithEnclosingCharactersInternal(value, separator, delimiter, delimiter, count, StringSplitOptions.None);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this instance that are delimited
        /// by elements of a specified Unicode character array, but considering everything within
        /// a specified pair of enclosing characters to be a single subtring. Parameters specify the
        /// maximum number of substrings to return and whether to return empty array elements.
        /// </summary>
        /// <param name="value">The given String instance.</param>
        /// <param name="separator">An array of Unicode characters that delimit the substrings
        /// in this instance, an empty array that contains no delimiters, or null.</param>
        /// <param name="delimiter">The opening and closing string.</param>
        /// <param name="count">The maximum number of substrings to return.</param>
        /// <param name="options">System.StringSplitOptions.RemoveEmptyEntries to omit empty array
        /// elements from the array returned; or System.StringSplitOptions.None to include empty
        /// array elements in the array returned.</param>
        public static IEnumerable<string> SplitWithEnclosingCharacters(
            this string value,
            char[] separator,
            string delimiter,
            int count,
            StringSplitOptions options)
        {
            return SplitWithEnclosingCharactersInternal(value, separator, delimiter, delimiter, count, options);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this instance that are delimited
        /// by elements of a specified Unicode character array, but considering everything within
        /// a specified pair of enclosing characters to be a single subtring. Parameters specify the
        /// maximum number of substrings to return and whether to return empty array elements.
        /// </summary>
        /// <param name="value">The given String instance.</param>
        /// <param name="separator">An array of Unicode characters that delimit the substrings
        /// in this instance, an empty array that contains no delimiters, or null.</param>
        /// <param name="openingDelimiter">The opening string.</param>
        /// <param name="closingDelimiter">The closing string.</param>
        public static IEnumerable<string> SplitWithEnclosingCharacters(
            this string value,
            char[] separator,
            string openingDelimiter,
            string closingDelimiter)
        {
            return SplitWithEnclosingCharactersInternal(value, separator, openingDelimiter, closingDelimiter, int.MaxValue, StringSplitOptions.None);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this instance that are delimited
        /// by elements of a specified Unicode character array, but considering everything within
        /// a specified pair of enclosing characters to be a single subtring. Parameters specify the
        /// maximum number of substrings to return and whether to return empty array elements.
        /// </summary>
        /// <param name="value">The given String instance.</param>
        /// <param name="separator">An array of Unicode characters that delimit the substrings
        /// in this instance, an empty array that contains no delimiters, or null.</param>
        /// <param name="openingDelimiter">The opening string.</param>
        /// <param name="closingDelimiter">The closing string.</param>
        /// <param name="options">System.StringSplitOptions.RemoveEmptyEntries to omit empty array
        /// elements from the array returned; or System.StringSplitOptions.None to include empty
        /// array elements in the array returned.</param>
        public static IEnumerable<string> SplitWithEnclosingCharacters(
            this string value,
            char[] separator,
            string openingDelimiter,
            string closingDelimiter,
            StringSplitOptions options)
        {
            return SplitWithEnclosingCharactersInternal(value, separator, openingDelimiter, closingDelimiter, int.MaxValue, options);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this instance that are delimited
        /// by elements of a specified Unicode character array, but considering everything within
        /// a specified pair of enclosing characters to be a single subtring. Parameters specify the
        /// maximum number of substrings to return and whether to return empty array elements.
        /// </summary>
        /// <param name="value">The given String instance.</param>
        /// <param name="separator">An array of Unicode characters that delimit the substrings
        /// in this instance, an empty array that contains no delimiters, or null.</param>
        /// <param name="openingDelimiter">The opening string.</param>
        /// <param name="closingDelimiter">The closing string.</param>
        /// <param name="count">The maximum number of substrings to return.</param>
        public static IEnumerable<string> SplitWithEnclosingCharacters(
            this string value,
            char[] separator,
            string openingDelimiter,
            string closingDelimiter,
            int count)
        {
            return SplitWithEnclosingCharactersInternal(value, separator, openingDelimiter, closingDelimiter, count, StringSplitOptions.None);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this instance that are delimited
        /// by elements of a specified Unicode character array, but considering everything within
        /// a specified pair of enclosing characters to be a single subtring. Parameters specify the
        /// maximum number of substrings to return and whether to return empty array elements.
        /// </summary>
        /// <param name="value">The given String instance.</param>
        /// <param name="separator">An array of Unicode characters that delimit the substrings
        /// in this instance, an empty array that contains no delimiters, or null.</param>
        /// <param name="openingDelimiter">The opening string.</param>
        /// <param name="closingDelimiter">The closing string.</param>
        /// <param name="count">The maximum number of substrings to return.</param>
        /// <param name="options">System.StringSplitOptions.RemoveEmptyEntries to omit empty array
        /// elements from the array returned; or System.StringSplitOptions.None to include empty
        /// array elements in the array returned.</param>
        public static IEnumerable<string> SplitWithEnclosingCharacters(
            this string value,
            char[] separator,
            string openingDelimiter,
            string closingDelimiter,
            int count,
            StringSplitOptions options)
        {
            return SplitWithEnclosingCharactersInternal(value, separator, openingDelimiter, closingDelimiter, count, options);
        }

        internal static IEnumerable<string> SplitWithEnclosingCharactersInternal(
            string value,
            char[] separator,
            string openingDelimiter,
            string closingDelimiter,
            int count,
            StringSplitOptions options)
        {
            string delimiterSplitRegexString = Regex.Escape(openingDelimiter) + "(.*?)" + Regex.Escape(closingDelimiter);
            var delimitedWords = Regex.Split(value, delimiterSplitRegexString, RegexOptions.Singleline);
            
            var words = new List<string>();

            var isInDelimiters = false;

            foreach (var word in delimitedWords)
            {
                if (!isInDelimiters)
                {
                    var splitWords = word.Split(separator);
                    words.AddRange(splitWords);
                }
                else
                {
                    words.Add(word);
                }

                isInDelimiters = !isInDelimiters;
            }

            IEnumerable<string> wordsAfterOptionHandling;

            if (options == StringSplitOptions.RemoveEmptyEntries)
            {
                wordsAfterOptionHandling = words.Where((w) => !string.IsNullOrEmpty(w));
            }
            else
            {
                wordsAfterOptionHandling = words;
            }

            if (count >= wordsAfterOptionHandling.Count())
            {
                return wordsAfterOptionHandling;
            }
            else
            {
                var finalWords = wordsAfterOptionHandling.Take(count - 1).ToList();
                finalWords.Add(string.Join(" ", wordsAfterOptionHandling.Skip(count - 1)));
                return finalWords;
            }

        }
    }
}
