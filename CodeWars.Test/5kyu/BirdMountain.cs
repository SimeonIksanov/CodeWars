using System;
namespace CodeWars.Test;

public class DinglemouseBirdMountainTest
{
    [Fact]
    public void OriginTest()
    {
        char[][] mountain =
        {
            "^^^^^^        ".ToCharArray(),
            " ^^^^^^^^     ".ToCharArray(),
            "  ^^^^^^^     ".ToCharArray(),
            "  ^^^^^       ".ToCharArray(),
            "  ^^^^^^^^^^^ ".ToCharArray(),
            "  ^^^^^^      ".ToCharArray(),
            "  ^^^^        ".ToCharArray()
        };
        Assert.Equal(3, Dinglemouse.PeakHeight(mountain));
    }

    [Fact]
    public void Simple2x2()
    {
        char[][] mountain =
        {
            "^^".ToCharArray(),
            " ^".ToCharArray()
        };
        Assert.Equal(1, Dinglemouse.PeakHeight(mountain));
    }

    [Fact]
    public void Twins()
    {
        char[][] mountain =
        {
            "^^^^^       ".ToCharArray(),
            "^^^^^       ".ToCharArray(),
            "^^^^^       ".ToCharArray(),
            "     ^^^^^^^".ToCharArray(),
            "     ^^^^^^^".ToCharArray(),
            "     ^^^^^^^".ToCharArray(),
            "     ^^^^^^^".ToCharArray(),
            "     ^^^^^^^".ToCharArray(),
            "     ^^^^^^^".ToCharArray(),
            "     ^^^^^^^".ToCharArray()
        };
        Assert.Equal(4, Dinglemouse.PeakHeight(mountain));
    }
}
