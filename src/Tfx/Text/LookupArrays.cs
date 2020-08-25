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

namespace Xyz.TForce.Text
{

  /// <summary>
  /// Internal table of constants
  /// </summary>
  internal sealed class LookupArrays
  {

    private static char[] s_constantLookup;
    public static char[] ConstantLookup
    {
      get
      {
        if (s_constantLookup == null)
        {
          s_constantLookup = new char[]
          {
            'A', 'a', '0', 'B', 'b', 'C', 'c', '1', 'D', 'd', 'E', 'e', '2', 'F', 'f', 'G', 'g', '3', 'H', 'h', 'I', 'i', '4', 'J', 'j',
            'K', 'k', '5', 'L', 'l', 'M', 'm', '6', 'N', 'n', 'O', 'o', '7', 'P', 'p', 'Q', 'q', '8', 'R', 'r', 'S', 's', '9', 'T', 't',
            'U', 'u', 'V', 'v', '-', 'W', 'w', 'X', 'x', 'Y', 'y', '_', 'Z', 'z'
          };
        }
        return s_constantLookup;
      }
    }

    private static char[] s_constantLookupLower;
    public static char[] ConstantLookupLower
    {
      get
      {
        if (s_constantLookupLower == null)
        {
          s_constantLookupLower = new char[]
          {
            ' ', 'a', 'b', '0', 'c', 'd', '1', 'e', 'f', '2', 'g', 'h', '3', 'i', 'j', '4', 'k', 'l', '5', 'm', 'n', '6', 'o', 'p', '7',
            'q', 'r', '8', 's', 't', '9', 'u', 'v', '-', 'w', 'x', '_', 'y', 'z'
          };
        }
        return s_constantLookupLower;
      }
    }

    private static char[] s_constantLookupUpper;
    public static char[] ConstantLookupUpper
    {
      get
      {
        if (s_constantLookupUpper == null)
        {
          s_constantLookupUpper = new char[]
          {
            ' ', 'A', 'B', '0', 'C', 'D', '1', 'E', 'F', '2', 'G', 'H', '3', 'I', 'J', '4', 'K', 'L', '5', 'M', 'N', '6', 'O', 'P', '7',
            'Q', 'R', '8', 'S', 'T', '9', 'U', 'V', '-', 'W', 'X', '_', 'Y', 'Z'
          };
        }
        return s_constantLookupUpper;
      }
    }

    private static char[] s_nbaseLookupLower;
    public static char[] NbaseLookupLower
    {
      get
      {
        if (s_nbaseLookupLower == null)
        {
          s_nbaseLookupLower = new char[]
          {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
            'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
          };
        }
        return s_nbaseLookupLower;
      }
    }

    private static char[] s_nbaseLookupUpper;
    public static char[] NbaseLookupUpper
    {
      get
      {
        if (s_nbaseLookupUpper == null)
        {
          s_nbaseLookupUpper = new char[]
          {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
            'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
          };
        }
        return s_nbaseLookupUpper;
      }
    }

    private static char[] s_tripleByteLookup;
    public static char[] TripleByteLookup
    {
      get
      {
        if (s_tripleByteLookup == null)
        {
          s_tripleByteLookup = new char[]
          {
            '-', '_', '`', '~', '=', '+', '[', '{', ']', '}', '|', '?', '.', ':',
            'a', 'b', '1', 'c', 'd', 'e', '#', '2', 'f', '$', 'g', '%', '3', 'h', '^', 'i', '&', '4', 'j', '*', 'k', '(', '5', 'l', ')',
            'm', '\'', '6', 'n', 'o', 'p', '<', '7', 'q', 'r', 's', '>', '8', 't', 'u', 'v', '@', '9', 'w', 'x', 'y', '!', '0', '/', 'z'
          };
        }
        return s_tripleByteLookup;
      }
    }
  }
}
