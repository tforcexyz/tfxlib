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

  public class CollectionToObjectMappingBuilder<TResult>
  {

    private readonly ExpressionDictionary<object, object, TResult> _mappingDictionary;

    public CollectionToObjectMappingBuilder()
    {
      _mappingDictionary = new ExpressionDictionary<object, object, TResult>();
    }

    public CollectionToObjectMappingBuilder<TResult> AddMapping(object sourceKey, Expression<Func<TResult, object>> targetExpression)
    {
      _mappingDictionary.Set(sourceKey, targetExpression);
      return this;
    }

    public TResult Map<TItem>(TItem[] collection, Expression<Func<TItem, object>> keyExpression, Expression<Func<TItem, object>> valueExpression)
    {
      TResult result = Activator.CreateInstance<TResult>();
      return Map(collection, keyExpression, valueExpression, result);
    }

    public TResult Map<TItem>(IEnumerable<TItem> collection, Expression<Func<TItem, object>> keyExpression, Expression<Func<TItem, object>> valueExpression)
    {
      TResult result = Activator.CreateInstance<TResult>();
      return Map(collection, keyExpression, valueExpression, result);
    }

    public TResult Map<TItem>(TItem[] collection, Expression<Func<TItem, object>> keyExpression, Expression<Func<TItem, object>> valueExpression, TResult result)
    {
      foreach (TItem item in collection)
      {
        object key = item.GetPropertyValue(keyExpression);
        if (_mappingDictionary.ContainsKey((object)key))
        {
          object value = item.GetPropertyValue(valueExpression);
          Expression<Func<TResult, object>> mapping = _mappingDictionary[key];
          result.SetPropertyValue(mapping, value);
        }
      }
      return result;
    }

    public TResult Map<TItem>(IEnumerable<TItem> collection, Expression<Func<TItem, object>> keyExpression, Expression<Func<TItem, object>> valueExpression, TResult result)
    {
      foreach (TItem item in collection)
      {
        object key = item.GetPropertyValue(keyExpression);
        if (_mappingDictionary.ContainsKey(key))
        {
          object value = item.GetPropertyValue(valueExpression);
          Expression<Func<TResult, object>> mapping = _mappingDictionary[key];
          result.SetPropertyValue(mapping, value);
        }
      }
      return result;
    }
  }
}
