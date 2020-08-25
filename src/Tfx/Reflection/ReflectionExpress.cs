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
using System.Linq;
using System.Reflection;

namespace Xyz.TForce.Reflection
{

  public class ReflectionExpress
  {

    public static bool? GetBool<TObject>(TObject @object, string propertyPath)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return null;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(bool))
      {
        return (bool)value;
      }
      else if (valueType == typeof(bool?))
      {
        return (bool?)value;
      }
      return null;
    }

    public static bool GetBool<TObject>(TObject @object, string propertyPath, bool defaultValue)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return defaultValue;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(bool))
      {
        return (bool)value;
      }
      else if (valueType == typeof(bool?))
      {
        return ((bool?)value).Value;
      }
      return defaultValue;
    }

    public static int? GetInteger<TObject>(TObject @object, string propertyPath)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return null;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(int))
      {
        return (int)value;
      }
      else if (valueType == typeof(int?))
      {
        return (int?)value;
      }
      return null;
    }

    public static int GetInteger<TObject>(TObject @object, string propertyPath, int defaultValue)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return defaultValue;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(int))
      {
        return (int)value;
      }
      else if (valueType == typeof(int?))
      {
        return ((int?)value).Value;
      }
      return defaultValue;
    }

    public static long? GetInteger64<TObject>(TObject @object, string propertyPath)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return null;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(long))
      {
        return (long)value;
      }
      else if (valueType == typeof(long?))
      {
        return (long?)value;
      }
      return null;
    }

    public static long GetInteger64<TObject>(TObject @object, string propertyPath, long defaultValue)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return defaultValue;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(long))
      {
        return (long)value;
      }
      else if (valueType == typeof(long?))
      {
        return ((long?)value).Value;
      }
      return defaultValue;
    }

    public static float? GetSingle<TObject>(TObject @object, string propertyPath)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return null;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(float))
      {
        return (float)value;
      }
      else if (valueType == typeof(float?))
      {
        return (float?)value;
      }
      return null;
    }

    public static float GetSingle<TObject>(TObject @object, string propertyPath, float defaultValue)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return defaultValue;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(float))
      {
        return (float)value;
      }
      else if (valueType == typeof(float?))
      {
        return ((float?)value).Value;
      }
      return defaultValue;
    }

    public static double? GetDouble<TObject>(TObject @object, string propertyPath)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return null;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(double))
      {
        return (double)value;
      }
      else if (valueType == typeof(double?))
      {
        return (double?)value;
      }
      return null;
    }

    public static double GetDouble<TObject>(TObject @object, string propertyPath, double defaultValue)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return defaultValue;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(double))
      {
        return (double)value;
      }
      else if (valueType == typeof(double?))
      {
        return ((double?)value).Value;
      }
      return defaultValue;
    }

    public static DateTime? GetDateTime<TObject>(TObject @object, string propertyPath)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return null;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(DateTime))
      {
        return (DateTime)value;
      }
      else if (valueType == typeof(DateTime?))
      {
        return (DateTime?)value;
      }
      return null;
    }

    public static DateTime GetDateTime<TObject>(TObject @object, string propertyPath, DateTime defaultValue)
    {
      object value = GetValue(@object, propertyPath);
      if (value == null)
      {
        return defaultValue;
      }
      Type valueType = value.GetType();
      if (valueType == typeof(DateTime))
      {
        return (DateTime)value;
      }
      else if (valueType == typeof(DateTime?))
      {
        return ((DateTime?)value).Value;
      }
      return defaultValue;
    }

    /// <summary>
    /// Get value of a property using their name
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="object"></param>
    /// <param name="propertyPath"></param>
    /// <returns></returns>
    public static object GetValue<TObject>(TObject @object, string propertyPath)
    {
      if (string.IsNullOrEmpty(propertyPath))
      {
        throw new ArgumentNullException(nameof(propertyPath));
      }

      string[] propertyPathParts = propertyPath.Split(new char[] { '.' })
          .ToArray();

      Type type = typeof(TObject);
      object value = @object;
      for (int i = 0; i < propertyPathParts.Length; i++)
      {
        string propertyName = propertyPathParts[i];
        PropertyInfo property = type.GetProperty(propertyName);
        if (property == null || value == null)
        {
          value = null;
          break;
        }
        else
        {
          type = property.PropertyType;
          value = property.GetValue(value, null);
        }
      }
      return value;
    }
  }
}
