// https://www.codewars.com/kata/59f4a0acbee84576800000af/csharp

using System;
using System.Linq;

namespace CodeWars;

public class PositionsAverage
{
    public static double PosAverage(string s)
    {
        var substrings = s.Split(", ").ToArray();

        var matches = substrings
            .SelectMany((x, i) => substrings
                .Skip(i + 1)
                .SelectMany(y => x.Zip(y, (a, b) => a == b)));

        return matches.Count(x => x) * 100 / matches.Count();
    }
}

