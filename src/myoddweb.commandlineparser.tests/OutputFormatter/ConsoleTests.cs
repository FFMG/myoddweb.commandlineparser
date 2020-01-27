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
using System.Diagnostics;
using System.IO;
using myoddweb.commandlineparser.OutputFormatter;
using myoddweb.commandlineparser.Rules;
using NUnit.Framework;

namespace myoddweb.commandlineparser.tests.OutputFormatter
{
  [TestFixture]
  internal class ConsoleTests
  {
    private string AppName { get; set; }
    [SetUp]
    public void SetUp()
    {
      AppName = Process.GetCurrentProcess().MainModule?.ModuleName ?? "<app>";
    }

    [Test]
    public void SingleHelp()
    {
      using var sw = new StringWriter();
      Console.SetOut(sw);

      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args, new CommandlineArgumentRules
      {
        new HelpCommandlineArgumentRule( "a", "This is help" )
      });

      var output = new ConsoleOutputFormatter( parser );
      var l = $"Usage: {AppName}";
      output.Write();
      var expected =
        $"{l}\r\n" +
         (new string(' ', l.Length+2)) + "[--a]\r\n"+
         "\r\n"+
         "a  :This is help\r\n";
      Assert.AreEqual(expected, sw.ToString() );
    }

    [Test]
    public void MultipleRequired()
    {
      using var sw = new StringWriter();
      Console.SetOut(sw);

      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args, new CommandlineArgumentRules
      {
        new RequiredCommandlineArgumentRule( new []{"a", "b"}, "Required AB" )
      });

      var output = new ConsoleOutputFormatter(parser);
      var l = $"Usage: {AppName}";
      output.Write();
      var expected =
        $"{l}\r\n" +
        (new string(' ', l.Length + 2)) + "--a, --b\r\n" +
        "\r\n" +
        "a, b  :Required AB\r\n";
      Assert.AreEqual(expected, sw.ToString());
    }

    [Test]
    public void RequiredParamsAreBeforeOptional()
    {
      using var sw = new StringWriter();
      Console.SetOut(sw);

      var args = new[] { "--a", "b", "--b", "b" };
      var parser = new CommandlineParser(args, new CommandlineArgumentRules
      {
        new OptionalCommandlineArgumentRule( "a", "Default A", "Optional"),
        new RequiredCommandlineArgumentRule( "b", "Required" )
      });

      var output = new ConsoleOutputFormatter(parser);
      var l = $"Usage: {AppName}";
      output.Write();
      var expected =
        $"{l}\r\n" +
        (new string(' ', l.Length + 2)) + "--b\r\n" +
        (new string(' ', l.Length + 2)) + "[--a=<Default A>]\r\n" +
        "\r\n" +
        "a  :Optional\r\n" +
        "b  :Required\r\n";
      Assert.AreEqual(expected, sw.ToString());
    }

    [Test]
    public void OptionalRuleDefaultValuesAreGiven()
    {
      using var sw = new StringWriter();
      Console.SetOut(sw);

      var args = new[] { "--a", "b", "--b", "b" };
      var parser = new CommandlineParser(args, new CommandlineArgumentRules
      {
        new OptionalCommandlineArgumentRule( "a", "Default A", "Optional A"),
        new OptionalCommandlineArgumentRule( "b", "Default B", "Optional B")
      });

      var output = new ConsoleOutputFormatter(parser);
      var l = $"Usage: {AppName}";
      output.Write();
      var expected =
        $"{l}\r\n" +
        (new string(' ', l.Length + 2)) + "[--a=<Default A>]\r\n" +
        (new string(' ', l.Length + 2)) + "[--b=<Default B>]\r\n" +
        "\r\n" +
        "a  :Optional A\r\n" +
        "b  :Optional B\r\n";
      Assert.AreEqual(expected, sw.ToString());
    }

    [Test]
    public void OptionalRuleDefaultValuesAreNotGiven()
    {
      using var sw = new StringWriter();
      Console.SetOut(sw);

      var args = new[] { "--a", "b", "--b", "b" };
      var parser = new CommandlineParser(args, new CommandlineArgumentRules
      {
        new OptionalCommandlineArgumentRule( "a", "Default A", "Optional A"),
        new OptionalCommandlineArgumentRule( "b", null, "Optional B")
      });

      var output = new ConsoleOutputFormatter(parser);
      var l = $"Usage: {AppName}";
      output.Write();
      var expected =
        $"{l}\r\n" +
        (new string(' ', l.Length + 2)) + "[--a=<Default A>]\r\n" +
        (new string(' ', l.Length + 2)) + "[--b]\r\n" +
        "\r\n" +
        "a  :Optional A\r\n" +
        "b  :Optional B\r\n";
      Assert.AreEqual(expected, sw.ToString());
    }
  }
}
