// https://www.codewars.com/kata/5390bac347d09b7da40006f6

using System;
using System.Text;

public static class JadenCase
{
    public static string ToJadenCase(this string phrase)
    {
        if (phrase is null || phrase.Length == 0)
            return phrase;

        var sb = new StringBuilder(phrase.Length);

        int index = 0;
        while (index < phrase.Length)
        {
            var nextSpaceIndex = phrase.IndexOf(' ', index);
            if (nextSpaceIndex == -1)
            {
                WordToJadenCase(sb, phrase, index, phrase.Length);
                index = phrase.Length;
            }
            else if (nextSpaceIndex - index == 0)
            {
                sb.Append(phrase[index++]);
            }
            else
            {
                WordToJadenCase(sb, phrase, index, nextSpaceIndex);
                index = nextSpaceIndex;
            }
        }
        return sb.ToString();
    }

    private static void WordToJadenCase(StringBuilder sb, string phrase, int start, int end)
    {
        sb.Append(char.IsLower(phrase[start]) ? char.ToUpper(phrase[start]) : phrase[start]);
        sb.Append(phrase, start + 1, end - start - 1);
    }
}
