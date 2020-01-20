namespace myoddweb.commandlineparser.Interfaces
{
  public interface IRulesOutputFormatter
  {
    /// <summary>
    /// Output the rules.
    /// </summary>
    /// <param name="rules"></param>
    void Write(IReadOnlyCommandlineArgumentRules rules);
  }
}
