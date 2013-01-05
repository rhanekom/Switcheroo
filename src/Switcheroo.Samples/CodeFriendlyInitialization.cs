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

namespace Switcheroo.Samples
{
    using System;
    using Toggles;

    public class CodeFriendlyInitialization
    {
        public void DoIt()
        {
            IFeatureConfiguration features = new FeatureConfiguration
                {
                    new BooleanToggle("Feature1", true),
                    new DateRangeToggle(
                        "Feature2",
                        true,
                        DateTime.Now.AddDays(-2),
                        DateTime.Now.AddDays(3)),
                    new EstablishedFeatureToggle("establishedFeature")
                };

            var mainFeature = new BooleanToggle("mainFeature", true);
            var subFeature1 = new BooleanToggle("subFeature1", true);
            var subFeature2 = new BooleanToggle("subFeature2", true);

            var dependency1 = new DependencyToggle(subFeature1, mainFeature);
            var dependency2 = new DependencyToggle(subFeature2, mainFeature);
            features.Add(dependency1);
            features.Add(dependency2);

            features.Add(new EstablishedFeatureToggle("establishedFeature"));

            Console.WriteLine(features.WhatDoIHave());
        }
    }
}
