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

namespace Xyz.TForce.Cryptography
{

  public class Pbkdf2Result
  {

    public Pbkdf2Result(byte[] password, byte[] salt, int iteration, byte[] key)
    {
      Password = BytesEx.FromBytes(password);
      Salt = BytesEx.FromBytes(salt);
      Iteration = iteration;
      Key = BytesEx.FromBytes(key);
    }

    public Pbkdf2Result(BytesEx password, BytesEx salt, int iteration, BytesEx key)
    {
      Password = password;
      Salt = salt;
      Iteration = iteration;
      Key = key;
    }

    public BytesEx Password { get; }

    public BytesEx Salt { get; }

    public int Iteration { get; }

    public BytesEx Key { get; }
  }
}
