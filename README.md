# Myoddweb.CommandlineParser [![Release](https://img.shields.io/badge/release-v0.1.6-brightgreen.png?style=flat)](https://github.com/FFMG/myoddweb.commandlineparser/)
A very simple c# command line arguments Parser

## What it does

- Very simple command line parser for c#
- Simple to import/install
- Simple to use
- You can create optional parameters
- You can create help output.

## Installing
### Nuget

[![NuGet Status](https://img.shields.io/nuget/v/MyOddWeb.CommandlineParser.svg)](https://www.nuget.org/packages/MyOddWeb.CommandlineParser/)
[![NuGet Count](https://img.shields.io/nuget/dt/MyOddWeb.CommandlineParser.svg)](https://www.nuget.org/packages/MyOddWeb.CommandlineParser/)

#### Package manager

`Install-Package MyOddWeb.CommandlineParser`

#### CLI

##### .NET

`dotnet add package MyOddWeb.CommandlineParser`

#### Packet

`packet add MyOddWeb.CommandlineParser`

## Example

Lets say that we have a application that has command line options to `install` and run as a `console`

```csharp
  using myoddweb.commandlineparser;

  private void Bar()
  {
    ...
    var arguments = new CommandlineParser(args, new CommandlineArgumentRules
      {
        new HelpCommandlineArgumentRule( new []{"help", "h"} ) },
        new OptionalCommandlineArgumentRule( "config",  "config.json" ) },
        new OptionalCommandlineArgumentRule( "install" ) },
        new OptionalCommandlineArgumentRule( "console" ) },
        new RequiredCommandlineArgumentRule( "name" )
      });
    ...
  }
```

We can then check if we are running as a console

```csharp
  ...
  if (arguments.IsSet("console"))
  {
    InvokeActionInstall();
    return;
  }
  ...
```

We can also call the config value as we know it exists... (we have a default value)

```csharp
  ...
  var config = _arguments["config"];
  ...
```

We can also get a default value directly

For example, in the example below, we will either get the number that was passed,  (`example.exe --a 32`), or the default value.

The value itself will be an integer, as this is what we expect... 

```csharp
  ...
  var theGalaxy = parser.Get<int>("a", 42)
  ...
```

The argument does not have to have a value, for example you could have `example.exe --a --b`

In that case you would simply default

```csharp
  ...
  var arguments = new CommandlineParser(args, null );
  ...
```

What if you want a different leading pattern ...? Something like `example.exe -a -b`

Simply pass it as a third argument ...

```csharp
  ...
  var arguments = new CommandlineParser(args, null, "-" );
  ...
```

## Strong Name

### Public key

00240000048000009400000006020000002400005253413100040000010001000140936ffbd158
39e58c0163725bf693f5cc01fe4cab7e06ecace3340ff8e98cd4fcbb735a9d75ce1d2a1d3764cc
bd5f6099d4c149cc369ccd2bdefd044955ab67585d7aecc64c3d0bd001b250911ce8b5361cba15
e3c16d99981a1d1a2ba33355954c9af6adb2923dcc56954bc8b1ec3fa8c6ba36a5ed20ff1d1167
882ce6b5

### Public key token

7ce229023134231c
