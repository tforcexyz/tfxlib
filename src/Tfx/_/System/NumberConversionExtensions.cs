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
using System.Linq.Expressions;
using System.Reflection;
using Xyz.TForce.Text;

namespace System
{

  /// <summary>
  /// Extension to convert between number and string, bin, oct, hex
  /// </summary>
  public static class NumberConversionExtensions
  {

    #region string -> number
    /// <summary>
    /// Cast current string to decimal. Return null if the string is an in valid number.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static decimal? ToDecimal(this string str)
    {
      decimal result;
      if (decimal.TryParse(str, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to decimal. Return defValue if the string is an invalid number.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string cannot be casted to decimal</param>
    /// <returns></returns>
    public static decimal ToDecimal(this string str, decimal defValue)
    {
      decimal result;
      if (decimal.TryParse(str, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to double. Return null if the string is an in valid number.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static double? ToDouble(this string str)
    {
      double result;
      if (double.TryParse(str, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to double. Return defValue if the string is an invalid number.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string cannot be casted to double</param>
    /// <returns></returns>
    public static double ToDouble(this string str, double defValue)
    {
      double result;
      if (double.TryParse(str, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to integer. Return null if the string is an in valid number.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int? ToInteger(this string str)
    {
      int result;
      if (int.TryParse(str, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to integer. Return defValue if the string is an in valid number.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string cannot be casted to integer</param>
    /// <returns></returns>
    public static int ToInteger(this string str, int defValue)
    {
      int result;
      if (int.TryParse(str, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to long integer. Return null if the string is an invalid number.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long? ToInteger64(this string str)
    {
      long result;
      if (long.TryParse(str, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to long integer. Return defValue if the string is an invalid number.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string cannot be casted to long integer</param>
    /// <returns></returns>
    public static long ToInteger64(this string str, long defValue)
    {
      long result;
      if (long.TryParse(str, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to float. Return null if the string is an in valid number.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float? ToSingle(this string str)
    {
      float result;
      if (float.TryParse(str, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to float. Return defValue if the string is an in valid number.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string cannot be casted to float</param>
    /// <returns></returns>
    public static float ToSingle(this string str, float defValue)
    {
      float result;
      if (float.TryParse(str, out result) == false)
      {
        return defValue;
      }
      return result;
    }
    #endregion

    #region bin <-> number
    /// <summary>
    /// Treat current string as binary string and convert it to integer. Return null if the current string is invalid binary string.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int? AsBinToInteger(this string str)
    {
      int? result = str.AsNBaseToInteger(2);
      return result;
    }

    /// <summary>
    /// Treat current string as binary string and convert it to integer. Return defValue if the current string is invalid binary string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string is invalid binary string</param>
    /// <returns></returns>
    public static int AsBinToInteger(this string str, int defValue)
    {
      int result = str.AsNBaseToInteger(2, defValue);
      return result;
    }

    /// <summary>
    /// Treat current string as binary string and convert it to long integer. Return null if the current string is invalid binary string.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long? AsBinToInteger64(this string str)
    {
      long? result = str.AsNBaseToInteger64(2);
      return result;
    }

    /// <summary>
    /// Treat current string as binary string and convert it to long integer. Return defValue if the current string is invalid binary string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string is invalid binary string</param>
    /// <returns></returns>
    public static long AsBinToInteger64(this string str, long defValue)
    {
      long result = str.AsNBaseToInteger64(2, defValue);
      return result;
    }

    public static string ToBin(this int number)
    {
      string result = number.ToNBase(2);
      return result;
    }

    public static string ToBin(this int? number)
    {
      string result = number.ToNBase(2);
      return result;
    }

    public static string ToBin(this long number)
    {
      string result = number.ToNBase(2);
      return result;
    }

    public static string ToBin(this long? number)
    {
      string result = number.ToNBase(2);
      return result;
    }
    #endregion

    #region oct <-> number
    /// <summary>
    /// Treat current string as octal string and convert it to integer. Return defValue if the current string is invalid octal string.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int? AsOctToInteger(this string str)
    {
      int? result = str.AsNBaseToInteger(8);
      return result;
    }

    /// <summary>
    /// Treat current string as octal string and convert it to integer. Return defValue if the current string is invalid octal string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string is invalid octal string</param>
    /// <returns></returns>
    public static int AsOctToInteger(this string str, int defValue)
    {
      int result = str.AsNBaseToInteger(8, defValue);
      return result;
    }

    /// <summary>
    /// Treat current string as octal string and convert it to long integer. Return defValue if the current string is invalid octal string.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long? AsOctToInteger64(this string str)
    {
      long? result = str.AsNBaseToInteger64(8);
      return result;
    }

    /// <summary>
    /// Treat current string as octal string and convert it to long integer. Return defValue if the current string is invalid octal string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string is invalid octal string</param>
    /// <returns></returns>
    public static long AsOctToInteger64(this string str, long defValue)
    {
      long result = str.AsNBaseToInteger64(8, defValue);
      return result;
    }

    public static string ToOct(this int number)
    {
      string result = number.ToNBase(8);
      return result;
    }

    public static string ToOct(this int? number)
    {
      string result = number.ToNBase(8);
      return result;
    }

    public static string ToOct(this long number)
    {
      string result = number.ToNBase(8);
      return result;
    }

    public static string ToOct(this long? number)
    {
      string result = number.ToNBase(8);
      return result;
    }
    #endregion

    #region hex <-> number
    /// <summary>
    /// Treat current string as hexadecimal string and convert it to integer. Return null if the current string is invalid hexadecimal string.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int? AsHexToInteger(this string str)
    {
      int? result = str.AsNBaseToInteger(16);
      return result;
    }

    /// <summary>
    /// Treat current string as hexadecimal string and convert it to integer. Return defValue if the current string is invalid hexadecimal string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string is invalid hexadecimal string</param>
    /// <returns></returns>
    public static int AsHexToInteger(this string str, int defValue)
    {
      int result = str.AsNBaseToInteger(16, defValue);
      return result;
    }

    /// <summary>
    /// Treat current string as hexadecimal string and convert it to long integer. Return null if the current string is invalid hexadecimal string.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long? AsHexToInteger64(this string str)
    {
      long? result = str.AsNBaseToInteger64(16);
      return result;
    }

    /// <summary>
    /// Treat current string as hexadecimal string and convert it to long integer. Return defValue if the current string is invalid hexadecimal string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value if current string is invalid hexadecimal string</param>
    /// <returns></returns>
    public static long AsHexToInteger64(this string str, long defValue)
    {
      long result = str.AsNBaseToInteger64(16, defValue);
      return result;
    }

    public static string ToHex(this int number)
    {
      string result = number.ToNBase(16);
      return result;
    }

    public static string ToHex(this int? number)
    {
      string result = number.ToNBase(16);
      return result;
    }

    public static string ToHex(this long number)
    {
      string result = number.ToNBase(16);
      return result;
    }

    public static string ToHex(this long? number)
    {
      string result = number.ToNBase(16);
      return result;
    }
    #endregion

    #region nbase <-> number
    /// <summary>
    /// Treat current string as uniform-base string and convert it to integer. Return null if the current string is invalid uniform-base string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="xbase">The base of the current string</param>
    /// <returns></returns>
    public static int? AsNBaseToInteger(this string str, int xbase)
    {
      if (str == null)
      {
        return null;
      }

      char[] lowers = LookupArrays.NbaseLookupLower;
      char[] uppers = LookupArrays.NbaseLookupUpper;

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
    /// Treat current string as uniform-base string and convert it to integer. Return defValue if the current string is invalid uniform-base string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="xbase">The base of the current string</param>
    /// <param name="defValue">Default value if current string is invalid uniform-base string</param>
    /// <returns></returns>
    public static int AsNBaseToInteger(this string str, int xbase, int defValue)
    {
      int? result = str.AsNBaseToInteger(xbase);
      if (result.HasValue)
      {
        return result.Value;
      }
      return defValue;
    }

    /// <summary>
    /// Treat current string as uniform-base string and convert it to long integer. Return null if the current string is invalid uniform-base string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="xbase">The base of the current string</param>
    /// <returns></returns>
    public static long? AsNBaseToInteger64(this string str, int xbase)
    {
      if (str == null)
      {
        return null;
      }

      char[] lowers = LookupArrays.NbaseLookupLower;
      char[] uppers = LookupArrays.NbaseLookupUpper;

      long result = 0L;
      long power = 1L;
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
    /// Treat current string as uniform-base string and convert it to long integer. Return defValue if the current string is invalid uniform-base string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="xbase">The base of the current string</param>
    /// <param name="defValue">Default value if current string is invalid uniform-base string</param>
    /// <returns></returns>
    public static long AsNBaseToInteger64(this string str, int xbase, long defValue)
    {
      long? result = str.AsNBaseToInteger64(xbase);
      if (result.HasValue)
      {
        return result.Value;
      }
      return defValue;
    }

    /// <summary>
    /// Convert current number to uniform-base in lowercase.
    /// </summary>
    /// <param name="number">Current number</param>
    /// <param name="xbase">New base of the number</param>
    /// <returns>Number with xbase</returns>
    public static string ToNBase(this int number, int xbase)
    {

      char[] lowers = LookupArrays.NbaseLookupLower;
      List<char> resultChars = new List<char>();
      int remain = number;
      while (remain > 0)
      {
        int mod = remain % xbase;
        resultChars.Add(lowers[mod]);
        remain /= xbase;
      }
      resultChars.Reverse();
      string resultString = new string(resultChars.ToArray());
      return resultString;
    }

    /// <summary>
    /// Convert current number to uniform-base in lowercase.
    /// </summary>
    /// <param name="number">Current number</param>
    /// <param name="xbase">New base of the number</param>
    /// <returns>Number with xbase</returns>
    public static string ToNBase(this int? number, int xbase)
    {
      if (number == null)
      {
        return null;
      }
      char[] lowers = LookupArrays.NbaseLookupLower;
      List<char> resultChars = new List<char>();
      int remain = number.Value;
      while (remain > 0)
      {
        int mod = remain % xbase;
        resultChars.Add(lowers[mod]);
        remain /= xbase;
      }
      resultChars.Reverse();
      string resultString = new string(resultChars.ToArray());
      return resultString;
    }

    /// <summary>
    /// Convert current number to uniform-base in lowercase.
    /// </summary>
    /// <param name="number">Current number</param>
    /// <param name="xbase">New base of the number</param>
    /// <returns>Number with xbase</returns>
    public static string ToNBase(this long number, int xbase)
    {

      char[] lowers = LookupArrays.NbaseLookupLower;
      List<char> resultChars = new List<char>();
      long remain = number;
      while (remain > 0)
      {
        int mod = (int)(remain % xbase);
        resultChars.Add(lowers[mod]);
        remain /= xbase;
      }
      resultChars.Reverse();
      string resultString = new string(resultChars.ToArray());
      return resultString;
    }

    /// <summary>
    /// Convert current number to uniform-base in lowercase.
    /// </summary>
    /// <param name="number">Current number</param>
    /// <param name="xbase">New base of the number</param>
    /// <returns>Number with xbase</returns>
    public static string ToNBase(this long? number, int xbase)
    {
      if (number == null)
      {
        return null;
      }
      char[] lowers = LookupArrays.NbaseLookupLower;
      List<char> resultChars = new List<char>();
      long remain = number.Value;
      while (remain > 0)
      {
        int mod = (int)(remain % xbase);
        resultChars.Add(lowers[mod]);
        remain /= xbase;
      }
      resultChars.Reverse();
      string resultString = new string(resultChars.ToArray());
      return resultString;
    }
    #endregion

    #region flag <- number
    /// <summary>
    /// Convert a number into boolean properties of a class specified in mapping dictionary
    /// </summary>
    /// <typeparam name="TFlag">Type of the flag</typeparam>
    /// <param name="number">Value of the mapping in integer</param>
    /// <param name="mapping">Dictionary of 1-based column mapping. The counting direction is from right to left</param>
    /// <returns>New instance of flag</returns>
    public static TFlag ToFlag<TFlag>(this int number, IDictionary<int, Expression<Func<TFlag, bool>>> mapping)
        where TFlag : new()
    {
      TFlag flag = Activator.CreateInstance<TFlag>();
      MapFlag(number, flag, mapping);
      return flag;
    }

    /// <summary>
    /// Convert a number into boolean properties of a class specified in mapping dictionary
    /// </summary>
    /// <typeparam name="TFlag">Type of the flag</typeparam>
    /// <param name="number">Value of the mapping in long integer</param>
    /// <param name="mapping">Dictionary of 1-based column mapping. The counting direction is from right to left</param>
    /// <returns>New instance of flag</returns>
    public static TFlag ToFlag<TFlag>(this long number, IDictionary<long, Expression<Func<TFlag, bool>>> mapping)
        where TFlag : new()
    {
      TFlag flag = Activator.CreateInstance<TFlag>();
      MapFlag(number, flag, mapping);
      return flag;
    }

    /// <summary>
    /// Map a number into boolean properties of a class specified in mapping dictionary
    /// </summary>
    /// <typeparam name="TFlag">Type of the flag</typeparam>
    /// <param name="number">Value of the mapping in integer</param>
    /// <param name="instance">Current instance of flag class</param>
    /// <param name="mapping">Dictionary of 1-based column mapping. The counting direction is from right to left</param>
    public static void MapFlag<TFlag>(this int number, TFlag instance, IDictionary<int, Expression<Func<TFlag, bool>>> mapping)
    {
      int remain = number;
      int digit = 1;
      while (remain > 0)
      {
        int mod = remain % 2;
        Expression<Func<TFlag, bool>> expression = mapping.SafeGetValue(digit);
        if (expression != null)
        {
          if (mod == 0)
          {
            instance.SetPropertyValue(expression, false);
          }
          else
          {
            instance.SetPropertyValue(expression, true);
          }
        }
        remain /= 2;
        digit++;
      }
    }

    /// <summary>
    /// Map a number into boolean properties of a class specified in mapping dictionary
    /// </summary>
    /// <typeparam name="TFlag">Type of the flag</typeparam>
    /// <param name="number">Value of the mapping in long integer</param>
    /// <param name="instance">Current instance of flag class</param>
    /// <param name="mapping">Dictionary of 1-based column mapping. The counting direction is from right to left</param>
    public static void MapFlag<TFlag>(this long number, TFlag instance, IDictionary<long, Expression<Func<TFlag, bool>>> mapping)
    {
      long remain = number;
      int digit = 1;
      while (remain > 0)
      {
        long mod = remain % 2;
        Expression<Func<TFlag, bool>> expression = mapping.SafeGetValue(digit);
        if (expression != null)
        {
          if (mod == 0)
          {
            instance.SetPropertyValue(expression, false);
          }
          else
          {
            instance.SetPropertyValue(expression, true);
          }
        }
        remain /= 2;
        digit++;
      }
    }
    #endregion

    #region limit value
    /// <summary>
    /// Limit value of a number within a range inclusive
    /// </summary>
    /// <param name="number">value to be limited</param>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    /// <returns>value after limited</returns>
    public static double? Limit(this double? number, double? min, double? max)
    {
      if (number == null)
      {
        return null;
      }
      double result = number.Value;
      if (min.HasValue)
      {
        if (result < min.Value)
        {
          result = min.Value;
        }
      }
      if (max.HasValue)
      {
        if (result > max.Value)
        {
          result = max.Value;
        }
      }
      return result;
    }

    /// <summary>
    /// Limit value of a number within a range inclusive
    /// </summary>
    /// <param name="number">value to be limited</param>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    /// <returns>value after limited</returns>
    public static double Limit(this double number, double? min, double? max)
    {
      double result = number;
      if (min.HasValue)
      {
        if (result < min.Value)
        {
          result = min.Value;
        }
      }
      if (max.HasValue)
      {
        if (result > max.Value)
        {
          result = max.Value;
        }
      }
      return result;
    }

    /// <summary>
    /// Limit value of a number within a range inclusive
    /// </summary>
    /// <param name="number">value to be limited</param>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    /// <returns>value after limited</returns>
    public static float? Limit(this float? number, float? min, float? max)
    {
      if (number == null)
      {
        return null;
      }
      float result = number.Value;
      if (min.HasValue)
      {
        if (result < min.Value)
        {
          result = min.Value;
        }
      }
      if (max.HasValue)
      {
        if (result > max.Value)
        {
          result = max.Value;
        }
      }
      return result;
    }

    /// <summary>
    /// Limit value of a number within a range inclusive
    /// </summary>
    /// <param name="number">value to be limited</param>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    /// <returns>value after limited</returns>
    public static float Limit(this float number, float? min, float? max)
    {
      float result = number;
      if (min.HasValue)
      {
        if (result < min.Value)
        {
          result = min.Value;
        }
      }
      if (max.HasValue)
      {
        if (result > max.Value)
        {
          result = max.Value;
        }
      }
      return result;
    }

    /// <summary>
    /// Limit value of a number within a range inclusive
    /// </summary>
    /// <param name="number">value to be limited</param>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    /// <returns>value after limited</returns>
    public static int? Limit(this int? number, int? min, int? max)
    {
      if (number == null)
      {
        return null;
      }
      int result = number.Value;
      if (min.HasValue)
      {
        if (result < min.Value)
        {
          result = min.Value;
        }
      }
      if (max.HasValue)
      {
        if (result > max.Value)
        {
          result = max.Value;
        }
      }
      return result;
    }

    /// <summary>
    /// Limit value of a number within a range inclusive
    /// </summary>
    /// <param name="number">value to be limited</param>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    /// <returns>value after limited</returns>
    public static int Limit(this int number, int? min, int? max)
    {
      int result = number;
      if (min.HasValue)
      {
        if (result < min.Value)
        {
          result = min.Value;
        }
      }
      if (max.HasValue)
      {
        if (result > max.Value)
        {
          result = max.Value;
        }
      }
      return result;
    }

    /// <summary>
    /// Limit value of a number within a range inclusive
    /// </summary>
    /// <param name="number">value to be limited</param>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    /// <returns>value after limited</returns>
    public static long? Limit(this long? number, long? min, long? max)
    {
      if (number == null)
      {
        return null;
      }
      long result = number.Value;
      if (min.HasValue)
      {
        if (result < min.Value)
        {
          result = min.Value;
        }
      }
      if (max.HasValue)
      {
        if (result > max.Value)
        {
          result = max.Value;
        }
      }
      return result;
    }

    /// <summary>
    /// Limit value of a number within a range inclusive
    /// </summary>
    /// <param name="number">value to be limited</param>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    /// <returns>value after limited</returns>
    public static long Limit(this long number, long? min, long? max)
    {
      long result = number;
      if (min.HasValue)
      {
        if (result < min.Value)
        {
          result = min.Value;
        }
      }
      if (max.HasValue)
      {
        if (result > max.Value)
        {
          result = max.Value;
        }
      }
      return result;
    }
    #endregion
  }
}
