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

namespace Xyz.TForce.Reflection.Mapping
{

  public class ClassMappingConfiguration<TSource, TTarget> : IClassMappingRegister<TSource, TTarget>, IClassMappingResolver
    where TSource : class
    where TTarget : class
  {

    private readonly Dictionary<string, string> _mappingDictionary;

    public ClassMappingConfiguration()
    {
      _mappingDictionary = new Dictionary<string, string>();
    }

    public ClassMappingConfiguration<TSource, TTarget> Map<TSourceValue, TTargetValue>(Expression<Func<TSource, TSourceValue>> sourceExpression,
        Expression<Func<TTarget, TTargetValue>> targetExpression)
    {
      if (sourceExpression == null)
      {
        throw new ArgumentNullException(nameof(sourceExpression));
      }
      string sourceName = GetNameFromExpression(sourceExpression);
      if (targetExpression == null)
      {
        _mappingDictionary[sourceName] = string.Empty;
      }
      else
      {
        string targetName = GetNameFromExpression(targetExpression);
        _mappingDictionary[sourceName] = targetName;
      }

      return this;
    }

    public string GetTargetName(string sourceName)
    {
      if (_mappingDictionary.ContainsKey(sourceName))
      {
        string targetName = _mappingDictionary[sourceName];
        return targetName;
      }
      return null;
    }

    private string GetNameFromExpression<TType, TProperty>(Expression<Func<TType, TProperty>> expression)
    {
      if (expression.Body.NodeType == ExpressionType.Convert)
      {
        UnaryExpression unaryExpression = expression.Body as UnaryExpression;
        if (unaryExpression != null)
        {
          if (unaryExpression.NodeType == ExpressionType.MemberAccess)
          {
            MemberExpression memberExpression = unaryExpression.Operand as MemberExpression;
            string propertyName = memberExpression.Member.Name;
            return propertyName;
          }
        }
      }
      else if (expression.Body.NodeType == ExpressionType.MemberAccess)
      {
        MemberExpression memberExpression = expression.Body as MemberExpression;
        string propertyName = memberExpression.Member.Name;
        return propertyName;
      }
      throw new InvalidOperationException(nameof(expression));
    }
  }
}
