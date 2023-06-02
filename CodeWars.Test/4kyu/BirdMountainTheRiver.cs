using System;
namespace CodeWars.Test;

public class BirdMountainTheRiverTest
{
    [Fact]
    public void Ex()
    {
        char[][] terrain =
        {
            "  ^^^^^^             ".ToCharArray(),
            "^^^^^^^^       ^^^   ".ToCharArray(),
            "^^^^^^^  ^^^         ".ToCharArray(),
            "^^^^^^^  ^^^         ".ToCharArray(),
            "^^^^^^^  ^^^         ".ToCharArray(),
            "---------------------".ToCharArray(),
            "^^^^^                ".ToCharArray(),
            "   ^^^^^^^^  ^^^^^^^ ".ToCharArray(),
            "^^^^^^^^     ^     ^ ".ToCharArray(),
            "^^^^^        ^^^^^^^ ".ToCharArray()
        };
        Assert.Equal(new int[] { 189, 99, 19, 3 }, Dinglemouse.DryGround(terrain));
    }

    [Fact]
    public void Wall()
    {
        char[][] terrain =
        {
            "                         ".ToCharArray(),
            "-------------------------".ToCharArray(),
            "                         ".ToCharArray(),
            "       ^^^^^^^^^^^^^^^^^^".ToCharArray(),
            "       ^^^^^^^^^^^^^^^^^^".ToCharArray(),
            "       ^^^^^^^^^^^^^^^^^^".ToCharArray(),
            "       ^^^^^        ^^^^^".ToCharArray(),
            "       ^^^^^        ^^^^^".ToCharArray(),
            "       ^^^^^^^^^^^^^^^^^^".ToCharArray(),
            "       ^^^^^^^^^^^^^^^^^^".ToCharArray(),
            "       ^^^^^^^^^^^^^^^^^^".ToCharArray(),
        };
        Assert.Equal(new int[] { 250, 144, 96, 12 }, Dinglemouse.DryGround(terrain));
    }

    [Fact]
    public void Nothing()
    {
        char[][] terrain = new char[0][];
        Assert.Equal(new int[] { 0, 0, 0, 0 }, Dinglemouse.DryGround(terrain));
    }

    [Fact]
    public void FlashFlood()
    {
        char[][] terrain =
        {
            "^^^^^^^^^^^^^^^^^^^^^^^^^^^^".ToCharArray(),
            "----------------------------".ToCharArray(),
            "^^^^^^^^^^^^^^^^^^^^^^^^^^^^".ToCharArray(),
            "^^^^ ^^^^^^^^ ^^^^^^^^^ ^^^^".ToCharArray(),
            "^^^^ ^^  ^  ^  ^  ^  ^^ ^^^^".ToCharArray(),
            "^^^^ ^^  ^  ^  ^  ^  ^^ ^^^^".ToCharArray(),
            "^^^^ ^^  ^  ^  ^  ^  ^^ ^^^^".ToCharArray(),
            "^^^^ ^^  ^  ^  ^  ^  ^^ ^^^^".ToCharArray(),
            "^^^^ ^^  ^  ^  ^  ^  ^^ ^^^^".ToCharArray(),
            "^^^^ ^^  ^  ^  ^  ^  ^^ ^^^^".ToCharArray(),
            "^^^^ ^^  ^  ^  ^  ^  ^^ ^^^^".ToCharArray(),
            "^^^^ ^^  ^  ^  ^  ^  ^^ ^^^^".ToCharArray(),
            "^^^^ ^^  ^  ^  ^  ^  ^^ ^^^^".ToCharArray(),
            "^^^^ ^^  ^  ^  ^  ^  ^^ ^^^^".ToCharArray(),
            "^^^^^^^^^^^^^^^^^^^^^^^^^^^^".ToCharArray(),
            "^^^^^^^^^^^^^^^^^^^^^^^^^^^^".ToCharArray(),
        };
        Assert.Equal(new int[] { 420, 420, 63, 0 }, Dinglemouse.DryGround(terrain));
    }
}

