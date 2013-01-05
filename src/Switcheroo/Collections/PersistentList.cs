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

namespace Switcheroo.Collections
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A persistent data structure in the form of a list.
    /// </summary>
    /// <typeparam name="T">The type of item in the list.</typeparam>
    public class PersistentList<T> : IEnumerable<T>
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistentList{T}" /> class.
        /// </summary>
        /// <param name="head">The head of the list.</param>
        /// <param name="tail">The tail of the list.</param>
        public PersistentList(T head, IEnumerable<T> tail)
        {
            Head = head;

            if (tail == null)
            {
                throw new ArgumentNullException("tail");
            }

            Tail = tail;
        }

        #endregion
        
        #region Public Members

        /// <summary>
        /// Gets the head of the list.
        /// </summary>
        /// <value>
        /// The head of the list.
        /// </value>
        public T Head { get; private set; }

        /// <summary>
        /// Gets the tail of the list.
        /// </summary>
        /// <value>
        /// The tail of the list.
        /// </value>
        public IEnumerable<T> Tail { get; private set; } 

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            yield return Head;

            foreach (var item in Tail)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
