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

**Checking feature status**

```c#
if (Feature.IsEnabled("Log.InColor"))
{
	// Implement feature
}
```


Toggle types
--------------

**Boolean (true/false)**

Feature toggles based on a static binary value - either on or off.

```c#
features.Add(new BooleanToggle("Feature1", true));
```

```xml
<features>
    <toggles>
      <add name="BooleanToggle.Enabled" enabled="true"/>
      <add name="BooleanToggle.Disabled" enabled="false"/>
    </toggles>
 </features>
```

**Date Range (true/false, within date range)**

Date Range feature toggles are evaluated on both the binary enabled value and the current date.

```c#
features.Add(new DateRangeToggle("Feature2", true, DateTime.Now.AddDays(5), null));
```

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
_From_ and _until_ dates can be any valid date format parseable by _DateTime.Parse_.


**Established features**

Marking a feature toggle as established makes the feature toggle throw a _FeatureEstablishedException_ exception to make sure that it is not queried any more.  

```c#
features.Add(new EstablishedFeatureToggle("establishedFeature"));
```

```xml
<features>
    <toggles>
      <add name="EstablishedFeature" established="true"/>
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
```

**IOC friendly through _IFeatureConfiguration_ instances, or the static _Feature.Instance_ backing instance**

```c#
For<IFeatureConfiguration>().Use(Features.Instance);
```

**Feature toggle diagnostics : _IFeatureConfiguration.WhatDoIHave_**

```c#
Console.WriteLine(features.WhatDoIHave());
```
```text
Name          Feature1
IsEnabled     True


Name          Feature2
IsEnabled     True
From          11/16/2012 3:32:23 PM
Until         11/21/2012 3:32:23 PM
```

**Loading from custom configuration resources :  build on top of _IConfigurationReader_**
