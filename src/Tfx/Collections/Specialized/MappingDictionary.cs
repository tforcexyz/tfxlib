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
using System.Collections.Generic;

namespace Xyz.TForce.Collections.Specialized
{

  public delegate TResult Convert<in TSource, out TResult>(TSource source);

  public class MappingDictionary
  {

    private readonly Dictionary<TypeMappingKey, Convert<object, object>> _internalDictionary;

    public MappingDictionary()
    {
      _internalDictionary = new Dictionary<TypeMappingKey, Convert<object, object>>();
    }

    public Convert<object, object> this[Type x, Type y]
    {
      get { return GetMapping(x, y); }
      set { SetMapping(x, y, value); }
    }

    public bool IsSupported(Type sourceType, Type targetType)
    {
      TypeMappingKey key = new TypeMappingKey(sourceType, targetType);
      return _internalDictionary.ContainsKey(key);
    }

    public bool IsSupported<TSource, TTarget>()
    {
      Type sourceType = typeof(TSource);
      Type targetType = typeof(TTarget);
      TypeMappingKey key = new TypeMappingKey(sourceType, targetType);
      return _internalDictionary.ContainsKey(key);
    }

    public Convert<object, object> GetMapping(Type sourceType, Type targetType)
    {
      TypeMappingKey key = new TypeMappingKey(sourceType, targetType);
      if (_internalDictionary.ContainsKey(key))
      {
        return _internalDictionary[key];
      }
      return null;
    }

    public Convert<object, object> GetMapping<TSource, TTarget>()
    {
      Type sourceType = typeof(TSource);
      Type targetType = typeof(TTarget);
      TypeMappingKey key = new TypeMappingKey(sourceType, targetType);
      if (_internalDictionary.ContainsKey(key))
      {
        return _internalDictionary[key];
      }
      return null;
    }

    public void SetMapping(Type sourceType, Type targetType, Convert<object, object> function)
    {
      TypeMappingKey key = new TypeMappingKey(sourceType, targetType);
      _internalDictionary[key] = function;
    }

    public void SetMapping<TSource, TTarget>(Convert<object, object> function)
    {
      Type sourceType = typeof(TSource);
      Type targetType = typeof(TTarget);
      TypeMappingKey key = new TypeMappingKey(sourceType, targetType);
      _internalDictionary[key] = function;
    }

    public class TypeMappingKey : IEquatable<TypeMappingKey>
    {

      public TypeMappingKey(Type sourceType, Type targetType)
      {
        SourceType = sourceType;
        TargetType = targetType;
      }

      public Type SourceType { get; }

      public Type TargetType { get; }

      public override int GetHashCode()
      {
        int hash = ObjectCompareExpress.GetHashCode(SourceType, TargetType);
        return hash;
      }

      public bool Equals(TypeMappingKey other)
      {
        if (other == null)
        {
          return false;
        }

        bool isEqual = SourceType == other.SourceType;
        isEqual &= TargetType == other.TargetType;
        return isEqual;
      }
    }
  }
}
