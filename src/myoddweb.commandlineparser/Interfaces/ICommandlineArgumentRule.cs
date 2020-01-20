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

namespace myoddweb.commandlineparser.Interfaces
{
  public interface ICommandlineArgumentRule
  {
    /// <summary>
    /// The keys we are looking for.
    /// </summary>
    IList<string> Keys { get; }

    /// <summary>
    /// Check if the value is required or not.
    /// </summary>
    bool IsRequired { get; }

    /// <summary>
    /// The default value, (null by default).
    /// </summary>
    string DefaultValue { get; }

    /// <summary>
    /// Check if the given value matches the key and/or aliases
    /// </summary>
    /// <param name="given"></param>
    /// <returns></returns>
    bool IsKeyOrAlias(string given);
  }
}
