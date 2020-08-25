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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace Xyz.TForce.Reflection
{

  public static class TypeExpress
  {

    private static readonly Dictionary<Type, TryGetValue> s_tryGetValueDelegateCache = new Dictionary<Type, TryGetValue>();
    private static readonly ReaderWriterLockSlim s_tryGetValueDelegateCacheLock = new ReaderWriterLockSlim();
    private static readonly MethodInfo s_strongTryGetValueImplInfo = typeof(TypeExpress).GetMethod("StrongTryGetValueImpl", BindingFlags.NonPublic | BindingFlags.Static);

    public static readonly Assembly MsCorLibAssembly = typeof(string).Assembly;

    /// <summary>
    /// Determine of a type can be assigned null value
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool AllowNullValue(Type type)
    {
      return !type.IsValueType || IsNullableValueType(type);
    }

    /// <summary>
    /// Check if an object can be assigned to current type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsCompatibleObject<T>(object value)
    {
      return (value == null && AllowNullValue(typeof(T))) || value is T;
    }

    /// <summary>
    /// Check if an object can be assigned to current type
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsCompatibleObject(Type type, object value)
    {
      return (value == null && AllowNullValue(type)) || type.IsInstanceOfType(value);
    }

    /// <summary>
    /// Determine of a type is a nullable type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsNullableValueType(Type type)
    {
      return Nullable.GetUnderlyingType(type) != null;
    }

    // method is used primarily for lighting up new .NET Framework features even if MVC targets the previous version
    // thisParameter is the 'this' parameter if target method is instance method, should be null for static method
    public static TDelegate CreateDelegate<TDelegate>(Assembly assembly, string typeName, string methodName, object thisParameter) where TDelegate : class
    {
      // ensure target type exists
      Type targetType = assembly.GetType(typeName, false /* throwOnError */);
      if (targetType == null)
      {
        return null;
      }

      return CreateDelegate<TDelegate>(targetType, methodName, thisParameter);
    }

    public static TDelegate CreateDelegate<TDelegate>(Type targetType, string methodName, object thisParameter) where TDelegate : class
    {
      // ensure target method exists
      ParameterInfo[] delegateParameters = typeof(TDelegate).GetMethod("Invoke").GetParameters();
      Type[] argumentTypes = Array.ConvertAll(delegateParameters, pInfo => { return pInfo.ParameterType; });
      MethodInfo targetMethod = targetType.GetMethod(methodName, argumentTypes);
      if (targetMethod == null)
      {
        return null;
      }

      TDelegate d = Delegate.CreateDelegate(typeof(TDelegate), thisParameter, targetMethod, false /* throwOnBindFailure */) as TDelegate;
      return d;
    }

    public static TryGetValue CreateTryGetValueDelegate(Type targetType)
    {
      TryGetValue result;

      s_tryGetValueDelegateCacheLock.EnterReadLock();
      try
      {
        if (s_tryGetValueDelegateCache.TryGetValue(targetType, out result))
        {
          return result;
        }
      }
      finally
      {
        s_tryGetValueDelegateCacheLock.ExitReadLock();
      }

      Type dictionaryType = ExtractGenericInterface(targetType, typeof(IDictionary<,>));

      // just wrap a call to the underlying IDictionary<TKey, TValue>.TryGetValue() where string can be cast to TKey
      if (dictionaryType != null)
      {
        Type[] typeArguments = dictionaryType.GetGenericArguments();
        Type keyType = typeArguments[0];
        Type returnType = typeArguments[1];

        if (keyType.IsAssignableFrom(typeof(string)))
        {
          MethodInfo strongImplInfo = s_strongTryGetValueImplInfo.MakeGenericMethod(keyType, returnType);
          result = (TryGetValue)Delegate.CreateDelegate(typeof(TryGetValue), strongImplInfo);
        }
      }

      // wrap a call to the underlying IDictionary.Item()
      if (result == null && typeof(IDictionary).IsAssignableFrom(targetType))
      {
        result = TryGetValueFromNonGenericDictionary;
      }

      s_tryGetValueDelegateCacheLock.EnterWriteLock();
      try
      {
        s_tryGetValueDelegateCache[targetType] = result;
      }
      finally
      {
        s_tryGetValueDelegateCacheLock.ExitWriteLock();
      }

      return result;
    }

    public static Type ExtractGenericInterface(Type queryType, Type interfaceType)
    {
      if (MatchesGenericType(queryType, interfaceType))
      {
        return queryType;
      }
      Type[] queryTypeInterfaces = queryType.GetInterfaces();
      return MatchGenericTypeFirstOrDefault(queryTypeInterfaces, interfaceType);
    }

    public static object GetDefaultValue(Type type)
    {
      return (AllowNullValue(type)) ? null : Activator.CreateInstance(type);
    }

    /// <summary>
    /// Provide a new <see cref="MissingMethodException"/> if original Message does not contain given full Type name.
    /// </summary>
    /// <param name="originalException"><see cref="MissingMethodException"/> to check.</param>
    /// <param name="fullTypeName">Full Type name which Message should contain.</param>
    /// <returns>New <see cref="MissingMethodException"/> if an update is required; null otherwise.</returns>
    public static MissingMethodException EnsureDebuggableException(MissingMethodException originalException, string fullTypeName)
    {
      MissingMethodException replacementException = null;
      if (!originalException.Message.Contains(fullTypeName))
      {
        string message = string.Format(
          CultureInfo.CurrentCulture,
          "MvcResources.TypeExpress_CannotCreateInstance",
          originalException.Message,
          fullTypeName);
        replacementException = new MissingMethodException(message, originalException);
      }

      return replacementException;
    }

    private static bool MatchesGenericType(Type type, Type matchType)
    {
      return type.IsGenericType && type.GetGenericTypeDefinition() == matchType;
    }

    private static Type MatchGenericTypeFirstOrDefault(Type[] types, Type matchType)
    {
      for (int i = 0; i < types.Length; i++)
      {
        Type type = types[i];
        if (MatchesGenericType(type, matchType))
        {
          return type;
        }
      }
      return null;
    }

    public static bool StrongTryGetValueImpl<TKey, TValue>(object dictionary, string key, out object value)
    {
      IDictionary<TKey, TValue> strongDict = (IDictionary<TKey, TValue>)dictionary;

      TValue strongValue;
      bool retVal = strongDict.TryGetValue((TKey)(object)key, out strongValue);
      value = strongValue;
      return retVal;
    }

    private static bool TryGetValueFromNonGenericDictionary(object dictionary, string key, out object value)
    {
      IDictionary weakDict = (IDictionary)dictionary;

      bool containsKey = weakDict.Contains(key);
      value = (containsKey) ? weakDict[key] : null;
      return containsKey;
    }

    public static Type[] GetTypeArgumentsIfMatch(Type closedType, Type matchingOpenType)
    {
      if (!closedType.IsGenericType)
      {
        return null;
      }

      Type openType = closedType.GetGenericTypeDefinition();
      return (matchingOpenType == openType) ? closedType.GetGenericArguments() : null;
    }
  }
}
