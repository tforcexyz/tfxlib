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
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xyz.TForce.Text;

namespace Xyz.TForce.Cryptography
{

  /// <summary>
  /// Generate random data
  /// </summary>
  public static class RandomExpress
  {

    /// <summary>
    /// Generate random bytes array
    /// </summary>
    /// <param name="length">Number of bytes to generate</param>
    /// <returns></returns>
    public static byte[] RandomizeBytes(int length)
    {

      byte[] randomBytes = new byte[length];
      using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
      {
        rng.GetBytes(randomBytes);
      }
      return randomBytes;
    }

    /// <summary>
    /// Generate random hex string
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string RandomizeHex(int length)
    {
      return RandomizeString(length, AlphabetTables.HexNumbers);
    }

    /// <summary>
    /// Pick a random item in a collection
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static TItem RandomizeItem<TItem>(IEnumerable<TItem> collection)
    {
      int size = collection.Size();
      if (size == 0)
      {
        return default(TItem);
      }
      int index = RandomizeInteger(0, size - 1);
      int i = 0;
      foreach (TItem item in collection)
      {
        if (i == index)
        {
          return item;
        }
        i++;
      }
      return default(TItem);
    }

    /// <summary>
    /// Pick a random item in an array
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static TItem RandomizeItem<TItem>(TItem[] array)
    {
      int size = array.Length;
      if (size == 0)
      {
        return default(TItem);
      }
      int index = RandomizeInteger(0, size - 1);
      return array[index];
    }

    /// <summary>
    /// Pick a numer of items in a collection
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="collection"></param>
    /// <param name="count"></param>
    /// <param name="noDuplicate"></param>
    /// <returns></returns>
    public static TItem[] RandomizeItems<TItem>(IEnumerable<TItem> collection, int count, bool noDuplicate = true)
    {
      TItem[] items = new TItem[count];
      IList<int> indexes = new List<int>();
      int size = collection.Size();
      for (int i = 0; i < count; i++)
      {
        if (size == 0)
        {
          items[i] = default(TItem);
          continue;
        }
        int index = RandomizeInteger(0, size - 1);
        if (noDuplicate)
        {
          if (indexes.Count == size)
          {
            indexes.Clear();
          }
          while (indexes.Contains(index))
          {
            index = RandomizeInteger(0, size - 1);
          }
          indexes.Add(index);
        }
        int j = 0;
        foreach (TItem item in collection)
        {
          if (j == index)
          {
            items[i] = item;
            break;
          }
          j++;
        }
      }
      return items;
    }

    /// <summary>
    /// Pick a numer of items in a collection
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="array"></param>
    /// <param name="count"></param>
    /// <param name="noDuplicate"></param>
    /// <returns></returns>
    public static TItem[] RandomizeItems<TItem>(TItem[] array, int count, bool noDuplicate = true)
    {
      TItem[] items = new TItem[count];
      IList<int> indexes = new List<int>();
      int size = array.Size();
      for (int i = 0; i < count; i++)
      {
        if (size == 0)
        {
          items[i] = default(TItem);
          continue;
        }
        int index = RandomizeInteger(0, size - 1);
        if (noDuplicate)
        {
          if (indexes.Count == size)
          {
            indexes.Clear();
          }
          while (indexes.Contains(index))
          {
            index = RandomizeInteger(0, size - 1);
          }
          indexes.Add(index);
        }
        items[i] = array[index];
      }
      return items;
    }

    /// <summary>
    /// Generate random boolean value
    /// </summary>
    /// <returns></returns>
    public static bool RandomizeBoolean()
    {
      int value = RandomizeInteger(0, 1);
      return value == 1;
    }

    /// <summary>
    /// Generate random double in specified range inclusive
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="precision">Number of floating digit</param>
    /// <returns></returns>
    public static double RandomizeDouble(double min, double max, int precision)
    {
      double precisionFactor = Math.Pow(10, precision);
      long precisionMin = (long)(min * precisionFactor);
      long precisionMax = (long)(max * precisionFactor);
      double randomValue = (double)RandomizeInteger64(precisionMin, precisionMax);
      double value = randomValue / precisionFactor;
      return value;
    }

    /// <summary>
    /// Generate random float in specified range inclusive
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="precision">Number of floating digit</param>
    /// <returns></returns>
    public static float RandomizeSingle(float min, float max, int precision)
    {
      float precisionFactor = (float)Math.Pow(10, precision);
      long precisionMin = (long)(min * precisionFactor);
      long precisionMax = (long)(max * precisionFactor);
      float randomValue = (float)RandomizeInteger64(precisionMin, precisionMax);
      float value = randomValue / precisionFactor;
      return value;
    }

    /// <summary>
    /// Generate random integer in specified range inclusive
    /// </summary>
    /// <param name="min">Lower bound of the range</param>
    /// <param name="max">Upper bound of the range</param>
    /// <returns></returns>
    public static int RandomizeInteger(int min, int max)
    {
      int maxSupported = 2147483647; // 2^31 - 1
      int[] modelLevels = new[] { 1, 256, 65536, 16777216, maxSupported }; // we consider the bytes array as an number with base of 256, this is the array of multipiler of each digit
      int modelLength = max - min + 1;
      if (modelLength > maxSupported)
      {
        throw new Exception();
      }

      // calculate requirement
      int modelLevel = modelLevels.Length - 1; // length of the random byte array, in this case is 4
      int modelThreshold = modelLevels[modelLevel] - (modelLevels[modelLevel] % modelLength) - 1; // get largest divisible of modelLength to give each output an equal probability

      // generate random number
      RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
      int result;

      byte[] randomBytes = new byte[modelLevel];
      int equivalentNumber;
      // convert the byte array to integer
      do
      {
        equivalentNumber = 0;
        rng.GetBytes(randomBytes);
        // sum all digits except the last one
        for (int i = 0; i < modelLevel - 1; i++)
        {
          equivalentNumber += randomBytes[i] * modelLevels[i];
        }
        // the last one doesn't have full 256 value
        if (modelLevel == randomBytes.Length && randomBytes[modelLevel - 1] > 127)
        {
          // cap it to maximum supported
          equivalentNumber = maxSupported;
        }
        else
        {
          // sum as a regular digit
          equivalentNumber += randomBytes[modelLevel - 1] * modelLevels[modelLevel - 1];
        }
      } while (equivalentNumber > modelThreshold);
      result = min + (equivalentNumber % modelLength);

      return result;
    }

    /// <summary>
    /// Generate random long integer in specified range inclusive
    /// </summary>
    /// <param name="min">Lower bound of the range</param>
    /// <param name="max">Upper bound of the range</param>
    /// <returns></returns>
    public static long RandomizeInteger64(long min, long max)
    {
      long maxSupported = 9223372036854775807L; // 2^63 - 1
      long[] modelLevels = new[] { 1L, 256L, 65536L, 16777216L, 4294967296L, 1099511627776L, 281474976710656L, 72057594037927936L, maxSupported }; // we consider the bytes array as an number with base of 256, this is the array of multipiler of each digit
      long modelLength = max - min + 1;
      if (modelLength > maxSupported)
      {
        throw new Exception();
      }

      // calculate requirement
      int modelLevel = modelLevels.Length - 1; // length of the random byte array, in this case is 8
      long modelThreshold = modelLevels[modelLevel] - (modelLevels[modelLevel] % modelLength) - 1; // get largest divisible of modelLength to give each output an equal probability

      // generate random number
      RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
      long result;

      byte[] randomBytes = new byte[modelLevel];
      long equivalentNumber;
      // convert the byte array to long integer
      do
      {
        equivalentNumber = 0L;
        rng.GetBytes(randomBytes);
        // sum all digits except the last one
        for (int i = 0; i < modelLevel - 1; i++)
        {
          equivalentNumber += randomBytes[i] * modelLevels[i];
        }
        // the last one doesn't have full 256 value
        if (modelLevel == randomBytes.Length && randomBytes[modelLevel - 1] > 127)
        {
          // cap it to maximum supported
          equivalentNumber = maxSupported;
        }
        else
        {
          // sum as a regular digit
          equivalentNumber += randomBytes[modelLevel - 1] * modelLevels[modelLevel - 1];
        }
      } while (equivalentNumber > modelThreshold);
      result = min + (equivalentNumber % modelLength);

      return result;
    }

    /// <summary>
    /// Generate random case-sensitive string
    /// </summary>
    /// <param name="length">Length of random string</param>
    /// <param name="customChar">Character space for generating random string. If not specified, alphanumeric will be used.</param>
    /// <returns></returns>
    public static string RandomizeString(int length, string customChar = null)
    {
      int maxSupported = 2147483647; // 2^31 - 1
      int[] modelLevels = new[] { 1, 256, 65536, 16777216, maxSupported }; // we consider the bytes array as an number with base of 256, this is the array of multipiler of each digit
      string modelString = AlphabetTables.All; // default charater space
      if (!string.IsNullOrEmpty(customChar))
      {
        modelString = customChar;
      }
      char[] modelArray = modelString.ToCharArray();
      int modelLength = modelString.Length;
      if (modelLength > 32768) // 2^15
      {
        throw new Exception();
      }

      // calculate requirement
      int modelThreshold = (modelLength * modelLength) - 1;
      int modelLevel = 0;
      //  determine number of bytes needed for randomization
      while (modelLevel < modelLevels.Length - 1 && modelThreshold > modelLevels[modelLevel])
      {
        modelLevel += 1;
      }
      modelThreshold = modelLevels[modelLevel] - (modelLevels[modelLevel] % modelLength) - 1; // get largest divisible of modelLength to give each output an equal probability

      // generate random string
      RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
      StringBuilder sb = new StringBuilder();
      while (sb.Length < length)
      {
        // generate each character of the string
        byte[] randomBytes = new byte[modelLevel];
        int equivalentNumber;
        // convert the byte array to integer
        do
        {
          equivalentNumber = 0;
          rng.GetBytes(randomBytes);
          // sum all digits except the last one
          for (int i = 0; i < modelLevel - 1; i++)
          {
            equivalentNumber += randomBytes[i] * modelLevels[i];
          }
          // the last one doesn't have full 256 value
          if (modelLevel == randomBytes.Length && randomBytes[modelLevel - 1] > 127)
          {
            // cap it to maximum supported
            equivalentNumber = maxSupported;
          }
          else
          {
            // sum as a regular digit
            equivalentNumber += randomBytes[modelLevel - 1] * modelLevels[modelLevel - 1];
          }
        } while (equivalentNumber > modelThreshold);
        char randomChar = modelArray[equivalentNumber % modelLength]; // loopup random char from character space
        _ = sb.Append(randomChar);
      }

      string result = sb.ToString();
      return result;
    }
  }
}
