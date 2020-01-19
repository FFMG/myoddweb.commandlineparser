using System.Collections.Generic;

namespace myoddweb.commandlineparser
{
  public class RequiredCommandlineArgumentRule : CommandlineArgumentRule
  {
    public RequiredCommandlineArgumentRule(string key) :
      this( new []{key})
    {
    }

    public RequiredCommandlineArgumentRule(IEnumerable<string> keys) :
      base(keys, true, null)
    {
    }
  }
}
