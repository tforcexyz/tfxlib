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

  public class ByteUnitEx
  {

    public static ByteUnitEx FromByte(long value)
    {
      ByteUnitEx result = new ByteUnitEx();
      result.Byte = value;
      return result;
    }

    public static ByteUnitEx FromKilobyte(long value)
    {
      ByteUnitEx result = new ByteUnitEx();
      result.Kilobyte = value;
      return result;
    }

    public static ByteUnitEx FromKilobyte(double value)
    {
      ByteUnitEx result = new ByteUnitEx();
      result.KilobytePercise = value;
      return result;
    }

    public static ByteUnitEx FromMegabyte(long value)
    {
      ByteUnitEx result = new ByteUnitEx();
      result.Megabyte = value;
      return result;
    }

    public static ByteUnitEx FromMegabyte(double value)
    {
      ByteUnitEx result = new ByteUnitEx();
      result.MegabytePercise = value;
      return result;
    }

    public static ByteUnitEx FromGigabyte(long value)
    {
      ByteUnitEx result = new ByteUnitEx();
      result.Gigabyte = value;
      return result;
    }

    public static ByteUnitEx FromGigabyte(double value)
    {
      ByteUnitEx result = new ByteUnitEx();
      result.GigabytePercise = value;
      return result;
    }

    public static ByteUnitEx FromTetrabyte(long value)
    {
      ByteUnitEx result = new ByteUnitEx();
      result.Tetrabyte = value;
      return result;
    }

    public static ByteUnitEx FromTetrabyte(double value)
    {
      ByteUnitEx result = new ByteUnitEx();
      result.TetrabytePercise = value;
      return result;
    }

    public long Byte { get; set; }

    public long Kilobyte
    {
      get { return Byte / ComputingUnitConversions.BytesPerKilobyte; }
      set { Byte = value * ComputingUnitConversions.BytesPerKilobyte; }
    }

    public long Megabyte
    {
      get { return Byte / ComputingUnitConversions.BytesPerMegabyte; }
      set { Byte = value * ComputingUnitConversions.BytesPerMegabyte; }
    }

    public long Gigabyte
    {
      get { return Byte / ComputingUnitConversions.BytesPerGigabyte; }
      set { Byte = value * ComputingUnitConversions.BytesPerGigabyte; }
    }

    public long Tetrabyte
    {
      get { return Byte / ComputingUnitConversions.BytesPerTetrabyte; }
      set { Byte = value * ComputingUnitConversions.BytesPerTetrabyte; }
    }

    public double KilobytePercise
    {
      get { return Byte / (double)ComputingUnitConversions.BytesPerKilobyte; }
      set { Byte = (long)(value * ComputingUnitConversions.BytesPerKilobyte); }
    }

    public double MegabytePercise
    {
      get { return Byte / (double)ComputingUnitConversions.BytesPerMegabyte; }
      set { Byte = (long)(value * ComputingUnitConversions.BytesPerMegabyte); }
    }

    public double GigabytePercise
    {
      get { return Byte / (double)ComputingUnitConversions.BytesPerGigabyte; }
      set { Byte = (long)(value * ComputingUnitConversions.BytesPerGigabyte); }
    }

    public double TetrabytePercise
    {
      get { return Byte / (double)ComputingUnitConversions.BytesPerTetrabyte; }
      set { Byte = (long)(value * ComputingUnitConversions.BytesPerTetrabyte); }
    }
  }
}
