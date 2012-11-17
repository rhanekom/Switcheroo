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

Features  
---------
* Loading from custom configuration resources.
* Code friendly initialization without a dependency on application configuration.
* IOC friendly through IFeatureConfiguration instances, or Feature.Instance backing instance.
* Feature toggle diagnostics (WhatDoIHave)

Toggle types
--------------
* Boolean (true/false, immutable)
* Mutable (true/false, mutable)
