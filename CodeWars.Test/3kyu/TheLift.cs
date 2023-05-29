using System;
namespace CodeWars.Test;

public class DinglemouseTest
{
    [Fact]
    public void TestUp()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[0], // 1
            new int[]{5,5,5}, // 2
            new int[0], // 3
            new int[0], // 4
            new int[0], // 5
            new int[0], // 6
        };
        var result = Dinglemouse.TheLift(queues, 5);
        Assert.Equal(new[] { 0, 2, 5, 0 }, result);
    }

    [Fact]
    public void TestDown()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[0], // 1
            new int[]{1,1}, // 2
            new int[0], // 3
            new int[0], // 4
            new int[0], // 5
            new int[0], // 6
        };
        var result = Dinglemouse.TheLift(queues, 5);
        Assert.Equal(new[] { 0, 2, 1, 0 }, result);
    }

    [Fact]
    public void TestUpAndUp()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[]{3}, // 1
            new int[]{4}, // 2
            new int[0], // 3
            new int[]{5}, // 4
            new int[0], // 5
            new int[0], // 6
        };
        var result = Dinglemouse.TheLift(queues, 5);
        Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 0 }, result);
    }

    [Fact]
    public void TestDownAndDown()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[]{0}, // 1
            new int[0], // 2
            new int[0], // 3
            new int[]{2}, // 4
            new int[]{3}, // 5
            new int[0], // 6
        };
        var result = Dinglemouse.TheLift(queues, 5);
        Assert.Equal(new[] { 0, 5, 4, 3, 2, 1, 0 }, result);
    }

    [Fact]
    public void TestUpCapasity1()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[]{2,2}, // 1
            new int[0], // 2
            new int[0], // 3
            new int[0], // 4
            new int[0], // 5
            new int[0], // 6
        };
        var result = Dinglemouse.TheLift(queues, 1);
        Assert.Equal(new[] { 0, 1, 2, 1, 2, 0 }, result);
    }

    [Fact]
    public void TestDownCapasity1()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[0], // 1
            new int[0], // 2
            new int[0], // 3
            new int[0], // 4
            new int[0], // 5
            new int[]{2,2}, // 6
        };
        var result = Dinglemouse.TheLift(queues, 1);
        Assert.Equal(new[] { 0, 6, 2, 6, 2, 0 }, result);
    }

    [Fact]
    public void TestUpAndDown1()
    {
        int[][] queues =
        {
            new int[] {6}, // G
            new int[] {6}, // 1
            new int[] {6}, // 2
            new int[] {6}, // 3
            new int[] {6}, // 4
            new int[] {6}, // 5
            new int[0], // 6
        };
        var result = Dinglemouse.TheLift(queues, 1);
        Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5, 6, 2, 3, 4, 5, 6, 3, 4, 5, 6, 4, 5, 6, 5, 6, 0 }, result);
    }

    [Fact]
    public void TestUpAndDown2()
    {
        int[][] queues =
        {
            new int[] {1}, // G
            new int[0], // 1
            new int[0], // 2
            new int[] {6}, // 3
            new int[0], // 4
            new int[0], // 5
            new int[0], // 6
        };
        var result = Dinglemouse.TheLift(queues, 1);
        Assert.Equal(new[] { 0, 1, 3, 6, 0 }, result);
    }

    [Fact]
    public void TestUpAndDown3()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[] {2}, // 1
            new int[] {3,3,3}, // 2
            new int[] {1}, // 3
            new int[0], // 4
            new int[0], // 5
            new int[0], // 6
        };
        var result = Dinglemouse.TheLift(queues, 1);
        Assert.Equal(new[] { 0, 1, 2, 3, 1, 2, 3, 2, 3, 0 }, result);
    }

    [Fact]
    public void TestUpAndDown4()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[] {0}, // 1
            new int[] {3,3,3}, // 2
            new int[0], // 3
        };
        var result = Dinglemouse.TheLift(queues, 1);
        Assert.Equal(new[] { 0, 2, 3, 1, 0, 2, 3, 2, 3, 0 }, result);
    }
}
