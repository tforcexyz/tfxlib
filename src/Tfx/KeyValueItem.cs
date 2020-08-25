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

namespace Xyz.TForce
{

  public class KeyValueItem : IEquatable<KeyValueItem>
  {

    public KeyValueItem(string key, string value, string group)
    {
      Group = group;
      Key = key;
      Value = value;
    }

    public bool Disabled { get; set; }

    public string Description { get; set; }

    public string Group { get; private set; }

    public string Key { get; private set; }

    public bool Selected { get; set; }

    public string Value { get; private set; }

    public bool Equals(KeyValueItem other)
    {
      if (other == null)
      {
        return false;
      }

      if (Key != other.Key)
      {
        return false;
      }
      if (Value != other.Value)
      {
        return false;
      }

      return true;
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }
      if (obj is KeyValueItem)
      {
        return Equals((KeyValueItem)obj);
      }
      return false;
    }

    public override int GetHashCode()
    {
      int hashCode = ObjectCompareExpress.GetHashCode(Group, Key, Value);
      return hashCode;
    }
  }
}
