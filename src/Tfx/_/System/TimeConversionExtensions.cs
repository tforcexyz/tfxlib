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

using System.Globalization;
using Xyz.TForce;

namespace System
{

  /// <summary>
  /// Extension to convert between DateTime and string, timestamp
  /// </summary>
  public static class TimeConversionExtensions
  {

    #region string -> DateTime
    /// <summary>
    /// Cast current string to DateTime. Return null if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static DateTime? ToDateTime(this string str)
    {
      DateTime result;
      if (DateTime.TryParse(str, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to DateTime. Return defValue if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string str, DateTime defValue)
    {
      DateTime result;
      if (DateTime.TryParse(str, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to DateTime using specified format. Return null if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format">C# DateTime format</param>
    /// <returns></returns>
    public static DateTime? ToDateTime(this string str, string format)
    {
      DateTime result;
      if (DateTime.TryParseExact(str, new[] { format }, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to DateTime using specified format. Return null if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <returns></returns>
    public static DateTime? ToDateTime(this string str, string format1, string format2)
    {
      DateTime result;
      if (DateTime.TryParseExact(str, new[] { format1, format2 }, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to DateTime using specified format. Return null if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <param name="format3">Third format to try</param>
    /// <returns></returns>
    public static DateTime? ToDateTime(this string str, string format1, string format2, string format3)
    {
      DateTime result;
      if (DateTime.TryParseExact(str, new[] { format1, format2 }, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to DateTime using specified format. Return null if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <param name="format3">Third format to try</param>
    /// <param name="format4">Fourth format to try</param>
    /// <returns></returns>
    public static DateTime? ToDateTime(this string str, string format1, string format2, string format3, string format4)
    {
      DateTime result;
      if (DateTime.TryParseExact(str, new[] { format1, format2 }, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to DateTime using specified format. Return null if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="formats">Formats to try</param>
    /// <returns></returns>
    public static DateTime? ToDateTime(this string str, string[] formats)
    {
      DateTime result;
      if (DateTime.TryParseExact(str, formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to DateTime using specified format. Return defValue if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format">C# DateTime format</param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string str, string format, DateTime defValue)
    {
      DateTime result;
      if (DateTime.TryParseExact(str, new[] { format }, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to DateTime using specified format. Return defValue if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string str, string format1, string format2, DateTime defValue)
    {
      DateTime result;
      if (DateTime.TryParseExact(str, new[] { format1, format2 }, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to DateTime using specified format. Return defValue if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <param name="format3">Third format to try</param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string str, string format1, string format2, string format3, DateTime defValue)
    {
      DateTime result;
      if (DateTime.TryParseExact(str, new[] { format1, format2, format3 }, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to DateTime using specified format. Return defValue if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <param name="format3">Third format to try</param>
    /// <param name="format4">Fourth format to try</param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string str, string format1, string format2, string format3, string format4, DateTime defValue)
    {
      DateTime result;
      if (DateTime.TryParseExact(str, new[] { format1, format2, format3, format4 }, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to DateTime using specified format. Return defValue if the string is an invalid DateTime string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="formats">Formats to try</param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string str, string[] formats, DateTime defValue)
    {
      DateTime result;
      if (DateTime.TryParseExact(str, formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) == false)
      {
        return defValue;
      }
      return result;
    }
    #endregion

    #region string -> TimeSpan
    /// <summary>
    /// Cast current string to TimeSpan. Return null if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static TimeSpan? ToTimeSpan(this string str)
    {
      TimeSpan result;
      if (TimeSpan.TryParse(str, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to TimeSpan. Return defValue if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static TimeSpan ToTimeSpan(this string str, TimeSpan defValue)
    {
      TimeSpan result;
      if (TimeSpan.TryParse(str, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to TimeSpan using specified format. Return null if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format">C# TimeSpan format</param>
    /// <returns></returns>
    public static TimeSpan? ToTimeSpan(this string str, string format)
    {
      TimeSpan result;
      if (TimeSpan.TryParseExact(str, new[] { format }, CultureInfo.InvariantCulture, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to TimeSpan using specified format. Return null if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <returns></returns>
    public static TimeSpan? ToTimeSpan(this string str, string format1, string format2)
    {
      TimeSpan result;
      if (TimeSpan.TryParseExact(str, new[] { format1, format2 }, CultureInfo.InvariantCulture, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to TimeSpan using specified format. Return null if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <param name="format3">Third format to try</param>
    /// <returns></returns>
    public static TimeSpan? ToTimeSpan(this string str, string format1, string format2, string format3)
    {
      TimeSpan result;
      if (TimeSpan.TryParseExact(str, new[] { format1, format2 }, CultureInfo.InvariantCulture, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to TimeSpan using specified format. Return null if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <param name="format3">Third format to try</param>
    /// <param name="format4">Fourth format to try</param>
    /// <returns></returns>
    public static TimeSpan? ToTimeSpan(this string str, string format1, string format2, string format3, string format4)
    {
      TimeSpan result;
      if (TimeSpan.TryParseExact(str, new[] { format1, format2 }, CultureInfo.InvariantCulture, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to TimeSpan using specified format. Return null if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="formats">Formats to try</param>
    /// <returns></returns>
    public static TimeSpan? ToTimeSpan(this string str, string[] formats)
    {
      TimeSpan result;
      if (TimeSpan.TryParseExact(str, formats, CultureInfo.InvariantCulture, out result) == false)
      {
        return null;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to TimeSpan using specified format. Return defValue if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format">C# TimeSpan format</param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static TimeSpan ToTimeSpan(this string str, string format, TimeSpan defValue)
    {
      TimeSpan result;
      if (TimeSpan.TryParseExact(str, new[] { format }, CultureInfo.InvariantCulture, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to TimeSpan using specified format. Return defValue if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static TimeSpan ToTimeSpan(this string str, string format1, string format2, TimeSpan defValue)
    {
      TimeSpan result;
      if (TimeSpan.TryParseExact(str, new[] { format1, format2 }, CultureInfo.InvariantCulture, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to TimeSpan using specified format. Return defValue if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <param name="format3">Third format to try</param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static TimeSpan ToTimeSpan(this string str, string format1, string format2, string format3, TimeSpan defValue)
    {
      TimeSpan result;
      if (TimeSpan.TryParseExact(str, new[] { format1, format2, format3 }, CultureInfo.InvariantCulture, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to TimeSpan using specified format. Return defValue if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="format1">First format to try</param>
    /// <param name="format2">Second format to try</param>
    /// <param name="format3">Third format to try</param>
    /// <param name="format4">Fourth format to try</param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static TimeSpan ToTimeSpan(this string str, string format1, string format2, string format3, string format4, TimeSpan defValue)
    {
      TimeSpan result;
      if (TimeSpan.TryParseExact(str, new[] { format1, format2, format3, format4 }, CultureInfo.InvariantCulture, out result) == false)
      {
        return defValue;
      }
      return result;
    }

    /// <summary>
    /// Cast current string to TimeSpan using specified format. Return defValue if the string is an invalid TimeSpan string.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="formats">Formats to try</param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static TimeSpan ToTimeSpan(this string str, string[] formats, TimeSpan defValue)
    {
      TimeSpan result;
      if (TimeSpan.TryParseExact(str, formats, CultureInfo.InvariantCulture, out result) == false)
      {
        return defValue;
      }
      return result;
    }
    #endregion

    #region DateTime <-> Tick
    /// <summary>
    /// Get equivalent tick from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>timestamp</returns>
    public static long ToTick(this DateTime dateTime)
    {
      DateTime date = dateTime;
      long ticks = date.Ticks;
      return ticks;
    }

    /// <summary>
    /// Get equivalent tick from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>timestamp</returns>
    public static long? ToTick(this DateTime? dateTime)
    {
      if (dateTime == null)
      {
        return null;
      }
      return dateTime.Value.ToTick();
    }

    /// <summary>
    /// Get equivalent timestamp in Utc from current DateTime.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>timestamp in Utc</returns>
    public static long ToTickUtc(this DateTime dateTime)
    {
      DateTime date = dateTime.ToUniversalTime();
      long ticks = date.Ticks;
      return ticks;
    }

    /// <summary>
    /// Get equivalent timestamp in Utc from current DateTime.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>timestamp in Utc</returns>
    public static long? ToTickUtc(this DateTime? dateTime)
    {
      if (dateTime == null)
      {
        return null;
      }
      return dateTime.Value.ToTickUtc();
    }

    /// <summary>
    /// Get local DateTime from C# tick
    /// </summary>
    /// <param name="value">current tick</param>
    /// <param name="isUniversalTime">current tick is universal time</param>
    /// <returns>local DateTime</returns>
    public static DateTime AsTickToDateTime(this long value, bool isUniversalTime = false)
    {
      DateTimeKind datekind = DateTimeKind.Unspecified;
      if (isUniversalTime)
      {
        datekind = DateTimeKind.Utc;
      }
      DateTime result = new DateTime(value, datekind);
      if (isUniversalTime)
      {
        result = result.ToLocalTime();
      }
      return result;
    }

    /// <summary>
    /// Get local DateTime from C# tick
    /// </summary>
    /// <param name="value">current tick</param>
    /// <param name="isUniversalTime">current tick is universal time</param>
    /// <returns>local DateTime</returns>
    public static DateTime? AsTickToDateTime(this long? value, bool isUniversalTime = false)
    {
      if (value == null)
      {
        return null;
      }
      return value.Value.AsTickToDateTime(isUniversalTime);
    }

    /// <summary>
    /// Get Utc DateTime from C# tick
    /// </summary>
    /// <param name="value">current tick</param>
    /// <param name="isUniversalTime">current tick is universal time</param>
    /// <returns>Utc DateTime</returns>
    public static DateTime AsTickToDateTimeUtc(this long value, bool isUniversalTime = false)
    {
      DateTimeKind datekind = DateTimeKind.Local;
      if (isUniversalTime)
      {
        datekind = DateTimeKind.Utc;
      }
      DateTime result = new DateTime(value, datekind);
      if (!isUniversalTime)
      {
        result = result.ToUniversalTime();
      }
      return result;
    }

    /// <summary>
    /// Get Utc DateTime from C# tick
    /// </summary>
    /// <param name="value">current tick</param>
    /// <param name="isUniversalTime">current tick is universal time</param>
    /// <returns>Utc DateTime</returns>
    public static DateTime? AsTickToDateTimeUtc(this long? value, bool isUniversalTime = false)
    {
      if (value == null)
      {
        return null;
      }
      return value.Value.AsTickToDateTimeUtc(isUniversalTime);
    }
    #endregion

    #region DateTime <-> Timestamp
    /// <summary>
    /// Get equivalent timestamp from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>timestamp</returns>
    public static long ToEpoch(this DateTime dateTime)
    {
      DateTime date = dateTime;
      long ticks = date.Ticks - TimeConversions.EpochVsTickOffset;
      long epoch = ticks / TimeConversions.TicksPerSecond;
      return epoch;
    }

    /// <summary>
    /// Get equivalent timestamp from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>timestamp</returns>
    public static long? ToEpoch(this DateTime? dateTime)
    {
      if (dateTime == null)
      {
        return null;
      }
      return dateTime.Value.ToEpoch();
    }

    /// <summary>
    /// Get equivalent timestamp in Utc from current DateTime.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>timestamp in Utc</returns>
    public static long ToEpochUtc(this DateTime dateTime)
    {
      DateTime date = dateTime.ToUniversalTime();
      long ticks = date.Ticks - TimeConversions.EpochVsTickOffset;
      long epoch = ticks / TimeConversions.TicksPerSecond;
      return epoch;
    }

    /// <summary>
    /// Get equivalent timestamp in Utc from current DateTime.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>timestamp in Utc</returns>
    public static long? ToEpochUtc(this DateTime? dateTime)
    {
      if (dateTime == null)
      {
        return null;
      }
      return dateTime.Value.ToEpochUtc();
    }

    /// <summary>
    /// Get local DateTime from Unix timestamp
    /// </summary>
    /// <param name="value">current timestamp</param>
    /// <param name="isUniversalTime">current timestamp is universal time</param>
    /// <returns>local DateTime</returns>
    public static DateTime AsEpochToDateTime(this long value, bool isUniversalTime = false)
    {
      long currentTick = (value * TimeConversions.TicksPerSecond) + TimeConversions.EpochVsTickOffset;
      DateTimeKind datekind = DateTimeKind.Unspecified;
      if (isUniversalTime)
      {
        datekind = DateTimeKind.Utc;
      }
      DateTime result = new DateTime(currentTick, datekind);
      if (isUniversalTime)
      {
        result = result.ToLocalTime();
      }
      return result;
    }

    /// <summary>
    /// Get local DateTime from Unix timestamp
    /// </summary>
    /// <param name="value">current timestamp</param>
    /// <param name="isUniversalTime">current timestamp is universal time</param>
    /// <returns>local DateTime</returns>
    public static DateTime? AsEpochToDateTime(this long? value, bool isUniversalTime = false)
    {
      if (value == null)
      {
        return null;
      }
      return value.Value.AsEpochToDateTime(isUniversalTime);
    }

    /// <summary>
    /// Get Utc DateTime from Unix timestamp
    /// </summary>
    /// <param name="value">current timestamp</param>
    /// <param name="isUniversalTime">current timestamp is universal time</param>
    /// <returns>Utc DateTime</returns>
    public static DateTime AsEpochToDateTimeUtc(this long value, bool isUniversalTime = false)
    {
      long currentTick = (value * TimeConversions.TicksPerSecond) + TimeConversions.EpochVsTickOffset;
      DateTimeKind datekind = DateTimeKind.Local;
      if (isUniversalTime)
      {
        datekind = DateTimeKind.Utc;
      }
      DateTime result = new DateTime(currentTick, datekind);
      if (!isUniversalTime)
      {
        result = result.ToUniversalTime();
      }
      return result;
    }

    /// <summary>
    /// Get Utc DateTime from Unix timestamp
    /// </summary>
    /// <param name="value">current timestamp</param>
    /// <param name="isUniversalTime">current timestamp is universal time</param>
    /// <returns>Utc DateTime</returns>
    public static DateTime? AsEpochToDateTimeUtc(this long? value, bool isUniversalTime = false)
    {
      if (value == null)
      {
        return null;
      }
      return value.Value.AsEpochToDateTimeUtc(isUniversalTime);
    }
    #endregion

    #region DateTime <-> Extended timestamp
    /// <summary>
    /// Get equivalent extended timestamp from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>extended timestamp</returns>
    public static long ToLongEpoch(this DateTime dateTime)
    {
      DateTime date = dateTime;
      long ticks = date.Ticks - TimeConversions.EpochVsTickOffset;
      long longEpoch = ticks / TimeConversions.TicksPerMilisecond;
      return longEpoch;
    }

    /// <summary>
    /// Get equivalent extended timestamp from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>extended timestamp</returns>
    public static long? ToLongEpoch(this DateTime? dateTime)
    {
      if (dateTime == null)
      {
        return null;
      }
      return dateTime.Value.ToLongEpoch();
    }

    /// <summary>
    /// Get equivalent extended timestamp in Utc from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>extended timestamp in Utc</returns>
    public static long ToLongEpochUtc(this DateTime dateTime)
    {
      DateTime date = dateTime.ToUniversalTime();
      long ticks = date.Ticks - TimeConversions.EpochVsTickOffset;
      long longEpoch = ticks / TimeConversions.TicksPerMilisecond;
      return longEpoch;
    }

    /// <summary>
    /// Get equivalent extended timestamp in Utc from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>extended timestamp in Utc</returns>
    public static long? ToLongEpochUtc(this DateTime? dateTime)
    {
      if (dateTime == null)
      {
        return null;
      }
      return dateTime.Value.ToLongEpochUtc();
    }

    /// <summary>
    /// Get local DateTime from extended Unix timestamp
    /// </summary>
    /// <param name="value">current extended timestamp</param>
    /// <param name="isUniversalTime">current extended timestamp is universal time</param>
    /// <returns>local DateTime</returns>
    public static DateTime AsLongEpochToDateTime(this long value, bool isUniversalTime = false)
    {
      long currentTick = (value * TimeConversions.TicksPerMilisecond) + TimeConversions.EpochVsTickOffset;
      DateTimeKind datekind = DateTimeKind.Unspecified;
      if (isUniversalTime)
      {
        datekind = DateTimeKind.Utc;
      }
      DateTime result = new DateTime(currentTick, datekind);
      if (isUniversalTime)
      {
        result = result.ToLocalTime();
      }
      return result;
    }

    /// <summary>
    /// Get local DateTime from extended Unix timestamp
    /// </summary>
    /// <param name="value">current extended timestamp</param>
    /// <param name="isUniversalTime">current extended timestamp is universal time</param>
    /// <returns>local DateTime</returns>
    public static DateTime? AsLongEpochToDateTime(this long? value, bool isUniversalTime = false)
    {
      if (value == null)
      {
        return null;
      }
      return value.Value.AsLongEpochToDateTime(isUniversalTime);
    }

    /// <summary>
    /// Get Utc DateTime from extended Unix timestamp
    /// </summary>
    /// <param name="value">current extended timestamp</param>
    /// <param name="isUniversalTime">current extended timestamp is universal time</param>
    /// <returns>Utc DateTime</returns>
    public static DateTime AsLongEpochToDateTimeUtc(this long value, bool isUniversalTime = false)
    {
      long currentTick = (value * TimeConversions.TicksPerMilisecond) + TimeConversions.EpochVsTickOffset;
      DateTimeKind datekind = DateTimeKind.Local;
      if (isUniversalTime)
      {
        datekind = DateTimeKind.Utc;
      }
      DateTime result = new DateTime(currentTick, datekind);
      if (!isUniversalTime)
      {
        result = result.ToUniversalTime();
      }
      return result;
    }

    /// <summary>
    /// Get Utc DateTime from extended Unix timestamp
    /// </summary>
    /// <param name="value">current extended timestamp</param>
    /// <param name="isUniversalTime">current extended timestamp is universal time</param>
    /// <returns>Utc DateTime</returns>
    public static DateTime? AsLongEpochToDateTimeUtc(this long? value, bool isUniversalTime = false)
    {
      if (value == null)
      {
        return null;
      }
      return value.Value.AsLongEpochToDateTimeUtc(isUniversalTime);
    }
    #endregion

    #region DateTime <-> Super timestamp
    /// <summary>
    /// Get equivalent super timestamp from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>super timestamp</returns>
    public static long ToSuperEpoch(this DateTime dateTime)
    {
      DateTime date = dateTime;
      long ticks = date.Ticks - TimeConversions.EpochVsTickOffset;
      long superEpoch = ticks;
      return superEpoch;
    }

    /// <summary>
    /// Get equivalent super timestamp from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>super timestamp</returns>
    public static long? ToSuperEpoch(this DateTime? dateTime)
    {
      if (dateTime == null)
      {
        return null;
      }
      return dateTime.Value.ToSuperEpoch();
    }

    /// <summary>
    /// Get equivalent super timestamp in Utc from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>super timestamp in Utc</returns>
    public static long ToSuperEpochUtc(this DateTime dateTime)
    {
      DateTime date = dateTime.ToUniversalTime();
      long ticks = date.Ticks - TimeConversions.EpochVsTickOffset;
      long superEpoch = ticks;
      return superEpoch;
    }

    /// <summary>
    /// Get equivalent super timestamp in Utc from current DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>super timestamp in Utc</returns>
    public static long? ToSuperEpochUtc(this DateTime? dateTime)
    {
      if (dateTime == null)
      {
        return null;
      }
      return dateTime.Value.ToSuperEpochUtc();
    }

    /// <summary>
    /// Get local DateTime from super Unix timestamp
    /// </summary>
    /// <param name="value">current super timestamp</param>
    /// <param name="isUniversalTime">current super timestamp is universal time</param>
    /// <returns>local DateTime</returns>
    public static DateTime AsSuperEpochToDateTime(this long value, bool isUniversalTime = false)
    {
      long currentTick = value + TimeConversions.EpochVsTickOffset;
      DateTimeKind datekind = DateTimeKind.Unspecified;
      if (isUniversalTime)
      {
        datekind = DateTimeKind.Utc;
      }
      DateTime result = new DateTime(currentTick, datekind);
      if (isUniversalTime)
      {
        result = result.ToLocalTime();
      }
      return result;
    }

    /// <summary>
    /// Get local DateTime from super Unix timestamp
    /// </summary>
    /// <param name="value">current super timestamp</param>
    /// <param name="isUniversalTime">current super timestamp is universal time</param>
    /// <returns>local DateTime</returns>
    public static DateTime? AsSuperEpochToDateTime(this long? value, bool isUniversalTime = false)
    {
      if (value == null)
      {
        return null;
      }
      return value.Value.AsSuperEpochToDateTime(isUniversalTime);
    }

    /// <summary>
    /// Get Utc DateTime from super Unix timestamp
    /// </summary>
    /// <param name="value">current super timestamp</param>
    /// <param name="isUniversalTime">current super timestamp is universal time</param>
    /// <returns>Utc DateTime</returns>
    public static DateTime AsSuperEpochToDateTimeUtc(this long value, bool isUniversalTime = false)
    {
      long currentTick = value + TimeConversions.EpochVsTickOffset;
      DateTimeKind datekind = DateTimeKind.Local;
      if (isUniversalTime)
      {
        datekind = DateTimeKind.Utc;
      }
      DateTime result = new DateTime(currentTick, datekind);
      if (!isUniversalTime)
      {
        result = result.ToUniversalTime();
      }
      return result;
    }

    /// <summary>
    /// Get Utc DateTime from super Unix timestamp
    /// </summary>
    /// <param name="value">current super timestamp</param>
    /// <param name="isUniversalTime">current super timestamp is universal time</param>
    /// <returns>Utc DateTime</returns>
    public static DateTime? AsSuperEpochToDateTimeUtc(this long? value, bool isUniversalTime = false)
    {
      if (value == null)
      {
        return null;
      }
      return value.Value.AsSuperEpochToDateTimeUtc(isUniversalTime);
    }
    #endregion

    #region TimeSpan <- Second
    /// <summary>
    /// Get Timestamp from seconds
    /// </summary>
    /// <param name="totalSeconds">Total seconds</param>
    /// <returns></returns>
    public static TimeSpan AsSecondToTimeSpan(this long totalSeconds)
    {
      TimeSpan timeSpan = new TimeSpan(0, 0, (int)totalSeconds);
      return timeSpan;
    }

    /// <summary>
    /// Get Timestamp from seconds
    /// </summary>
    /// <param name="totalSeconds">Total seconds</param>
    /// <returns></returns>
    public static TimeSpan? AsSecondToTimeSpan(this long? totalSeconds)
    {
      if (totalSeconds == null)
      {
        return null;
      }
      TimeSpan timeSpan = new TimeSpan(0, 0, (int)totalSeconds.Value);
      return timeSpan;
    }

    /// <summary>
    /// Get Timestamp from ticks
    /// </summary>
    /// <param name="tick">Total ticks</param>
    /// <returns></returns>
    public static TimeSpan AsTickToTimeSpan(this long tick)
    {
      TimeSpan timeSpan = new TimeSpan(tick);
      return timeSpan;
    }

    /// <summary>
    /// Get Timestamp from ticks
    /// </summary>
    /// <param name="tick">Total ticks</param>
    /// <returns></returns>
    public static TimeSpan? AsTickToTimeSpan(this long? tick)
    {
      if (tick == null)
      {
        return null;
      }
      TimeSpan timeSpan = new TimeSpan(tick.Value);
      return timeSpan;
    }
    #endregion

    #region Time range
    /// <summary>
    /// Get the date of today in current timezone
    /// </summary>
    /// <param name="now"></param>
    /// <returns></returns>
    public static DateTime ToToday(this DateTime now)
    {
      DateTime today = now.Date;
      return today;
    }

    /// <summary>
    /// Get the date of today in current timezone
    /// </summary>
    /// <param name="now"></param>
    /// <returns></returns>
    public static DateTime? ToToday(this DateTime? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.ToToday();
    }

    /// <summary>
    /// Get the date of tomorrow in current timezone
    /// </summary>
    /// <param name="now"></param>
    /// <returns></returns>
    public static DateTime ToTomorrow(this DateTime now)
    {
      DateTime today = now.Date;
      DateTime tomorrow = today.AddDays(1);
      return tomorrow;
    }

    /// <summary>
    /// Get the date of tomorrow in current timezone
    /// </summary>
    /// <param name="now"></param>
    /// <returns></returns>
    public static DateTime? ToTomorrow(this DateTime? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.ToTomorrow();
    }

    /// <summary>
    /// Get the date of yesterday in current timezone
    /// </summary>
    /// <param name="now"></param>
    /// <returns></returns>
    public static DateTime ToYesterday(this DateTime now)
    {
      DateTime today = now.Date;
      DateTime yesterday = today.AddDays(-1);
      return yesterday;
    }

    /// <summary>
    /// Get the date of yesterday in current timezone
    /// </summary>
    /// <param name="now"></param>
    /// <returns></returns>
    public static DateTime? ToYesterday(this DateTime? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.ToYesterday();
    }

    public static long AsTickToToday(this long now)
    {
      long tickOfDay = now % TimeConversions.TicksPerDay;
      long todayTick = now - tickOfDay;
      return todayTick;
    }

    public static long? AsTickToToday(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsTickToToday();
    }

    public static long AsTickToTomorrow(this long now)
    {
      long tickOfDay = now % TimeConversions.TicksPerDay;
      long todayTick = now - tickOfDay;
      long tomorrowTick = todayTick + TimeConversions.TicksPerDay;
      return tomorrowTick;
    }

    public static long? AsTickToTomorrow(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsTickToTomorrow();
    }

    public static long AsTickToYesterday(this long now)
    {
      long tickOfDay = now % TimeConversions.TicksPerDay;
      long todayTick = now - tickOfDay;
      long yesterdayTick = todayTick - TimeConversions.TicksPerDay;
      return yesterdayTick;
    }

    public static long? AsTickToYesterday(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsTickToYesterday();
    }

    public static long AsEpochToToday(this long now)
    {
      long secondsOfDay = now % TimeConversions.SecondsPerDay;
      long todayEpoch = now - secondsOfDay;
      return todayEpoch;
    }

    public static long? AsEpochToToday(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsEpochToToday();
    }

    public static long AsEpochToTomorrow(this long now)
    {
      long secondsOfDay = now % TimeConversions.SecondsPerDay;
      long todayEpoch = now - secondsOfDay;
      long tomorrowEpoch = todayEpoch + TimeConversions.SecondsPerDay;
      return tomorrowEpoch;
    }

    public static long? AsEpochToTomorrow(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsEpochToTomorrow();
    }

    public static long AsEpochToYesterday(this long now)
    {
      long secondsOfDay = now % TimeConversions.SecondsPerDay;
      long todayEpoch = now - secondsOfDay;
      long yesterdayEpoch = todayEpoch - TimeConversions.SecondsPerDay;
      return yesterdayEpoch;
    }

    public static long? AsEpochToYesterday(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsEpochToYesterday();
    }

    public static long AsLongEpochToToday(this long now)
    {
      long milisecondsOfDay = now % TimeConversions.MilisecondsPerDay;
      long todayLongEpoch = now - milisecondsOfDay;
      return todayLongEpoch;
    }

    public static long? AsLongEpochToToday(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsLongEpochToToday();
    }

    public static long AsLongEpochToTomorrow(this long now)
    {
      long milisecondsOfDay = now % TimeConversions.MilisecondsPerDay;
      long todayLongEpoch = now - milisecondsOfDay;
      long tomorrowLongEpoch = todayLongEpoch + TimeConversions.MilisecondsPerDay;
      return tomorrowLongEpoch;
    }

    public static long? AsLongEpochToTomorrow(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsLongEpochToTomorrow();
    }

    public static long AsLongEpochToYesterday(this long now)
    {
      long milisecondsOfDay = now % TimeConversions.MilisecondsPerDay;
      long todayLongEpoch = now - milisecondsOfDay;
      long yesterdayLongEpoch = todayLongEpoch - TimeConversions.MilisecondsPerDay;
      return yesterdayLongEpoch;
    }

    public static long? AsLongEpochToYesterday(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsLongEpochToYesterday();
    }

    public static long AsSuperEpochToToday(this long now)
    {
      long tickOfDay = now % TimeConversions.TicksPerDay;
      long todayTick = now - tickOfDay;
      return todayTick;
    }

    public static long? AsSuperEpochToToday(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsSuperEpochToToday();
    }

    public static long AsSuperEpochToTomorrow(this long now)
    {
      long tickOfDay = now % TimeConversions.TicksPerDay;
      long todayTick = now - tickOfDay;
      long tomorrowTick = todayTick + TimeConversions.TicksPerDay;
      return tomorrowTick;
    }

    public static long? AsSuperEpochToTomorrow(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsSuperEpochToTomorrow();
    }

    public static long AsSuperEpochToYesterday(this long now)
    {
      long tickOfDay = now % TimeConversions.TicksPerDay;
      long todayTick = now - tickOfDay;
      long yesterdayTick = todayTick - TimeConversions.TicksPerDay;
      return yesterdayTick;
    }

    public static long? AsSuperEpochToYesterday(this long? now)
    {
      if (now == null)
      {
        return null;
      }
      return now.Value.AsSuperEpochToYesterday();
    }
    #endregion
  }
}
