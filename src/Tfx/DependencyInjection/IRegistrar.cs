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

namespace Xyz.TForce.DependencyInjection
{

  public interface IRegistrar
  {

    object GetContainer();

    void Register<TRegister>(RegisterOptions options = null)
      where TRegister : class;

    void Register(Type tRegister, RegisterOptions options = null);

    void Register<TRegister>(TRegister instance, RegisterOptions options = null)
      where TRegister : class;

    void Register<TRegister, TResolve>(RegisterOptions options = null)
      where TResolve : TRegister;

    void Register(Type tRegister, Type tResolve, RegisterOptions options = null);

    void RegisterModule<TModule>()
      where TModule : IModule;

    void RegisterModule(IModule instance);

    void RegisterModule(Type tModule);

    TRegister Resolve<TRegister>();

    IEnumerable<TRegister> ResolveMany<TRegister>();

    TRegister SafeResolve<TRegister>();

    IEnumerable<TRegister> SafeResolveMany<TRegister>();
  }
}
