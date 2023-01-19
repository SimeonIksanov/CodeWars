// https://www.codewars.com/kata/5518a860a73e708c0a000027

using System;
using System.Numerics;

namespace CodeWars;

public class LastDigitOfAHugeNumber
{
    public static int LastDigit(int[] array)
    {
        BigInteger lastDigit = 1;

        for (int i = array.Length - 1; i > -1; i--)
            if (lastDigit == 0)
                lastDigit = 1;
            else if (lastDigit == 1)
                lastDigit = (BigInteger)array[i];
            else
            {
                lastDigit = BigInteger.Pow(array[i], (int)((lastDigit % 4) + 4));
            }
        return (int)(lastDigit % 10);
    }
}
