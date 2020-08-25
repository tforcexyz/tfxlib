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

  public class BitUnitEx
  {

    public static BitUnitEx FromBit(long value)
    {
      BitUnitEx result = new BitUnitEx();
      result.Bit = value;
      return result;
    }

    public static BitUnitEx FromKilobit(long value)
    {
      BitUnitEx result = new BitUnitEx();
      result.Kilobit = value;
      return result;
    }

    public static BitUnitEx FromKilobit(double value)
    {
      BitUnitEx result = new BitUnitEx();
      result.KilobitPercise = value;
      return result;
    }

    public static BitUnitEx FromMegabit(long value)
    {
      BitUnitEx result = new BitUnitEx();
      result.Megabit = value;
      return result;
    }

    public static BitUnitEx FromMegabit(double value)
    {
      BitUnitEx result = new BitUnitEx();
      result.MegabitPercise = value;
      return result;
    }

    public static BitUnitEx FromGigabit(long value)
    {
      BitUnitEx result = new BitUnitEx();
      result.Gigabit = value;
      return result;
    }

    public static BitUnitEx FromGigabit(double value)
    {
      BitUnitEx result = new BitUnitEx();
      result.GigabitPercise = value;
      return result;
    }

    public static BitUnitEx FromByte(long value)
    {
      BitUnitEx result = new BitUnitEx();
      result.Byte = value;
      return result;
    }

    public static BitUnitEx FromByte(double value)
    {
      BitUnitEx result = new BitUnitEx();
      result.BytePercise = value;
      return result;
    }

    public static BitUnitEx FromKilobyte(long value)
    {
      BitUnitEx result = new BitUnitEx();
      result.Kilobyte = value;
      return result;
    }

    public static BitUnitEx FromKilobyte(double value)
    {
      BitUnitEx result = new BitUnitEx();
      result.KilobytePercise = value;
      return result;
    }

    public static BitUnitEx FromMegabyte(long value)
    {
      BitUnitEx result = new BitUnitEx();
      result.Megabyte = value;
      return result;
    }

    public static BitUnitEx FromMegabyte(double value)
    {
      BitUnitEx result = new BitUnitEx();
      result.MegabytePercise = value;
      return result;
    }

    public static BitUnitEx FromGigabyte(long value)
    {
      BitUnitEx result = new BitUnitEx();
      result.Gigabyte = value;
      return result;
    }

    public static BitUnitEx FromGigabyte(double value)
    {
      BitUnitEx result = new BitUnitEx();
      result.GigabytePercise = value;
      return result;
    }

    public long Bit { get; set; }

    public long Kilobit
    {
      get { return Bit / ComputingUnitConversions.BytesPerKilobyte; }
      set { Bit = value * ComputingUnitConversions.BytesPerKilobyte; }
    }

    public long Megabit
    {
      get { return Bit / ComputingUnitConversions.BytesPerMegabyte; }
      set { Bit = value * ComputingUnitConversions.BytesPerMegabyte; }
    }

    public long Gigabit
    {
      get { return Bit / ComputingUnitConversions.BytesPerGigabyte; }
      set { Bit = value * ComputingUnitConversions.BytesPerGigabyte; }
    }

    public long Byte
    {
      get { return Bit / ComputingUnitConversions.BitsPerByte; }
      set { Bit = value * ComputingUnitConversions.BitsPerByte; }
    }

    public long Kilobyte
    {
      get { return Bit / ComputingUnitConversions.BitsPerKilobyte; }
      set { Bit = value * ComputingUnitConversions.BitsPerKilobyte; }
    }

    public long Megabyte
    {
      get { return Bit / ComputingUnitConversions.BitsPerMegabyte; }
      set { Bit = value * ComputingUnitConversions.BitsPerMegabyte; }
    }

    public long Gigabyte
    {
      get { return Bit / ComputingUnitConversions.BitsPerGigabyte; }
      set { Bit = value * ComputingUnitConversions.BitsPerGigabyte; }
    }

    public double KilobitPercise
    {
      get { return Bit / (double)ComputingUnitConversions.BytesPerKilobyte; }
      set { Bit = (long)(value * ComputingUnitConversions.BytesPerKilobyte); }
    }

    public double MegabitPercise
    {
      get { return Bit / (double)ComputingUnitConversions.BytesPerMegabyte; }
      set { Bit = (long)(value * ComputingUnitConversions.BytesPerMegabyte); }
    }

    public double GigabitPercise
    {
      get { return Bit / (double)ComputingUnitConversions.BytesPerGigabyte; }
      set { Bit = (long)(value * ComputingUnitConversions.BytesPerGigabyte); }
    }

    public double BytePercise
    {
      get { return Bit / (double)ComputingUnitConversions.BitsPerByte; }
      set { Bit = (long)(value * ComputingUnitConversions.BitsPerByte); }
    }

    public double KilobytePercise
    {
      get { return Bit / (double)ComputingUnitConversions.BitsPerKilobyte; }
      set { Bit = (long)(value * ComputingUnitConversions.BitsPerKilobyte); }
    }

    public double MegabytePercise
    {
      get { return Bit / (double)ComputingUnitConversions.BitsPerMegabyte; }
      set { Bit = (long)(value * ComputingUnitConversions.BitsPerMegabyte); }
    }

    public double GigabytePercise
    {
      get { return Bit / (double)ComputingUnitConversions.BitsPerGigabyte; }
      set { Bit = (long)(value * ComputingUnitConversions.BitsPerGigabyte); }
    }
  }
}
