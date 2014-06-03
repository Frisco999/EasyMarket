using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EasyMarket.Classes
{
    public class Scramble
    {
        public static string Encrypt(string text)
        {
            string output = BitConverter.ToString(((SHA512)new SHA512Managed()).ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "");
            return output;
        }

        public static string Decrypt(string text)
        {
            string output = "";
            for (int i = 0; i < text.Length; i++)
            {
                string currentSymbol = text.Substring(i, 1);
                output += (char)((int)text[i] - 20);
            }

            return output;
        }
    }
}