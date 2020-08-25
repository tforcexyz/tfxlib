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
using System.Threading;

namespace Xyz.TForce.Collections.Specialized
{

  /// <summary>
  /// This is a multithreadsafe version of System.Collections.Specialized.BitVector32.
  /// </summary>
  [Serializable]
  internal struct SafeBitVector32
  {
    private volatile int _data;

    internal bool this[int bit]
    {
      get
      {
        int data = _data;
        return (data & bit) == bit;
      }
      set
      {
        for (; ; )
        {
          int oldData = _data;
          int newData = value ? oldData | bit : oldData & ~bit;

#pragma warning disable 0420
          int result = Interlocked.CompareExchange(ref _data, newData, oldData);
#pragma warning restore 0420

          if (result == oldData)
          {
            break;
          }
        }
      }
    }

    internal bool ChangeValue(int bit, bool value)
    {
      for (; ; )
      {
        int oldData = _data;
        int newData = value ? oldData | bit : oldData & ~bit;

        if (oldData == newData)
        {
          return false;
        }

#pragma warning disable 0420
        int result = Interlocked.CompareExchange(ref _data, newData, oldData);
#pragma warning restore 0420

        if (result == oldData)
        {
          return true;
        }
      }
    }
  }
}
