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
using System.Collections.Generic;
using NUnit.Framework;
using myoddweb.commandlineparser;

namespace myoddweb.commandlineparser.tests
{
  [TestFixture]
  public class CommandlineParserTest
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestArgumentsCanBeNull()
    {
      // the arguments
      var parser = new CommandlineParser(null);

      // and we can get values.
      Assert.IsFalse(parser.IsSet("a"));
      Assert.IsFalse(parser.IsSet("b"));
    }

    [Test]
    public void TestArgumentsCanBeEmpty()
    {
      // the arguments
      var args = new string[] { };
      var parser = new CommandlineParser(args);

      // and we can get values.
      Assert.IsFalse(parser.IsSet("a"));
      Assert.IsFalse(parser.IsSet("b"));
    }

    [Test]
    public void TestSimpleWithNoRules()
    {
      // the arguments
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual("b", parser["a"]); //  direct value.
      Assert.AreEqual("b", parser.Get("a")); //  no default
      Assert.AreEqual("b", parser.Get("a", "c")); //  fake default
    }

    [Test]
    public void TestNoDefaultValueGiven()
    {
      // the arguments
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual(null, parser.Get("c"));
    }

    [Test]
    public void TestDefaultValues()
    {
      // the arguments
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual("d", parser.Get("c", "d"));
    }

    [Test]
    public void TestFlagsWithNoValues()
    {
      // the arguments
      var args = new[] { "--a", "--b", "--c", "hello" };
      var parser = new CommandlineParser(args);

      //  "c" has a value
      Assert.AreEqual("hello", parser.Get("c"));

      // the exist
      Assert.IsTrue(parser.IsSet("a"));
      Assert.IsTrue(parser.IsSet("b"));

      // but a and b are empty
      Assert.AreEqual(null, parser.Get("a"));
      Assert.AreEqual(null, parser.Get("b"));

      // even with a default
      Assert.AreEqual(null, parser.Get("a", "nope"));
      Assert.AreEqual(null, parser.Get("b", "nope"));
    }

    [Test]
    public void TestFlagsWithNoValueAlone()
    {
      // the arguments
      var args = new[] { "--a" };
      var parser = new CommandlineParser(args);

      // "a" exist
      Assert.IsTrue(parser.IsSet("a"));

      // not "b"
      Assert.IsFalse(parser.IsSet("b"));

      // but a and b are empty
      Assert.AreEqual(null, parser.Get("a"));
      Assert.AreEqual(null, parser.Get("b"));

      // even with a default
      Assert.AreEqual(null, parser.Get("a", "nope"));
      Assert.AreEqual("nope", parser.Get("b", "nope"));
    }

    [Test]
    public void TestFlagsWithNoValueAloneNonDefaultLeadingPattern()
    {
      // the arguments
      var args = new[] { "-a" };
      var parser = new CommandlineParser(args, null, "-");

      // "a" exist
      Assert.IsTrue(parser.IsSet("a"));
    }

    [Test]
    public void TestFlagsWithValuesAndNoValuesNonDefaultLeadingPattern()
    {
      // the arguments
      var args = new[] { "-a", "-b", "world" };
      var parser = new CommandlineParser(args, null, "-");

      Assert.IsTrue(parser.IsSet("a"));
      Assert.IsTrue(parser.IsSet("b"));
      Assert.AreEqual("world", parser.Get("b", "nope"));
    }

    [Test]
    public void TestNonRequiredValueWithDefaultArgumentsInRules()
    {
      // the arguments
      var args = new[] { "--a" };
      var parser = new CommandlineParser(args, new Dictionary<string, CommandlineData>
                                                  {
                                                    { "a", new CommandlineData{ IsRequired = true} },
                                                    { "b", new CommandlineData{ IsRequired = false, DefaultValue = "World"}}
                                                  });

      Assert.IsTrue(parser.IsSet("a"));
      Assert.IsFalse(parser.IsSet("b"));
      Assert.AreEqual("World", parser.Get("b"));
      Assert.AreEqual("Hello", parser.Get("b", "Hello")); // if we are explicitely given another default, we will use it.
    }

    [Test]
    public void TestNonRequiredValueWithDefaultArgumentsInRulesButValueDoesExist()
    {
      // the arguments
      var args = new[] { "--a", "--b", "Hello" };
      var parser = new CommandlineParser(args, new Dictionary<string, CommandlineData>
                                                  {
                                                    { "a", new CommandlineData{ IsRequired = true} },
                                                    { "b", new CommandlineData{ IsRequired = false, DefaultValue = "World"}}
                                                  });

      Assert.IsTrue(parser.IsSet("a"));
      Assert.IsTrue(parser.IsSet("b"));
      Assert.AreEqual("Hello", parser.Get("b"));
      Assert.AreEqual("Hello", parser.Get("b", "World"));
    }

    [Test]
    public void TestNullArgumentsButhHasRules()
    {
      // the arguments
      var parser = new CommandlineParser(null, new Dictionary<string, CommandlineData>
                                                  {
                                                    { "a", new CommandlineData{ IsRequired = false, DefaultValue = "Hello"}},
                                                    { "b", new CommandlineData{ IsRequired = false, DefaultValue = "World"}}
                                                  });

      Assert.IsFalse(parser.IsSet("a"));
      Assert.IsFalse(parser.IsSet("b"));
      Assert.AreEqual("Hello", parser.Get("a"));
      Assert.AreEqual("World", parser.Get("b"));
    }

    [Test]
    public void TestEmptyArgumentsButhHasRules()
    {
      var args = new string[] { };
      // the arguments
      var parser = new CommandlineParser(args, new Dictionary<string, CommandlineData>
                                                  {
                                                    { "a", new CommandlineData{ IsRequired = false, DefaultValue = "Hello"}},
                                                    { "b", new CommandlineData{ IsRequired = false, DefaultValue = "World"}}
                                                  });

      Assert.IsFalse(parser.IsSet("a"));
      Assert.IsFalse(parser.IsSet("b"));
      Assert.AreEqual("Hello", parser.Get("a"));
      Assert.AreEqual("World", parser.Get("b"));
    }

    [Test]
    public void TestMissingRequiredArgument()
    {
      // the arguments
      var args = new[] { "--a" };

      // 'b' is missing.
      Assert.Throws<ArgumentException>(() =>
      {
        // Missing required argument
        new CommandlineParser(args, new Dictionary<string, CommandlineData>
        {
          {"a", new CommandlineData {IsRequired = true}},
          {"b", new CommandlineData {IsRequired = true}}
        });
      });

    }

    [Test]
    public void TestMissingRequiredArgumentNullArguments()
    {
      // 'b' is missing.
      Assert.Throws<ArgumentException>(() =>
      {
        // Missing required argument.
        new CommandlineParser(null, new Dictionary<string, CommandlineData>
        {
          {"a", new CommandlineData {IsRequired = true}},
          {"b", new CommandlineData {IsRequired = true}}
        });
      });
    }

    [Test]
    public void CloneValue()
    {
      // the arguments
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);
      var clone = parser.Clone();

      Assert.AreEqual("b", clone["a"]); //  direct value.
    }

    [Test]
    public void CloneMultipleValues()
    {
      // the arguments
      var args = new[] { "--a", "b", "--c", "d" };
      var parser = new CommandlineParser(args);
      var clone = parser.Clone();

      Assert.AreEqual("b", clone["a"]); //  direct value.
      Assert.AreEqual("d", clone["c"]); //  direct value.
    }

    [Test]
    public void ToStringMultipleValues()
    {
      // the arguments
      var args = new[] { "--a", "b", "--c", "d" };
      var parser = new CommandlineParser(args);
      var clone = parser.Clone();

      Assert.AreEqual("--a b --c d", clone.ToString());
    }

    [Test]
    public void ToStringMultipleValuesWithSomeNoValue()
    {
      // the arguments
      var args = new[] { "--a", "b", "--e", "--c", "d" };
      var parser = new CommandlineParser(args);
      var clone = parser.Clone();

      Assert.AreEqual("--a b --e --c d", clone.ToString());
    }

    [Test]
    public void ToStringRecoverLeadingValueMultipleValuesWithSomeNoValue()
    {
      // the arguments
      var args = new[] { "-a", "b", "-e", "-c", "d" };
      var parser = new CommandlineParser(args, null, "-");
      var clone = parser.Clone();

      Assert.AreEqual("-a b -e -c d", clone.ToString());
    }

    [Test]
    public void ToStringRemoveMultipleValuesWithSomeNoValue()
    {
      // the arguments
      var args = new[] { "--a", "b", "--e", "--c", "d" };
      var parser = new CommandlineParser(args);
      var clone = parser.Clone().Remove("a");

      Assert.AreEqual("--e --c d", clone.ToString());
    }

    [Test]
    public void CloneAndRemoveDoesNotChangeTheOriginal()
    {
      // the arguments
      var args = new[] { "--a", "b", "--e", "--c", "d" };
      var parser = new CommandlineParser(args);
      var clone = parser.Clone().Remove("a");

      Assert.AreEqual("--a b --e --c d", parser.ToString());  //  unchanged
      Assert.AreEqual("--e --c d", clone.ToString());
    }

    [Test]
    public void ToStringRemoveNonExistentMultipleValuesWithSomeNoValue()
    {
      // the arguments
      var args = new[] { "--a", "b", "--e", "--c", "d" };
      var parser = new CommandlineParser(args);
      var clone = parser.Clone().Remove("z");

      Assert.AreEqual("--a b --e --c d", clone.ToString());
    }

    [Test]
    public void ToStingWithSpaces()
    {
      // the arguments
      var args = new[] { "--a", "b", "--c", "Hello World" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual("--a b --c \"Hello World\"", parser.ToString());
    }

    [Test]
    public void CloneToStingWithSpacesDoesNotAddExtraQuotes()
    {
      // the arguments
      var args = new[] { "--a", "b", "--c", "Hello World" };
      var parser = new CommandlineParser(args);
      var clone = parser.Clone();

      Assert.AreEqual("--a b --c \"Hello World\"", clone.ToString());
    }

    [Test]
    public void GetIntType()
    {
      // the arguments
      var args = new[] { "--a", "12" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual(12, parser.Get<int>("a", 0)); //  fake default
    }

    [Test]
    public void GetDoubleType()
    {
      // the arguments
      var args = new[] { "--a", "12.34" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual(12.34, parser.Get<double>("a", 0)); //  fake default
    }

    [Test]
    public void GetIntTypeThatIsNotAnInt()
    {
      // the arguments
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual(default(int), parser.Get<int>("a", 12)); //  there is a value, so we will return the default.
    }

    [Test]
    public void GetDoubleTypeThatIsNotADouble()
    {
      // the arguments
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual(default(double), parser.Get<double>("a", 12)); //  there is a value, so we will return the default.
    }

    [Test]
    public void GetDoubleTypeWithNoGivenDefault()
    {
      // the arguments
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual(default(double), parser.Get<double>("c")); //  there is a value, so we will return the default.
    }

    [Test]
    public void GetIntTypeReturnTheDefault()
    {
      // the arguments
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual(12, parser.Get<int>("c", 12)); // return the default value.
    }

    [Test]
    public void GetIntTypeWithNoGivenDefaultReturnTheDefault()
    {
      // the arguments
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual(default(int), parser.Get<int>("c")); // return the default value.
    }

    [Test]
    public void GetDoubleTypeReturnTheDefault()
    {
      // the arguments
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);

      Assert.AreEqual(12.34, parser.Get<double>("c", 12.34)); // return the default value.
    }

    [Test]
    public void YouCannotGetWithANullKeyWithString()
    {
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);

      Assert.Throws<ArgumentNullException>(() =>
      {
        parser.Get(null);
      });
    }

    [Test]
    public void YouCannotGetWithANullKeyWithNonString()
    {
      var args = new[] { "--a", "b" };
      var parser = new CommandlineParser(args);

      Assert.Throws<ArgumentNullException>(() =>
      {
        parser.Get<double>(null, 12.34);
      });
    }
  }
}