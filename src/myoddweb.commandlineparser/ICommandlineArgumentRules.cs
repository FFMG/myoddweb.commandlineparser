using System.Collections.Generic;

namespace myoddweb.commandlineparser
{
  /// <summary>
  /// List of rules
  /// </summary>
  public interface ICommandlineArgumentRules : IEnumerable<ICommandlineArgumentRule>
  {
    /// <summary>
    /// Add the rule to our list of rules.
    /// </summary>
    /// <param name="rule"></param>
    void Add(ICommandlineArgumentRule rule);
  }
}
