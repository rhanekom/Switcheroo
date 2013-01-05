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

using Moq;

namespace Switcheroo.Tests.Toggles
{
    using System.Linq;
    using Exceptions;
    using NUnit.Framework;
    using Switcheroo.Toggles;

    [TestFixture]
    public class DependencyToggleTests
    {
        #region Globals

        private const string ToggleName = "dsfsdfds";

        #endregion
 
        #region Tests

        [Test]
        public void Construction_Saves_Name()
        {
            var toggle = new DependencyToggle(new BooleanToggle(ToggleName, true), new IFeatureToggle[0]);
            Assert.AreEqual(ToggleName, toggle.Name);
        }

        [Test]
        public void Construction_Saves_Dependencies()
        {
            var toggles = new IFeatureToggle[] { new BooleanToggle("a", true), new BooleanToggle("b", true) };
            
            var toggle = new DependencyToggle(new BooleanToggle(ToggleName, true), toggles);
            CollectionAssert.AreEquivalent(toggles, toggle.Dependencies);
        }

        [Test]
        public void Evaluate_Returns_False_If_Disabled()
        {
            var toggle = new DependencyToggle(new BooleanToggle(ToggleName, false), new IFeatureToggle[0]);
            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Evaluate_Returns_True_If_Enabled_With_No_Dependencies()
        {
            var toggle = new DependencyToggle(new BooleanToggle(ToggleName, true), new IFeatureToggle[0]);
            Assert.IsTrue(toggle.IsEnabled());
        }

        [Test]
        public void Evaluate_Returns_False_If_Dependencies_Disabled()
        {
            var toggles = new IFeatureToggle[] { new BooleanToggle("a", true), new BooleanToggle("b", false) };
            
            var toggle = new DependencyToggle(new BooleanToggle(ToggleName, true), toggles);
            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Evaluate_Returns_True_If_Dependencies_Not_Disabled()
        {
            var toggles = new IFeatureToggle[] { new BooleanToggle("a", true), new BooleanToggle("b", true) };
            
            var toggle = new DependencyToggle(new BooleanToggle(ToggleName, true), toggles);
            Assert.IsTrue(toggle.IsEnabled());
        }

        [Test]
        public void Can_Add_Dependencies_To_Toggle()
        {
            var toggle = new DependencyToggle(new BooleanToggle(ToggleName, true));

            Assert.IsFalse(toggle.Dependencies.Any());

            var dependency = new BooleanToggle("a", true);
            toggle.AddDependency(dependency);

            Assert.IsTrue(toggle.Dependencies.Any());
            Assert.AreSame(toggle.Dependencies.Single(), dependency);
        }

        [Test]
        public void AddDependency_Throws_If_Object_Is_Frozen()
        {
            var toggle = new DependencyToggle(new BooleanToggle(ToggleName, true));
            toggle.Freeze();
            
            var dependency = new BooleanToggle("a", true);
            Assert.Throws<ToggleFrozenException>(() => toggle.AddDependency(dependency));
        }

        [Test]
        public void AssertConfigurationIsValid_Throws_For_CircularDependency()
        {
            var toggle1 = new DependencyToggle(new BooleanToggle(ToggleName, true));
            var toggle2 = new DependencyToggle(new BooleanToggle(ToggleName, true));
            toggle1.AddDependency(toggle2);
            toggle2.AddDependency(toggle1);

            Assert.Throws<CircularDependencyException>(toggle1.AssertConfigurationIsValid);
        }

        [Test]
        public void AssertConfigurationIsValid_Throws_For_Second_Level_Circular_Dependency()
        {
            var toggle1 = new DependencyToggle(new BooleanToggle(ToggleName, true));
            var toggle2 = new DependencyToggle(new BooleanToggle(ToggleName, true));
            var toggle3 = new DependencyToggle(new BooleanToggle(ToggleName, true));
            
            toggle1.AddDependency(toggle2);
            toggle2.AddDependency(toggle3);
            toggle3.AddDependency(toggle1);

            Assert.Throws<CircularDependencyException>(toggle1.AssertConfigurationIsValid);
        }

        [Test]
        public void AssertConfigurationIsValid_Returns_False_For_Non_Circular_Dpendency()
        {
            var toggle1 = new DependencyToggle(new BooleanToggle(ToggleName, true));
            var toggle2 = new DependencyToggle(new BooleanToggle(ToggleName, true));
            var toggle3 = new DependencyToggle(new BooleanToggle(ToggleName, true));
            var toggle4 = new BooleanToggle(ToggleName, true);
 
            toggle1.AddDependency(toggle2);
            toggle2.AddDependency(toggle3);
            toggle2.AddDependency(toggle4);
            toggle3.AddDependency(toggle4);

            Assert.DoesNotThrow(toggle1.AssertConfigurationIsValid);
        }

        [Test]
        public void Toggle_Is_Unfrozen_By_Default()
        {
            var toggle1 = new DependencyToggle(new BooleanToggle(ToggleName, true));
            Assert.IsFalse(toggle1.IsFrozen);
        }

        [Test]
        public void Freeze_Freezes_Toggle()
        {
            var toggle1 = new DependencyToggle(new BooleanToggle(ToggleName, true));
            toggle1.Freeze();
            Assert.IsTrue(toggle1.IsFrozen);
        }

        [Test]
        public void Freeze_Freezes_Dependencies()
        {
            var toggle1 = new DependencyToggle(new BooleanToggle(ToggleName, true));
            var dependency1 = new Mock<IFeatureToggle>();
            var dependency2 = new Mock<IFeatureToggle>();

            toggle1.AddDependency(dependency1.Object);
            toggle1.AddDependency(dependency2.Object);

            dependency1.Setup(x => x.Freeze());
            dependency2.Setup(x => x.Freeze());

            toggle1.Freeze();

            Assert.IsTrue(toggle1.IsFrozen);
            dependency1.VerifyAll();
            dependency2.VerifyAll();
        }

        #endregion
    }
}