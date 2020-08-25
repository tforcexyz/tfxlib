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
using Xyz.TForce.Net;
using Xyz.TForce.Text;

namespace System
{

  /// <summary>
  /// Work with encoded url
  /// </summary>
  public static class UrlExtensions
  {

    /// <summary>
    /// Encode current string to URL encoding format
    /// </summary>
    /// <param name="str"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    public static string ToUrlEncoded(this string str, UrlEncodeMethod method = UrlEncodeMethod.Html5)
    {
      char[] lookupArray = LookupArrays.NbaseLookupUpper;
      StringBuilder urlEncodedBuilder = new StringBuilder();
      foreach (char chr in str)
      {
        int charCode = (int)chr;
        if (97 <= charCode && charCode <= 122) // lowercase
        {
          _ = urlEncodedBuilder.Append(chr);
        }
        else if (65 <= charCode && charCode <= 90) // uppercase
        {
          _ = urlEncodedBuilder.Append(chr);
        }
        else if (48 <= charCode && charCode <= 57) // number
        {
          _ = urlEncodedBuilder.Append(chr);
        }
        else if ((charCode == 32) // space
            && method == UrlEncodeMethod.Html5)
        {
          _ = urlEncodedBuilder.Append('+');
        }
        else if ((charCode == 46 || charCode == 45 || charCode == 95 || charCode == 42)  // .-_*
            && method == UrlEncodeMethod.Html5)
        {
          _ = urlEncodedBuilder.Append(chr);
        }
        else
        {
          int firstCharCode = charCode / 16;
          int secondCharCode = charCode % 16;
          _ = urlEncodedBuilder.Append('%');
          _ = urlEncodedBuilder.Append(lookupArray[firstCharCode]);
          _ = urlEncodedBuilder.Append(lookupArray[secondCharCode]);
        }
      }
      string urlEncodedString = urlEncodedBuilder.ToString();
      return urlEncodedString;
    }


    /// <summary>
    /// Decode URL encoding content
    /// </summary>
    /// <param name="urlEncodedString"></param>
    /// <returns></returns>
    public static string AsUrlEncodedToString(this string urlEncodedString)
    {
      StringBuilder strBuilder = new StringBuilder();
      int length = urlEncodedString.Length;
      for (int i = 0; i < length; i++)
      {
        char chr = urlEncodedString[i];
        if (chr == '%')
        {
          string encodedSegment = urlEncodedString.Substring(i + 1, 2);
          int idx = encodedSegment.AsHexToInteger(-1);
          if (idx > -1)
          {
            _ = strBuilder.Append((char)idx);
            i += 2;
            continue;
          }
        }
        else if (chr == '+')
        {
          _ = strBuilder.Append(' ');
          continue;
        }
        _ = strBuilder.Append(chr);
      }
      string str = strBuilder.ToString();
      return str;
    }
  }
}
