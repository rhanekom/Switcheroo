Switcheroo
========

A lightweight framework for [feature toggling](http://martinfowler.com/bliki/FeatureToggle.html) to enable trunk based development.

Why not library X?
-------------------

Switcheroo aims for simplicity, with a clean syntax and a minimal feature set while not compromising on extensibility and testability.

License
--------

Switcheroo is licensed under the [MIT license](http://opensource.org/licenses/MIT).


Quick Start
------------

**Installation**

Nuget packages can be found [here](https://www.nuget.org/packages/Switcheroo).

**Add configuration**

```xml
<configuration>
  <configSections>
    <section name="features" type="Switcheroo.Configuration.FeatureToggleConfiguration, Switcheroo"/>
  </configSections>
  <features>
    <toggles>
      <add name="Log.InColor" enabled="true"/>
    </toggles>
  </features>
</configuration>
```

**Initializing the library**

```c#
Feature.Initialize(x => x.FromApplicationConfig());
```

**Enaling/disabling  features**

```c#
if (Feature.IsEnabled("Log.InColor"))
{
	// Implement feature
}
```

Toggle types
--------------

**Boolean (true/false)**

```xml
<features>
    <toggles>
      <add name="BooleanToggle.Enabled" enabled="true"/>
      <add name="BooleanToggle.Disabled" enabled="false"/>
    </toggles>
 </features>
```

**Date Range (true/false, within date range)**

_From_ and _until_ dates can be any valid date format parseable by _DateTime.Parse_.

```xml
<features>
    <toggles>
      <add name="Date.Enabled.InRange" enabled="true" from="1 January 2010" until="31 December 2050"/>
      <add name="Date.Enabled.Expired" enabled="true" until="31 December 2010"/>
      <add name="Date.Enabled.Future"  enabled="true" from="1 January 2050"/>
      <add name="Date.Disabled" enabled="false"/>
    </toggles>
 </features>
```

Other features  
----------------

**Code-friendly initialization**

```c#
IFeatureConfiguration features = new FeatureConfiguration
	{
		new BooleanToggle("Feature1", true),
		new DateRangeToggle(
			"Feature2",
			true,
			DateTime.Now.AddDays(-2),
			DateTime.Now.AddDays(3))
	};
```c#

**Loading from custom configuration resources :  build on top of _IConfigurationReader_**

**IOC friendly through _IFeatureConfiguration_ instances, or Feature.Instance backing instance**

**Feature toggle diagnostics (_IFeatureConfiguration.WhatDoIHave_)**
