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
using System.IO;
using System.Runtime.InteropServices;
using Xyz.TForce.IO;

namespace Xyz.TForce.Interop
{

  public class InteropExpress
  {

    public static void LoadNativeDirectory(params string[] paths)
    {
      foreach (string path in paths)
      {
        string realPath = PathExpress.ResolvePath(path);
        if (!Directory.Exists(realPath))
        {
          return;
        }
        string[] filePaths = Directory.GetFiles(realPath);
        foreach (string filePath in filePaths)
        {
          _ = LoadNativeLibrary(filePath);
        }
      }
    }

    public static IntPtr LoadNativeLibrary(string path)
    {
      IntPtr ptr;
      if (!File.Exists(path))
      {
        throw new FileNotFoundException("Messages.InteropExpress_FileNotFound", path);
      }
      if ((ptr = Kernel32Dll.LoadLibrary(path)) == IntPtr.Zero)
      {
        throw new InvalidOperationException("Messages.InteropExpress_LibraryNotLoaded");
      }
      return ptr;
    }

    public static TComInf CreateInstance<TComInf>(IntPtr libraryPtr, string classId, string interfaceId)
        where TComInf : class

    {
      IntPtr createObjectEntryPoint = Kernel32Dll.GetProcAddress(libraryPtr, "CreateObject");
      Kernel32Dll.CreateObjectDelegate createObject = Marshal.GetDelegateForFunctionPointer(createObjectEntryPoint, typeof(Kernel32Dll.CreateObjectDelegate)) as Kernel32Dll.CreateObjectDelegate;
      if (createObject == null)
      {
        throw new InvalidOperationException("Messages.InteropExpress_CreateInstanceFailed");
      }
      object result;
      Guid classGuid = new Guid(classId);
      Guid interfaceGuid = new Guid(interfaceId);
      _ = createObject(ref classGuid, ref interfaceGuid, out result);
      TComInf inf = result as TComInf;
      return inf;
    }
  }
}
