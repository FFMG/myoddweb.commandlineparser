//This file is part of Myoddweb.CommandlineParser.
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the "Software"), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//    SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;

namespace myoddweb.commandlineparser
{
  public class CommandlineArgumentRule : ICommandlineArgumentRule
  {
    /// <inheritdoc />
    public bool IsRequired { get; }

    /// <inheritdoc />
    public string DefaultValue { get; }

    /// <inheritdoc />
    public IList<string> Keys { get;  }

    protected CommandlineArgumentRule(string key, bool isRequired = false, string defaultValue = null)
    {
      Keys = new List<string> {ValidKey(key)};
      IsRequired = isRequired;
      DefaultValue = defaultValue;
    }

    /// <summary>
    /// Constructor when we have a default value.
    /// As we have a default value, the argument is not required.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    protected CommandlineArgumentRule(string key, string defaultValue ) :
      this( key, false, defaultValue )
    {
      if (null == defaultValue)
      {
        throw new ArgumentNullException( nameof(defaultValue), "You cannot pass a null argument as a default value.");
      }
    }

    /// <summary>
    /// Make sure that the given key is trimmed, lower case, not null and not empty.
    /// </summary>
    /// <param name="given"></param>
    /// <returns></returns>
    public static string ValidKey(string given)
    {
      var key = given ?? throw new ArgumentNullException(nameof(given));
      key = key.ToLower().Trim();
      return key.Length > 0 ? key : throw new ArgumentException(given);
    }

    public bool IsKeyOrAlias(string given)
    {
      // clean the key
      var cleanGiven = ValidKey(given);
      
      // do any of the keys matches?
      return Keys.Any(a => a == cleanGiven);
    }
  }
}
