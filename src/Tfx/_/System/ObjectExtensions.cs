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

using System.Collections;

namespace System
{

  public static class ObjectExtensions
  {

    /// <summary>
    /// Javascript style truthy check
    /// </summary>
    /// <param name="object"></param>
    /// <returns></returns>
    public static bool IsTruthy(this object @object)
    {
      if (@object == null)
      {
        return false;
      }

      if (@object is bool || @object is bool?)
      {
        return @object == (object)true;
      }

      if (@object is int || @object is int?)
      {
        return @object != (object)0;
      }

      if (@object is long || @object is long?)
      {
        return @object != (object)0L;
      }

      if (@object is Array)
      {
        return ((Array)@object).Length > 0;
      }

      if (@object is ICollection)
      {
        return ((ICollection)@object).Count > 0;
      }

      return true;
    }
  }
}
