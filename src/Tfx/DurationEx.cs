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

namespace Xyz.TForce
{

  public class DurationEx
  {

    public static DurationEx FromMillisecond(long value)
    {
      DurationEx result = new DurationEx();
      result.Millisecond = value;
      return result;
    }

    public static DurationEx FromSecond(long value)
    {
      DurationEx result = new DurationEx();
      result.Second = value;
      return result;
    }

    public static DurationEx FromSecond(double value)
    {
      DurationEx result = new DurationEx();
      result.SecondPercise = value;
      return result;
    }

    public static DurationEx FromMinute(long value)
    {
      DurationEx result = new DurationEx();
      result.Minute = value;
      return result;
    }

    public static DurationEx FromMinute(double value)
    {
      DurationEx result = new DurationEx();
      result.MinutePercise = value;
      return result;
    }

    public static DurationEx FromHour(long value)
    {
      DurationEx result = new DurationEx();
      result.Hour = value;
      return result;
    }

    public static DurationEx FromHour(double value)
    {
      DurationEx result = new DurationEx();
      result.HourPercise = value;
      return result;
    }

    public static DurationEx FromDay(long value)
    {
      DurationEx result = new DurationEx();
      result.Day = value;
      return result;
    }

    public static DurationEx FromDay(double value)
    {
      DurationEx result = new DurationEx();
      result.DayPercise = value;
      return result;
    }

    public long Millisecond { get; set; }

    public long Second
    {
      get { return Millisecond / TimeConversions.MilisecondsPerSecond; }
      set { Millisecond = value * TimeConversions.MilisecondsPerSecond; }
    }

    public long Minute
    {
      get { return Millisecond / TimeConversions.MilisecondsPerMinute; }
      set { Millisecond = value * TimeConversions.MilisecondsPerMinute; }
    }

    public long Hour
    {
      get { return Millisecond / TimeConversions.MilisecondsPerHour; }
      set { Millisecond = value * TimeConversions.MilisecondsPerHour; }
    }

    public long Day
    {
      get { return Millisecond / TimeConversions.MilisecondsPerDay; }
      set { Millisecond = value * TimeConversions.MilisecondsPerDay; }
    }

    public double SecondPercise
    {
      get { return Millisecond / (double)TimeConversions.MilisecondsPerSecond; }
      set { Millisecond = (long)(value * TimeConversions.MilisecondsPerSecond); }
    }

    public double MinutePercise
    {
      get { return Millisecond / (double)TimeConversions.MilisecondsPerMinute; }
      set { Millisecond = (long)(value * TimeConversions.MilisecondsPerMinute); }
    }

    public double HourPercise
    {
      get { return Millisecond / (double)TimeConversions.MilisecondsPerHour; }
      set { Millisecond = (long)(value * TimeConversions.MilisecondsPerHour); }
    }

    public double DayPercise
    {
      get { return Millisecond / (double)TimeConversions.MilisecondsPerDay; }
      set { Millisecond = (long)(value * TimeConversions.MilisecondsPerDay); }
    }
  }
}
