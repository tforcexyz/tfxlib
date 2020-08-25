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
using System.IO;
using System.Security.Cryptography;

namespace Xyz.TForce.Cryptography
{

  public class HashExpress
  {

    internal const int StreamBufferSize = 65536;

    public static byte[] Hash(byte[] input, HashAlgorithm algorithm)
    {
      System.Security.Cryptography.HashAlgorithm hasher = GetAlgorithm(algorithm);
      byte[] hashByte = hasher.ComputeHash(input);
      return hashByte;
    }

    public static BytesEx Hash(BytesEx input, HashAlgorithm algorithm)
    {
      System.Security.Cryptography.HashAlgorithm hasher = GetAlgorithm(algorithm);
      byte[] hashByte = hasher.ComputeHash(input.Values);
      BytesEx hash = BytesEx.FromBytes(hashByte);
      return hash;
    }

    public static string Hash(Stream input, HashAlgorithm algorithm)
    {
      System.Security.Cryptography.HashAlgorithm hasher = GetAlgorithm(algorithm);
      byte[] buffer = new byte[StreamBufferSize];
      int read;
      long currentPosition = 0L;
      long streamLength = input.Length;
      while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
      {
        currentPosition += read;
#pragma warning disable IDE0045 // Convert to conditional expression
        if (currentPosition < streamLength)
        {
          _ = hasher.TransformBlock(buffer, 0, read, buffer, 0);
        }
        else
        {
          _ = hasher.TransformFinalBlock(buffer, 0, read);
        }
#pragma warning restore IDE0045 // Convert to conditional expression
      }
      byte[] hashBytes = hasher.Hash;
      string hastStr = hashBytes.ToHex();
      return hastStr;
    }

    public static BytesEx Md5(BytesEx input)
    {
      BytesEx hash = Hash(input, HashAlgorithm.Md5);
      return hash;
    }

    public static string Md5(Stream input)
    {
      string hashStr = Hash(input, HashAlgorithm.Md5);
      return hashStr;
    }

    public static BytesEx Sha1(BytesEx input)
    {
      BytesEx hash = Hash(input, HashAlgorithm.Sha1);
      return hash;
    }

    public static string Sha1(Stream input)
    {
      string hashStr = Hash(input, HashAlgorithm.Sha1);
      return hashStr;
    }

    public static BytesEx Sha256(BytesEx input)
    {
      BytesEx hash = Hash(input, HashAlgorithm.Sha256);
      return hash;
    }

    public static string Sha256(Stream input)
    {
      string hashStr = Hash(input, HashAlgorithm.Sha256);
      return hashStr;
    }

    public static BytesEx Sha384(BytesEx input)
    {
      BytesEx hash = Hash(input, HashAlgorithm.Sha384);
      return hash;
    }

    public static string Sha384(Stream input)
    {
      string hashStr = Hash(input, HashAlgorithm.Sha384);
      return hashStr;
    }

    public static BytesEx Sha512(BytesEx input)
    {
      BytesEx hash = Hash(input, HashAlgorithm.Sha512);
      return hash;
    }

    public static string Sha512(Stream input)
    {
      string hashStr = Hash(input, HashAlgorithm.Sha512);
      return hashStr;
    }

    internal static System.Security.Cryptography.HashAlgorithm GetAlgorithm(HashAlgorithm algorithm)
    {
      System.Security.Cryptography.HashAlgorithm crypt;
      switch (algorithm)
      {
        case HashAlgorithm.Sha1:
          crypt = SHA1.Create();
          break;
        case HashAlgorithm.Sha256:
          crypt = SHA256.Create();
          break;
        case HashAlgorithm.Sha384:
          crypt = SHA384.Create();
          break;
        case HashAlgorithm.Sha512:
          crypt = SHA512.Create();
          break;
        default:
          crypt = MD5.Create();
          break;
      }
      return crypt;
    }
  }
}
