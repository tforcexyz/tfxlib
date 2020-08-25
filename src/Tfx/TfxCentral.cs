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
using System.Linq;
using System.Reflection;
using Xyz.TForce.Diagnostics;
using Xyz.TForce.Ioc;

namespace Xyz.TForce
{

  /// <summary>
  /// Control node for TFX base library
  /// </summary>
  public static class TfxCentral
  {

    private static IIocRegistrar s_iocRegistrar;

    /// <summary>
    /// Initialize default variables
    /// </summary>
    static TfxCentral()
    {
      BufferLength = 65536; // 64K
      DefaultLogTag = Diagnostics.DefaultLogTag.Default;
      ProgressReportInterval = 125;  // 1/8 seconds
      RandomIdLength = 32;
    }

    /// <summary>
    /// Default buffer length for stream operations. In byte.
    /// </summary>
    public static int BufferLength { get; set; }

    /// <summary>
    /// Default log tag
    /// </summary>
    public static string DefaultLogTag { get; set; }

    /// <summary>
    /// Enable log for all libraries
    /// </summary>
    internal static bool EnableLog { get; private set; }

    /// <summary>
    /// Enable log for all libraries
    /// </summary>
    public static void EnableSystemLog()
    {
      if (Logger == null)
      {
        throw new InvalidOperationException("Messages.TfxCentral_EnableLogWhenLoggerIsNull");
      }
      EnableLog = true;
    }

    /// <summary>
    /// Enable log for all libraries
    /// </summary>
    public static void DisableSystemLog()
    {
      EnableLog = false;
    }

    /// <summary>
    /// Logger used for all libraries
    /// </summary>
    internal static IAdvancedLog Logger { get; private set; }

    /// <summary>
    /// Set system logger
    /// </summary>
    /// <param name="logger">Logger instance</param>
    public static void SetSystemLogger(IAdvancedLog logger)
    {
      if (EnableLog && logger == null)
      {
        throw new InvalidOperationException("Messages.TfxCentral_SetLoggerNullWhenEnableLog");
      }
      Logger = logger;
    }

    /// <summary>
    /// Control flag determining TFX platform has been initialized sucessfully
    /// </summary>
    public static bool Initialized { get; private set; }

    /// <summary>
    /// Mimimum time between each progress report in miliseconds
    /// </summary>
    public static long ProgressReportInterval { get; set; }

    /// <summary>
    /// Default Length for Random Id
    /// </summary>
    public static int RandomIdLength { get; set; }

    /// <summary>
    /// Initialize the TFX platform's advanced features
    /// </summary>
    public static void Initialize()
    {
      InitializeIoc();
    }

    private static void InitializeIoc()
    {
      if (s_iocRegistrar == null)
      {
        // Initialize default dependency injection engine
        // Scan for internal assemblies only
        s_iocRegistrar = new DefaultIocRegistrar();
        Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        Type iocModuleInterface = typeof(IIocModule);
        s_iocRegistrar.RegisterModule<TfxModule>();
        foreach (Assembly assembly in loadedAssemblies)
        {
          if (!assembly.FullName.StartsWith("Tfx."))
          {
            continue;
          }
          IEnumerable<Type> assemblyIocModules = assembly.GetTypes()
              .Where(x =>
              {
                return iocModuleInterface.IsAssignableFrom(x);
              });
          foreach (Type iocModule in assemblyIocModules)
          {
            s_iocRegistrar.RegisterModule(iocModule);
          }
        }
      }
    }

    /// <summary>
    /// Initialize the TFX platform's if not and return configured Ioc container
    /// </summary>
    /// <returns></returns>
    public static IIocRegistrar DefaultIoc()
    {
      Initialize();
      return s_iocRegistrar;
    }
  }
}
