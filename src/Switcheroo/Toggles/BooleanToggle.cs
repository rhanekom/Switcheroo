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

namespace Switcheroo.Toggles
{
    /// <summary>
    /// A simple bit feature toggle that switches values between <c>true</c> and <c>false</c>.
    /// </summary>
    public class BooleanToggle : StaticFeatureToggleBase
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToggle" /> class.
        /// </summary>
        /// <param name="name">The name of the feature toggle.</param>
        /// <param name="enabled">if set to <c>true</c> enable the feature, else disable it.</param>
        /// <exception cref="System.ArgumentNullException">If name is <c>null</c>.</exception>
        public BooleanToggle(string name, bool enabled) : base(name, enabled)
        {
        }

        #endregion
    }
}
