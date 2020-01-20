using System;
using System.Collections.Generic;
using System.Linq;
using myoddweb.commandlineparser.Interfaces;

namespace myoddweb.commandlineparser.OutputFormatter
{
  public class ConsoleOutputFormatter : IRulesOutputFormatter
  {
    /// <inheritdoc />
    public void Write(IReadOnlyCommandlineArgumentRules rules)
    {
      // find the max key len
      var len = FindMaxKeyLength(rules);
      var lenWithPadding = len + 2;

      // then we can output everything
      foreach (var rule in rules)
      {
        // build the string
        Console.WriteLine($"{FlattenKeys(rule.Keys).PadRight(lenWithPadding)}:{rule.Description}");
      }
    }

    /// <summary>
    /// Find the longest flattened key.
    /// </summary>
    /// <param name="rules"></param>
    /// <returns></returns>
    private static int FindMaxKeyLength(IReadOnlyCommandlineArgumentRules rules)
    {
      return rules.Select(rule => FlattenKeys(rule.Keys)).Select(fullKey => fullKey.Length).Concat(new[] {0}).Max();
    }

    /// <summary>
    /// Flaten all the keys into a single string
    /// For example, []{ "h", "help"} would become "h, help"
    /// </summary>
    /// <param name="ruleKeys"></param>
    /// <returns></returns>
    private static string FlattenKeys(IEnumerable<string> ruleKeys)
    {
      return string.Join(", ", ruleKeys);
    }
  }
}
