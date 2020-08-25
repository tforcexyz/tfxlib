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
using System.Globalization;

namespace Xyz.TForce.Ioc
{

  internal class SingleServiceResolver<TService>
      where TService : class
  {

    private readonly Lazy<TService> _currentValueFromResolver;
    private readonly Func<TService> _currentValueThunk;
    private readonly TService _defaultValue;
    private readonly Func<IIocRegistrar> _resolverThunk;
    private readonly string _callerMethodName;

    public SingleServiceResolver(Func<TService> currentValueThunk, TService defaultValue, string callerMethodName)
    {
      if (currentValueThunk == null)
      {
        throw new ArgumentNullException(nameof(currentValueThunk));
      }
      if (defaultValue == null)
      {
        throw new ArgumentNullException(nameof(defaultValue));
      }

      _resolverThunk = TfxCentral.DefaultIoc;
      _currentValueFromResolver = new Lazy<TService>(GetValueFromResolver);
      _currentValueThunk = currentValueThunk;
      _defaultValue = defaultValue;
      _callerMethodName = callerMethodName;
    }

    internal SingleServiceResolver(Func<TService> staticAccessor, TService defaultValue, IIocRegistrar resolver, string callerMethodName)
        : this(staticAccessor, defaultValue, callerMethodName)
    {
      if (resolver != null)
      {
        _resolverThunk = TfxCentral.DefaultIoc;
      }
    }

    public TService Current
    {
      get { return _currentValueFromResolver.Value ?? _currentValueThunk() ?? _defaultValue; }
    }

    private TService GetValueFromResolver()
    {
      TService result = _resolverThunk().SafeResolve<TService>();

      if (result != null && _currentValueThunk() != null)
      {
        throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "MvcResources.SingleServiceResolver_CannotRegisterTwoInstances", typeof(TService).Name, _callerMethodName));
      }

      return result;
    }
  }
}
