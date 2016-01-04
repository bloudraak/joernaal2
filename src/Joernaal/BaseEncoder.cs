using System;
using System.Numerics;
using System.Text;

namespace Joernaal
{
    public class BaseEncoder
    {
        public static string Encode(byte[] bytes)
        {
            var alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";
            var result = new StringBuilder(4096);
            var dividend = new BigInteger(bytes);
            var divisor = new BigInteger(alphabet.Length);
            while (!dividend.IsZero)
            {
                BigInteger remainder;
                dividend = BigInteger.DivRem(dividend, divisor, out remainder);
                var i = Math.Abs((int) remainder);
                result.Append(alphabet[i]);
            }

            return result.ToString();
        }
    }
}