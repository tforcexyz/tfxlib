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

namespace Xyz.TForce.IO
{

  public class FileMetadata
  {
    public FileMetadata(bool archive, bool hidden, bool readOnly, bool system, long length, DateTime createdTime, DateTime accessTime, DateTime modifiedTime)
    {
      Archive = archive;
      Hidden = hidden;
      ReadOnly = readOnly;
      System = system;
      Length = length;
      CreatedTime = createdTime;
      AccessTime = accessTime;
      ModifiedTime = modifiedTime;
    }

    public bool Archive { get; private set; }

    public bool Hidden { get; private set; }

    public bool ReadOnly { get; private set; }

    public bool System { get; private set; }

    public long Length { get; private set; }

    public DateTime CreatedTime { get; private set; }

    public DateTime AccessTime { get; private set; }

    public DateTime ModifiedTime { get; private set; }
  }
}
