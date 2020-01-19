namespace myoddweb.commandlineparser
{
  public class OptionalCommandlineArgumentRule : CommandlineArgumentRule
  {
    public OptionalCommandlineArgumentRule(string key) :
      base(key, false, null)
    {
    }

    public OptionalCommandlineArgumentRule(string key, string defaultValue ) :
      base(key, defaultValue)
    {
    }
  }
}
