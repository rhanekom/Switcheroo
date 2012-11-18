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
                        DateTime.Now.AddDays(3))
                };

            Console.WriteLine(features.WhatDoIHave());
        }
    }
}
