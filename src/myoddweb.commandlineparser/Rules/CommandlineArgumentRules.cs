﻿//This file is part of Myoddweb.CommandlineParser.
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using myoddweb.commandlineparser.Interfaces;

namespace myoddweb.commandlineparser.Rules
{
  /// <inheritdoc cref="ICommandlineArgumentRules" />
  public class CommandlineArgumentRules : ICommandlineArgumentRules
  {
    /// <summary>
    /// The list of rules.
    /// </summary>
    private readonly List<ICommandlineArgumentRule> _rules = new List<ICommandlineArgumentRule>();

    /// <inheritdoc cref="ICommandlineArgumentRules" />
    public void Add(ICommandlineArgumentRule rule)
    {
      _rules.Add( rule );
      ThrowIfDuplicates();
    }

    /// <summary>
    /// Do not allow duplicate keys and aliases.
    /// </summary>
    private void ThrowIfDuplicates()
    {
      if (_rules.SelectMany( x => x.Keys ).GroupBy(y => y).Any(g => g.Count() > 1))
      {
        throw new ArgumentException( "You cannot have duplicate keys." );
      }
    }

    /// <inheritdoc />
    public IEnumerator<ICommandlineArgumentRule> GetEnumerator()
    {
      return _rules.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}
