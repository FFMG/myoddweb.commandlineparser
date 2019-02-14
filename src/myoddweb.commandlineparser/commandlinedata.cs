namespace myoddweb.commandlineparser
{
  /// <summary>
  /// The argument data, indicate required values.
  /// It can also contain description as well as default values.
  /// </summary>
  public class CommandlineData
  {
    /// <summary>
    /// Check if the value is required or not.
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// The default value, (null by default).
    /// </summary>
    public string DefaultValue { get; set; }
  }
}
