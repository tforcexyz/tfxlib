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

namespace Xyz.TForce.IO
{

  public class PathExpress
  {

    private static readonly string s_directorySpectatorStr = new string(Path.DirectorySeparatorChar, 1);

    /// <summary>
    /// Get current excutable path
    /// </summary>
    /// <returns></returns>
    public static string GetExecutableDirectory()
    {
      // Source: http://stackoverflow.com/questions/3991933/get-path-for-my-exe
      string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
      return path;
    }

    /// <summary>
    /// Get environment variable value
    /// </summary>
    /// <param name="variable"></param>
    /// <returns></returns>
    public static string GetEnvironmentVariablePath(string variable)
    {
      // Trim % at start and end
      string envVarName = variable
        .TrimStart('%')
        .TrimEnd('%');
      // Resolve to absolute path
      string result = Environment.GetEnvironmentVariable(envVarName);
      return result;
    }

    public static PathDescriptor GetPathDescriptor(string path)
    {
      string parentDirectoryPath = Path.GetDirectoryName(path);
      string fullName = Path.GetFileName(path);
      string name = Path.GetFileNameWithoutExtension(fullName);
      string extension = Path.GetExtension(fullName);
      PathDescriptor result = new PathDescriptor(name, extension, parentDirectoryPath);
      return result;
    }

    private static string MakeSafePath(string path)
    {
      char[] safePath = new char[path.Length];
      IList<char> invalidChars = Path.GetInvalidFileNameChars()
          .ToList();
      _ = invalidChars.Remove('\\');
      int startIdx = 0;
      if (path.Length > 1 && char.IsLetter(path, 0) && path[1] == ':')
      {
        safePath[0] = path[0];
        safePath[1] = path[1];
        startIdx = 2;
      }
      for (int i = startIdx; i < path.Length; i++)
      {
        char chr = path[i];
        safePath[i] = invalidChars.Contains(chr) ? '_' : chr;
      }
      string result = new string(safePath);
      return result;
    }

    private static string ProcessVariable(string path)
    {
      string result = path;
      // result = TryToResolve(result, SpecialDirectory.Cache, (string)Registry.GetValue(UserShellFolders, "Cache", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.Cookies, (string)Registry.GetValue(UserShellFolders, "Cookies", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.Desktop, (string)Registry.GetValue(UserShellFolders, "Desktop", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.Documents, (string)Registry.GetValue(UserShellFolders, "Personal", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.Downloads, (string)Registry.GetValue(UserShellFolders, "{374DE290-123F-4565-9164-39C4925E467B}", string.Empty));
      // result = TryToResolve(result, SpecialDirectories.ExecutableLogs, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"));
      result = TryToResolve(result, SpecialDirectories.ExecutablePath, AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'));
      // result = TryToResolve(result, SpecialDirectories.ExecutableRequired, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "required"));
      // result = TryToResolve(result, SpecialDirectories.ExecutableWorkspace, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "workspaces"));
      // result = TryToResolve(result, SpecialDirectory.Favorites, (string)Registry.GetValue(UserShellFolders, "Favorites", string.Empty));
      // result = TryToResolve(result, SpecialDirectories.Fonts, Path.Combine(SpecialDirectories.SystemRoot, "fonts"));
      // result = TryToResolve(result, SpecialDirectory.History, (string)Registry.GetValue(UserShellFolders, "History", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.Music, (string)Registry.GetValue(UserShellFolders, "My Music", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.Pictures, (string)Registry.GetValue(UserShellFolders, "My Pictures", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.Recent, (string)Registry.GetValue(UserShellFolders, "Recent", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.SendTo, (string)Registry.GetValue(UserShellFolders, "SendTo", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.StartMenu, (string)Registry.GetValue(UserShellFolders, "Start Menu", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.StartMenu, (string)Registry.GetValue(UserShellFolders, "Startup", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.System32, Path.Combine(SpecialDirectory.SystemRoot, "system32"));
      // result = TryToResolve(result, SpecialDirectory.Templates, (string)Registry.GetValue(UserShellFolders, "Templates", string.Empty));
      // result = TryToResolve(result, SpecialDirectory.Videos, (string)Registry.GetValue(UserShellFolders, "My Video", string.Empty));
      return result;
    }

    private static string TryToResolve(string path, string variable, string value)
    {
      string result = path;
      if (string.IsNullOrEmpty(value))
      {
        return path;
      }
      result = result.Replace(variable, value);
      return result;
    }

    private static string ProcessEnvironmentVariable(string path)
    {
      string result = path;
      int percentPos = result.IndexOf("%", StringComparison.Ordinal);
      while (percentPos > -1)
      {
        int nextPercentPos = result.IndexOf("%", percentPos + 1, StringComparison.Ordinal);
        if (nextPercentPos > -1)
        {
          string envVarName = result.Substring(percentPos, nextPercentPos - percentPos + 1);
          string envVarValue = GetEnvironmentVariablePath(envVarName);
          if (envVarValue != null)
          {
            result = result.Replace(envVarName, envVarValue);
          }
          percentPos = result.IndexOf("%", StringComparison.Ordinal);
        }
        else
        {
          percentPos = -1;
        }
      }
      return result;
    }

    public static string ResolvePath(string path)
    {
      string result = path.TrimEnd(Path.DirectorySeparatorChar);
      result = MakeSafePath(result);
      if (result.StartsWith("{") || result.Contains("}"))
      {
        result = ProcessVariable(result);
      }
      if (result.StartsWith("%"))
      {
        result = ProcessEnvironmentVariable(result);
      }
      if (result.StartsWith(".")) // relative path
      {
        return ResolveRelativePath(result);
      }

      if (result.StartsWith(s_directorySpectatorStr)) // root path
      {
        return ResolveRootPath(result);
      }

      if (result.IndexOf(":", StringComparison.Ordinal) == 1) // absolute path (windows)
      {
        return result;
      }
      // relative path
      return ResolveRelativePath(result);
    }

    private static string ResolveRootPath(string path)
    {
      string currentPath = Environment.CurrentDirectory;
      string currentDrive = Path.GetPathRoot(currentPath);
      string resultPath = Path.Combine(currentDrive, path);
      return resultPath;
    }

    private static string ResolveRelativePath(string path)
    {
      bool throwException = false;
      string currentPath = Environment.CurrentDirectory;
      IList<string> currentPathParts = currentPath.Split('\\').ToList();
      string[] pathParts = path.Split('\\');
      for (int i = 0; i < pathParts.Length; i++)
      {
        string currentPart = pathParts[i];
        if (currentPart.Equals("."))
        {
          // bypass
          if (throwException)
          {
            throw new ArgumentException("Messages.PathExpress_InvalidPath", nameof(path));
          }
        }
        else if (currentPart.Equals(".."))
        {
          currentPathParts.RemoveAt(currentPathParts.Count - 1);
          if (throwException)
          {
            throw new ArgumentException("Messages.PathExpress_InvalidPath", nameof(path));
          }
        }
        else
        {
          throwException = true;
          currentPathParts.Add(currentPart);
        }
      }
      string resultPath = string.Join(s_directorySpectatorStr, currentPathParts);
      return resultPath;
    }
  }
}
