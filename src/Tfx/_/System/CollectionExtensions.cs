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
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Xyz.TForce;
using Xyz.TForce.Collections;

namespace System
{

  public static class CollectionExtensions
  {

    [Obsolete]
    public static List<List<T>> Divide<T>(this IEnumerable<T> enumarable, int childCount)
    {
      return DivideAsList(enumarable, childCount);
    }

    public static List<List<T>> DivideAsList<T>(this IEnumerable<T> enumarable, int childCount)
    {
      List<List<T>> results = new List<List<T>>();
      if (enumarable == null)
      {
        return results;
      }
      int i = 0;
      List<T> result = new List<T>();
      foreach (T item in enumarable)
      {
        if (i % childCount == 0)
        {
          result = new List<T>();
          results.Add(result);
        }
        result.Add(item);
        i++;
      }
      return results;
    }

    public static T[][] DivideAsArray<T>(this IEnumerable<T> enumarable, int childCount)
    {
      if (enumarable == null)
      {
        return new T[0][];
      }
      int count = enumarable.Size();
      int sizex = (int)Math.Ceiling((double)(count / childCount));
      T[][] results = new T[sizex][];
      int i = 0;
      T[] result = new T[childCount];
      foreach (T item in enumarable)
      {
        if (i % childCount == 0)
        {
          result = new T[Math.Min(childCount, childCount - i)];
          results[i / childCount] = result;
        }
        result[i % childCount] = item;
        i++;
      }
      return results;
    }

    public static void ForIndex<T>(this ICollection<T> collection, ForIndexCallback<T> eachItemCallback)
    {
      if (collection == null || collection.Count == 0)
      {
        return;
      }
      for (int i = 0; i < collection.Count; i++)
      {
        T item = collection.ElementAt(i);
        eachItemCallback(item, i);
      }
    }

    /// <summary>
    /// Try to get get value of an item with specified key in a dictionary. Does not throw exception if the key is not found
    /// </summary>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of returned value</typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <returns>Value of the key in the dictionary. If not found return default value of specified data type</returns>
    public static TValue SafeGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
      if (dictionary.ContainsKey(key))
      {
        TValue value = dictionary[key];
        return value;
      }
      return default(TValue);
    }

    /// <summary>
    /// Try to get get value of an item with specified key in a dictionary. Does not throw exception if the key is not found
    /// </summary>
    /// <typeparam name="TValue">Type of returned value</typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <returns>Value of the key in the dictionary. If not found return default value of specified data type</returns>
    public static TValue SafeGetValue<TValue>(this IDictionary<string, object> dictionary, string key)
    {
      if (dictionary.ContainsKey(key))
      {
        TValue value = (TValue)dictionary[key];
        return value;
      }
      return default(TValue);
    }

    /// <summary>
    /// Determine the size of any collection
    /// </summary>
    /// <param name="enumarable"></param>
    /// <returns></returns>
    public static int Size(this IEnumerable enumarable)
    {
      Type type = enumarable.GetType();
      if (type.IsSubclassOf(typeof(ICollection)))
      {
        return ((ICollection)enumarable).Count;
      }
      else
      {
        int count = 0;
        foreach (object unused in enumarable)
        {
          count++;
        }
        return count;
      }
    }
    /// <summary>
    /// Try to get get value of an item with specified key in a dictionary. Does not throw exception if the key is not found
    /// </summary>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of returned value</typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>True if the key is found, otherwise False</returns>
    public static bool TryGetValueEx<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
    {
      if (dictionary.ContainsKey(key))
      {
        value = dictionary[key];
        return true;
      }
      value = default(TValue);
      return false;
    }

    /// <summary>
    /// Try to get get value of an item with specified string-typed key in a dictionary. Does not throw exception if the key is not found
    /// </summary>
    /// <typeparam name="TValue">Type of returned value</typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>True if the key is found, otherwise False</returns>
    public static bool TryGetValueEx<TValue>(this IDictionary<string, object> dictionary, string key, out TValue value)
    {
      if (dictionary.ContainsKey(key))
      {
        value = (TValue)dictionary[key];
        return true;
      }
      value = default(TValue);
      return false;
    }

    #region Microsoft System.Web.WebPages.dll
    /// <summary>
    /// Return a new array with the value added to the end. Slow and best suited to long lived arrays with few writes relative to reads.
    /// </summary>
    public static T[] AppendAndReallocate<T>(this T[] array, T value)
    {
      Contract.Assert(array != null);

      int originalLength = array.Length;
      T[] newArray = new T[originalLength + 1];
      array.CopyTo(newArray, 0);
      newArray[originalLength] = value;
      return newArray;
    }

    /// <summary>
    /// Return the enumerable as an Array, copying if required. Optimized for common case where it is an Array. 
    /// Avoid mutating the return value.
    /// </summary>
    public static T[] AsArray<T>(this IEnumerable<T> values)
    {
      Contract.Assert(values != null);

      T[] array = values as T[];
      if (array == null)
      {
        array = values.ToArray();
      }
      return array;
    }

    /// <summary>
    /// Return the enumerable as a Collection of T, copying if required. Optimized for the common case where it is 
    /// a Collection of T and avoiding a copy if it implements IList of T. Avoid mutating the return value.
    /// </summary>
    public static Collection<T> AsCollection<T>(this IEnumerable<T> enumerable)
    {
      Contract.Assert(enumerable != null);

      Collection<T> collection = enumerable as Collection<T>;
      if (collection != null)
      {
        return collection;
      }
      // Check for IList so that collection can wrap it instead of copying
      IList<T> list = enumerable as IList<T>;
      if (list == null)
      {
        list = new List<T>(enumerable);
      }
      return new Collection<T>(list);
    }

    /// <summary>
    /// Return the enumerable as a IList of T, copying if required. Avoid mutating the return value.
    /// </summary>
    public static IList<T> AsIList<T>(this IEnumerable<T> enumerable)
    {
      Contract.Assert(enumerable != null);

      IList<T> list = enumerable as IList<T>;
      if (list == null)
      {
        list = new List<T>(enumerable);
      }
      return list;
    }

    /// <summary>
    /// Return the enumerable as a List of T, copying if required. Optimized for common case where it is an List of T 
    /// or a ListWrapperCollection of T. Avoid mutating the return value.
    /// </summary>
    public static List<T> AsList<T>(this IEnumerable<T> enumerable)
    {
      Contract.Assert(enumerable != null);

      List<T> list = enumerable as List<T>;
      if (list != null)
      {
        return list;
      }
      ListWrapperCollection<T> listWrapper = enumerable as ListWrapperCollection<T>;
      list = listWrapper == null ? new List<T>(enumerable) : listWrapper.ItemsList;
      return list;
    }

    public static bool DoesAnyKeyHavePrefix<TValue>(this IDictionary<string, TValue> dictionary, string prefix)
    {
      return FindKeysWithPrefix(dictionary, prefix).Any();
    }

    public static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(this IDictionary<string, TValue> dictionary, string prefix)
    {
      TValue exactMatchValue;
      if (dictionary.TryGetValue(prefix, out exactMatchValue))
      {
        yield return new KeyValuePair<string, TValue>(prefix, exactMatchValue);
      }

      foreach (KeyValuePair<string, TValue> entry in dictionary)
      {
        string key = entry.Key;

        if (key.Length <= prefix.Length)
        {
          continue;
        }

        if (!key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
          continue;
        }

        char charAfterPrefix = key[prefix.Length];
        switch (charAfterPrefix)
        {
          case '[':
          case '.':
            yield return entry;
            break;
        }
      }
    }

    public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue @default)
    {
      TValue value;
      if (dict.TryGetValue(key, out value))
      {
        return value;
      }
      return @default;
    }

    /// <summary>
    /// Remove values from the list starting at the index start.
    /// </summary>
    public static void RemoveFrom<T>(this List<T> list, int start)
    {
      Contract.Assert(list != null);
      Contract.Assert(start >= 0 && start <= list.Count);

      list.RemoveRange(start, list.Count - start);
    }

    /// <summary>
    /// Return the only value from list, the type's default value if empty, or call the errorAction for 2 or more.
    /// </summary>
    public static T SingleDefaultOrError<T, TArg1>(this IList<T> list, Action<TArg1> errorAction, TArg1 errorArg1)
    {
      Contract.Assert(list != null);
      Contract.Assert(errorAction != null);

      switch (list.Count)
      {
        case 0:
          return default(T);

        case 1:
          T value = list[0];
          return value;

        default:
          errorAction(errorArg1);
          return default(T);
      }
    }

    /// <summary>
    /// Returns a single value in list matching type TMatch if there is only one, null if there are none of type TMatch or calls the
    /// errorAction with errorArg1 if there is more than one.
    /// </summary>
    public static TMatch SingleOfTypeDefaultOrError<TInput, TMatch, TArg1>(this IList<TInput> list, Action<TArg1> errorAction, TArg1 errorArg1) where TMatch : class
    {
      Contract.Assert(list != null);
      Contract.Assert(errorAction != null);

      TMatch result = null;
      for (int i = 0; i < list.Count; i++)
      {
        TMatch typedValue = list[i] as TMatch;
        if (typedValue != null)
        {
          if (result == null)
          {
            result = typedValue;
          }
          else
          {
            errorAction(errorArg1);
            return null;
          }
        }
      }
      return result;
    }

    /// <summary>
    /// Convert an ICollection to an array, removing null values. Fast path for case where there are no null values.
    /// </summary>
    public static T[] ToArrayWithoutNulls<T>(this ICollection<T> collection) where T : class
    {
      Contract.Assert(collection != null);

      T[] result = new T[collection.Count];
      int count = 0;
      foreach (T value in collection)
      {
        if (value != null)
        {
          result[count] = value;
          count++;
        }
      }
      if (count == collection.Count)
      {
        return result;
      }
      else
      {
        T[] trimmedResult = new T[count];
        Array.Copy(result, trimmedResult, count);
        return trimmedResult;
      }
    }

    /// <summary>
    /// Convert the array to a Dictionary using the keySelector to extract keys from values and the specified comparer. Optimized for array input.
    /// </summary>
    public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this TValue[] array, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Assert(array != null);
      Contract.Assert(keySelector != null);

      Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(array.Length, comparer);
      for (int i = 0; i < array.Length; i++)
      {
        TValue value = array[i];
        dictionary.Add(keySelector(value), value);
      }
      return dictionary;
    }

    /// <summary>
    /// Convert the list to a Dictionary using the keySelector to extract keys from values and the specified comparer. Optimized for IList of T input with fast path for array.
    /// </summary>
    public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this IList<TValue> list, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Assert(list != null);
      Contract.Assert(keySelector != null);

      TValue[] array = list as TValue[];
      if (array != null)
      {
        return ToDictionaryFast(array, keySelector, comparer);
      }
      return ToDictionaryFastNoCheck(list, keySelector, comparer);
    }

    /// <summary>
    /// Convert the enumerable to a Dictionary using the keySelector to extract keys from values and the specified comparer. Fast paths for array and IList of T.
    /// </summary>
    public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Assert(enumerable != null);
      Contract.Assert(keySelector != null);

      TValue[] array = enumerable as TValue[];
      if (array != null)
      {
        return ToDictionaryFast(array, keySelector, comparer);
      }
      IList<TValue> list = enumerable as IList<TValue>;
      if (list != null)
      {
        return ToDictionaryFastNoCheck(list, keySelector, comparer);
      }
      Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(comparer);
      foreach (TValue value in enumerable)
      {
        dictionary.Add(keySelector(value), value);
      }
      return dictionary;
    }

    /// <summary>
    /// Convert the list to a Dictionary using the keySelector to extract keys from values and the specified comparer. Optimized for IList of T input. No checking for other types.
    /// </summary>
    private static Dictionary<TKey, TValue> ToDictionaryFastNoCheck<TKey, TValue>(IList<TValue> list, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
    {
      Contract.Assert(list != null);
      Contract.Assert(keySelector != null);

      int listCount = list.Count;
      Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(listCount, comparer);
      for (int i = 0; i < listCount; i++)
      {
        TValue value = list[i];
        dictionary.Add(keySelector(value), value);
      }
      return dictionary;
    }
    #endregion
  }
}
