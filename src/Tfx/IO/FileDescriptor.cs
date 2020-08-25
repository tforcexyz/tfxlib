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

namespace Xyz.TForce.IO
{

  public class FileDescriptor : PathDescriptor
  {

    internal FileDescriptor(string name, string extension, string parent, FileMetadata property = null, BytesEx md5 = null, BytesEx sha1 = null, BytesEx sha256 = null, BytesEx sha512 = null)
      : base(name, extension, parent)
    {
      Property = property;
      Md5 = md5;
      Sha1 = sha1;
      Sha256 = sha256;
      Sha512 = sha512;
    }

    public FileMetadata Property { get; private set; }

    public BytesEx Md5 { get; private set; }

    public BytesEx Sha1 { get; private set; }

    public BytesEx Sha256 { get; private set; }

    public BytesEx Sha512 { get; private set; }
  }
}
