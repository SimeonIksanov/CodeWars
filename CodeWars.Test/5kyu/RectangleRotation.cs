// https://www.codewars.com/kata/5886e082a836a691340000c3/csharp

using System;
namespace CodeWars.Test;

public class RectangleRotationTest
{
    [Theory]
    [InlineData(2, 2, 5)]
    [InlineData(2, 8, 17)]
    [InlineData(2, 6, 13)]
    [InlineData(4, 4, 13)]
    [InlineData(6, 6, 41)]
    [InlineData(4, 2, 7)]
    [InlineData(6, 4, 23)]
    [InlineData(30, 2, 65)]
    [InlineData(8, 6, 49)]
    [InlineData(16, 20, 333)]
    public void BasicTests(int a, int b, int expected)
    {
        var kata = new RectangleRotationKata();

        Assert.Equal(expected, kata.RectangleRotation(a, b));
    }
}

