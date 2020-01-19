# Change Log

Notable changes

## 0.1.4 - xx-yy-2020

### Added

- Change log file :)
- Support for `.NET Core 3`
- Support for `.NET Framework 4.6.2`
- CommandLine Rule
  - `RequiredCommandlineArgumentRule` for required rules.
  - `OptionalCommandlineArgumentRule` for optional rules.
  - `HelpCommandlineArgumentRule` for help rules.

### Changed

- Replaced `CommandlineData` with `CommandlineArgumentRule`, (required or optional)

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
      new OptionalCommandlineArgumentRule( "config", "config.json" ) },
      new OptionalCommandlineArgumentRule( "install" ) },
      new OptionalCommandlineArgumentRule( "console" ) },
      new RequiredCommandlineArgumentRule( "name" )
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
