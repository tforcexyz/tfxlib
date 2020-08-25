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
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using Xyz.TForce.Cryptography;

namespace Xyz.TForce.IO
{

  public class FileExpress
  {

    public static MemoryStream GetManifestResource(Assembly assembly, string source)
    {
      MemoryStream writer;
      using (Stream reader = assembly.GetManifestResourceStream(source))
      {
        if (reader == null)
        {
          throw new NullReferenceException(nameof(reader));
        }
        writer = new MemoryStream();
        _ = reader.Seek(0, SeekOrigin.Begin);
        reader.CopyStream(writer);
        _ = writer.Seek(0, SeekOrigin.Begin);
        writer.Close();
      }
      return writer;
    }

    public static void ExtractManifestResources(Assembly assembly, string source, string target, bool overwrite = false)
    {
      string realTarget = PathExpress.ResolvePath(target);
      if (overwrite == false && File.Exists(realTarget))
      {
        return;
      }
      DirectoryExpress.CreateParentDirectory(realTarget);
      using (Stream reader = assembly.GetManifestResourceStream(source))
      {
        if (reader == null)
        {
          throw new NullReferenceException(nameof(reader));
        }
        Stream writer = File.Create(realTarget);
        _ = reader.Seek(0, SeekOrigin.Begin);
        reader.CopyTo(writer);
        writer.Close();
      }
    }

    public static long FileSize(string path)
    {
      string realPath = PathExpress.ResolvePath(path);
      FileInfo fileInfo = new FileInfo(realPath);
      long length = fileInfo.Length;
      return length;
    }

    public static FileDescriptor GetFileDescriptor(string path, bool includeHash = false)
    {
      FileGetFileDescriptorArgument fileGetFileDecriptorArgument = new FileGetFileDescriptorArgument
      {
        Path = path,
        IncludePathInfo = true,
        IncludeAttribute = true
      };
      if (includeHash)
      {
        fileGetFileDecriptorArgument.IncludeMd5 = true;
        fileGetFileDecriptorArgument.IncludeSha1 = true;
        fileGetFileDecriptorArgument.IncludeSha256 = true;
        fileGetFileDecriptorArgument.IncludeSha512 = true;
      }
      FileDescriptor result = GetFileDescriptor(fileGetFileDecriptorArgument);
      return result;
    }

    internal static FileDescriptor GetFileDescriptor(FileGetFileDescriptorArgument args)
    {
      string realPath = PathExpress.ResolvePath(args.Path);
      string parent = Path.GetDirectoryName(realPath);
      string fullName = Path.GetFileName(realPath);
      string name = Path.GetFileNameWithoutExtension(fullName);
      string extension = Path.GetExtension(fullName);
      FileMetadata metadata = null;
      BytesEx md5Hash = null;
      BytesEx sha1Hash = null;
      BytesEx sha256Hash = null;
      BytesEx sha512Hash = null;
      if (args.IncludeAttribute)
      {
        FileInfo fileInfo = new FileInfo(realPath);
        bool isArchive = fileInfo.Attributes.HasFlag(FileAttributes.Archive);
        bool isReadOnly = fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly);
        bool isHidden = fileInfo.Attributes.HasFlag(FileAttributes.Hidden);
        bool isSystem = fileInfo.Attributes.HasFlag(FileAttributes.System);
        long length = fileInfo.Length;
        DateTime createdTime = fileInfo.CreationTimeUtc;
        DateTime modifiedTime = fileInfo.LastWriteTimeUtc;
        DateTime accessTime = fileInfo.LastAccessTimeUtc;
        metadata = new FileMetadata(isArchive, isHidden, isReadOnly, isSystem, length, createdTime, accessTime, modifiedTime);
        
      }
      if (args.IncludeMd5 || args.IncludeSha1 || args.IncludeSha256 || args.IncludeSha512)
      {
        string md5 = "md5";
        string sha1 = "sha1";
        string sha256 = "sha256";
        string sha512 = "sha512";
        Dictionary<string, System.Security.Cryptography.HashAlgorithm> cryptDictionary = new Dictionary<string, System.Security.Cryptography.HashAlgorithm>();
        if (args.IncludeMd5)
        {
          cryptDictionary[md5] = HashExpress.GetAlgorithm(HashAlgorithm.Md5);
        }
        if (args.IncludeSha1)
        {
          cryptDictionary[sha1] = HashExpress.GetAlgorithm(HashAlgorithm.Sha1);
        }
        if (args.IncludeSha256)
        {
          cryptDictionary[sha256] = HashExpress.GetAlgorithm(HashAlgorithm.Sha256);
        }
        if (args.IncludeSha512)
        {
          cryptDictionary[sha512] = HashExpress.GetAlgorithm(HashAlgorithm.Sha512);
        }
        System.Security.Cryptography.HashAlgorithm[] crypts = cryptDictionary.Values.ToArray();
        using (Stream fileStream = new FileStream(realPath, FileMode.Open))
        {
          byte[] buffer = new byte[HashExpress.StreamBufferSize];
          int read;
          long currentPosition = 0L;
          long streamLength = fileStream.Length;
          while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
          {
            currentPosition += read;
            foreach (System.Security.Cryptography.HashAlgorithm crypt in crypts)
            {
              if (currentPosition < streamLength)
              {
                int _ = crypt.TransformBlock(buffer, 0, read, buffer, 0);
              }
              else
              {
                byte[] _ = crypt.TransformFinalBlock(buffer, 0, read);
              }
            }
          }
          if (args.IncludeMd5)
          {
            md5Hash = BytesEx.FromBytes(cryptDictionary[md5].Hash);
          }
          if (args.IncludeSha1)
          {
            sha1Hash = BytesEx.FromBytes(cryptDictionary[sha1].Hash);
          }
          if (args.IncludeSha256)
          {
            sha256Hash = BytesEx.FromBytes(cryptDictionary[sha256].Hash);
          }
          if (args.IncludeSha512)
          {
            sha512Hash = BytesEx.FromBytes(cryptDictionary[sha512].Hash);
          }
        }
      }
      FileDescriptor result = new FileDescriptor(name, extension, parent, metadata, md5Hash, sha1Hash, sha256Hash, sha512Hash);
      return result;
    }

    public static FileDescriptor SaveToDisk(string path, Stream stream, bool overwrite = false)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      if (overwrite && File.Exists(realPath))
      {
        File.Delete(realPath);
      }
      using (Stream outputStream = new FileStream(realPath, FileMode.CreateNew))
      {
        if (stream.CanSeek)
        {
          stream.Position = 0;
        }
        stream.CopyTo(outputStream, TfxCentral.BufferLength);
      }
      FileGetFileDescriptorArgument fileGetFileDecriptorArgument = new FileGetFileDescriptorArgument
      {
        Path = realPath,
        IncludePathInfo = true,
        IncludeAttribute = true,
        IncludeMd5 = true,
        IncludeSha1 = true,
        IncludeSha256 = true,
        IncludeSha512 = true
      };
      FileDescriptor fileDescriptor = GetFileDescriptor(fileGetFileDecriptorArgument);
      return fileDescriptor;
    }

    public static FileDescriptor SaveToDisk(string path, byte[] contents, bool overwrite = false)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      if (overwrite && File.Exists(realPath))
      {
        File.Delete(realPath);
      }
      using (Stream outputStream = new FileStream(realPath, FileMode.CreateNew))
      {
        outputStream.Write(contents, 0, contents.Length);
      }
      FileGetFileDescriptorArgument fileGetFileDecriptorArgument = new FileGetFileDescriptorArgument
      {
        Path = realPath,
        IncludePathInfo = true,
        IncludeAttribute = true,
        IncludeMd5 = true,
        IncludeSha1 = true,
        IncludeSha256 = true,
        IncludeSha512 = true
      };
      FileDescriptor fileDescriptor = GetFileDescriptor(fileGetFileDecriptorArgument);
      return fileDescriptor;
    }

    public static void SetAttribute(string path, FileAttributeSetArgument attr)
    {
      string realPath = PathExpress.ResolvePath(path);
      FileInfo fileInfo = new FileInfo(realPath);
      FileAttributes fileAttrs = fileInfo.Attributes;
      if (attr.Archive.HasValue)
      {
        fileAttrs = attr.Archive == true ? fileAttrs | FileAttributes.Archive : fileAttrs & ~FileAttributes.Archive;
      }
      if (attr.Hidden.HasValue)
      {
        fileAttrs = attr.Hidden == true ? fileAttrs | FileAttributes.Hidden : fileAttrs & ~FileAttributes.Hidden;
      }
      if (attr.ReadOnly.HasValue)
      {
        fileAttrs = attr.ReadOnly == true ? fileAttrs | FileAttributes.ReadOnly : fileAttrs & ~FileAttributes.ReadOnly;
      }
      if (attr.System.HasValue)
      {
        fileAttrs = attr.System == true ? fileAttrs | FileAttributes.System : fileAttrs & ~FileAttributes.System;
      }
      fileInfo.Attributes = fileAttrs;
      if (fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly))
      {
        return;
      }
      if (attr.CreatedTime.HasValue)
      {
        fileInfo.CreationTime = attr.CreatedTime.Value;
      }
      if (attr.AccessedTime.HasValue)
      {
        fileInfo.LastAccessTime = attr.AccessedTime.Value;
      }
      if (attr.ModifiedTime.HasValue)
      {
        fileInfo.LastWriteTime = attr.ModifiedTime.Value;
      }
    }

    public static void AppendAllLines(string path, string[] contents, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      if (encoding == null)
      {
        File.AppendAllLines(realPath, contents);
      }
      else
      {
        File.AppendAllLines(realPath, contents, encoding);
      }
    }

    public static void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      if (encoding == null)
      {
        File.AppendAllLines(realPath, contents);
      }
      else
      {
        File.AppendAllLines(realPath, contents, encoding);
      }
    }

    public static void AppendAllText(string path, string content, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      if (encoding == null)
      {
        File.AppendAllText(realPath, content);
      }
      else
      {
        File.AppendAllText(realPath, content, encoding);
      }
    }

    public static FileStream Create(string path)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      return File.Create(path);
    }

    public static FileStream Create(string path, int bufferSize)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      return File.Create(path, bufferSize);
    }

    public static FileStream Create(string path, int bufferSize, FileOptions fileOptions)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      return File.Create(path, bufferSize, fileOptions);
    }

#if !NETSTANDARD2_0
    public static FileStream Create(string path, int bufferSize, FileOptions fileOptions, FileSecurity fileSecurity)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      return File.Create(path, bufferSize, fileOptions, fileSecurity);
    }
#endif

    public static void Copy(string sourcePath, string tartgetPath, bool overwrite = false)
    {
      string realSourcePath = PathExpress.ResolvePath(sourcePath);
      string realTargetPath = PathExpress.ResolvePath(tartgetPath);
      File.Copy(realSourcePath, realTargetPath, overwrite);
    }

    public static bool Exist(string path)
    {
      string realPath = PathExpress.ResolvePath(path);
      return File.Exists(realPath);
    }

    public static void Move(string sourcePath, string tartgetPath)
    {
      string realSourcePath = PathExpress.ResolvePath(sourcePath);
      string realTargetPath = PathExpress.ResolvePath(tartgetPath);
      DirectoryExpress.CreateParentDirectory(tartgetPath);
      File.Move(realSourcePath, realTargetPath);
    }

    public static FileStream Open(string path, FileMode fileMode)
    {
      return OpenInner(path, fileMode);
    }

    public static FileStream Open(string path, FileMode fileMode, FileAccess fileAccess)
    {
      return OpenInner(path, fileMode, fileAccess);
    }

    public static FileStream Open(string path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
    {
      return OpenInner(path, fileMode, fileAccess, fileShare);
    }

    private static FileStream OpenInner(string path, FileMode fileMode, FileAccess? fileAccess = null, FileShare? fileShare = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      if (fileMode == FileMode.Append || fileMode == FileMode.Create || fileMode == FileMode.CreateNew || fileMode == FileMode.OpenOrCreate)
      {
        DirectoryExpress.CreateParentDirectory(realPath);
      }
      if (fileAccess == null)
      {
        return File.Open(realPath, fileMode);
      }
      else if (fileShare == null)
      {
        return File.Open(realPath, fileMode, fileAccess.Value);
      }
      else
      {
        return File.Open(realPath, fileMode, fileAccess.Value, fileShare.Value);
      }
    }

    public FileStream OpenRead(string path)
    {
      return OpenInner(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
    }

    public FileStream OpenWrite(string path)
    {
      return OpenInner(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
    }

    public static void PrependAllLines(string path, string[] contents, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      IList<string> oldContents = new List<string>();
      if (File.Exists(realPath))
      {
        oldContents = File.ReadAllLines(realPath)
            .ToList();
      }
      IList<string> newContents = new List<string>(contents);
      foreach(string oldContent in oldContents)
      {
        newContents.Add(oldContent);
      }
      if (encoding == null)
      {
        File.WriteAllLines(realPath, newContents);
      }
      else
      {
        File.WriteAllLines(realPath, newContents, encoding);
      }
    }

    public static void PrependAllLines(string path, List<string> contents, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      IList<string> oldContents = new List<string>();
      if (File.Exists(realPath))
      {
        oldContents = File.ReadAllLines(realPath)
          .ToList();
      }
      IList<string> newContents = new List<string>(contents);
      foreach (string oldContent in oldContents)
      {
        newContents.Add(oldContent);
      }
      if (encoding == null)
      {
        File.WriteAllLines(realPath, newContents);
      }
      else
      {
        File.WriteAllLines(realPath, newContents, encoding);
      }
    }

    public static void PrependAllText(string path, string content, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      string oldContent = string.Empty;
      if (File.Exists(realPath))
      {
        oldContent = File.ReadAllText(realPath);
      }
      string newContent = content + oldContent;
      if (encoding == null)
      {
        File.WriteAllText(realPath, newContent);
      }
      else
      {
        File.WriteAllText(realPath, newContent, encoding);
      }
    }

    public byte[] ReadAllBytes(string path)
    {
      string realPath = PathExpress.ResolvePath(path);
      return File.ReadAllBytes(realPath);
    }

    public string[] ReadAllLines(string path, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      if (encoding == null)
      {
        return File.ReadAllLines(realPath);
      }
      return File.ReadAllLines(realPath, encoding);
    }

    public string ReadAllText(string path, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      if (encoding == null)
      {
        return File.ReadAllText(realPath);
      }
      return File.ReadAllText(realPath, encoding);
    }

    public IEnumerable<string> ReadLines(string path, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      if (encoding == null)
      {
        return File.ReadLines(realPath);
      }
      return File.ReadLines(realPath, encoding);
    }

    public static void Replace(string sourcePath, string tartgetPath, string backupPath, bool ignoreMetadataErrors = false)
    {
      string realSourcePath = PathExpress.ResolvePath(sourcePath);
      string realTargetPath = PathExpress.ResolvePath(tartgetPath);
      string realBackupPath = PathExpress.ResolvePath(backupPath);
      DirectoryExpress.CreateParentDirectory(tartgetPath);
      File.Replace(realSourcePath, realTargetPath, realBackupPath, ignoreMetadataErrors);
    }

    public static void WriteAllBytes(string path, byte[] contents)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      File.WriteAllBytes(realPath, contents);
    }

    public static void WriteAllLines(string path, string[] contents, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      if (encoding == null)
      {
        File.WriteAllLines(realPath, contents);
      }
      else
      {
        File.WriteAllLines(realPath, contents, encoding);
      }
    }

    public static void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      if (encoding == null)
      {
        File.WriteAllLines(realPath, contents);
      }
      else
      {
        File.WriteAllLines(realPath, contents, encoding);
      }
    }

    public static void WriteAllText(string path, string contents, Encoding encoding = null)
    {
      string realPath = PathExpress.ResolvePath(path);
      DirectoryExpress.CreateParentDirectory(realPath);
      if (encoding == null)
      {
        File.WriteAllText(realPath, contents);
      }
      else
      {
        File.WriteAllText(realPath, contents, encoding);
      }
    }
  }
}
