// Pretzel.Image plugin
using System.Collections.Generic;
using System.Linq;

namespace Pretzel.Image
{
    /// <summary>
    /// Extensions method for the string class.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Splits a string depending on a delimiter and in respecting strings enclosed in quotes.
        /// </summary>
        /// <param name="s">The string to split.</param>
        /// <param name="delimiters">The delimiters to use.</param>
        /// <returns>The split string as an <see cref="IList{String}"/></returns>
        internal static IList<string> SplitEnclosedInQuotes(this string s, string[] delimiters)
        {
            var allLineFields = new List<string>();
            using (var reader = new System.IO.StringReader(s))
            {
                using (var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(reader))
                {
                    parser.Delimiters = delimiters;
                    parser.HasFieldsEnclosedInQuotes = true;
                    string[] fields;
                    while ((fields = parser.ReadFields()) != null)
                    {
                        allLineFields = fields.ToList();
                    }
                }
            }

            return allLineFields;
        }
    }
}