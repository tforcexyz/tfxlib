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

namespace Xyz.TForce.Reflection.Mapping
{

  public sealed class ObjectMappingConfiguration : IObjectMappingRegister, IObjectMappingResolver
  {

    private readonly Dictionary<ObjectMappingSignature, IClassMappingResolver> _mappingDictionary;

    public ObjectMappingConfiguration()
    {
      _mappingDictionary = new Dictionary<ObjectMappingSignature, IClassMappingResolver>();
    }

    public IClassMappingRegister<TSource, TTarget> Configure<TSource, TTarget>()
        where TSource : class
        where TTarget : class
    {
      ObjectMappingSignature mappingSignature = new ObjectMappingSignature();
      mappingSignature.SourceType = typeof(TSource);
      mappingSignature.TargetType = typeof(TTarget);
      ClassMappingConfiguration<TSource, TTarget> mappingConfig;
      if (_mappingDictionary.ContainsKey(mappingSignature))
      {
        mappingConfig = _mappingDictionary[mappingSignature] as ClassMappingConfiguration<TSource, TTarget>;
      }
      else
      {
        mappingConfig = new ClassMappingConfiguration<TSource, TTarget>();
        _mappingDictionary[mappingSignature] = mappingConfig;
      }
      return mappingConfig;
    }

    public IClassMappingResolver GetConfiuration(Type sourceType, Type targetType)
    {
      ObjectMappingSignature mappingSignature = new ObjectMappingSignature();
      mappingSignature.SourceType = sourceType;
      mappingSignature.TargetType = targetType;
      if (_mappingDictionary.ContainsKey(mappingSignature))
      {
        IClassMappingResolver mappingConfig = _mappingDictionary[mappingSignature];
        return mappingConfig;
      }
      return null;
    }
  }
}
