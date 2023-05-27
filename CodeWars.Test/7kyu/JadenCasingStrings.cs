using System;
namespace CodeWars.Test;

public class JadenCasingStrings
{
    [Theory]
    [InlineData(
        "How can mirrors be real if our eyes aren't real",
        "How Can Mirrors Be Real If Our Eyes Aren't Real")]
    public void Test(string input, string expected)
    {
        var actual = input.ToJadenCase();

        Assert.Equal(actual, expected);
    }
}

