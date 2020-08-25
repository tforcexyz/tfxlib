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
using System.Security.Cryptography;

namespace Xyz.TForce.Cryptography
{

  public sealed class Pbkdf2Crypt
  {

    public static Pbkdf2Result Hash(string password, int? saltLength, int? iteration, int? keyLength)
    {
      Pbkdf2Parameter parameter = new Pbkdf2Parameter();
      parameter.Password = BytesEx.FromString(password);
      parameter.SaltLength = saltLength;
      parameter.Iteration = iteration;
      parameter.KeyLength = keyLength;
      Pbkdf2Result pbkdf2 = Hash(parameter);
      return pbkdf2;
    }

    public static Pbkdf2Result Hash(Pbkdf2Parameter parameter)
    {
      if (parameter == null || parameter.Password == null)
      {
        throw new ArgumentNullException();
      }
      if (parameter.Salt == null && parameter.SaltLength == null)
      {
        parameter.SaltLength = 16;
      }

      byte[] passwordBytes = parameter.Password.Values;
      byte[] saltBytes = parameter.SaltLength.HasValue ?
          RandomExpress.RandomizeBytes(parameter.SaltLength.Value)
          : parameter.Salt.Values;
      int iteration = parameter.Iteration ?? 17;
      int keyLength = parameter.KeyLength ?? 16;
      Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(passwordBytes, saltBytes, iteration);
      byte[] keyBytes = hasher.GetBytes(keyLength);
      Pbkdf2Result pbkdf2 = new Pbkdf2Result(passwordBytes, saltBytes, iteration, keyBytes);
      return pbkdf2;
    }

    public static bool Verify(string password, byte[] salt, int iteration, byte[] key)
    {
      BytesEx passwordBytes = BytesEx.FromString(password);
      BytesEx saltBytes = BytesEx.FromBytes(salt);
      BytesEx keyBytes = BytesEx.FromBytes(key);
      Pbkdf2Result pbkdf2 = new Pbkdf2Result(passwordBytes, saltBytes, iteration, keyBytes);
      return Verify(pbkdf2);
    }

    public static bool Verify(Pbkdf2Result pbkdf2)
    {
      byte[] passwordBytes = pbkdf2.Password.Values;
      byte[] saltBytes = pbkdf2.Salt.Values;
      int iteration = pbkdf2.Iteration;
      int keyLength = pbkdf2.Key.Values.Length;
      Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(passwordBytes, saltBytes, iteration);
      byte[] keyBytes = hasher.GetBytes(keyLength);
      return pbkdf2.Key.Equals(keyBytes);
    }
  }
}
