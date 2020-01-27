# Change Log

Notable changes

## 0.1.5 - 27-01-2020

### Added

### Changed

- The command line leading pattern cannot be null.

### Fixed

- Given values with aliases were not being parsed properly.
- `RequiredCommandlineArgumentRule` with a single key did not accept a description.

  ```csharp
    ...
    var helpRule = new RequiredCommandlineArgumentRule( "h", "This is the required command" );
    ...
  ```

- `HelpCommandlineArgumentRule` with a single key did not accept a description.

  ```csharp
    ...
    var helpRule = new HelpCommandlineArgumentRule( "h", "This is the help command" );
    ...
  ```

### Removed

## 0.1.4 - 20-01-2020

### Added

- Change log file :)
- Support for `.NET Core 3`
- Support for `.NET Framework 4.6.2`
- CommandLine Rule
  - `RequiredCommandlineArgumentRule` for required rules.
  - `OptionalCommandlineArgumentRule` for optional rules.
  - `HelpCommandlineArgumentRule` for help rules.
- You can now have aliases
  
```csharp
  ...
  var arguments = new CommandlineParser(args, new CommandlineArgumentRules
    {
      new HelpCommandlineArgumentRule( new []{"help", "h"} ) }
    });
  ...
```

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
      new HelpCommandlineArgumentRule( new []{"help", "h"} ) },
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
