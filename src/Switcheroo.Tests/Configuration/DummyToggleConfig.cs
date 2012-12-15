namespace Switcheroo.Tests.Configuration
{
    using Switcheroo.Configuration;

    public class DummyToggleConfig : IFeatureToggleConfiguration
    {
        public FeatureToggleCollection Toggles { get; set; }
    }
}
