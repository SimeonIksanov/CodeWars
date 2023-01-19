// https://www.codewars.com/kata/585894545a8a07255e0002f1

using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars;

public static class ScreenLockingPatterns
{
    static Dictionary<char, char[]> Neighbours = new Dictionary<char, char[]>
    {
        {'A',new [] { 'B','D','E','F','H'} },
        {'B',new [] { 'A','C','D','E','F','G','I' } },
        {'C',new [] { 'B','D','E','F','H'} },
        {'D',new [] { 'A','B','C','E','G','H','I'} },
        {'E',new [] { 'A','B','C','D','F','G','H','I'} },
        {'F',new [] { 'A','B','C','E','G','H','I'} },
        {'G',new [] { 'B','D','E','F','H'} },
        {'H',new [] { 'A','C','D','E','F','G','I'} },
        {'I',new [] { 'B','D','E','F','H'} }
    };

    static Dictionary<char, List<char[]>> NeighboursWithCond = new Dictionary<char, List<char[]>>
    {
        { 'A', new List<char[]>{new [] { 'B','C' }, new [] { 'D','G' }, new[] { 'E', 'I' } } },
        { 'B', new List<char[]>{new [] { 'E','H' } } },
        { 'C', new List<char[]>{new [] { 'B','A' }, new [] { 'E','G' }, new [] { 'F','I' } } },
        { 'D', new List<char[]>{new [] { 'E','F' } } },
        { 'F', new List<char[]>{new [] { 'E','D' } } },
        { 'G', new List<char[]>{new [] { 'D','A' }, new [] { 'E','C' }, new [] { 'H','I' } } },
        { 'H', new List<char[]>{new [] { 'E','B' } } },
        { 'I', new List<char[]>{new [] { 'H','G' }, new [] { 'E','A' }, new [] { 'F','C' } } }
    };

    public static int CountPatternsFrom(char firstDot, int length)
    {
        if (length <= 0 || length > 9) return 0;
        if (length == 1) return 1;

        bool[] visited = new bool[9];

        return CountPatternsRecursivly(visited, firstDot, length);

    }

    static int CountPatternsRecursivly(bool[] visited, char dot, int length)
    {
        if (length == 0) return 0;
        if (length == 1) return 1;
        var result = 0;

        visited[ToIndex(dot)] = true;

        foreach (var n in GetNeighbours(dot).Where(x => !visited[ToIndex(x)]))
        {
            result += CountPatternsRecursivly(visited, n, length - 1);
        }

        foreach (var n in GetNeighboursWithCond(dot, visited).Where(x => !visited[ToIndex(x)]))
        {
            result += CountPatternsRecursivly(visited, n, length - 1);
        }

        visited[ToIndex(dot)] = false;

        return result;
    }

    static int ToIndex(char c) => c - 'A';

    static char[] GetNeighbours(char dot)
    {
        if (Neighbours.ContainsKey(dot))
            return Neighbours[dot];
        else
            return new char[0];
    }

    static char[] GetNeighboursWithCond(char dot, bool[] visited)
    {
        if (NeighboursWithCond.ContainsKey(dot))
        {
            var nwc = NeighboursWithCond[dot];
            return nwc.Where(x => visited[ToIndex(x[0])]).Select(i => i[1]).ToArray();
        }
        else
        {
            return new char[0];
        }
    }
}