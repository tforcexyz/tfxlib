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

  public static class MappingExpress
  {

    /// <summary>
    /// Copy all properties from source object to target object
    /// </summary>
    /// <param name="source">Source object</param>
    /// <param name="target">Target object</param>
    public static void CopyProperties(object source, object target)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }
      if (target == null)
      {
        throw new ArgumentNullException(nameof(target));
      }
      Type sourceType = source.GetType();
      PropertyInfo[] sourceProperties = sourceType.GetProperties();
      Type targetType = target.GetType();
      PropertyInfo[] targetProperties = targetType.GetProperties();
      foreach (PropertyInfo sourceProperty in sourceProperties)
      {
        PropertyInfo targetProperty = targetProperties.FirstOrDefault(x => { return x.Name == sourceProperty.Name; });
        if (targetProperty == null)
        {
          // equivalent properties not found
          continue;
        }
        if (!sourceProperty.CanRead || !targetProperty.CanWrite)
        {
          // read/write permission not allow value copy
          continue;
        }

        object value = sourceProperty.GetValue(source, null);
        SetValue(target, targetProperty, value);
      }
    }

    /// <summary>
    /// Copy all properties from source object to target object. Exclude properties in source object those are nulled
    /// </summary>
    /// <param name="source">Source object</param>
    /// <param name="target">Target object</param>
    public static void CopyPropertiesWithValue(object source, object target)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }
      if (target == null)
      {
        throw new ArgumentNullException(nameof(target));
      }
      Type sourceType = source.GetType();
      PropertyInfo[] sourceProperties = sourceType.GetProperties();
      Type targetType = target.GetType();
      PropertyInfo[] targetProperties = targetType.GetProperties();
      foreach (PropertyInfo sourceProperty in sourceProperties)
      {
        PropertyInfo targetProperty = targetProperties.FirstOrDefault(x => { return x.Name == sourceProperty.Name; });
        if (targetProperty == null)
        {
          // equivalent properties not found
          continue;
        }
        if (!sourceProperty.CanRead || !targetProperty.CanWrite)
        {
          // read/write permission not allow value copy
          continue;
        }
        object value = sourceProperty.GetValue(source, null);
        if (value == null)
        {
          // skip null value
          continue;
        }

        SetValue(target, targetProperty, value);
      }
    }

    internal static bool CanSetValue(PropertyInfo sourceProperty, PropertyInfo targetProperty)
    {
      return ConvertExpress.CanConvert(sourceProperty.PropertyType, targetProperty.PropertyType);
    }

    internal static void SetValue(object target, PropertyInfo targetProperty, object value)
    {
      if (value == null)
      {
        if (TypeExpress.AllowNullValue(targetProperty.PropertyType))
        {
          targetProperty.SetValue(target, null);
        }
        return;
      }
      Type sourceType = value.GetType();
      Type targetType = targetProperty.PropertyType;
      if (sourceType == targetType)
      {
        targetProperty.SetValue(target, value);
        return;
      }
      if (ConvertExpress.CanConvert(sourceType, targetType))
      {
        object convertedValue = ConvertExpress.Convert(value, targetType);
        if (convertedValue == null)
        {
          if (TypeExpress.AllowNullValue(targetProperty.PropertyType))
          {
            targetProperty.SetValue(target, null);
          }
        }
        else
        {
          targetProperty.SetValue(target, convertedValue);
        }
      }
    }
  }
}
