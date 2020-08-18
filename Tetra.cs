using System;
using System.Collections.Generic;
using System.Text;

namespace mmix
{
    public class Tetra
    {
        private byte[] bytes;
        public Tetra(byte[] bytes)
        {
            if (bytes.Length != 4)
            {
                throw new Exception("Tetras must have 4 bytes.");
            }
            this.bytes = bytes;
        }

        public Tetra() : this(new byte[4]) {
        }

        public byte OpCode => bytes[0];

        public byte X => bytes[1];
        public byte Y => bytes[2];
        public byte Z => bytes[3];

        internal void StoreInt(int value)
        {
            byte[] intBytes = BitConverter.GetBytes(value);
            bytes.SetArray(intBytes);
        }

        public byte FetchByte(int n)
        {
            return bytes[n];
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
