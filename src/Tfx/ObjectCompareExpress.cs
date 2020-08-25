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
using System.Linq;

namespace Xyz.TForce
{

  public class ObjectCompareExpress
  {

    public bool Equals<TModel>(TModel x, TModel y)
    {
      if (x == null && y == null)
      {
        return true;
      }
      if (x == null)
      {
        return false;
      }
      if (y == null)
      {
        return false;
      }

      return x.Equals(y);
    }

    public static int GetHashCode(object obj)
    {
      if (obj == null)
      {
        return 0;
      }
      int hash = obj.GetHashCode();
      return hash;
    }

    public static int GetHashCode(params object[] objs)
    {
      if (objs.Length == 0)
      {
        return 0;
      }
      int hash = GetHashCode(objs.First());
      for (int i = 1; i < objs.Length; i++)
      {
        object obj = objs[i];
        if (obj == null)
        {
          hash ^= 0;
          continue;
        }
        int objHash = obj.GetHashCode();
        int shiftedHash = ShiftAndWrap(objHash, i % 17);
        hash ^= shiftedHash;
      }
      return hash;
    }

    // Source: https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode
    public static int ShiftAndWrap(int value, int positions)
    {
      positions &= 0x1F;
      // Save the existing bit pattern, but interpret it as an unsigned integer.
      uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
      // Preserve the bits to be discarded.
      uint wrapped = number >> (32 - positions);
      // Shift and wrap the discarded bits.
      return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
    }
  }
}
