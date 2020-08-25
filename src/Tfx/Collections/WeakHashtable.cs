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

namespace Xyz.TForce.Collections
{

  /// <summary>
  /// This is a hashtable that stores object keys as weak references. It monitors memory usage and will periodically scavenge the hash table to clean out dead references.
  /// </summary>
  internal sealed class WeakHashtable : Hashtable
  {

    private static readonly IEqualityComparer s_comparer = new WeakKeyComparer();

    private long _lastGlobalMem;
    private int _lastHashCount;

    internal WeakHashtable()
      : base(s_comparer)
    {
    }

    /// <summary>
    /// Override of Item that wraps a weak reference around the
    /// key and performs a scavenge.
    /// </summary>
    public void SetWeak(object key, object value)
    {
      ScavengeKeys();
      this[new EqualityWeakReference(key)] = value;
    }

    /// <summary>
    /// This method checks to see if it is necessary to
    /// scavenge keys, and if it is it performs a scan
    /// of all keys to see which ones are no longer valid.
    /// To determine if we need to scavenge keys we need to
    /// try to track the current GC memory.  Our rule of
    /// thumb is that if GC memory is decreasing and our
    /// key count is constant we need to scavenge.  We
    /// will need to see if this is too often for extreme
    /// use cases like the CompactFramework (they add
    /// custom type data for every object at design time).
    /// </summary>
    private void ScavengeKeys()
    {
      int hashCount = Count;

      if (hashCount == 0)
      {
        return;
      }

      if (_lastHashCount == 0)
      {
        _lastHashCount = hashCount;
        return;
      }

      long globalMem = GC.GetTotalMemory(false);

      if (_lastGlobalMem == 0)
      {
        _lastGlobalMem = globalMem;
        return;
      }

      float memDelta = (globalMem - _lastGlobalMem) / (float)_lastGlobalMem;
      float hashDelta = (hashCount - _lastHashCount) / (float)_lastHashCount;

      if (memDelta < 0 && hashDelta >= 0)
      {
        // Perform a scavenge through our keys, looking
        // for dead references.                
        ArrayList cleanupList = null;
        foreach (object o in Keys)
        {
          EqualityWeakReference wr = o as EqualityWeakReference;
          if (wr != null && !wr.IsAlive)
          {
            if (cleanupList == null)
            {
              cleanupList = new ArrayList();
            }

            _ = cleanupList.Add(wr);
          }
        }

        if (cleanupList != null)
        {
          foreach (object o in cleanupList)
          {
            Remove(o);
          }
        }
      }

      _lastGlobalMem = globalMem;
      _lastHashCount = hashCount;
    }

    private class WeakKeyComparer : IEqualityComparer
    {
      bool IEqualityComparer.Equals(object x, object y)
      {
        if (x == null)
        {
          return y == null;
        }

        if (y != null && x.GetHashCode() == y.GetHashCode())
        {
          EqualityWeakReference wX = x as EqualityWeakReference;
          EqualityWeakReference wY = y as EqualityWeakReference;

          //Both WeakReferences are gc'd and they both had the same hash
          //Since this is only used in Weak Hash table ----s are not really an issue.
          if (wX != null && wY != null && !wY.IsAlive && !wX.IsAlive)
          {
            return true;
          }

          if (wX != null)
          {
            x = wX.Target;
          }

          if (wY != null)
          {

            y = wY.Target;
          }

          return ReferenceEquals(x, y);
        }

        return false;
      }

      int IEqualityComparer.GetHashCode(object obj)
      {
        return obj.GetHashCode();
      }
    }

    /// <summary>
    /// A wrapper of WeakReference that overrides GetHashCode and
    /// Equals so that the weak reference returns the same equality
    /// semantics as the object it wraps.  This will always return
    /// the object's hash code and will return True for a Equals
    /// comparison of the object it is wrapping.  If the object
    /// it is wrapping has finalized, Equals always returns false.
    /// </summary>
    internal sealed class EqualityWeakReference
    {
      private readonly int _hashCode;
      private readonly WeakReference _weakRef;

      internal EqualityWeakReference(object o)
      {
        _weakRef = new WeakReference(o);
        _hashCode = o.GetHashCode();
      }

      public bool IsAlive
      {
        get { return _weakRef.IsAlive; }
      }

      public object Target
      {
        get { return _weakRef.Target; }
      }

      public override bool Equals(object o)
      {
        if (o == null)
        {
          return false;
        }

        if (o.GetHashCode() != _hashCode)
        {
          return false;
        }

        if (o == this || ReferenceEquals(o, Target))
        {
          return true;
        }

        return false;
      }

      public override int GetHashCode()
      {
        return _hashCode;
      }
    }
  }
}
