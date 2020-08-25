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

namespace Xyz.TForce
{

  /// <summary>
  /// Container for result with collection of generic return values
  /// </summary>
  /// <typeparam name="TResult">Type of each item in the results</typeparam>
  public class CollectionResult<TResult> : BaseResult
  {

    public CollectionResult()
    {
      Results = new TResult[0];
    }

    public override long Type { get { return ResultType.Collection; } }

    public TResult[] Results { get; set; }

    public int Count
    {
      get { return (int)LongCount; }
      set { LongCount = value; }
    }

    public long LongCount { get; set; }
  }
}
