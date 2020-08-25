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
using System.IO;
using System.Linq;
using Xyz.TForce.Diagnostics;

namespace Xyz.TForce.IO
{

  public class DirectoryExpress
  {

    private static readonly DirectoryExpress s_instance;

    static DirectoryExpress()
    {
      s_instance = new DirectoryExpress();
    }

    public static long CalculateSize(string path)
    {
      DirectoryInfo dirInfo = new DirectoryInfo(path);
      return CalculateSize(dirInfo);
    }

    public static long CalculateSize(DirectoryInfo root)
    {
      long size = 0L;
      FileInfo[] fileInfos = root.GetFiles();
      foreach (FileInfo fileInfo in fileInfos)
      {
        size += fileInfo.Length;
      }
      DirectoryInfo[] dirInfos = root.GetDirectories();
      foreach (DirectoryInfo dirInfo in dirInfos)
      {
        size += CalculateSize(dirInfo);
      }
      return size;
    }

    public static void CreateDirectory(string path)
    {
      string realPath = PathExpress.ResolvePath(path);
      _ = Directory.CreateDirectory(realPath);
    }

    public static void CreateParentDirectory(string path)
    {
      string realPath = PathExpress.ResolvePath(path);
      string parentPath = Path.GetDirectoryName(realPath);
      if (parentPath == null)
      {
        throw new ArgumentNullException(nameof(parentPath));
      }
      _ = Directory.CreateDirectory(parentPath);
    }

    public static DirectoryDescriptor GetDirectoryDescriptor(string path, bool includeProperty = false)
    {
      string realPath = PathExpress.ResolvePath(path);
      string name = Path.GetFileName(realPath);
      string parentPath = Path.GetDirectoryName(realPath);
      DirectoryDescriptor result = new DirectoryDescriptor(name, null, parentPath);
      return result;
    }

    public static DirectoryDescriptor[] GetDirectoryDescriptors(string path, bool recursive)
    {
      DirectoryDescriptor[] results = GetDirectoryDescriptors(path, null, recursive);
      return results;
    }

    public static DirectoryDescriptor[] GetDirectoryDescriptors(string path, string pattern = null, bool recursive = false)
    {
      string realPath = PathExpress.ResolvePath(path);
      ICollection<DirectoryDescriptor> results = new List<DirectoryDescriptor>();
      string[] directoryPathArray = pattern == null ? Directory.GetDirectories(realPath) : Directory.GetDirectories(realPath, pattern);
      foreach (string directoryPath in directoryPathArray)
      {
        DirectoryDescriptor directoryDescriptor = GetDirectoryDescriptor(directoryPath);
        results.Add(directoryDescriptor);
      }
      if (recursive)
      {
        string[] subDirectoryPaths = Directory.GetDirectories(realPath);
        foreach (string directoryPath in subDirectoryPaths)
        {
          DirectoryDescriptor[] directoryDescriptors = GetDirectoryDescriptors(directoryPath, pattern, true);
          foreach (DirectoryDescriptor directoryDescriptor in directoryDescriptors)
          {
            results.Add(directoryDescriptor);
          }
        }
      }
      return results.ToArray();
    }

    public static FileDescriptor[] GetFileDescriptors(string path, bool recursive)
    {
      FileDescriptor[] results = GetFileDescriptors(path, null, recursive);
      return results;
    }

    public static FileDescriptor[] GetFileDescriptors(string path, string pattern = null, bool recursive = false)
    {
      string realPath = PathExpress.ResolvePath(path);
      ICollection<FileDescriptor> results = new List<FileDescriptor>();
      string[] filePaths = pattern == null ? Directory.GetFiles(realPath) : Directory.GetFiles(realPath, pattern);
      foreach (string filePath in filePaths)
      {
        FileDescriptor fileDescriptor = FileExpress.GetFileDescriptor(filePath);
        results.Add(fileDescriptor);
      }
      if (recursive)
      {
        string[] directoryPaths = Directory.GetDirectories(realPath);
        foreach (string directoryPath in directoryPaths)
        {
          FileDescriptor[] fileDescriptors = GetFileDescriptors(directoryPath, pattern, true);
          foreach (FileDescriptor fileDescriptor in fileDescriptors)
          {
            results.Add(fileDescriptor);
          }
        }
      }
      return results.ToArray();
    }

    public static void RecursiveSetAttributes(string path, FileAttributeSetArgument attr)
    {
      string realPath = PathExpress.ResolvePath(path);
      string[] dirPaths = Directory.GetDirectories(realPath);
      string[] filePaths = Directory.GetFiles(realPath);
      try
      {
        SetAttribute(realPath, attr);
      }
      catch (Exception ex)
      {
        TfxLogger.Error(DefaultLogTag.System, s_instance, "RecursiveSetAttributes", string.Empty, ex);
      }
      foreach (string dirPath in dirPaths)
      {
        RecursiveSetAttributes(dirPath, attr);
      }
      foreach (string filePath in filePaths)
      {
        try
        {
          FileExpress.SetAttribute(filePath, attr);
        }
        catch (Exception ex)
        {
          TfxLogger.Error(DefaultLogTag.System, s_instance, "RecursiveSetAttributes", string.Empty, ex);
        }
      }
    }

    public static void RecursiveDelete(string path, bool aggressiveMode = false)
    {
      string realPath = PathExpress.ResolvePath(path);
      if (aggressiveMode)
      {
        try
        {
          Directory.Delete(realPath, true);
        }
        catch (Exception ex)
        {
          TfxLogger.Error(DefaultLogTag.System, s_instance, "RecursiveDelete", string.Empty, ex);
          string[] dirPaths = Directory.GetDirectories(realPath);
          string[] filePaths = Directory.GetFiles(realPath);
          foreach (string dirPath in dirPaths)
          {
            RecursiveDelete(dirPath, true);
          }
          foreach (string filePath in filePaths)
          {
            try
            {
              File.Delete(filePath);
            }
            catch (Exception exc)
            {
              TfxLogger.Error(DefaultLogTag.System, s_instance, "RecursiveDelete", string.Empty, exc);
            }
          }
        }
      }
      else
      {
        Directory.Delete(realPath, true);
      }
    }

    public static void SetAttribute(string path, FileAttributeSetArgument attr)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryInfo dirInfo = new DirectoryInfo(realPath);
      FileAttributes dirAttrs = dirInfo.Attributes;
      if (attr.Archive.HasValue)
      {
        dirAttrs = attr.Archive == true ? dirAttrs | FileAttributes.Archive : dirAttrs & ~FileAttributes.Archive;
      }
      if (attr.Hidden.HasValue)
      {
        dirAttrs = attr.Hidden == true ? dirAttrs | FileAttributes.Hidden : dirAttrs & ~FileAttributes.Hidden;
      }
      if (attr.ReadOnly.HasValue)
      {
        dirAttrs = attr.ReadOnly == true ? dirAttrs | FileAttributes.ReadOnly : dirAttrs & ~FileAttributes.ReadOnly;
      }
      if (attr.System.HasValue)
      {
        dirAttrs = attr.System == true ? dirAttrs | FileAttributes.System : dirAttrs & ~FileAttributes.System;
      }
      dirInfo.Attributes = dirAttrs;
      if (dirInfo.Attributes.HasFlag(FileAttributes.ReadOnly))
      {
        return;
      }
      if (attr.CreatedTime.HasValue)
      {
        dirInfo.CreationTime = attr.CreatedTime.Value;
      }
      if (attr.AccessedTime.HasValue)
      {
        dirInfo.LastAccessTime = attr.AccessedTime.Value;
      }
      if (attr.ModifiedTime.HasValue)
      {
        dirInfo.LastWriteTime = attr.ModifiedTime.Value;
      }
    }
  }
}
