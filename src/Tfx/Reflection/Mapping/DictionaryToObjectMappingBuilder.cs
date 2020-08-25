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
using System.Linq.Expressions;
using System.Reflection;
using Xyz.TForce.Collections;

namespace Xyz.TForce.Reflection.Mapping
{

  public class DictionaryToObjectMappingBuilder<TResult>
  {

    private readonly ExpressionDictionary<object, object, TResult> _mappingDictionary;

    public DictionaryToObjectMappingBuilder()
    {
      _mappingDictionary = new ExpressionDictionary<object, object, TResult>();
    }

    public DictionaryToObjectMappingBuilder<TResult> AddMapping(object sourceKey, Expression<Func<TResult, object>> targetExpression)
    {
      _mappingDictionary.Set(sourceKey, targetExpression);
      return this;
    }

    public TResult Map<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
      TResult result = Activator.CreateInstance<TResult>();
      return Map(dictionary, result);
    }

    public TResult Map<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TResult result)
    {
      Dictionary<TKey, TValue>.KeyCollection keys = dictionary.Keys;
      foreach (TKey key in keys)
      {
        if (_mappingDictionary.ContainsKey(key))
        {
          TValue value = dictionary[key];
          Expression<Func<TResult, object>> mapping = _mappingDictionary[key];
          result.SetPropertyValue(mapping, value);
        }
      }
      return result;
    }
  }
}
