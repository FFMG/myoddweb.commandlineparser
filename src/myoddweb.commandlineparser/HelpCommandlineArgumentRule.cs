namespace myoddweb.commandlineparser
{
  public class HelpCommandlineArgumentRule : CommandlineArgumentRule, IHelpCommandlineArgumentRule
  {
    public HelpCommandlineArgumentRule(string key) :
      base(key, false, null)
    {
    }
  }
}
