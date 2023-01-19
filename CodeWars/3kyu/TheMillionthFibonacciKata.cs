// https://www.codewars.com/kata/53d40c1e2f13e331fc000c26

using System;
using System.Numerics;

namespace CodeWars;

public class TheMillionthFibonacciKata
{
    public static BigInteger fib(int n)
    {
        return n == 0 ? 0
            : n == 1 ? 1
            : n > 1 ? GetFib(n)
            : GetFib(n * (-1)) * (n % 2 == 0 ? -1 : 1);
    }

    private static BigInteger GetFib(int n)
    {
        // https://ru.wikibooks.org/wiki/%D0%92%D1%8B%D1%87%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5_%D1%87%D0%B8%D1%81%D0%B5%D0%BB_%D0%A4%D0%B8%D0%B1%D0%BE%D0%BD%D0%B0%D1%87%D1%87%D0%B8
        BigInteger a = 1, ta,
                   b = 1, tb,
                   c = 1, rc = 0, tc,
                   d = 0, rd = 1;

        while (n > 0)
        {
            if (n % 2 == 1)    // Если степень нечетная
            {
                // Умножаем вектор R на матрицу A
                tc = rc;
                rc = rc * a + rd * c;
                rd = tc * b + rd * d;
            }

            // Умножаем матрицу A на саму себя
            ta = a; tb = b; tc = c;
            a = a * a + b * c;
            b = ta * b + b * d;
            c = c * ta + d * c;
            d = tc * tb + d * d;

            n >>= 1;  // Уменьшаем степень вдвое

        }
        return rc;
    }
}