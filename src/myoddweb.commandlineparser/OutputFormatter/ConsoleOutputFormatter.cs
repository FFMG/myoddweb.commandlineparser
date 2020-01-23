using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using myoddweb.commandlineparser.Interfaces;
using myoddweb.commandlineparser.Rules;

namespace myoddweb.commandlineparser.OutputFormatter
{
  public class ConsoleOutputFormatter : IRulesOutputFormatter
  { 
    /// <summary>
    /// The rules we want to dispay
    /// </summary>
    private readonly IReadOnlyCommandlineArgumentRules _rules;

    /// <summary>
    /// The calculated max key length
    /// </summary>
    private int? _maxKeyLength;

    /// <summary>
    /// The command line leading pattern
    /// </summary>
    private readonly string _leadingPattern;

    /// <summary>
    /// Find the longest flattened key.
    /// </summary>
    /// <returns></returns>
    private int MaxKeyLength
    {
      get
      {
        if (_maxKeyLength.HasValue)
        {
          return _maxKeyLength.Value;
        }

        _maxKeyLength = _rules.Select(rule => FlattenKeys(rule.Keys, false)).Select(fullKey => fullKey.Length)
          .Concat(new[] { 0 }).Max();
        return _maxKeyLength.Value;
      }
    }

    public ConsoleOutputFormatter(ICommandlineParser parser) :
      this( parser.Rules, parser.LeadingPattern )
    {
    }

    public ConsoleOutputFormatter(IReadOnlyCommandlineArgumentRules rules, string leadingPattern)
    {
      _rules = rules ?? throw new ArgumentNullException( nameof(rules));
      _leadingPattern = leadingPattern ?? throw new ArgumentNullException(nameof(leadingPattern));
    }

    /// <inheritdoc />
    public void Write()
    {
      WriteUsage();
      Console.WriteLine();

      // find the max key len
      var lenWithPadding = MaxKeyLength + 2;

      // then we can output everything
      foreach (var rule in _rules)
      {
        // build the string
        Console.WriteLine($"{FlattenKeys(rule.Keys, false ).PadRight(lenWithPadding)}:{rule.Description}");
      }
    }

    /// <summary>
    /// Output the help usage.
    /// </summary>
    private void WriteUsage()
    {
      // the app name.
      var fileName = Process.GetCurrentProcess().MainModule?.ModuleName ?? "<app>";
      var usage = $"Usage: {fileName}";
      Console.WriteLine(usage);
      var usageLenWithPadding = usage.Length + 2;
      foreach (var rule in _rules.Where(r => r is RequiredCommandlineArgumentRule))
      {
        WriteUsageRule(rule, usageLenWithPadding);
      }
      foreach (var rule in _rules.Where( r => r is OptionalCommandlineArgumentRule ))
      {
        WriteUsageRule(rule, usageLenWithPadding);
      }
      foreach (var rule in _rules.Where(r => r is HelpCommandlineArgumentRule))
      {
        WriteUsageRule(rule, usageLenWithPadding);
      }
    }

    private void WriteUsageRule(ICommandlineArgumentRule rule, in int usageLenWithPadding)
    {
      // build the string
      var line = FlattenKeys(rule.Keys, true);
      if (rule.DefaultValue != null)
      {
        line = $"{line}=<{rule.DefaultValue}>";
      }
      if (!rule.IsRequired)
      {
        line = $"[{line}]";
      }

      line = (new string(' ', usageLenWithPadding)) + line;
      Console.WriteLine(line);
    }

    /// <summary>
    /// Flaten all the keys into a single string
    /// For example, []{ "h", "help"} would become "h, help"
    /// </summary>
    /// <param name="ruleKeys"></param>
    /// <param name="addLeadingPattern"></param>
    /// <returns></returns>
    private string FlattenKeys(ICollection<string> ruleKeys, bool addLeadingPattern )
    {
      var subKeys = new List<string>( ruleKeys.Count);
      subKeys.AddRange(ruleKeys.Select(ruleKey => addLeadingPattern ? $"{_leadingPattern}{ruleKey}" : ruleKey));
      return string.Join( ", ", subKeys);
    }
  }
}
