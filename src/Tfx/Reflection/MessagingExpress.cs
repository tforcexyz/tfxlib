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

namespace Xyz.TForce.Reflection
{

  /// <summary>
  /// Handle communications between components
  /// </summary>
  public sealed class MessagingExpress
  {

    // static singleton instance
    private static readonly MessagingExpress s_container;

    // registration dictionary
    private readonly Dictionary<MessageExpressItem, List<Tuple<WeakReference, Action<object, object>>>> _dictCallback;

    static MessagingExpress()
    {
      s_container = new MessagingExpress();
    }

    private MessagingExpress()
    {
      _dictCallback = new Dictionary<MessageExpressItem, List<Tuple<WeakReference, Action<object, object>>>>();
    }

    #region Subcribe
    public static void Register<TSender, TArgs>(string message, object subcriber, Action<TSender, TArgs> callback)
    {
      Action<object, object> callbackWrapper = (Action<object, object>)((sender, args) =>
      {
        callback((TSender)sender, (TArgs)args);
      });
      s_container.RegisterInner(typeof(TSender), typeof(TArgs), message, subcriber, callbackWrapper);
    }

    public static void Register<TSender>(string message, object subcriber, Action<TSender> callback)
    {
      Action<object, object> callbackWrapper = (Action<object, object>)((sender, args) =>
      {
        callback((TSender)sender);
      });
      s_container.RegisterInner(typeof(TSender), null, message, subcriber, callbackWrapper);
    }

    public static void RegisterGlobal<TArgs>(string message, object subcriber, Action<TArgs> callback)
    {
      Action<object, object> callbackWrapper = (Action<object, object>)((sender, args) =>
      {
        callback((TArgs)args);
      });
      s_container.RegisterInner(typeof(MessagingExpress), typeof(TArgs), message, subcriber, callbackWrapper);
    }

    public static void RegisterGlobal(string message, object subcriber, Action callback)
    {
      Action<object, object> callbackWrapper = (Action<object, object>)((sender, args) =>
      {
        callback();
      });
      s_container.RegisterInner(typeof(MessagingExpress), null, message, subcriber, callbackWrapper);
    }
    #endregion

    #region UnSubcribe
    public static void UnRegister<TSender, TArgs>(string message, object subcriber)
    {
      s_container.UnRegisterInner(typeof(TSender), typeof(TArgs), message, subcriber);
    }

    public static void UnRegister<TSender>(string message, object subcriber)
    {
      s_container.UnRegisterInner(typeof(TSender), null, message, subcriber);
    }

    public static void UnRegisterGlobal<TArgs>(string message, object subcriber)
    {
      s_container.UnRegisterInner(typeof(MessagingExpress), typeof(TArgs), message, subcriber);
    }

    public static void UnRegisterGlobal(string message, object subcriber)
    {
      s_container.UnRegisterInner(typeof(MessagingExpress), null, message, subcriber);
    }
    #endregion

    #region Broadcast
    public static void Send<TSender, TArgs>(string message, TSender sender, TArgs args)
    {
      s_container.SendInner(typeof(TSender), typeof(TArgs), message, sender, args);
    }

    public static void Send<TSender>(string message, TSender sender)
    {
      s_container.SendInner(typeof(TSender), null, message, sender, null);
    }

    public static void SendGlobal<TArgs>(string message, TArgs args)
    {
      s_container.SendInner(typeof(MessagingExpress), typeof(TArgs), message, s_container, args);
    }

    public static void SendGlobal(string message)
    {
      s_container.SendInner(typeof(MessagingExpress), null, message, s_container, null);
    }
    #endregion

    #region Internal mechanism
    private void RegisterInner(Type senderType, Type argsType, string messsage, object subcriber, Action<object, object> callback)
    {
      MessageExpressItem key = new MessageExpressItem(senderType, argsType, messsage);
      Tuple<WeakReference, Action<object, object>> value = new Tuple<WeakReference, Action<object, object>>(new WeakReference(subcriber), callback);
      if (_dictCallback.ContainsKey(key))
      {
        _dictCallback[key].Add(value);
      }
      else
      {
        List<Tuple<WeakReference, Action<object, object>>> list = new List<Tuple<WeakReference, Action<object, object>>>
          {
              value
          };
        _dictCallback[key] = list;
      }
    }

    private void UnRegisterInner(Type senderType, Type argsType, string messsage, object subcriber)
    {
      MessageExpressItem key = new MessageExpressItem(senderType, argsType, messsage);
      if (_dictCallback.ContainsKey(key))
      {
        _ = _dictCallback[key].RemoveAll((match) =>
        {
          if (match.Item1.IsAlive)
          {
            return match.Item1.Target == subcriber;
          }
          return true;
        });
      }
      List<Tuple<WeakReference, Action<object, object>>> list = _dictCallback[key];
      if (list.Count > 0)
      {
        _ = _dictCallback.Remove(key);
      }
    }

    private void SendInner(Type senderType, Type argsType, string messsage, object sender, object args)
    {
      MessageExpressItem key = new MessageExpressItem(senderType, argsType, messsage);
      if (_dictCallback.ContainsKey(key))
      {
        List<Tuple<WeakReference, Action<object, object>>> list = _dictCallback[key];
        foreach (Tuple<WeakReference, Action<object, object>> item in list)
        {
          if (item.Item1.IsAlive)
          {
            item.Item2(sender, args);
          }
        }
      }
    }
    #endregion

    public class MessageExpressItem : IEquatable<MessageExpressItem>
    {

      public MessageExpressItem()
      {
      }

      public MessageExpressItem(Type senderType, Type argsType, string message)
      {
        SenderType = senderType;
        ArgsType = argsType;
        Message = message;
      }

      public Type SenderType { get; set; }

      public Type ArgsType { get; set; }

      public string Message { get; set; }

      public override string ToString()
      {
        string args = string.Empty;
        if (SenderType != null)
        {
          args += $"!{SenderType.AssemblyQualifiedName}";
        }
        if (ArgsType != null)
        {
          args += $"!{ArgsType.AssemblyQualifiedName}";
        }
        if (Message != null)
        {
          args += $"!{Message}";
        }
        return args;
      }

      public override int GetHashCode()
      {
        // ReSharper disable NonReadonlyMemberInGetHashCode
        int hashCode = ObjectCompareExpress.GetHashCode(SenderType, ArgsType, Message);
        return hashCode;
      }

      public bool Equals(MessageExpressItem other)
      {
        if (other == null)
        {
          return false;
        }
        return ToString() == other.ToString();
      }
    }
  }
}
