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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xyz.TForce.Reflection;
using Xyz.TForce.Reflection.Mapping;

namespace System.Reflection
{

  /// <summary>
  /// Extension that utilize reflection and lamda expression
  /// </summary>
  public static class ReflectionExtensions
  {

    /// <summary>
    /// Get value of a property using lamda expression
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="object"></param>
    /// <param name="expression"></param>
    /// <returns>Value of the property. Return null if lamda expression is invalid.</returns>
    public static object GetPropertyValue<TObject>(this TObject @object, Expression<Func<TObject, object>> expression)
    {
      string propertyPath = BuildPropertyPath(expression);
      return GetPropertyValue(@object, propertyPath);
    }

    /// <summary>
    /// Build property path from lamda expression
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="expression">Lamda expression</param>
    /// <returns></returns>
    public static string BuildPropertyPath<TObject, TValue>(Expression<Func<TObject, TValue>> expression)
    {
      Expression propertyExpression = expression.Body;
      if (propertyExpression.NodeType == ExpressionType.Convert)
      {
        UnaryExpression castExpression = (UnaryExpression)propertyExpression;
        if (castExpression.Operand.NodeType == ExpressionType.MemberAccess
            || castExpression.Operand.NodeType == ExpressionType.Call
            || castExpression.Operand.NodeType == ExpressionType.Constant
            || castExpression.Operand.NodeType == ExpressionType.Parameter)
        {
          propertyExpression = castExpression.Operand;
        }
      }
      if (propertyExpression.NodeType == ExpressionType.MemberAccess
          || propertyExpression.NodeType == ExpressionType.Call
          || propertyExpression.NodeType == ExpressionType.Constant
          || propertyExpression.NodeType == ExpressionType.Parameter)
      {
        string propertyPath = BuildPropertyPath(propertyExpression);
        return propertyPath;
      }
      return null;
    }

    /// <summary>
    /// Build property path from supported expression (MemberAccess, Call)
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static string BuildPropertyPath(Expression expression)
    {
      IList<string> pathParts = new List<string>();
      if (expression.NodeType == ExpressionType.MemberAccess)
      {
        MemberExpression memberExpresion = expression as MemberExpression;
        while (memberExpresion != null)
        {
          pathParts.Add(memberExpresion.Member.Name);
          memberExpresion = memberExpresion.Expression as MemberExpression;
        }
        _ = pathParts.Reverse();
        string propertyPath = string.Join(".", pathParts);
        return propertyPath;
      }
      if (expression.NodeType == ExpressionType.Parameter)
      {
        return string.Empty;
      }
      return null;
    }

    /// <summary>
    /// Set value to a property using property path
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="object"></param>
    /// <param name="propertyPath"></param>
    /// <returns></returns>
    public static object GetPropertyValue<TObject>(this TObject @object, string propertyPath)
    {
      if (propertyPath == null)
      {
        throw new ArgumentException(nameof(propertyPath));
      }
      if (string.IsNullOrWhiteSpace(propertyPath))
      {
        return @object;
      }
      string[] pathParts = propertyPath.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
      object value = @object;
      for (int i = 0; i < pathParts.Length; i++)
      {
        string pathPart = pathParts[i];
        if (value == null)
        {
          return default(TObject);
        }
        Type valueType = value.GetType();
        PropertyInfo prop = valueType.GetProperty(pathPart);
        if (prop == null)
        {
          return default(TObject);
        }
        value = prop.GetValue(value, null);
      }
      return value;
    }

    /// <summary>
    /// Set value to a property using lamda expression
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="object"></param>
    /// <param name="expression"></param>
    /// <param name="value"></param>
    /// Source: http://stackoverflow.com/questions/9601707/how-to-set-property-value-using-expressions
    public static void SetPropertyValue<TObject>(this TObject @object, Expression<Func<TObject, object>> expression, object value)
    {
      SetPropertyValue<TObject, object>(@object, expression, value);
    }

    /// <summary>
    /// Set value to a property using lamda expression
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="object"></param>
    /// <param name="expression"></param>
    /// <param name="value"></param>
    /// Source: http://stackoverflow.com/questions/9601707/how-to-set-property-value-using-expressions
    public static void SetPropertyValue<TObject, TValue>(this TObject @object, Expression<Func<TObject, TValue>> expression, TValue value)
    {
      if (expression.Body.NodeType == ExpressionType.Convert)
      {
        UnaryExpression unaryExpression = expression.Body as UnaryExpression;
        if (unaryExpression != null)
        {
          MemberExpression memberSelectorExpression = unaryExpression.Operand as MemberExpression;
          if (memberSelectorExpression != null)
          {
            PropertyInfo property = memberSelectorExpression.Member as PropertyInfo;
            if (property != null)
            {
              MappingExpress.SetValue(@object, property, value);
            }
          }
        }
      }
      else
      {
        MemberExpression memberSelectorExpression = expression.Body as MemberExpression;
        if (memberSelectorExpression != null)
        {
          @object.SetPropertyValue(memberSelectorExpression, value);
        }
      }
    }

    private static void SetPropertyValue<TObject, TValue>(this TObject @object, MemberExpression expression, TValue value)
    {
      IList<Expression> hierarchy = BuildExpressionHierarchy(expression);
      object parentObject = @object;
      foreach (Expression subExpression in hierarchy)
      {
        if (subExpression.NodeType == ExpressionType.MemberAccess)
        {
          PropertyInfo propertyInfo = ((MemberExpression)subExpression).Member as PropertyInfo;
          if (propertyInfo == null || parentObject == null)
          {
            break;
          }
          if (subExpression == expression)
          {
            MappingExpress.SetValue(@object, propertyInfo, value);
            continue;
          }
          parentObject = propertyInfo.GetValue(parentObject);
          if (parentObject == null)
          {
            parentObject = InitObject(propertyInfo.PropertyType);
          }
        }
        if (subExpression.NodeType == ExpressionType.Call)
        {
          MethodInfo methodInfo = ((MethodCallExpression)subExpression).Method;
          if (methodInfo.ReturnType == typeof(void))
          {
            break;
          }
          object[] arguments = GetExpressionArguments((MethodCallExpression)subExpression);
          parentObject = methodInfo.Invoke(@parentObject, arguments);
        }
      }
    }

    private static IList<Expression> BuildExpressionHierarchy(MemberExpression expression)
    {
      IList<Expression> results = new List<Expression>();
      Expression innerExpression = expression;
      while (innerExpression != null && innerExpression.NodeType != ExpressionType.Parameter)
      {
        results.Insert(0, innerExpression);
        if (innerExpression.NodeType == ExpressionType.MemberAccess)
        {
          innerExpression = ((MemberExpression)innerExpression).Expression;
        }
        else if (innerExpression.NodeType == ExpressionType.Call)
        {
          innerExpression = ((MethodCallExpression)innerExpression).Object;
        }
      }
      return results;
    }

    private static object[] GetExpressionArguments(MethodCallExpression expression)
    {
      IList<object> results = new List<object>();
      foreach (Expression argumentExpression in expression.Arguments)
      {
        if (argumentExpression.NodeType == ExpressionType.Constant)
        {
          object result = ((ConstantExpression)argumentExpression).Value;
          results.Add(result);
        }
      }
      return results.AsArray();
    }

    public static object InitObject(Type type)
    {
      return null;
    }

    /// <summary>
    /// Set value to a property using property path
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="object"></param>
    /// <param name="propertyPath"></param>
    /// <param name="value"></param>
    public static void SetPropertyValue<TObject, TValue>(this TObject @object, string propertyPath, TValue value)
    {
      if (string.IsNullOrWhiteSpace(propertyPath))
      {
        throw new ArgumentException(nameof(propertyPath));
      }
      string[] pathParts = propertyPath.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
      object parent = @object;
      for (int i = 0; i < pathParts.Length; i++)
      {
        string pathPart = pathParts[i];
        if (parent == null)
        {
          break;
        }
        Type parentType = parent.GetType();
        PropertyInfo prop = parentType.GetProperty(pathPart);
        if (prop == null)
        {
          break;
        }
        if (i + 1 >= pathParts.Length)
        {
          MappingExpress.SetValue(parent, prop, value);
          break;
        }
        parent = prop.GetValue(parent, null);
      }
    }

    /// <summary>
    /// Detemined where as type is collection or scalar
    /// </summary>
    /// <param name="type"></param>
    /// <returns>True if current type is collection, otherwise False</returns>
    public static bool IsCollection(this Type type)
    {
      if (type.IsArray)
      {
        return true;
      }
      Type[] exclusions = new Type[]
      {
        typeof(string)
      };
      if (exclusions.Contains(type))
      {
        return false;
      }
      if (typeof(IEnumerable).IsAssignableFrom(type))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Deletemine whether a type is not primity or collection
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsPreference(this Type type)
    {
      return !IsPrimity(type);
    }

    /// <summary>
    /// Determite whether a type is simple type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsPrimity(this Type type)
    {
      Type[] primityTypes = new Type[]
      {
        typeof(int),
        typeof(int?),
        typeof(long),
        typeof(long?),
        typeof(float),
        typeof(float?),
        typeof(decimal),
        typeof(decimal?),
        typeof(string),
        typeof(Guid),
        typeof(DateTime),
        typeof(DateTime?),
        typeof(TimeSpan),
        typeof(TimeSpan?),
        typeof(Type),
        typeof(Exception),
        typeof(void)
      };
      if (primityTypes.Contains(type))
      {
        return true;
      }
      return false;
    }

    public static TResult ToClass<TSource, TResult>(this TSource @object, Action<TSource, TResult> postAction, Action<IObjectMappingRegister> customMappings = null, ObjectMappingConflictResolution conflictResolution = ObjectMappingConflictResolution.Exception)
    {
      TResult result = (TResult)ToClass(@object, typeof(TResult), customMappings, conflictResolution);
      if (@object != null && result != null)
      {
        if (postAction != null)
        {
          postAction.Invoke(@object, result);
        }
      }
      return result;
    }

    /// <summary>
    /// Perform deep copy of current object to desired type. Generic version.
    /// </summary>
    /// <typeparam name="TResult">Type of the class you want to copy to</typeparam>
    /// <param name="object"></param>
    /// <param name="customMappingAction">Callback to create custom mapping for specified type</param>
    /// <param name="conflictResolution">What should be done if a conflict between source type and target type cannot be resolved</param>
    /// <returns></returns>
    public static TResult ToClass<TResult>(this object @object, Action<IObjectMappingRegister> customMappingAction = null, ObjectMappingConflictResolution conflictResolution = ObjectMappingConflictResolution.Exception)
    {
      object result = ToClass(@object, typeof(TResult), customMappingAction, conflictResolution);
      return (TResult)result;
    }

    /// <summary>
    /// Perform deep copy of current object to desired type. Standard version.
    /// </summary>
    /// <param name="object"></param>
    /// <param name="resultType">Type of the class you want to copy to</param>
    /// <param name="customMappingAction">Callback to create custom mapping for specified type</param>
    /// <param name="conflictResolution">What should be done if a conflict between source type and target type cannot be resolved</param>
    /// <returns></returns>
    public static object ToClass(this object @object, Type resultType, Action<IObjectMappingRegister> customMappingAction = null, ObjectMappingConflictResolution conflictResolution = ObjectMappingConflictResolution.Exception)
    {

      if (@object == null)
      {
        return null;
      }
      Type sourceType = @object.GetType();
      if (!resultType.IsInterface && !resultType.IsClass)
      {
        if (conflictResolution == ObjectMappingConflictResolution.Exception)
        {
          throw new ClassMappingException(sourceType, resultType);
        }
      }

      ObjectMappingConfiguration customMappingContainer = new ObjectMappingConfiguration();
      if (customMappingAction != null)
      {
        customMappingAction.Invoke(customMappingContainer);
      }

      object result;
#pragma warning disable IDE0045 // Convert to conditional expression
      if (sourceType.IsCollection())
#pragma warning restore IDE0045 // Convert to conditional expression
      {
        result = MapClass((IEnumerable)@object, sourceType, resultType, customMappingContainer, conflictResolution);
      }
      else
      {
        result = MapClass(@object, sourceType, resultType, customMappingContainer, conflictResolution);
      }
      return result;
    }

    private static object ToClass(this object @object, Type resultType, IObjectMappingResolver customMappings, ObjectMappingConflictResolution conflictResolution = ObjectMappingConflictResolution.Exception)
    {
      if (@object == null)
      {
        return null;
      }
      Type sourceType = @object.GetType();
      if (!resultType.IsInterface && !resultType.IsClass)
      {
        if (conflictResolution == ObjectMappingConflictResolution.Exception)
        {
          throw new ClassMappingException(sourceType, resultType);
        }
      }

      object result;
#pragma warning disable IDE0045 // Convert to conditional expression
      if (sourceType.IsCollection())
#pragma warning restore IDE0045 // Convert to conditional expression
      {
        result = MapClass((IEnumerable)@object, sourceType, resultType, customMappings, conflictResolution);
      }
      else
      {
        result = MapClass(@object, sourceType, resultType, customMappings, conflictResolution);
      }
      return result;
    }

    private static object MapClass(object source, Type sourceType, Type resultType, IObjectMappingResolver customMappings, ObjectMappingConflictResolution conflictResolution)
    {

      if (sourceType.IsCollection())
      {
        if (conflictResolution == ObjectMappingConflictResolution.Exception)
        {
          throw new ClassMappingException(sourceType, resultType);
        }
        return null;
      }

      object result = Activator.CreateInstance(resultType);
      PropertyInfo[] sourceProperties = sourceType.GetProperties();
      PropertyInfo[] resultProperties = resultType.GetProperties();

      IClassMappingResolver mappingConfig = customMappings.GetConfiuration(sourceType, resultType);
      foreach (PropertyInfo sourceProperty in sourceProperties)
      {
        string targetName = sourceProperty.Name;
        if (mappingConfig != null)
        {
          string remappedName = mappingConfig.GetTargetName(sourceProperty.Name);
          if (remappedName != null)
          {
            targetName = remappedName;
          }
        }
        PropertyInfo resultProperty = resultProperties.FirstOrDefault(x => { return x.Name == targetName; });
        if (resultProperty == null)
        {
          continue;
        }
        if (!sourceProperty.CanRead)
        {
          continue;
        }
        if (!resultProperty.CanWrite)
        {
          continue;
        }

        object value = sourceProperty.GetValue(source, null);
        if (MappingExpress.CanSetValue(sourceProperty, resultProperty))
        {
          MappingExpress.SetValue(result, resultProperty, value);
        }
        else
        {
          Type type = sourceProperty.PropertyType;
          if (type.IsInterface || type.IsClass)
          {
            MapProperty(value, result, sourceProperty, resultProperty, customMappings, conflictResolution);
          }
          else if (conflictResolution == ObjectMappingConflictResolution.Exception)
          {
            throw new PropertyMappingException(sourceProperty, resultProperty);
          }
        }
      }
      return result;
    }

    private static object MapClass(IEnumerable sources, Type sourceType, Type resultType, IObjectMappingResolver customMappings, ObjectMappingConflictResolution conflictResolution)
    {

      if (!sourceType.IsCollection())
      {
        if (conflictResolution == ObjectMappingConflictResolution.Exception)
        {
          throw new ClassMappingException(sourceType, resultType);
        }
        return null;
      }

      int count = sources.Size();
      if (resultType.IsArray)
      {
        Type itemType = resultType.GetElementType();
        Array results = Array.CreateInstance(itemType, count);
        MapItems(sources, results, resultType, customMappings, conflictResolution);
        return results;
      }
      else if (resultType == typeof(IEnumerable) || resultType == typeof(ICollection) || resultType == typeof(IList)
        || (resultType.IsGenericType && (resultType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
            || resultType.GetGenericTypeDefinition() == typeof(ICollection<>)
            || resultType.GetGenericTypeDefinition() == typeof(IList<>))
            )
      )
      {
        if (resultType.IsGenericType)
        {
          Type itemType = resultType.GetGenericArguments().First();
          Type listType = typeof(List<>);
          listType = listType.MakeGenericType(itemType);
          IList results = Activator.CreateInstance(listType) as IList;
          MapItems(sources, results, resultType, customMappings, conflictResolution);
          return results;
        }
        else
        {
          Type listType = typeof(ArrayList);
          IList results = Activator.CreateInstance(listType) as IList;
          MapItems(sources, results, resultType, customMappings, conflictResolution);
          return results;
        }
      }
      else if (typeof(IList).IsAssignableFrom(resultType) && resultType.IsClass && !resultType.IsAbstract)
      {
        IList results = Activator.CreateInstance(resultType) as IList;
        MapItems(sources, results, resultType, customMappings, conflictResolution);
        return results;
      }
      else if (conflictResolution == ObjectMappingConflictResolution.Exception)
      {
        throw new InvalidOperationException();
      }
      return null;
    }

    private static void MapItems(IEnumerable sources, Array results, Type resultType, IObjectMappingResolver customMappings, ObjectMappingConflictResolution conflictResolution)
    {
      Type itemType = resultType.GetElementType();
      int counter = 0;
      foreach (object item in sources)
      {
        object result = MapClass(item, item.GetType(), itemType, customMappings, conflictResolution);
        results.SetValue(result, counter);
        counter++;
      }
    }

    private static void MapItems(IEnumerable sources, IList results, Type resultType, IObjectMappingResolver customMappings, ObjectMappingConflictResolution conflictResolution)
    {
      if (resultType.IsGenericType)
      {
        Type itemType = resultType.GetGenericArguments().First();
        foreach (object item in sources)
        {
          object result = MapClass(item, item.GetType(), itemType, customMappings, conflictResolution);
          _ = results.Add(result);
        }
      }
      else
      {
        foreach (object item in sources)
        {
          object result = MapClass(item, item.GetType(), item.GetType(), customMappings, conflictResolution);
          _ = results.Add(result);
        }
      }
    }

    private static void MapProperty(object value, object result, PropertyInfo sourceProperty, PropertyInfo resultProperty, IObjectMappingResolver customMappings, ObjectMappingConflictResolution conflictResolution)
    {
      Type type = resultProperty.PropertyType;
      if (value != null)
      {
        if (type.IsInterface || (type.IsClass && !type.IsAbstract))
        {
          object convertedVallue = ToClass(value, type, customMappings, conflictResolution);
          resultProperty.SetValue(result, convertedVallue, null);
        }
        else if (conflictResolution == ObjectMappingConflictResolution.Exception)
        {
          throw new PropertyMappingException(sourceProperty, resultProperty);
        }
      }
    }
  }
}
