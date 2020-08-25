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

using System.Text;
using Xyz.TForce.Text;

namespace System
{

  /// <summary>
  /// Extension to convert between bytes and string, hex, base64
  /// </summary>
  public static class BinaryConversionExtensions
  {

    #region bytes <-> base64
    /// <summary>
    /// Convert current bytes to base64 representation
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string ToBase64(this byte[] bytes)
    {
      if (bytes == null)
      {
        return null;
      }
      string base64String = Convert.ToBase64String(bytes);
      return base64String;
    }

    /// <summary>
    /// Treat current string as base64 string and convert it to bytes
    /// </summary>
    /// <param name="base64Str"></param>
    /// <returns></returns>
    public static byte[] AsBase64ToBytes(this string base64Str)
    {
      if (base64Str == null)
      {
        return null;
      }
      byte[] strBytes = Convert.FromBase64String(base64Str);
      return strBytes;
    }
    #endregion

    #region bytes <-> hex
    /// <summary>
    /// Convert current bytes to hex representation
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string ToHex(this byte[] bytes)
    {
      if (bytes == null)
      {
        return null;
      }
      char[] lookupArray = LookupArrays.NbaseLookupLower;
      StringBuilder hexBuilder = new StringBuilder();
      foreach (byte @byte in bytes)
      {
        int byteValue = (int)@byte;
        int firstCharCode = byteValue / 16;
        int secondCharCode = byteValue % 16;
        _ = hexBuilder.Append(lookupArray[firstCharCode]);
        _ = hexBuilder.Append(lookupArray[secondCharCode]);
      }
      string hexStr = hexBuilder.ToString();
      return hexStr;
    }

    /// <summary>
    /// Treat current string as hexadecimal string and convert it to bytes
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static byte[] AsHexToBytes(this string str)
    {
      if (str == null)
      {
        return null;
      }
      string hexStr = str.Length % 2 == 0 ? str : "0" + str;
      int iterationCount = hexStr.Length / 2;
      byte[] strBytes = new byte[iterationCount];
      for (int i = 0; i < iterationCount; i++)
      {
        string segment = hexStr.Substring(i * 2, 2);
        int byteCode = segment.AsHexToInteger(0);
        strBytes[i] = (byte)byteCode;
      }
      return strBytes;
    }
    #endregion

    #region bytes <-> string
    /// <summary>
    /// Convert current bytes to string representation. UTF8 is used by default if not specified
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="targetEncoding">Encoding to use to get string</param>
    /// <returns></returns>
    public static string ToString(this byte[] bytes, Encoding targetEncoding = null)
    {
      if (bytes == null)
      {
        return null;
      }
      string str = targetEncoding == null ? Encoding.UTF8.GetString(bytes) : targetEncoding.GetString(bytes);
      return str;
    }

    /// <summary>
    /// Cast current string to equivalent bytes representation. UTF8 is used by default if not specified
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sourceEncoding">Encoding to use to get bytes</param>
    /// <returns></returns>
    public static byte[] ToBytes(this string str, Encoding sourceEncoding = null)
    {
      if (str == null)
      {
        return null;
      }
      byte[] bytes = sourceEncoding == null ? Encoding.UTF8.GetBytes(str) : sourceEncoding.GetBytes(str);
      return bytes;
    }
    #endregion

    #region string <-> base64
    /// <summary>
    /// Cast current string to equivalent base64 representation. UTF8 is used by default if not specified
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sourceEncoding">Encoding to use to get bytes</param>
    /// <returns></returns>
    public static string ToBase64(this string str, Encoding sourceEncoding = null)
    {
      byte[] strBytes = str.ToBytes(sourceEncoding);
      string base64Str = Convert.ToBase64String(strBytes, 0, strBytes.Length);
      return base64Str;
    }

    /// <summary>
    /// Treat current string as base64 string and convert it to string
    /// </summary>
    /// <param name="base64Str"></param>
    /// <param name="targetEncoding">Encoding to use to get string</param>
    /// <returns></returns>
    public static string AsBase64ToString(this string base64Str, Encoding targetEncoding = null)
    {
      byte[] strBytes = Convert.FromBase64String(base64Str);
      string str = strBytes.ToString(targetEncoding);
      return str;
    }
    #endregion

    #region string <-> hex
    /// <summary>
    /// Cast current string to equivalent base64 representation. UTF8 is used by default if not specified
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sourceEncoding">Encoding to use to get bytes</param>
    /// <returns></returns>
    public static string ToHex(this string str, Encoding sourceEncoding = null)
    {
      byte[] strBytes = str.ToBytes(sourceEncoding);
      string hex = strBytes.ToHex();
      return hex;
    }

    public static string AsHexToString(this string hex, Encoding targetEncoding = null)
    {
      byte[] strBytes = hex.AsHexToBytes();
      string str = strBytes.ToString(targetEncoding);
      return str;
    }
    #endregion
  }
}
