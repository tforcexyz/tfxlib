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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xyz.TForce.Reflection.Mapping;

namespace System
{

  public static class CollectionConversionExtensions
  {

    /// <summary>
    /// Perform deep copy of current object to desired type. Generic version.
    /// </summary>
    /// <typeparam name="TSource">Type of the class you want to copy from</typeparam>
    /// <typeparam name="TResult">Type of the class you want to copy to</typeparam>
    /// <param name="collection"></param>
    /// <param name="postAction"></param>
    /// <param name="customMappingAction">Callback to create custom mapping for specified type</param>
    /// <param name="resolution">What should be done if a conflict between source type and target type cannot be resolved</param>
    /// <returns></returns>
    public static TResult[] ToArray<TSource, TResult>(this IEnumerable<TSource> collection, Action<TSource, TResult> postAction = null, Action<IObjectMappingRegister> customMappingAction = null, ObjectMappingConflictResolution resolution = ObjectMappingConflictResolution.Exception)
    {
      ICollection<TResult> results = new List<TResult>();
      if (collection == null)
      {
        return results.ToArray();
      }
      foreach (TSource item in collection)
      {
        TResult result = item.ToClass(postAction, customMappingAction, resolution);
        results.Add(result);
      }
      return results.ToArray();
    }

    /// <summary>
    /// Perform deep copy of current object to desired type. Generic version.
    /// </summary>
    /// <typeparam name="TResult">Type of the class you want to copy to</typeparam>
    /// <param name="collection"></param>
    /// <param name="customMappingAction">Callback to create custom mapping for specified type</param>
    /// <param name="resolution">What should be done if a conflict between source type and target type cannot be resolved</param>
    /// <returns></returns>
    public static TResult[] ToArray<TResult>(this IEnumerable collection, Action<IObjectMappingRegister> customMappingAction = null, ObjectMappingConflictResolution resolution = ObjectMappingConflictResolution.Exception)
    {
      ICollection<TResult> results = new List<TResult>();
      if (collection == null)
      {
        return results.ToArray();
      }
      foreach (object item in collection)
      {
        TResult result = item.ToClass<TResult>(customMappingAction, resolution);
        results.Add(result);
      }
      return results.ToArray();
    }

    /// <summary>
    /// Perform deep copy of current object to desired type. Generic version.
    /// </summary>
    /// <typeparam name="TSource">Type of the class you want to copy from</typeparam>
    /// <typeparam name="TResult">Type of the class you want to copy to</typeparam>
    /// <param name="collection"></param>
    /// <param name="postAction"></param>
    /// <param name="customMappingAction">Callback to create custom mapping for specified type</param>
    /// <param name="resolution">What should be done if a conflict between source type and target type cannot be resolved</param>
    /// <returns></returns>
    public static List<TResult> ToList<TSource, TResult>(this IEnumerable<TSource> collection, Action<TSource, TResult> postAction = null, Action<IObjectMappingRegister> customMappingAction = null, ObjectMappingConflictResolution resolution = ObjectMappingConflictResolution.Exception)
    {
      List<TResult> results = new List<TResult>();
      if (collection == null)
      {
        return results;
      }
      foreach (TSource item in collection)
      {
        TResult result = item.ToClass(postAction, customMappingAction, resolution);
        results.Add(result);
      }
      return results;
    }

    /// <summary>
    /// Perform deep copy of current object to desired type. Generic version.
    /// </summary>
    /// <typeparam name="TResult">Type of the class you want to copy to</typeparam>
    /// <param name="collection"></param>
    /// <param name="customMappingAction">Callback to create custom mapping for specified type</param>
    /// <param name="resolution">What should be done if a conflict between source type and target type cannot be resolved</param>
    /// <returns></returns>
    public static List<TResult> ToList<TResult>(this IEnumerable collection, Action<IObjectMappingRegister> customMappingAction = null, ObjectMappingConflictResolution resolution = ObjectMappingConflictResolution.Exception)
    {
      List<TResult> results = new List<TResult>();
      if (collection == null)
      {
        return results;
      }
      foreach (object item in collection)
      {
        TResult result = item.ToClass<TResult>(customMappingAction, resolution);
        results.Add(result);
      }
      return results;
    }
  }
}
