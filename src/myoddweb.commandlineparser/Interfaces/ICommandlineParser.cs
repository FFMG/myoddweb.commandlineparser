namespace myoddweb.commandlineparser.Interfaces
{
  public interface ICommandlineParser
  {
    /// <summary>
    /// Get a value directly, with no default value.
    /// </summary>
    /// <param name="key">The key we are looking for.</param>
    /// <returns></returns>
    string this[string key] { get; }

    /// <summary>
    /// The patern we want our command line to lead with
    /// For example, "-hello world" the leading pattern is '-', (it can be more than one char).
    /// </summary>
    string LeadingPattern { get; }

    /// <summary>
    /// Get the all the rules.
    /// </summary>
    IReadOnlyCommandlineArgumentRules Rules { get; }

    /// <summary>
    /// Get a value given a template.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    T Get<T>(string key);

    /// <summary>
    /// Get a value with a valid key
    /// </summary>
    /// <param name="key">the key we are looking for</param>
    /// <returns></returns>
    string Get(string key);

    /// <summary>
    /// Get a value, and if it does not exist we will return the default.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    T Get<T>(string key, T defaultValue);

    /// <summary>
    /// Get a value with a valid key
    /// </summary>
    /// <param name="key">the key we are looking for</param>
    /// <param name="defaultValue">if the value does not exist, this is what we will return.</param>
    /// <returns></returns>
    string Get(string key, string defaultValue);

    /// <summary>
    /// Check if a value exists or not.
    /// </summary>
    /// <param name="key">the key we are looking for</param>
    /// <returns></returns>
    bool IsSet(string key);

    /// <summary>
    /// Check if the help was requested or not.
    /// </summary>
    /// <returns></returns>
    bool IsHelp();

    /// <summary>
    ///  Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    string ToString();

    /// <summary>
    /// Remove a key from the list and return the updated value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    ICommandlineParser Remove(string key);

    /// <summary>
    /// Clone the interface into a brand new interface.
    /// </summary>
    /// <returns></returns>
    ICommandlineParser Clone();
  }
}
