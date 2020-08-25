using System;
using System.Collections.Generic;
using System.Text;

namespace mmix
{
    public class Wyde
    {
        private byte[] bytes;
        public Wyde(byte[] bytes)
        {
            if (bytes.Length != 2)
            {
                throw new Exception("Wydes must have 2 bytes.");
            }
            this.bytes = bytes;
        }

        public Wyde() : this(new byte[2])
        {
        }

        public static implicit operator Wyde(int n)
        {
            byte[] intBytes = BitConverter.GetBytes(n);
            return new Wyde(new byte[] { intBytes[2], intBytes[3] });
        }

        public int ToInt()
        {
            return BitConverter.ToInt32(bytes);
        }

        public override string ToString()
        {
            return bytes.ToHexString();
        }
    }
}

