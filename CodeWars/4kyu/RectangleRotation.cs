// https://www.codewars.com/kata/5886e082a836a691340000c3/csharp

using System;
namespace CodeWars;

public class RectangleRotationKata
{
    public int RectangleRotation(int a, int b)
    {
        double elDiag = Math.Sqrt(2); // 1.4142...

        var da = (int)(a / elDiag) + 1;
        var db = (int)(b / elDiag) + 1;

        return da % 2 == db % 2 ?
            da * db + (da - 1) * (db - 1)
            : da * (db - 1) + db * (da - 1);
    }
}
