using System;
using System.Collections.Generic;

namespace myoddweb.commandlineparser
{
  public class OptionalCommandlineArgumentRule : CommandlineArgumentRule
  {
    public OptionalCommandlineArgumentRule(string key) :
      this( new []{key})
    {
    }

    public OptionalCommandlineArgumentRule(string key, string defaultValue ) :
      this( new []{key}, defaultValue)
    {
      if (null == defaultValue)
      {
        throw new ArgumentNullException(nameof(defaultValue), "You cannot pass a null argument as a default value.");
      }
    }

    public OptionalCommandlineArgumentRule(IEnumerable<string> keys) :
      base( keys, false, null)
    {
    }

    public OptionalCommandlineArgumentRule(IEnumerable<string> keys, string defaultValue) :
      base( keys, false, defaultValue)
    {
      if (null == defaultValue)
      {
        throw new ArgumentNullException(nameof(defaultValue), "You cannot pass a null argument as a default value.");
      }
    }
  }
}
