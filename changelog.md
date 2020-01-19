# Change Log

Notable changes

## 0.1.4 - xx-yy-2020

### Added

- Change log file :)
- Support for `.NET Core 3`
- Support for `.NET Framework 4.6.2`

### Changed

- Replaced `CommandlineData` with `CommandlineArgumentRule`

```csharp
  ...
  var arguments = new CommandlineParser(args, new Dictionary<string, CommandlineData>
    {
      { "config", new CommandlineData{ IsRequired = false, DefaultValue = "config.json"}},
      { "install", new CommandlineData{ IsRequired = false} },
      { "console", new CommandlineData{ IsRequired = false} }
    });
  ...
```

Changed to

```csharp
  ...
  var arguments = new CommandlineParser(args, new CommandlineArgumentRules
    {
      new CommandlineArgumentRule( "config",  false, "config.json" ) },
      new CommandlineArgumentRule( "install", false ) },
      new CommandlineArgumentRule( "console", false ) }
    });
  ...
```

### Fixed

### Removed

- Support for `netstandard1.3`

## Initial release

### Added

### Changed

### Fixed

### Removed
