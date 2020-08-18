using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Net;
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

        public static int ToInt(this byte b)
        {
            return (int)b;
        }
    }
}
