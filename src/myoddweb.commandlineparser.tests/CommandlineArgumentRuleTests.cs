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
using myoddweb.commandlineparser.Rules;
using NUnit.Framework;

namespace myoddweb.commandlineparser.tests
{
  [TestFixture]
  internal class CommandlineArgumentRuleTests
  {
    [Test]
    public void OptionalKeyCannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        var _ = new OptionalCommandlineArgumentRule( (string)null);
      });
    }

    [Test]
    public void RequiredKeyCannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        var _ = new RequiredCommandlineArgumentRule( (string)null);
      });
    }

    [TestCase("Test", "test")]
    [TestCase("T", "t")]
    [TestCase("test", "test")]
    [TestCase("TEST", "test")]
    [TestCase("teST", "test")]
    public void OptionalKeyIsConvertedToLowerCase(string given, string expected)
    {
      var car = new OptionalCommandlineArgumentRule( given );
      Assert.True( car.Keys.Contains(expected) );
      Assert.True( car.Keys.Count == 1 );
    }

    [TestCase("Test", "test")]
    [TestCase("T", "t")]
    [TestCase("test", "test")]
    [TestCase("TEST", "test")]
    [TestCase("teST", "test")]
    public void RequiredKeyIsConvertedToLowerCase(string given, string expected)
    {
      var car = new RequiredCommandlineArgumentRule(given);
      Assert.True(car.Keys.Contains(expected));
      Assert.True(car.Keys.Count == 1);
    }

    [TestCase("test   ", "test")]
    [TestCase("   test   ", "test")]
    [TestCase("     test", "test")]
    [TestCase("test", "test")]
    public void KeyIsTrimmed(string given, string expected)
    {
      var car = new OptionalCommandlineArgumentRule(given);
      Assert.True(car.Keys.Contains(expected));
      Assert.True(car.Keys.Count == 1);
    }

    [TestCase("test   ", "test")]
    [TestCase("   test   ", "test")]
    [TestCase("     test", "test")]
    [TestCase("test", "test")]
    public void RequiredKeyIsTrimmed(string given, string expected)
    {
      var car = new RequiredCommandlineArgumentRule(given);
      Assert.True(car.Keys.Contains(expected));
      Assert.True(car.Keys.Count == 1);
    }

    [TestCase("")]
    [TestCase("   ")]
    public void OptionalKeyCannotBeEmpty(string given)
    {
      Assert.Throws<ArgumentException>(() =>
      {
        var _ = new OptionalCommandlineArgumentRule(given);
      });
    }

    [TestCase("")]
    [TestCase("   ")]
    public void RequiredKeyCannotBeEmpty(string given)
    {
      Assert.Throws<ArgumentException>(() =>
      {
        var _ = new RequiredCommandlineArgumentRule(given);
      });
    }

    [Test]
    public void DefaultVAlues()
    {
      const string key = "key";
      var car = new OptionalCommandlineArgumentRule( key  );
      Assert.True( car.Keys.Contains(key) );
      Assert.True( car.Keys.Count == 1 );
      Assert.IsFalse( car.IsRequired );
      Assert.IsNull( car.DefaultValue );
    }

    [Test]
    public void RequiredDefaultVAlues()
    {
      const string key = "key";
      var car = new RequiredCommandlineArgumentRule(key);
      Assert.True(car.Keys.Contains(key));
      Assert.True(car.Keys.Count == 1);
      Assert.IsTrue(car.IsRequired);
      Assert.IsNull(car.DefaultValue);
    }

    [Test]
    public void OptionalDefaultValuesWhenDefaultValueIsGiven()
    {
      const string key = "key";
      const string val = "value";
      var car = new OptionalCommandlineArgumentRule(key, val);
      Assert.True(car.Keys.Contains(key));
      Assert.True(car.Keys.Count == 1);
      Assert.IsFalse(car.IsRequired);
      Assert.AreEqual(val, car.DefaultValue);
    }

    [Test]
    public void OptionalAddMultipleKeysAllSet()
    {
      var car = new OptionalCommandlineArgumentRule( new []{ "key1", "key2"});
      Assert.True(car.Keys.Contains("key1"));
      Assert.True(car.Keys.Contains("key2"));
      Assert.True(car.Keys.Count == 2);
      Assert.IsFalse(car.IsRequired);
    }

    [Test]
    public void RequiredAddMultipleKeysAllSet()
    {
      var car = new RequiredCommandlineArgumentRule(new[] { "key1", "key2" });
      Assert.True(car.Keys.Contains("key1"));
      Assert.True(car.Keys.Contains("key2"));
      Assert.True(car.Keys.Count == 2);
      Assert.IsTrue(car.IsRequired);
    }

    [Test]
    public void HelpAddMultipleKeysAllSet()
    {
      var car = new HelpCommandlineArgumentRule(new[] { "key1", "key2" });
      Assert.True(car.Keys.Contains("key1"));
      Assert.True(car.Keys.Contains("key2"));
      Assert.True(car.Keys.Count == 2);
      Assert.IsFalse(car.IsRequired);
    }

    [Test]
    public void OptionalCannotHaveDuplicatesKeys()
    {
      Assert.Throws<ArgumentException>(() =>
      {
        var _ = new OptionalCommandlineArgumentRule(new []{"a", "a"});
      });
    }

    [Test]
    public void RequiredCannotHaveDuplicatesKeys()
    {
      Assert.Throws<ArgumentException>(() =>
      {
        var _ = new RequiredCommandlineArgumentRule(new[] { "a", "a" });
      });
    }

    [Test]
    public void OptionalKeysCannotBeEmpty()
    {
      Assert.Throws<ArgumentException>(() =>
      {
        var _ = new OptionalCommandlineArgumentRule( new string[]{});
      });
    }

    [Test]
    public void RequiredKeysCannotBeEmpty()
    {
      Assert.Throws<ArgumentException>(() =>
      {
        var _ = new RequiredCommandlineArgumentRule(new string[] { });
      });
    }

    [Test]
    public void HelpKeysCannotBeEmpty()
    {
      Assert.Throws<ArgumentException>(() =>
      {
        var _ = new HelpCommandlineArgumentRule(new string[] { });
      });
    }

    [Test]
    public void OptionalKeysCannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        var _ = new OptionalCommandlineArgumentRule((List<string>)null);
      });
    }

    [Test]
    public void RequiredKeysCannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        var _ = new RequiredCommandlineArgumentRule((List<string>)null);
      });
    }

    [Test]
    public void HelpKeysCannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        var _ = new HelpCommandlineArgumentRule( (List<string>)null );
      });
    }
  }
}
