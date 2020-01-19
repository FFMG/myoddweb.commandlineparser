using System.Collections.Generic;

namespace myoddweb.commandlineparser
{
  public class HelpCommandlineArgumentRule : CommandlineArgumentRule, IHelpCommandlineArgumentRule
  {
    public HelpCommandlineArgumentRule(string key) :
      this( new []{key} )
    {
    }

    public HelpCommandlineArgumentRule(IEnumerable<string> keys) :
      base(keys, false, null)
    {
    }
  }
}
