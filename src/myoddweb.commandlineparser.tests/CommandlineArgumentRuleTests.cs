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
using NUnit.Framework;

namespace myoddweb.commandlineparser.tests
{
  [TestFixture]
  internal class CommandlineArgumentRuleTests
  {
    [Test]
    public void KeyCannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        var _ = new CommandlineArgumentRule(null);
      });
    }

    [TestCase("Test", "test")]
    [TestCase("T", "t")]
    [TestCase("test", "test")]
    [TestCase("TEST", "test")]
    [TestCase("teST", "test")]
    public void KeyIsConvertedToLowerCase(string given, string expected)
    {
      var car = new CommandlineArgumentRule( given );
      Assert.AreEqual( expected, car.Key );
    }

    [TestCase("test   ", "test")]
    [TestCase("   test   ", "test")]
    [TestCase("     test", "test")]
    [TestCase("test", "test")]
    public void KeyIsTrimmed(string given, string expected)
    {
      var car = new CommandlineArgumentRule(given);
      Assert.AreEqual(expected, car.Key);
    }

    [TestCase("")]
    [TestCase("   ")]
    public void KeyCannotBeEmpty(string given)
    {
      Assert.Throws<ArgumentException>(() =>
      {
        var _ = new CommandlineArgumentRule(given);
      });
    }

    [Test]
    public void DefaultVAlues()
    {
      const string key = "key";
      var car = new CommandlineArgumentRule( key  );
      Assert.AreEqual(key, car.Key);
      Assert.IsFalse( car.IsRequired );
      Assert.IsNull( car.DefaultValue );
    }

    [Test]
    public void DefaultValuesWhenDefaultValueIsGiven()
    {
      const string key = "key";
      const string val = "value";
      var car = new CommandlineArgumentRule(key, val);
      Assert.AreEqual(key, car.Key);
      Assert.IsFalse(car.IsRequired);
      Assert.AreEqual(val, car.DefaultValue);
    }
  }
}
