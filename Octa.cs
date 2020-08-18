using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace mmix
{
    public class Octa
    {
        private byte[] bytes;
        public Octa(byte[] bytes)
        {
            if (bytes.Length != 8)
            {
                throw new Exception("Octas must have 8 bytes.");
            }
            this.bytes = bytes;
        }

        public Octa() : this(new byte[8]) {
        }

        public Wyde OpCode => new Wyde(new byte[] { bytes[0], bytes[1] });

        public Wyde X => new Wyde(new byte[] { bytes[2], bytes[3] });
        public Wyde Y => new Wyde(new byte[] { bytes[4], bytes[5] });
        public Wyde Z => new Wyde(new byte[] { bytes[6], bytes[7] });

        public void StoreLong(long value)
        {
            byte[] intBytes = BitConverter.GetBytes(value).Reverse().ToArray();
            bytes.SetArray(intBytes);
        }

        public void StoreBytes(byte[] bytes)
        {
            this.bytes.SetArray(bytes);
        }

        public byte FetchByte(int n)
        {
            return bytes[n];
        }
        public long ToLong()
        {
            return BitConverter.ToInt64(bytes.Reverse().ToArray());
        }

        public override string ToString()
        {
            return bytes.ToHexString();
        }
    }
}
