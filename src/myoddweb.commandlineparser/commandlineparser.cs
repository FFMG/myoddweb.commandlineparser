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
  public class CommandlineParser
  {
    /// <summary>
    /// The patern we want our command line to lead with
    /// For example, "-hello world" the leading pattern is '-', (it can be more than one char).
    /// </summary>
    private readonly string _leadingPattern;

    /// <summary>
    /// A dictionalry with key->value of all the arguments.
    /// </summary>
    private readonly IDictionary<string, string> _parsedArguments;

    /// <summary>
    /// Rules that our arguments have to follow, for example required.
    /// Or, if not required, the default value.
    /// </summary>
    private readonly IDictionary<string, CommandlineData> _commandlineData;

    /// <summary>
    /// The constructor
    /// </summary>
    /// <param name="args">the given arguments.</param>
    /// <param name="commandlineData">argument data telling us about required data.</param>
    /// <param name="leadingPattern">the leading pattern to delimit arguments.</param>
    public CommandlineParser(IReadOnlyList<string> args, IDictionary<string, CommandlineData> commandlineData = null, string leadingPattern = "--")
    {
      // the leading pattern
      _leadingPattern = leadingPattern;

      // set the arguments data.
      _commandlineData = commandlineData ?? new Dictionary<string, CommandlineData>();

      // the arguments
      _parsedArguments = new Dictionary<string, string>();

      // parse the daa
      Parse(args);

      // validate the required arguments.
      ValidateArgumentsData();
    }

    public CommandlineParser Clone()
    {
      return new CommandlineParser(Arguments(false), _commandlineData, _leadingPattern);
    }

    /// <summary>
    /// Remove a key from the list and return the updated value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public CommandlineParser Remove(string key)
    {
      if (!IsSet(key))
      {
        return this;
      }

      // adjust the key value
      var adjustedKey = AdjustedKey(key);

      // remove the key
      _parsedArguments.Remove(adjustedKey);

      // return us
      return this;
    }

    //
    // Summary:
    //     Returns a string that represents the current object.
    //
    // Returns:
    //     A string that represents the current object.
    public override string ToString()
    {
      return string.Join(" ", Arguments(true));
    }

    //
    // Summary:
    //     Returns a string that represents the current object.
    //
    // Returns:
    //     A string that represents the current object.
    private string[] Arguments(bool quoteValues)
    {
      var values = new List<string>();
      foreach (var argumentAndKey in _parsedArguments)
      {
        values.Add($"{_leadingPattern}{argumentAndKey.Key}");
        if (!(argumentAndKey.Value?.Length > 0))
        {
          continue;
        }

        // do we want to quote this?
        var value = argumentAndKey.Value;
        if (quoteValues && value.Any(char.IsWhiteSpace))
        {
          value = $"\"{value}\"";
        }
        values.Add(value);
      }
      return values.ToArray();  
    }

    /// <summary>
    /// Validate that we have all the required arguments, we will
    /// throw in case the required arguments are missing.
    /// </summary>
    private void ValidateArgumentsData()
    {
      foreach (var argument in _commandlineData)
      {
        // adjust the key value.
        var adjustedKey = AdjustedKey(argument.Key);

        // does it exist?
        if (_parsedArguments.ContainsKey(adjustedKey))
        {
          continue;
        }

        // is it required?
        if (!argument.Value.IsRequired)
        {
          continue;
        }

        // yes, it is required and missing, so we have to throw an error.
        throw new ArgumentException("Missing required argument", adjustedKey);
      }
    }

    /// <summary>
    /// Get a value directly, with no default value.
    /// </summary>
    /// <param name="key">The key we are looking for.</param>
    /// <returns></returns>
    public string this[string key] => Get(key);

    /// <summary>
    /// Check if the value is a valid key
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private bool IsKey(string str)
    {
      // check for the leading pattern
      return str.StartsWith(_leadingPattern);
    }

    private static string AdjustedKey(string key)
    {
      if (null == key)
      {
        // the key cannot be null.
        throw new ArgumentNullException(nameof(key));
      }
      return key.ToLower();
    }

    /// <summary>
    /// Get a value given a template.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T Get<T>(string key)
    {
      var adjustedKey = AdjustedKey(key);
      var s = Get(adjustedKey);
      if (s == null)
      {
        return default(T);
      }

      try
      {
        return (T)Convert.ChangeType(s, typeof(T));
      }
      catch (FormatException)
      {
        return default(T);
      }
    }

    /// <summary>
    /// Get a value, and if it does not exist we will return the default.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public T Get<T>(string key, T defaultValue)
    {
      var adjustedKey = AdjustedKey(key);
      var s = Get(adjustedKey);
      if (s == null)
      {
        return defaultValue;
      }

      try
      {
        return (T)Convert.ChangeType(s, typeof(T));
      }
      catch (FormatException)
      {
        return default(T);
      }
    }

    /// <summary>
    /// Get a value with a valid key
    /// </summary>
    /// <param name="key">the key we are looking for</param>
    /// <param name="defaultValue">if the value does not exist, this is what we will return.</param>
    /// <returns></returns>
    public string Get(string key, string defaultValue)
    {
      // adjust the key value
      var adjustedKey = AdjustedKey(key);

      // return it if we have it, the default otherwise.
      return !_parsedArguments.ContainsKey(adjustedKey) ? defaultValue : _parsedArguments[adjustedKey];
    }

    /// <summary>
    /// Get a value with a valid key
    /// </summary>
    /// <param name="key">the key we are looking for</param>
    /// <returns></returns>
    public string Get(string key)
    {
      // adjust the key value
      var adjustedKey = AdjustedKey(key);

      // look for that key in our default arguments.
      var defaultValue = _commandlineData.ContainsKey(adjustedKey) ? _commandlineData[adjustedKey].DefaultValue : null;

      // return it if we have it, the default otherwise.
      return !_parsedArguments.ContainsKey(adjustedKey) ? defaultValue : _parsedArguments[adjustedKey];
    }

    /// <summary>
    /// Check if a value exists or not.
    /// </summary>
    /// <param name="key">the key we are looking for</param>
    /// <returns></returns>
    public bool IsSet(string key)
    {
      // adjust the key value
      var adjustedKey = AdjustedKey(key);

      // look for that key in our default arguments.
      return _parsedArguments.ContainsKey(adjustedKey);
    }


    /// <summary>
    /// Parse the given arguments.
    /// </summary>
    /// <param name="args"></param>
    private void Parse(IReadOnlyList<string> args)
    {
      if (args == null)
      {
        // if we have null, it is fine, this implies we have no data at all.
        return;
      }

      for (var i = 0; i < args.Count; i++)
      {
        if (args[i] == null)
        {
          continue;
        }

        string key = null;
        string val = null;

        if (IsKey(args[i]))
        {
          key = args[i].Substring(_leadingPattern.Length);

          if (i + 1 < args.Count && !IsKey(args[i + 1]))
          {
            val = args[i + 1];
            i++;
          }
        }
        else
        {
          val = args[i];
        }

        // adjustment
        if (key == null)
        {
          key = val;
          val = null;
        }
        if (key != null)
        {
          _parsedArguments[AdjustedKey(key)] = val;
        }
      }
    }
  }
}
