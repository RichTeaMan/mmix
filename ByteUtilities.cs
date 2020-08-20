using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace mmix
{
    public static class ByteUtilities
    {
        public static string ToHexString(this byte[] bytes)
        {
            return "#" + BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        public static void SetArray(this byte[] bytes, byte[] newBytes)
        {
            int low = Math.Min(bytes.Length, newBytes.Length);
            for (int i = 0; i < low; i++)
            {
                bytes[i] = newBytes[i];
            }
        }

        /// <summary>
        /// Pads the given byte array with leading 0s to the given length. Eg, if length is 10 the result array will be always be 10 elements long.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] Pad(this byte[] bytes, int length)
        {
            byte[] result = new byte[length];
            int position = length - bytes.Length;
            foreach(var b in bytes)
            {
                result[position] = b;
                position++;
            }
            return result;
        }

        public static int ToInt(this byte[] b)
        {
            if (b?.Length != 4)
            {
                throw new ArgumentException("Must have 4 bytes.");
            }
            return BitConverter.ToInt32(b.Reverse().ToArray());
        }

        public static uint ToUInt(this byte[] b)
        {
            if (b?.Length != 4)
            {
                throw new ArgumentException("Must have 4 bytes.");
            }
            return BitConverter.ToUInt32(b.Reverse().ToArray());
        }

        public static long ToLong(this byte[] b)
        {
            if (b?.Length != 8)
            {
                throw new ArgumentException("Must have 8 bytes.");
            }
            return BitConverter.ToInt64(b.Reverse().ToArray());
        }

        public static ulong ToULong(this byte[] b)
        {
            if (b?.Length != 8)
            {
                throw new ArgumentException("Must have 8 bytes.");
            }
            return BitConverter.ToUInt64(b.Reverse().ToArray());
        }

        public static byte[] ToBytes(this int n)
        {
            return BitConverter.GetBytes(n).Reverse().ToArray();
        }

        public static byte[] ToBytes(this uint n)
        {
            return BitConverter.GetBytes(n).Reverse().ToArray();
        }

        public static byte[] ToBytes(this long n)
        {
            return BitConverter.GetBytes(n).Reverse().ToArray();
        }

        public static byte[] ToBytes(this ulong n)
        {
            return BitConverter.GetBytes(n).Reverse().ToArray();
        }
    }
}
