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
using System.Linq;

namespace Xyz.TForce.Collections
{

  public class DoubledDictionary<TKey, TValue> :
      IDictionary<TKey, TValue>
  {

    private readonly Dictionary<TKey, TValue> _forwardDictionary;
    private readonly Dictionary<TValue, HashSet<TKey>> _reverseDictionary;

    public DoubledDictionary()
    {
      _forwardDictionary = new Dictionary<TKey, TValue>();
      _reverseDictionary = new Dictionary<TValue, HashSet<TKey>>();
    }

    public DoubledDictionary(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
    {
      _forwardDictionary = keyComparer == null ? new Dictionary<TKey, TValue>() : new Dictionary<TKey, TValue>(keyComparer);
      _reverseDictionary = valueComparer == null ? new Dictionary<TValue, HashSet<TKey>>() : new Dictionary<TValue, HashSet<TKey>>(valueComparer);
    }

    public int Count { get { return _forwardDictionary.Count; } }

    public bool IsReadOnly { get { return false; } }

    public ICollection<TKey> Keys
    {
      get { return _forwardDictionary.Keys; }
    }

    public ICollection<TValue> Values
    {
      get { return _forwardDictionary.Values; }
    }

    public TValue this[TKey key]
    {
      get { return _forwardDictionary[key]; }
      set { Set(key, value); }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return _forwardDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return _forwardDictionary.GetEnumerator();
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
      Add(item.Key, item.Value);
    }

    public void Add(TKey key, TValue value)
    {
      _forwardDictionary.Add(key, value);
      HashSet<TKey> keySet = _reverseDictionary.SafeGetValue(value);
      if (keySet == null)
      {
        keySet = new HashSet<TKey>
        {
            key
        };
        _reverseDictionary.Add(value, keySet);
      }
      else
      {
        _ = keySet.Add(key);
      }
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      KeyValuePair<TKey, TValue> item = array[arrayIndex];
      Add(item.Key, item.Value);
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      TValue value = _forwardDictionary.SafeGetValue(item.Key);
      if (value == null)
      {
        return false;
      }
      if (item.Value.Equals(value))
      {
        return true;
      }
      return false;
    }

    public bool ContainsKey(TKey key)
    {
      return _forwardDictionary.ContainsKey(key);
    }

    public bool ContainValue(TValue value)
    {
      return _reverseDictionary.ContainsKey(value);
    }

    public TKey[] GetKeys(TValue value)
    {
      HashSet<TKey> keySet = _reverseDictionary[value];
      return keySet.ToArray();
    }

    public TValue GetValue(TKey key)
    {
      return _forwardDictionary[key];
    }

    public void Set(KeyValuePair<TKey, TValue> item)
    {
      Set(item.Key, item.Value);
    }

    public void Set(TKey key, TValue value)
    {
      if (_forwardDictionary.ContainsKey(key))
      {
        TValue tValue = _forwardDictionary[key];
        HashSet<TKey> keySet = _reverseDictionary.SafeGetValue(tValue);
        if (keySet != null)
        {
          if (keySet.Any(x => { return x.Equals(key); }))
          {
            _ = keySet.Remove(key);
          }
          if (keySet.Any())
          {
            _reverseDictionary[tValue] = keySet;
          }
          else
          {
            _ = _reverseDictionary.Remove(tValue);
          }
        }
        _forwardDictionary[key] = value;
        keySet = _reverseDictionary.SafeGetValue(value);
        if (keySet == null)
        {
          keySet = new HashSet<TKey>
            {
              key
            };
          _reverseDictionary.Add(value, keySet);
        }
        else
        {
          _ = keySet.Add(key);
        }
      }
      else
      {
        _forwardDictionary.Add(key, value);
        HashSet<TKey> keySet = _reverseDictionary.SafeGetValue(value);
        if (keySet == null)
        {
          keySet = new HashSet<TKey>
            {
              key
            };
          _reverseDictionary.Add(value, keySet);
        }
        else
        {
          _ = keySet.Add(key);
        }
      }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      return Remove(item.Key);
    }

    public bool Remove(TKey key)
    {
      TValue value = _forwardDictionary.SafeGetValue(key);
      if (value == null)
      {
        return false;
      }
      _ = _forwardDictionary.Remove(key);
      HashSet<TKey> keySet = _reverseDictionary.SafeGetValue(value);
      if (keySet == null)
      {
        return false;
      }
      if (keySet.Any(x => { return x.Equals(key); }))
      {
        _ = keySet.Remove(key);
      }
      if (keySet.Any())
      {
        _reverseDictionary[value] = keySet;
      }
      else
      {
        _ = _reverseDictionary.Remove(value);
      }
      return true;
    }

    public bool RemoveValue(TValue value)
    {
      HashSet<TKey> keySet = _reverseDictionary.SafeGetValue(value);
      if (keySet == null)
      {
        return false;
      }
      _ = _reverseDictionary.Remove(value);
      foreach (TKey key in keySet)
      {
        _ = _forwardDictionary.Remove(key);
      }
      return true;
    }

    public void Clear()
    {
      _forwardDictionary.Clear();
      _reverseDictionary.Clear();
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      return _forwardDictionary.TryGetValue(key, out value);
    }
  }
}
