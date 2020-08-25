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
using System.Linq;

namespace System
{

  /// <summary>
  /// Extension to convert string to other datatype quickly
  /// </summary>
  public static class PrimitiveDataConversionExtensions
  {

    public static string[] GetMessages(this Exception ex)
    {
      List<string> messages = new List<string>();
      Exception subException = ex;
      while (subException != null)
      {
        messages.Add(subException.Message);
        subException = subException.InnerException;
      }
      string[] messageArray = messages.ToArray();
      return messageArray;
    }

    /// <summary>
    /// Cast current string to boolean. Return null if the string is an invalid boolean string.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool? ToBool(this string str)
    {
      bool result;
      if (bool.TryParse(str, out result) == false)
      {
        return null;
      }

      return result;
    }

    /// <summary>
    /// Cast current string to boolean. Return defValue if the string is an invalid boolean string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static bool ToBool(this string str, bool defValue)
    {
      bool result;
      if (bool.TryParse(str, out result) == false)
      {
        return defValue;
      }

      return result;
    }

    /// <summary>
    /// Convert a string to camelCase
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToCamel(this string str)
    {
      if (string.IsNullOrEmpty(str))
      {
        return str;
      }
      if (str.Length == 1)
      {
        return str.ToLower();
      }
      string result = str.Substring(0, 1).ToLower() + str.Substring(1);
      return result;
    }

    /// <summary>
    /// Convert a string to PascalCase
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToPascal(this string str)
    {
      if (string.IsNullOrEmpty(str))
      {
        return str;
      }
      else if (str.Length == 1)
      {
        return str.ToUpper();
      }
      string result = str.Substring(0, 1).ToUpper() + str.Substring(1);
      return result;
    }

    /// <summary>
    /// Split a string into list of DateTime using common token separators (comma, semicolon, vertical bar, line-break, new-line)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format">Specify format of the date. Can be null</param>
    /// <param name="removeEmptyEntries">Should remove empty entries</param>
    /// <returns></returns>
    public static List<DateTime?> SplitDateTime(this string str, string format = null, bool removeEmptyEntries = false)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        return new List<DateTime?>();
      }
      StringSplitOptions options = removeEmptyEntries ?
        StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
      IEnumerable<DateTime?> tokens = str.Split(new[] { ',', ';', '|', '\r', '\n' }, options)
        .Select(x => { return x.ToDateTime(); });
      return tokens.ToList();
    }

    /// <summary>
    /// Split a string into list of DateTime using common token separators (comma, semicolon, vertical bar, line-break, new-line)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value to fallback if conversion is failed</param>
    /// <param name="format">Specify format of the date. Can be null</param>
    /// <param name="removeEmptyEntries">Should remove empty entries</param>
    /// <returns></returns>
    public static List<DateTime> SplitDateTime(this string str, DateTime defValue, string format = null, bool removeEmptyEntries = false)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        return new List<DateTime>();
      }
      StringSplitOptions options = removeEmptyEntries ?
        StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
      IEnumerable<DateTime> tokens = str.Split(new[] { ',', ';', '|', '\r', '\n' }, options)
        .Select(x => { return x.ToDateTime(defValue); });
      return tokens.ToList();
    }

    /// <summary>
    /// Split a string into list of double using common token separators (comma, semicolon, vertical bar, line-break, new-line)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="removeEmptyEntries">Should remove empty entries</param>
    /// <returns></returns>
    public static List<double?> SplitDouble(this string str, bool removeEmptyEntries = false)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        return new List<double?>();
      }
      StringSplitOptions options = removeEmptyEntries ?
        StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
      IEnumerable<double?> tokens = str.Split(new[] { ',', ';', '|', '\r', '\n' }, options)
        .Select(x => { return x.ToDouble(); });
      return tokens.ToList();
    }

    /// <summary>
    /// Split a string into list of double using common token separators (comma, semicolon, vertical bar, line-break, new-line)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value to fallback if conversion is failed</param>
    /// <param name="removeEmptyEntries">Should remove empty entries</param>
    /// <returns></returns>
    public static List<double> SplitDouble(this string str, double defValue, bool removeEmptyEntries = false)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        return new List<double>();
      }
      StringSplitOptions options = removeEmptyEntries ?
        StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
      IEnumerable<double> tokens = str.Split(new[] { ',', ';', '|', '\r', '\n' }, options)
        .Select(x => { return x.ToDouble(defValue); });
      return tokens.ToList();
    }

    /// <summary>
    /// Split a string into list of single using common token separators (comma, semicolon, vertical bar, line-break, new-line)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="removeEmptyEntries">Should remove empty entries</param>
    /// <returns></returns>
    public static List<float?> SplitSingle(this string str, bool removeEmptyEntries = false)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        return new List<float?>();
      }
      StringSplitOptions options = removeEmptyEntries ?
        StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
      IEnumerable<float?> tokens = str.Split(new[] { ',', ';', '|', '\r', '\n' }, options)
        .Select(x => { return x.ToSingle(); });
      return tokens.ToList();
    }

    /// <summary>
    /// Split a string into list of single using common token separators (comma, semicolon, vertical bar, line-break, new-line)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value to fallback if conversion is failed</param>
    /// <param name="removeEmptyEntries">Should remove empty entries</param>
    /// <returns></returns>
    public static List<float> SplitSingle(this string str, float defValue, bool removeEmptyEntries = false)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        return new List<float>();
      }
      StringSplitOptions options = removeEmptyEntries ?
        StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
      IEnumerable<float> tokens = str.Split(new[] { ',', ';', '|', '\r', '\n' }, options)
        .Select(x => { return x.ToSingle(defValue); });
      return tokens.ToList();
    }

    /// <summary>
    /// Split a string into list of integer using common token separators (comma, semicolon, vertical bar, line-break, new-line)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="removeEmptyEntries">Should remove empty entries</param>
    /// <returns></returns>
    public static List<int?> SplitInteger(this string str, bool removeEmptyEntries = false)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        return new List<int?>();
      }
      StringSplitOptions options = removeEmptyEntries ?
        StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
      IEnumerable<int?> tokens = str.Split(new[] { ',', ';', '|', '\r', '\n' }, options)
        .Select(x => { return x.ToInteger(); });
      return tokens.ToList();
    }

    /// <summary>
    /// Split a string into list of integer using common token separators (comma, semicolon, vertical bar, line-break, new-line)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value to fallback if conversion is failed</param>
    /// <param name="removeEmptyEntries">Should remove empty entries</param>
    /// <returns></returns>
    public static List<int> SplitInteger(this string str, int defValue, bool removeEmptyEntries = false)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        return new List<int>();
      }
      StringSplitOptions options = removeEmptyEntries ?
        StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
      IEnumerable<int> tokens = str.Split(new[] { ',', ';', '|', '\r', '\n' }, options)
        .Select(x => { return x.ToInteger(defValue); });
      return tokens.ToList();
    }

    /// <summary>
    /// Split a string into list of long integer using common token separators (comma, semicolon, vertical bar, line-break, new-line)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="removeEmptyEntries">Should remove empty entries</param>
    /// <returns></returns>
    public static List<long?> SplitInteger64(this string str, bool removeEmptyEntries = false)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        return new List<long?>();
      }
      StringSplitOptions options = removeEmptyEntries ?
        StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
      IEnumerable<long?> tokens = str.Split(new[] { ',', ';', '|', '\r', '\n' }, options)
        .Select(x => { return x.ToInteger64(); });
      return tokens.ToList();
    }

    /// <summary>
    /// Split a string into list of long integer using common token separators (comma, semicolon, vertical bar, line-break, new-line)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue">Default value to fallback if conversion is failed</param>
    /// <param name="removeEmptyEntries">Should remove empty entries</param>
    /// <returns></returns>
    public static List<long> SplitInteger64(this string str, long defValue, bool removeEmptyEntries = false)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        return new List<long>();
      }
      StringSplitOptions options = removeEmptyEntries ?
        StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
      IEnumerable<long> tokens = str.Split(new[] { ',', ';', '|', '\r', '\n' }, options)
        .Select(x => { return x.ToInteger64(defValue); });
      return tokens.ToList();
    }

    /// <summary>
    /// Split a string using common token separators (comma, semicolon, vertical bar, line-break, new-line)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="removeEmptyEntries">Should remove empty entries</param>
    /// <returns></returns>
    public static List<string> SplitTokens(this string str, bool removeEmptyEntries = false)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        return new List<string>();
      }
      StringSplitOptions options = removeEmptyEntries ?
        StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
      IEnumerable<string> tokens = str.Split(new[] { ',', ';', '|', '\r', '\n' }, options);
      return tokens.ToList();
    }
  }
}
