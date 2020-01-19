namespace myoddweb.commandlineparser
{
  public class RequiredCommandlineArgumentRule : CommandlineArgumentRule
  {
    public RequiredCommandlineArgumentRule(string key) :
      base(key, true, null)
    {
    }
  }
}
