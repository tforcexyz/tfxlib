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

using System;
using Xyz.TForce.Collections.Specialized;

namespace Xyz.TForce.Reflection
{

  public static class ConvertExpress
  {

    private static readonly Lazy<MappingDictionary> s_mappingDictionary = new Lazy<MappingDictionary>(InitMappingDictionary);

    private static MappingDictionary InitMappingDictionary()
    {
      MappingDictionary dictionary = new MappingDictionary();
      dictionary.SetMapping<bool, bool?>(BoolToBool);
      dictionary.SetMapping<bool?, bool>(BoolToBool);
      dictionary.SetMapping<bool, double>(BoolToDouble);
      dictionary.SetMapping<bool?, double>(BoolToDouble);
      dictionary.SetMapping<bool, double?>(BoolToDouble);
      dictionary.SetMapping<bool?, double?>(BoolToDouble);
      dictionary.SetMapping<bool, float>(BoolToSingle);
      dictionary.SetMapping<bool?, float>(BoolToSingle);
      dictionary.SetMapping<bool, float?>(BoolToSingle);
      dictionary.SetMapping<bool?, float?>(BoolToSingle);
      dictionary.SetMapping<bool, int>(BoolToInteger);
      dictionary.SetMapping<bool?, int>(BoolToInteger);
      dictionary.SetMapping<bool, int?>(BoolToInteger);
      dictionary.SetMapping<bool?, int?>(BoolToInteger);
      dictionary.SetMapping<bool, long>(BoolToInteger64);
      dictionary.SetMapping<bool?, long>(BoolToInteger64);
      dictionary.SetMapping<bool, long?>(BoolToInteger64);
      dictionary.SetMapping<bool?, long?>(BoolToInteger64);
      dictionary.SetMapping<bool, string>(ObjectToString);
      dictionary.SetMapping<bool?, string>(ObjectToString);
      dictionary.SetMapping<double, double?>(DoubleToDouble);
      dictionary.SetMapping<double?, double>(DoubleToDouble);
      dictionary.SetMapping<double, bool>(DoubleToBool);
      dictionary.SetMapping<double?, bool>(DoubleToBool);
      dictionary.SetMapping<double, bool?>(DoubleToBool);
      dictionary.SetMapping<double?, bool?>(DoubleToBool);
      dictionary.SetMapping<double, float>(DoubleToSingle);
      dictionary.SetMapping<double?, float>(DoubleToSingle);
      dictionary.SetMapping<double, float?>(DoubleToSingle);
      dictionary.SetMapping<double?, float?>(DoubleToSingle);
      dictionary.SetMapping<double, int>(DoubleToInteger);
      dictionary.SetMapping<double?, int>(DoubleToInteger);
      dictionary.SetMapping<double, int?>(DoubleToInteger);
      dictionary.SetMapping<double?, int?>(DoubleToInteger);
      dictionary.SetMapping<double, long>(DoubleToInteger64);
      dictionary.SetMapping<double?, long>(DoubleToInteger64);
      dictionary.SetMapping<double, long?>(DoubleToInteger64);
      dictionary.SetMapping<double?, long?>(DoubleToInteger64);
      dictionary.SetMapping<double, string>(ObjectToString);
      dictionary.SetMapping<double?, string>(ObjectToString);
      dictionary.SetMapping<DateTime, DateTime?>(DateTimeToDateTime);
      dictionary.SetMapping<DateTime?, DateTime>(DateTimeToDateTime);
      dictionary.SetMapping<DateTime, int>(DateTimeToInteger);
      dictionary.SetMapping<DateTime?, int>(DateTimeToInteger);
      dictionary.SetMapping<DateTime, int?>(DateTimeToInteger);
      dictionary.SetMapping<DateTime?, int?>(DateTimeToInteger);
      dictionary.SetMapping<DateTime, long>(DateTimeToInteger64);
      dictionary.SetMapping<DateTime?, long>(DateTimeToInteger64);
      dictionary.SetMapping<DateTime, long?>(DateTimeToInteger64);
      dictionary.SetMapping<DateTime?, long?>(DateTimeToInteger64);
      dictionary.SetMapping<DateTime, string>(DateTimeToString);
      dictionary.SetMapping<DateTime?, string>(DateTimeToString);
      dictionary.SetMapping<float, float?>(SingleToSingle);
      dictionary.SetMapping<float?, float>(SingleToSingle);
      dictionary.SetMapping<float, bool>(SingleToBool);
      dictionary.SetMapping<float?, bool>(SingleToBool);
      dictionary.SetMapping<float, bool?>(SingleToBool);
      dictionary.SetMapping<float?, bool?>(SingleToBool);
      dictionary.SetMapping<float, double>(SingleToDouble);
      dictionary.SetMapping<float?, double>(SingleToDouble);
      dictionary.SetMapping<float, double?>(SingleToDouble);
      dictionary.SetMapping<float?, double?>(SingleToDouble);
      dictionary.SetMapping<float, int>(SingleToInteger);
      dictionary.SetMapping<float?, int>(SingleToInteger);
      dictionary.SetMapping<float, int?>(SingleToInteger);
      dictionary.SetMapping<float?, int?>(SingleToInteger);
      dictionary.SetMapping<float, long>(SingleToInteger64);
      dictionary.SetMapping<float?, long>(SingleToInteger64);
      dictionary.SetMapping<float, long?>(SingleToInteger64);
      dictionary.SetMapping<float?, long?>(SingleToInteger64);
      dictionary.SetMapping<float, string>(ObjectToString);
      dictionary.SetMapping<float?, string>(ObjectToString);
      dictionary.SetMapping<int, int?>(IntegerToInteger);
      dictionary.SetMapping<int?, int>(IntegerToInteger);
      dictionary.SetMapping<int, bool>(IntegerToBool);
      dictionary.SetMapping<int?, bool>(IntegerToBool);
      dictionary.SetMapping<int, bool?>(IntegerToBool);
      dictionary.SetMapping<int?, bool?>(IntegerToBool);
      dictionary.SetMapping<int, DateTime>(IntegerToDateTime);
      dictionary.SetMapping<int?, DateTime>(IntegerToDateTime);
      dictionary.SetMapping<int, DateTime?>(IntegerToDateTime);
      dictionary.SetMapping<int?, DateTime?>(IntegerToDateTime);
      dictionary.SetMapping<int, double>(IntegerToDouble);
      dictionary.SetMapping<int?, double>(IntegerToDouble);
      dictionary.SetMapping<int, double?>(IntegerToDouble);
      dictionary.SetMapping<int?, double?>(IntegerToDouble);
      dictionary.SetMapping<int, float>(IntegerToSingle);
      dictionary.SetMapping<int?, float>(IntegerToSingle);
      dictionary.SetMapping<int, float?>(IntegerToSingle);
      dictionary.SetMapping<int?, float?>(IntegerToSingle);
      dictionary.SetMapping<int, long>(IntegerToInteger64);
      dictionary.SetMapping<int?, long>(IntegerToInteger64);
      dictionary.SetMapping<int, long?>(IntegerToInteger64);
      dictionary.SetMapping<int?, long?>(IntegerToInteger64);
      dictionary.SetMapping<int, string>(ObjectToString);
      dictionary.SetMapping<int?, string>(ObjectToString);
      dictionary.SetMapping<int, TimeSpan>(IntegerToTimeSpan);
      dictionary.SetMapping<int?, TimeSpan>(IntegerToTimeSpan);
      dictionary.SetMapping<int, TimeSpan?>(IntegerToTimeSpan);
      dictionary.SetMapping<int?, TimeSpan?>(IntegerToTimeSpan);
      dictionary.SetMapping<long, TimeSpan>(IntegerToTimeSpan);
      dictionary.SetMapping<long?, TimeSpan>(IntegerToTimeSpan);
      dictionary.SetMapping<long, TimeSpan?>(IntegerToTimeSpan);
      dictionary.SetMapping<long?, TimeSpan?>(IntegerToTimeSpan);
      dictionary.SetMapping<long, long?>(Integer64ToInteger64);
      dictionary.SetMapping<long?, long>(Integer64ToInteger64);
      dictionary.SetMapping<long, bool>(Integer64ToBool);
      dictionary.SetMapping<long?, bool>(Integer64ToBool);
      dictionary.SetMapping<long, bool?>(Integer64ToBool);
      dictionary.SetMapping<long?, bool?>(Integer64ToBool);
      dictionary.SetMapping<long, DateTime>(Integer64ToDateTime);
      dictionary.SetMapping<long?, DateTime>(Integer64ToDateTime);
      dictionary.SetMapping<long, DateTime?>(Integer64ToDateTime);
      dictionary.SetMapping<long?, DateTime?>(Integer64ToDateTime);
      dictionary.SetMapping<long, double>(Integer64ToDouble);
      dictionary.SetMapping<long?, double>(Integer64ToDouble);
      dictionary.SetMapping<long, double?>(Integer64ToDouble);
      dictionary.SetMapping<long?, double?>(Integer64ToDouble);
      dictionary.SetMapping<long, float>(Integer64ToSingle);
      dictionary.SetMapping<long?, float>(Integer64ToSingle);
      dictionary.SetMapping<long, float?>(Integer64ToSingle);
      dictionary.SetMapping<long?, float?>(Integer64ToSingle);
      dictionary.SetMapping<long, int>(Integer64ToInteger);
      dictionary.SetMapping<long?, int>(Integer64ToInteger);
      dictionary.SetMapping<long, int?>(Integer64ToInteger);
      dictionary.SetMapping<long?, int?>(Integer64ToInteger);
      dictionary.SetMapping<long, string>(ObjectToString);
      dictionary.SetMapping<long?, string>(ObjectToString);
      dictionary.SetMapping<long, TimeSpan>(Integer64ToTimeSpan);
      dictionary.SetMapping<long?, TimeSpan>(Integer64ToTimeSpan);
      dictionary.SetMapping<long, TimeSpan?>(Integer64ToTimeSpan);
      dictionary.SetMapping<long?, TimeSpan?>(Integer64ToTimeSpan);
      dictionary.SetMapping<string, bool>(StringToBool);
      dictionary.SetMapping<string, bool?>(StringToBool);
      dictionary.SetMapping<string, double>(StringToDouble);
      dictionary.SetMapping<string, double?>(StringToDouble);
      dictionary.SetMapping<string, DateTime>(StringToDateTime);
      dictionary.SetMapping<string, DateTime?>(StringToDateTime);
      dictionary.SetMapping<string, float>(StringToSingle);
      dictionary.SetMapping<string, float?>(StringToSingle);
      dictionary.SetMapping<string, int>(StringToInteger);
      dictionary.SetMapping<string, int?>(StringToInteger);
      dictionary.SetMapping<string, long>(StringToInteger64);
      dictionary.SetMapping<string, long?>(StringToInteger64);
      dictionary.SetMapping<string, TimeSpan>(StringToTimeSpan);
      dictionary.SetMapping<string, TimeSpan?>(StringToTimeSpan);
      dictionary.SetMapping<TimeSpan, TimeSpan?>(TimeSpanToTimeSpan);
      dictionary.SetMapping<TimeSpan?, TimeSpan>(TimeSpanToTimeSpan);
      dictionary.SetMapping<TimeSpan, int>(TimeSpanToInteger);
      dictionary.SetMapping<TimeSpan?, int>(TimeSpanToInteger);
      dictionary.SetMapping<TimeSpan, int?>(TimeSpanToInteger);
      dictionary.SetMapping<TimeSpan?, int?>(TimeSpanToInteger);
      dictionary.SetMapping<TimeSpan, long>(TimeSpanToInteger64);
      dictionary.SetMapping<TimeSpan?, long>(TimeSpanToInteger64);
      dictionary.SetMapping<TimeSpan, long?>(TimeSpanToInteger64);
      dictionary.SetMapping<TimeSpan?, long?>(TimeSpanToInteger64);
      dictionary.SetMapping<TimeSpan, string>(TimeSpanToString);
      dictionary.SetMapping<TimeSpan?, string>(TimeSpanToString);
      return dictionary;
    }

    public static bool CanConvert(Type sourceType, Type targetType)
    {
      if (sourceType == targetType)
      {
        return true;
      }
      return s_mappingDictionary.Value.IsSupported(sourceType, targetType);
    }

    public static object Convert(object value, Type targetType)
    {
      if (value == null)
      {
        return null;
      }
      Type sourceType = value.GetType();
      if (sourceType == targetType)
      {
        return value;
      }
      Convert<object, object> func = s_mappingDictionary.Value.GetMapping(sourceType, targetType);
      if (func != null)
      {
        object convertedValue = func(value);
        return convertedValue;
      }
      return null;
    }

    public static TResult Convert<TResult>(object value)
    {
      Type targetType = typeof(TResult);
      if (value == null)
      {
        if (TypeExpress.AllowNullValue(targetType))
        {
          return default(TResult);
        }
        throw new InvalidCastException();
      }
      Type sourceType = value.GetType();
      if (sourceType == targetType)
      {
        return (TResult)value;
      }
      Convert<object, object> func = s_mappingDictionary.Value.GetMapping(sourceType, targetType);
      if (func != null)
      {
        object convertedValue = func(value);
        if (convertedValue == null)
        {
          if (TypeExpress.AllowNullValue(targetType))
          {
            return default(TResult);
          }
        }
        else
        {
          return (TResult)convertedValue;
        }
      }
      throw new InvalidCastException();
      ;
    }

    internal static string ObjectToString(object value)
    {
      return value.ToString();
    }

    internal static object BoolToBool(object value)
    {
      return value;
    }

    internal static object BoolToDouble(object value)
    {
      bool boolValue = (bool)value;
      return boolValue ? 1D : 0D;
    }

    internal static object BoolToSingle(object value)
    {
      bool boolValue = (bool)value;
      return boolValue ? 1F : 0F;
    }

    internal static object BoolToInteger(object value)
    {
      bool boolValue = (bool)value;
      return boolValue ? 1 : 0;
    }

    internal static object BoolToInteger64(object value)
    {
      bool boolValue = (bool)value;
      return boolValue ? 1L : 0L;
    }

    internal static object DateTimeToDateTime(object value)
    {
      return value;
    }

    internal static object DateTimeToInteger(object value)
    {
      return (int)((DateTime)value).ToEpoch();
    }

    internal static object DateTimeToInteger64(object value)
    {
      return ((DateTime)value).ToSuperEpoch();
    }

    internal static object DateTimeToString(object value)
    {
      return ((DateTime)value).ToString("O");
    }

    internal static object DoubleToDouble(object value)
    {
      return value;
    }

    internal static object DoubleToBool(object value)
    {
      double doubleValue = (double)value;
      return Math.Round(doubleValue) != 0D;
    }

    public static object DoubleToSingle(object value)
    {
      return (float)(double)value;
    }

    public static object DoubleToInteger(object value)
    {
      return (int)(double)value;
    }

    public static object DoubleToInteger64(object value)
    {
      return (long)(double)value;
    }

    internal static object SingleToSingle(object value)
    {
      return value;
    }

    internal static object SingleToBool(object value)
    {
      float floatValue = (float)value;
      return Math.Round(floatValue) != 0D;
    }

    public static object SingleToDouble(object value)
    {
      return (double)(float)value;
    }

    public static object SingleToInteger(object value)
    {
      return (int)(float)value;
    }

    public static object SingleToInteger64(object value)
    {
      return (long)(float)value;
    }

    public static object IntegerToInteger(object value)
    {
      return value;
    }

    public static object IntegerToBool(object value)
    {
      int integerValue = (int)value;
      return integerValue != 0;
    }

    public static object IntegerToDateTime(object value)
    {
      return ((long)(int)value).AsEpochToDateTime();
    }

    public static object IntegerToDouble(object value)
    {
      return (double)(int)value;
    }

    public static object IntegerToSingle(object value)
    {
      return (float)(int)value;
    }

    public static object IntegerToInteger64(object value)
    {
      return (long)(int)value;
    }

    public static object IntegerToTimeSpan(object value)
    {
      TimeSpan timespan = ((long)value).AsSecondToTimeSpan();
      return timespan;
    }

    public static object Integer64ToInteger64(object value)
    {
      return value;
    }

    public static object Integer64ToBool(object value)
    {
      long integer64Value = (long)value;
      return integer64Value != 0L;
    }

    public static object Integer64ToDateTime(object value)
    {
      return ((long)value).AsSuperEpochToDateTime();
    }

    public static object Integer64ToDouble(object value)
    {
      return (double)(long)value;
    }

    public static object Integer64ToSingle(object value)
    {
      return (float)(long)value;
    }

    public static object Integer64ToInteger(object value)
    {
      return (int)(long)value;
    }

    public static object Integer64ToTimeSpan(object value)
    {
      TimeSpan timespan = ((long)value).AsTickToTimeSpan();
      return timespan;
    }

    internal static object StringToBool(object value)
    {
      return ((string)value).ToBool();
    }

    internal static object StringToDateTime(object value)
    {
      return ((string)value).ToDateTime();
    }

    internal static object StringToDouble(object value)
    {
      return ((string)value).ToDouble();
    }

    internal static object StringToSingle(object value)
    {
      return ((string)value).ToSingle();
    }

    internal static object StringToInteger(object value)
    {
      return ((string)value).ToInteger();
    }

    internal static object StringToInteger64(object value)
    {
      return ((string)value).ToInteger64();
    }

    internal static object StringToTimeSpan(object value)
    {
      return ((string)value).ToDateTime();
    }

    internal static object TimeSpanToTimeSpan(object value)
    {
      return ((string)value).ToTimeSpan();
    }

    internal static object TimeSpanToInteger(object value)
    {
      return (int)((TimeSpan)value).TotalSeconds;
    }

    internal static object TimeSpanToInteger64(object value)
    {
      return ((TimeSpan)value).Ticks;
    }

    internal static object TimeSpanToString(object value)
    {
      return ((TimeSpan)value).ToString("G");
    }
  }
}
