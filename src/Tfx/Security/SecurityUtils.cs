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
using System.Reflection;
using System.Security;

namespace Xyz.TForce.Security
{

  /// <summary>
  /// Useful methods to securely call 'dangerous' managed APIs (especially reflection).
  /// See http://wiki/default.aspx/Microsoft.Projects.DotNetClient.SecurityConcernsAroundReflection
  /// for more information specifically about why we need to be careful about reflection invocations.
  /// </summary>
  internal static class SecurityUtils
  {

    private static void DemandReflectionAccess(Type type)
    {
      try
      {
      }
      catch (SecurityException)
      {
        DemandGrantSet(type.Assembly);
      }
    }

    [SecuritySafeCritical]
    private static void DemandGrantSet(Assembly assembly)
    {
      _ = assembly;
    }

    private static bool HasReflectionPermission(Type type)
    {
      try
      {
        DemandReflectionAccess(type);
        return true;
      }
      catch (SecurityException)
      {
      }

      return false;
    }


    /// <summary>
    ///     This helper method provides safe access to Activator.CreateInstance.
    ///     NOTE: This overload will work only with public .ctors. 
    /// </summary>
    internal static object SecureCreateInstance(Type type)
    {
      return SecureCreateInstance(type, null, false);
    }


    /// <summary>
    ///     This helper method provides safe access to Activator.CreateInstance.
    ///     Set allowNonPublic to true if you want non public ctors to be used. 
    /// </summary>
    internal static object SecureCreateInstance(Type type, object[] args, bool allowNonPublic)
    {
      if (type == null)
      {
        throw new ArgumentNullException("type");
      }

      BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;

      // if it's an internal type, we demand reflection permission.
      if (!type.IsVisible)
      {
        DemandReflectionAccess(type);
      }
      else if (allowNonPublic && !HasReflectionPermission(type))
      {
        // Someone is trying to instantiate a public type in *our* assembly, but does not
        // have full reflection permission. We shouldn't pass BindingFlags.NonPublic in this case.
        // The reason we don't directly demand the permission here is because we don't know whether
        // a public or non-public .ctor will be invoked. We want to allow the public .ctor case to
        // succeed.
        allowNonPublic = false;
      }

      if (allowNonPublic)
      {
        flags |= BindingFlags.NonPublic;
      }

      return Activator.CreateInstance(type, flags, null, args, null);
    }

#if (!WINFORMS_NAMESPACE)

    /// <summary>
    ///     This helper method provides safe access to Activator.CreateInstance.
    ///     NOTE: This overload will work only with public .ctors. 
    /// </summary>
    internal static object SecureCreateInstance(Type type, object[] args)
    {
      return SecureCreateInstance(type, args, false);
    }


    /// <summary>
    ///     Helper method to safely invoke a .ctor. You should prefer SecureCreateInstance to this.
    ///     Set allowNonPublic to true if you want non public ctors to be used. 
    /// </summary>
    internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args, bool allowNonPublic)
    {
      return SecureConstructorInvoke(type, argTypes, args, allowNonPublic, BindingFlags.Default);
    }

    /// <summary>
    ///     Helper method to safely invoke a .ctor. You should prefer SecureCreateInstance to this.
    ///     Set allowNonPublic to true if you want non public ctors to be used. 
    ///     The 'extraFlags' parameter is used to pass in any other flags you need, 
    ///     besides Public, NonPublic and Instance.
    /// </summary>
    internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args,
                                                   bool allowNonPublic, BindingFlags extraFlags)
    {
      if (type == null)
      {
        throw new ArgumentNullException("type");
      }

      // if it's an internal type, we demand reflection permission.
      if (!type.IsVisible)
      {
        DemandReflectionAccess(type);
      }
      else if (allowNonPublic && !HasReflectionPermission(type))
      {
        // Someone is trying to invoke a ctor on a public type, but does not
        // have full reflection permission. We shouldn't pass BindingFlags.NonPublic in this case.
        allowNonPublic = false;
      }

      BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | extraFlags;
      if (!allowNonPublic)
      {
        flags &= ~BindingFlags.NonPublic;
      }

      ConstructorInfo ctor = type.GetConstructor(flags, null, argTypes, null);
      if (ctor != null)
      {
        return ctor.Invoke(args);
      }

      return null;
    }

    private static bool GenericArgumentsAreVisible(MethodInfo method)
    {
      if (method.IsGenericMethod)
      {
        Type[] parameterTypes = method.GetGenericArguments();
        foreach (Type type in parameterTypes)
        {
          if (!type.IsVisible)
          {
            return false;
          }
        }
      }
      return true;
    }

    /// <summary>
    ///     This helper method provides safe access to FieldInfo's GetValue method.
    /// </summary>
    internal static object FieldInfoGetValue(FieldInfo field, object target)
    {
      Type type = field.DeclaringType;
      if (type == null)
      {
        // Type is null for Global fields.
        if (!field.IsPublic)
        {
          DemandGrantSet(field.Module.Assembly);
        }
      }
      else if (!(type.IsVisible && field.IsPublic))
      {
        DemandReflectionAccess(type);
      }
      return field.GetValue(target);
    }

    /// <summary>
    ///     This helper method provides safe access to MethodInfo's Invoke method.
    /// </summary>
    internal static object MethodInfoInvoke(MethodInfo method, object target, object[] args)
    {
      Type type = method.DeclaringType;
      if (type == null)
      {
        // Type is null for Global methods. In this case we would need to demand grant set on 
        // the containing assembly for internal methods.
        if (!(method.IsPublic && GenericArgumentsAreVisible(method)))
        {
          DemandGrantSet(method.Module.Assembly);
        }
      }
      else if (!(type.IsVisible && method.IsPublic && GenericArgumentsAreVisible(method)))
      {
        // this demand is required for internal types in system.dll and its friend assemblies. 
        DemandReflectionAccess(type);
      }
      return method.Invoke(target, args);
    }

    /// <summary>
    ///     This helper method provides safe access to ConstructorInfo's Invoke method.
    ///     Constructors can't be generic, so we don't check if argument types are visible
    /// </summary>
    internal static object ConstructorInfoInvoke(ConstructorInfo ctor, object[] args)
    {
      Type type = ctor.DeclaringType;
      if ((type != null) && !(type.IsVisible && ctor.IsPublic))
      {
        DemandReflectionAccess(type);
      }
      return ctor.Invoke(args);
    }

    /// <summary>
    ///     This helper method provides safe access to Array.CreateInstance.
    /// </summary>
    internal static object ArrayCreateInstance(Type type, int length)
    {
      if (!type.IsVisible)
      {
        DemandReflectionAccess(type);
      }
      return Array.CreateInstance(type, length);
    }
#endif
  }
}
