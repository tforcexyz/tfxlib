// Copyright 2017 T-Force Xyz
// Please refer to LICENSE & CONTRIB files in the project root for license information.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using Xyz.TForce.Text;

namespace System
{

  public static class ConstantConversionExtensions
  {

    /// <summary>
    /// Treat current string as a constant and convert it to integer. Maximum 5 characters.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int? AsConstantToInteger(this string str)
    {
      if (str == null)
      {
        return null;
      }
      char[] lowers = LookupArrays.ConstantLookupLower;
      char[] uppers = LookupArrays.ConstantLookupUpper;
      int xbase = lowers.Length;

      int result = 0;
      int power = 1;
      for (int i = str.Length - 1; i >= 0; i--)
      {
        char chr = str[i];
        int idx = Math.Max(Array.IndexOf(lowers, chr), Array.IndexOf(uppers, chr));
        if (idx < 0)
        {
          return null;
        }
        result += power * idx;
        power *= xbase;
      }
      return result;
    }

    /// <summary>
    /// Treat current string as a constant and convert it to integer. Maximum 5 characters.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string is invalid constant</param>
    /// <returns></returns>
    public static int AsConstantToInteger(this string str, int defValue)
    {
      int? result = str.AsConstantToInteger();
      if (result.HasValue)
      {
        return result.Value;
      }
      return defValue;
    }

    /// <summary>
    /// Treat current string as a constant and convert it to long integer. Maximum 11 characters.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long? AsConstantToInteger64(this string str)
    {
      if (str == null)
      {
        return null;
      }
      char[] lowers = LookupArrays.ConstantLookupLower;
      char[] uppers = LookupArrays.ConstantLookupUpper;
      int xbase = lowers.Length;

      long result = 0L;
      long power = 1L;
      for (int i = str.Length - 1; i >= 0; i--)
      {
        char chr = str[i];
        int idx = Math.Max(Array.IndexOf(lowers,  chr), Array.IndexOf(uppers, chr));
        if (idx < 0)
        {
          return null;
        }
        result += power * idx;
        power *= xbase;
      }
      return result;
    }

    /// <summary>
    /// Treat current string as a constant and convert it to long integer. Maximum 5 characters.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string is invalid constant</param>
    /// <returns></returns>
    public static long AsConstantToInteger64(this string str, long defValue)
    {
      long? result = str.AsConstantToInteger64();
      if (result.HasValue)
      {
        return result.Value;
      }
      return defValue;
    }

    /// <summary>
    /// Convert a number to constant
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToConstant(this int value)
    {
      char[] lookup = LookupArrays.ConstantLookupLower;
      int xbase = lookup.Length;

      List<char> resultChar = new List<char>();
      int number = value;
      do
      {
        int remainder = number % xbase;
        resultChar.Add(lookup[remainder]);
        number = (number - remainder) / xbase;
      } while (number > 0);
      resultChar.Reverse();
      string resultString = new string(resultChar.ToArray());
      return resultString;
    }

    /// <summary>
    /// Convert a number to constant
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToConstant(this int? value)
    {
      if (value == null)
      {
        return null;
      }
      return value.Value.ToConstant();
    }

    /// <summary>
    /// Convert a number to constant
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToConstant(this long value)
    {
      char[] lookup = LookupArrays.ConstantLookupLower;
      int xbase = lookup.Length;

      List<char> resultChar = new List<char>();
      long number = value;
      do
      {
        int remainder = (int)(number % xbase);
        resultChar.Add(lookup[remainder]);
        number = (number - remainder) / xbase;
      } while (number > 0);
      resultChar.Reverse();
      string resultString = new string(resultChar.ToArray());
      return resultString;
    }

    /// <summary>
    /// Convert a number to constant
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToConstant(this long? value)
    {
      if (value == null)
      {
        return null;
      }
      return value.Value.ToConstant();
    }
  }
}
