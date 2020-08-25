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
using LightInject;

namespace Xyz.TForce.Ioc
{

  /// <summary>
  /// Default implementation IoC registrar for internal use. Currently using DryIoc
  /// </summary>
  public class DefaultIocRegistrar : IIocRegistrar
  {

    private readonly ServiceContainer _container;

    public DefaultIocRegistrar()
    {
      _container = new ServiceContainer();
    }

    public object GetContainer()
    {
      return _container;
    }

    public void Register<TRegister>(RegisterOptions options = null)
                where TRegister : class
    {
      _ = _container.Register<TRegister>();
    }

    public void Register(Type tRegister, RegisterOptions options = null)
    {
      _ = _container.Register(tRegister);
    }

    public void Register<TRegister>(TRegister instance, RegisterOptions options = null)
        where TRegister : class
    {
      _ = _container.RegisterInstance(instance);
    }

    public void Register<TRegister, TResolve>(RegisterOptions options = null)
        where TResolve : TRegister
    {
      _ = _container.Register<TRegister, TResolve>();
    }

    public void Register(Type tRegister, Type tResolve, RegisterOptions options = null)
    {
      _ = _container.Register(tRegister, tResolve);
    }

    public void RegisterModule<TModule>() where TModule : IIocModule
    {
      IServiceContainer moduleContainer = new ServiceContainer();
      _ = moduleContainer.RegisterInstance<IIocRegistrar>(this);
      _ = moduleContainer.Register<TModule>();
      TModule module = moduleContainer.GetInstance<TModule>();
      module.Initialize(this);
    }

    public void RegisterModule(IIocModule module)
    {
      module.Initialize(this);
    }

    public void RegisterModule(Type tModule)
    {
      Type iocModuleInterface = typeof(IIocModule);
      if (!iocModuleInterface.IsAssignableFrom(tModule))
      {
        throw new InvalidOperationException();
      }
      IServiceContainer moduleContainer = new ServiceContainer();
      _ = moduleContainer.RegisterInstance<IIocRegistrar>(this);
      _ = moduleContainer.Register(typeof(IIocModule), tModule);
      IIocModule module = moduleContainer.GetInstance<IIocModule>();
      module.Initialize(this);
    }

    public TRegister Resolve<TRegister>()
    {
      return _container.GetInstance<TRegister>();
    }

    public IEnumerable<TRegister> ResolveMany<TRegister>()
    {
      return _container.GetAllInstances<TRegister>();
    }

    public TRegister SafeResolve<TRegister>()
    {
      try
      {
        TRegister instance = _container.GetInstance<TRegister>();
        return instance;
      }
      catch (Exception)
      {
        // ignored
      }

      return default(TRegister);
    }

    public IEnumerable<TRegister> SafeResolveMany<TRegister>()
    {
      try
      {
        IEnumerable<TRegister> instances = _container.GetAllInstances<TRegister>();
        return instances;
      }
      catch (Exception)
      {
        // ignored
      }

      return new TRegister[0];
    }
  }
}
