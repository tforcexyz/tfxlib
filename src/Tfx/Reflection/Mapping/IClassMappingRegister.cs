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
using System.Linq.Expressions;

namespace Xyz.TForce.Reflection.Mapping
{

  public interface IClassMappingRegister<TSource, TTarget>
      where TSource : class
      where TTarget : class
  {

    ClassMappingConfiguration<TSource, TTarget> Map<TSourceValue, TTargetValue>(Expression<Func<TSource, TSourceValue>> sourceExpression,
      Expression<Func<TTarget, TTargetValue>> targetExpression);
  }
}
