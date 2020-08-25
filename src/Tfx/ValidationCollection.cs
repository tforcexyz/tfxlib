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

using System.Collections.Generic;
using System.Linq;

namespace Xyz.TForce
{

  public class ValidationCollection
  {

    internal ValidationCollection()
    {
      Items = new List<ValidationItem>();
    }

    public bool IsValidated
    {
      get { return !Items.Any(); }
    }

    public List<ValidationItem> Items { get; private set; }

    public void Clear()
    {
      Items.Clear();
    }

    public HashSet<string> GetMessages(string validationId)
    {
      ValidationItem item = Items.FirstOrDefault(x => { return x.Id == validationId; });
      if (item == null)
      {
        return new HashSet<string>();
      }
      return item.Messages;
    }

    public HashSet<string> GetTags(string validationId)
    {
      ValidationItem item = Items.FirstOrDefault(x => { return x.Id == validationId; });
      if (item == null)
      {
        return new HashSet<string>();
      }
      return item.Tags;
    }

    public void AddItem(string validationId, IEnumerable<string> messages, IEnumerable<string> tags)
    {
      ValidationItem item = Items.FirstOrDefault(x => { return x.Id == validationId; });
      if (item == null)
      {
        item = new ValidationItem(validationId, messages, tags);
        Items.Add(item);
      }
    }

    public bool ContainItem(string validationId)
    {
      return Items.Any(x => { return x.Id == validationId; });
    }

    public void RemoveItem(string validationId)
    {
      int index = Items.FindIndex(x => { return x.Id == validationId; });
      if (index > -1)
      {
        Items.RemoveAt(index);
      }
    }
  }
}
