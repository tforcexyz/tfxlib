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
using System.Diagnostics.Contracts;

namespace Xyz.TForce.Collections
{

  /// <summary>
  /// A <see cref="IDictionary{TKey,TValue}"/> that defers creating a shallow copy of the source dictionary until
  /// a mutative operation has been performed on it.
  /// </summary>
  public class CopyOnWriteDictionary<TKey, TValue> : IDictionary<TKey, TValue>
  {

    private readonly IDictionary<TKey, TValue> _sourceDictionary;
    private readonly IEqualityComparer<TKey> _comparer;
    private IDictionary<TKey, TValue> _innerDictionary;

    public CopyOnWriteDictionary(IDictionary<TKey, TValue> sourceDictionary, IEqualityComparer<TKey> comparer)
    {
      Contract.Assert(sourceDictionary != null);
      Contract.Assert(comparer != null);

      _sourceDictionary = sourceDictionary;
      _comparer = comparer;
    }

    private IDictionary<TKey, TValue> ReadDictionary
    {
      get
      {
        return _innerDictionary ?? _sourceDictionary;
      }
    }

    private IDictionary<TKey, TValue> WriteDictionary
    {
      get
      {
        if (_innerDictionary == null)
        {
          _innerDictionary = new Dictionary<TKey, TValue>(_sourceDictionary, _comparer);
        }

        return _innerDictionary;
      }
    }

    public virtual ICollection<TKey> Keys
    {
      get
      {
        return ReadDictionary.Keys;
      }
    }

    public virtual ICollection<TValue> Values
    {
      get
      {
        return ReadDictionary.Values;
      }
    }

    public virtual int Count
    {
      get
      {
        return ReadDictionary.Count;
      }
    }

    public virtual bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public virtual TValue this[TKey key]
    {
      get
      {
        return ReadDictionary[key];
      }
      set
      {
        WriteDictionary[key] = value;
      }
    }

    public virtual bool ContainsKey(TKey key)
    {
      return ReadDictionary.ContainsKey(key);
    }

    public virtual void Add(TKey key, TValue value)
    {
      WriteDictionary.Add(key, value);
    }

    public virtual bool Remove(TKey key)
    {
      return WriteDictionary.Remove(key);
    }

    public virtual bool TryGetValue(TKey key, out TValue value)
    {
      return ReadDictionary.TryGetValue(key, out value);
    }

    public virtual void Add(KeyValuePair<TKey, TValue> item)
    {
      WriteDictionary.Add(item);
    }

    public virtual void Clear()
    {
      WriteDictionary.Clear();
    }

    public virtual bool Contains(KeyValuePair<TKey, TValue> item)
    {
      return ReadDictionary.Contains(item);
    }

    public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      ReadDictionary.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      return WriteDictionary.Remove(item);
    }

    public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return ReadDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
