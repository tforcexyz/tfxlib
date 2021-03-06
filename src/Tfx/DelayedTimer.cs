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
using System.Timers;

namespace Xyz.TForce
{

  public class DelayedTimer<T> : IActionTimer<T>
  {

    private readonly int _interval;
    private readonly Timer _timer;
    private T _augments;

    public DelayedTimer(int interval)
    {
      _interval = interval;
      Timer timer = new Timer();
      timer.AutoReset = false;
      timer.Enabled = false;
      timer.Elapsed += DelayedActionHandler;
      _timer = timer;
    }

    public Action<T> Action { get; set; }

    public void StartTimer(T parameters)
    {
      _augments = parameters;
      _timer.Interval = _interval;
      _timer.Start();
    }

    public void StartTimer(T parameters, int duration)
    {
      _augments = parameters;
      _timer.Interval = duration;
      _timer.Start();
    }

    public void StopTimer()
    {
      _timer.Stop();
    }

    protected void DelayedActionHandler(object sender, ElapsedEventArgs e)
    {
      if (Action != null)
      {
        Action.Invoke(_augments);
      }
    }
  }
}
