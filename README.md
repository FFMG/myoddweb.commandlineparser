# Myoddweb.CommandlineParser [![Release](https://img.shields.io/badge/release-v0.1.3-brightgreen.png?style=flat)](https://github.com/FFMG/myoddweb.commandlineparser/)
A very simple c# command line arguments Parser

## What it does

- Very simple command line parser for c#
- Simple to import/install
- Simple to use
- You can create optional parametters
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
`paket add MyOddWeb.CommandlineParser`

## Example

Lets say that we have a application that has command line options to `install` and run as a `console`

```csharp
  using myoddweb.commandlineparser;

  private void Bar()
  {
    ...
    var arguments = new CommandlineParser(args, new Dictionary<string, CommandlineData>
    {
      { "config", new CommandlineData{ IsRequired = false, DefaultValue = "config.json"}},
      { "install", new CommandlineData{ IsRequired = false} },
      { "console", new CommandlineData{ IsRequired = false} }
    });
    ...
  }
```

We can then check if we are running as a console

```csharp
      if (arguments.IsSet("console"))
      {
        InvokeActionInstall();
        return;
      }
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

In that case you would simply defaule

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
