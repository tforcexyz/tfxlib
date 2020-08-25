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
using System.Text;

namespace Xyz.TForce
{

  /// <summary>
  /// Represent of array of bytes in different form
  /// </summary>
  public class BytesEx
  {

    public static BytesEx FromBase64(string base64)
    {
      byte[] bytes = base64.AsBase64ToBytes();
      BytesEx result = new BytesEx
      {
        Values = bytes
      };
      return result;
    }

    public static BytesEx FromBytes(byte[] bytes)
    {
      BytesEx result = new BytesEx
      {
        Values = bytes
      };
      return result;
    }

    public static BytesEx FromHex(string hex)
    {
      byte[] bytes = hex.AsHexToBytes();
      BytesEx result = new BytesEx
      {
        Values = bytes
      };
      return result;
    }

    public static BytesEx FromString(string @string)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(@string);
      BytesEx result = new BytesEx
      {
        Values = bytes
      };
      result.Values = bytes;
      return result;
    }

    public byte[] Values { get; set; }

    public string Base64
    {
      get { return Values.ToBase64(); }
      set { Values = value.AsBase64ToBytes(); }
    }

    public string Hex
    {
      get { return Values.ToHex(); }
      set { Values = value.AsHexToBytes(); }
    }

    public string String
    {
      get { return Values.ToString(Encoding.UTF8); }
      set { Values = value.ToBytes(Encoding.UTF8); }
    }

    public bool Equals(byte[] bytes)
    {
      if (Values.Length != bytes.Length)
      {
        return false;
      }
      for (int i = 0; i < Values.Length; i++)
      {
        if (Values[i] != bytes[i])
        {
          return false;
        }
      }
      return true;
    }
  }
}
