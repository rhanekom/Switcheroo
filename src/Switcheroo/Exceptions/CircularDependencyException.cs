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

namespace Switcheroo.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Indicates that a toggle has a circular dependecy and will result in an error condition if evaluated.
    /// </summary>
    [Serializable]
    public class CircularDependencyException : InvalidConfigurationException 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircularDependencyException" /> class.
        /// </summary>
        public CircularDependencyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularDependencyException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CircularDependencyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularDependencyException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public CircularDependencyException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularDependencyException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected CircularDependencyException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }    
}
