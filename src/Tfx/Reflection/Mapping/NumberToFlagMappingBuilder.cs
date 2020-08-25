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
using System.Linq.Expressions;
using System.Reflection;
using Xyz.TForce.Collections;

namespace Xyz.TForce.Reflection.Mapping
{

  public class NumberToFlagMappingBuilder<TResult>
  {

    private readonly ExpressionDictionary<int, bool, TResult> _mappingDictionary;

    public NumberToFlagMappingBuilder()
    {
      _mappingDictionary = new ExpressionDictionary<int, bool, TResult>();
    }

    public NumberToFlagMappingBuilder<TResult> AddMapping(int digit, Expression<Func<TResult, bool>> targetExpression)
    {
      _mappingDictionary.Set(digit, targetExpression);
      return this;
    }

    public NumberToFlagMappingBuilder<TResult> AddMapping(FlagValue digit, Expression<Func<TResult, bool>> targetExpression)
    {
      _mappingDictionary.Set((int)digit, targetExpression);
      return this;
    }

    public TResult Map(int number)
    {
      TResult result = Activator.CreateInstance<TResult>();
      return Map((ulong)number, result);
    }

    public TResult Map(int number, TResult result)
    {
      return Map((ulong)number, result);
    }

    public TResult Map(uint number)
    {
      TResult result = Activator.CreateInstance<TResult>();
      return Map((ulong)number, result);
    }

    public TResult Map(uint number, TResult result)
    {
      return Map((ulong)number, result);
    }

    public TResult Map(long number)
    {
      TResult result = Activator.CreateInstance<TResult>();
      return Map((ulong)number, result);
    }

    public TResult Map(long number, TResult result)
    {
      return Map((ulong)number, result);
    }

    public TResult Map(ulong number)
    {
      TResult result = Activator.CreateInstance<TResult>();
      return Map(number, result);
    }

    public TResult Map(ulong number, TResult result)
    {
      ulong remain = number;
      int digit = 0;
      while (remain > 0)
      {
        ulong mod = remain % 2;
        if (mod == 1 && _mappingDictionary.ContainsKey(digit))
        {
          Expression<Func<TResult, bool>> mapping = _mappingDictionary[digit];
          result.SetPropertyValue(mapping, true);
        }
        remain /= 2;
        digit += 1;
      }
      return result;
    }
  }
}
