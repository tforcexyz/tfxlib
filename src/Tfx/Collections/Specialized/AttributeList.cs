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
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Xyz.TForce.Collections.Specialized
{

  /// <summary>
  /// Wrapper for AttributeCollection to provide generic collection implementation.
  /// </summary>
  public sealed class AttributeList : IList<Attribute>
  {

    private readonly AttributeCollection _attributes;

    public AttributeList(AttributeCollection attributes)
    {
      Contract.Assert(attributes != null);
      _attributes = attributes;
    }

    public int Count
    {
      get
      {
        return _attributes.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    public Attribute this[int index]
    {
      get
      {
        return _attributes[index];
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    public void Add(Attribute attribute)
    {
      throw new NotSupportedException();
    }

    public void Clear()
    {
      throw new NotSupportedException();
    }

    public bool Contains(Attribute attribute)
    {
      return _attributes.Contains(attribute);
    }

    public void CopyTo(Attribute[] target, int startIndex)
    {
      _attributes.CopyTo(target, startIndex);
    }

    public IEnumerator<Attribute> GetEnumerator()
    {
      for (int i = 0; i < _attributes.Count; i++)
      {
        yield return _attributes[i];
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable)_attributes).GetEnumerator();
    }

    public int IndexOf(Attribute attribute)
    {
      for (int i = 0; i < _attributes.Count; i++)
      {
        if (attribute == _attributes[i])
        {
          return i;
        }
      }
      return -1;
    }

    public void Insert(int index, Attribute attribute)
    {
      throw new NotSupportedException();
    }

    bool ICollection<Attribute>.Remove(Attribute attribute)
    {
      throw new NotSupportedException();
    }

    public void RemoveAt(int index)
    {
      throw new NotSupportedException();
    }
  }
}
