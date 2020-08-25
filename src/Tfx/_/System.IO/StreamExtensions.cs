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

using System.Text;
using Xyz.TForce;

namespace System.IO
{

  public static class StreamExtensions
  {

    public static void CopyStream(this Stream inStream, Stream outStream)
    {
      byte[] buffer = new byte[TfxCentral.BufferLength];
      int read;
      while ((read = inStream.Read(buffer, 0, buffer.Length)) > 0)
      {
        outStream.Write(buffer, 0, read);
      }
    }

    public static string ReadString(this Stream steam, Encoding encoding = null)
    {
      MemoryStream menoryStream = new MemoryStream();
      CopyStream(steam, menoryStream);
      byte[] bytes = menoryStream.ToArray();
      string @string = bytes.ToString(encoding);
      return @string;
    }

    public static void WriteString(this Stream stream, string @string, Encoding encoding = null)
    {
      byte[] bytes = @string.ToBytes(encoding);
      stream.Write(bytes, 0, bytes.Length);
    }
  }
}
