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

namespace Xyz.TForce.Diagnostics
{

  public static class TfxLogger
  {

    public static void Error(string tag, string message, Exception ex)
    {
      if (TfxCentral.EnableLog)
      {
        TfxCentral.Logger.Error(tag, message, ex);
      }
    }

    public static void Warn(string tag, string message)
    {
      if (TfxCentral.EnableLog)
      {
        TfxCentral.Logger.Warn(tag, message);
      }
    }

    public static void Info(string tag, string message)
    {
      if (TfxCentral.EnableLog)
      {
        TfxCentral.Logger.Info(tag, message);
      }
    }

    public static void Debug(string tag, string message)
    {
      if (TfxCentral.EnableLog)
      {
        TfxCentral.Logger.Debug(tag, message);
      }
    }

    public static void Trace(string tag, string message)
    {
      if (TfxCentral.EnableLog)
      {
        TfxCentral.Logger.Trace(tag, message);
      }
    }

    public static void Error(string tag, object instance, string method, string message, Exception ex)
    {
      if (TfxCentral.EnableLog)
      {
        TfxCentral.Logger.Error(tag, instance, method, message, ex);
      }
    }

    public static void Warn(string tag, object instance, string method, string message)
    {
      if (TfxCentral.EnableLog)
      {
        TfxCentral.Logger.Warn(tag, instance, method, message);
      }
    }

    public static void Info(string tag, object instance, string method, string message)
    {
      if (TfxCentral.EnableLog)
      {
        TfxCentral.Logger.Info(tag, instance, method, message);
      }
    }

    public static void Debug(string tag, object instance, string method, string message)
    {
      if (TfxCentral.EnableLog)
      {
        TfxCentral.Logger.Debug(tag, instance, method, message);
      }
    }

    public static void Trace(string tag, object instance, string method, string message)
    {
      if (TfxCentral.EnableLog)
      {
        TfxCentral.Logger.Trace(tag, instance, method, message);
      }
    }
  }
}
