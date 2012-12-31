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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Collections;

    /// <summary>
    /// A toggle that has other feature toggles as dependencies.
    /// </summary>
    public class DependencyToggle : FeatureToggleBase, IDependencyFeatureToggle
    {
        #region Globals

        private readonly IFeatureToggle innerToggle;
        private readonly ConcurrentBag<IFeatureToggle> dependencies;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyToggle" /> class.
        /// </summary>
        /// <param name="innerToggle">The inner toggle for evaluation of this toggle.</param>
        public DependencyToggle(IFeatureToggle innerToggle) : this(innerToggle, new IFeatureToggle[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyToggle" /> class.
        /// </summary>
        /// <param name="innerToggle">The inner toggle for evaluation of this toggle.</param>
        /// <param name="dependencies">The feature toggles that this toggle depends on.</param>
        public DependencyToggle(IFeatureToggle innerToggle, params IFeatureToggle[] dependencies) 
            : base(innerToggle.Name)
        {
            this.innerToggle = innerToggle;
            this.dependencies = new ConcurrentBag<IFeatureToggle>(dependencies);
        }

        #endregion

        #region DynamicFeatureToggleBase Members

        /// <summary>
        /// Evaluates the dynamic rules for this instance to determine whether it is enabled.
        /// </summary>
        /// <returns>
        /// An indication of whether this feature toggle instance is enabled.
        /// </returns>
        public override bool IsEnabled()
        {
            return (innerToggle == null || innerToggle.IsEnabled()) && dependencies.All(x => x.IsEnabled());
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Adds the specified feature toggle as a dependency to this one.
        /// </summary>
        /// <param name="toggle">The toggle to add as a dependency.</param>
        public void AddDependency(IFeatureToggle toggle)
        {
            dependencies.Add(toggle);
        }

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

        /*
         ← Set of all nodes with no incoming edges
        for each node n in S do
            visit(n) 
        function visit(node n)
            if n has not been visited yet then
                mark n as visited
                for each node m with an edge from n to m do
                    visit(m)
                add n to L
            }
         */

        /// <summary>
        /// Determines whether this instance has cyclic dependencies.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has cyclic dependencies; otherwise, <c>false</c>.
        /// </returns>
        public bool HasCycle()
        {
            return HasCycle(null);
        }

        #endregion

        #region Private Members

        private bool HasCycle(PersistentList<IFeatureToggle> visitedToggles)
        {
            visitedToggles = visitedToggles == null 
                ? new PersistentList<IFeatureToggle>(this, Enumerable.Empty<IFeatureToggle>()) 
                : new PersistentList<IFeatureToggle>(this, visitedToggles);

            foreach (var toggle in dependencies)
            {
                // Verify that this node has not been visited before
                if (visitedToggles.Contains(toggle))
                {
                    return true;
                }

                var dependencyToggle = toggle as DependencyToggle;

                if ((dependencyToggle != null) && dependencyToggle.HasCycle(visitedToggles))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
