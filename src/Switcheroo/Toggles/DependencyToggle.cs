/*
The MIT License

Copyright (c) 2012 Riaan Hanekom

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
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A toggle that has other feature toggles as dependencies.
    /// </summary>
    public class DependencyToggle : DynamicFeatureToggleBase, IDependencyFeatureToggle
    {
        #region Globals

        private readonly IFeatureToggle[] dependencies;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyToggle" /> class.
        /// </summary>
        /// <param name="name">The name of this toggle.</param>
        /// <param name="enabled">if set to <c>true</c> enable this toggle.</param>
        /// <param name="dependencies">The feature toggles that this toggle depends on.</param>
        public DependencyToggle(string name, bool enabled, params IFeatureToggle[] dependencies) 
            : base(name, enabled)
        {
            this.dependencies = dependencies;
        }

        #endregion

        #region DynamicFeatureToggleBase

        /// <summary>
        /// Evaluates the dynamic rules for this instance to determine whether it is enabled.
        /// </summary>
        /// <returns>
        /// An indication of whether this feature toggle instance is enabled.
        /// </returns>
        protected override bool Evaluate()
        {
            return dependencies.All(x => x.IsEnabled());
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Gets the dependencies of this feature toggle.
        /// </summary>
        /// <value>
        /// The dependencies of this feature toggle.
        /// </value>
        public IEnumerable<IFeatureToggle> Dependencies
        {
            get { return dependencies; }
        }

        #endregion
    }
}
