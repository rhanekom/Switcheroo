/*
The MIT License

Copyright (c) 2013 Riaan Hanekom

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

namespace Switcheroo.Extensions
{
    /// <summary>
    /// Extension methods on <see cref="System.String"/>.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Prepares a string for display by ensuring it's within the maximum size.
        /// A string will be truncated if longer than <paramref name="maxLength"/>,
        /// or padded with spaces if shorter and <paramref name="pad"/> is set to <c>true</c>.
        /// </summary>
        /// <param name="str">The string to prepare for display.</param>
        /// <param name="maxLength">The maximum allowed length of a string.</param>
        /// <param name="pad">if set to <c>true</c> then right pad the string with spaces up to the maximum length.</param>
        /// <returns>
        /// A string prepared for display in a tabular format.
        /// </returns>
        public static string PrepareForDisplay(this string str, int maxLength, bool pad)
        {
            str = str ?? string.Empty;
            str = str.Length > maxLength
                ? str.Substring(0, maxLength)
                : pad ? str.PadRight(maxLength, ' ') : str;

            return str;
        }
    }
}
