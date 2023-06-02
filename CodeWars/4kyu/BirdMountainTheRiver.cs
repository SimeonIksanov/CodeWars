using System;
using System.Drawing;
using System.Reflection;

namespace CodeWars;

public partial class Dinglemouse
{
    public static int[] DryGround(char[][] terrain)
    {
        var queue = new Queue<(int X, int Y)>();
        var wetQueue = new Queue<(int X, int Y)>();
        var landscape = GetTerrain(terrain, queue, wetQueue);
        var landWidth = landscape.GetUpperBound(0);
        var landHeight = landscape.GetUpperBound(1);
        while (queue.Count != 0)
        {
            var point = queue.Dequeue();
            var pointHeight = landscape[point.X, point.Y].height;

            foreach ((int x, int y) dif in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                var newX = point.X + dif.x;
                var newY = point.Y + dif.y;
                bool correctCoords = newX >= 0 && newX <= landWidth && newY >= 0 && newY <= landHeight;
                bool needSpecifyHeight = correctCoords
                    && (landscape[newX, newY].height == -1 || landscape[newX, newY].height > pointHeight + 1);
                if (needSpecifyHeight)
                {
                    landscape[newX, newY].height = pointHeight + 1;
                    queue.Enqueue((newX, newY));
                }
            }
        }
        while (wetQueue.Count != 0)
        {
            var point = wetQueue.Dequeue();
            var pointWet = landscape[point.X, point.Y].wet;
            var pointHeight = landscape[point.X, point.Y].height;
            foreach ((int x, int y) dif in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                var newX = point.X + dif.x;
                var newY = point.Y + dif.y;
                bool correctCoords = newX >= 0 && newX <= landWidth && newY >= 0 && newY <= landHeight;
                bool needSpecifyWet = correctCoords
                    && (landscape[newX, newY].wet > pointWet);
                if (needSpecifyWet)
                {
                    if (landscape[newX, newY].height > pointHeight)
                    {
                        if (pointWet - pointHeight == 1)
                            landscape[newX, newY].wet = (byte)(pointWet + 1);
                        else
                            landscape[newX, newY].wet = pointWet;
                    }
                    else
                    {
                        landscape[newX, newY].wet = pointWet;
                    }
                    wetQueue.Enqueue((newX, newY));
                }
            }
        }

        return GetDryGrounds(landscape);
    }


    private static (Single height, byte wet)[,] GetTerrain(char[][] terrain, Queue<(int X, int Y)> queue, Queue<(int X, int Y)> wetQueue)
    {
        if (terrain.Length == 0) return new (Single height, byte wet)[0, 0];
        int width = terrain[0].Length;
        int height = terrain.Length;
        var landscape = new (Single height, byte wet)[width, height];
        var watter = new List<(int X, int Y)>();

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                if (terrain[y][x] == '-')
                {
                    landscape[x, y] = (-0.5f, 0);
                    watter.Add((x, y));
                    continue;
                }

                bool isEdge = x == 0 || y == 0 || x == width - 1 || y == height - 1;
                if (terrain[y][x] == '^' && isEdge)
                {
                    landscape[x, y] = (1, byte.MaxValue);
                    queue.Enqueue((x, y));
                    continue;
                }

                if (terrain[y][x] == '^')
                {
                    landscape[x, y] = (-1, byte.MaxValue);
                    continue;
                }

                landscape[x, y] = (0, byte.MaxValue);
                queue.Enqueue((x, y));
            }

        foreach (var riverPoint in watter)
        {
            foreach ((int x, int y) dif in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                var newX = riverPoint.X + dif.x;
                var newY = riverPoint.Y + dif.y;
                bool correctCoords = newX >= 0 && newX < width && newY >= 0 && newY < height;
                bool needSpecify = correctCoords
                    && (landscape[newX, newY].height != -0.5f);
                if (needSpecify)
                {
                    if (landscape[newX, newY].height == -1)
                    {
                        landscape[newX, newY].height = 1;
                        landscape[newX, newY].wet = (byte)2;
                        queue.Enqueue((newX, newY));
                    }
                    if (landscape[newX, newY].height == 0)
                    {
                        landscape[newX, newY].wet = (byte)1;
                    }
                    if (landscape[newX, newY].height == 1)
                    {
                        landscape[newX, newY].wet = (byte)2;
                    }
                    wetQueue.Enqueue((newX, newY));
                }
            }
        }

        return landscape;
    }
    private static int[] GetDryGrounds((Single height, byte wet)[,] landscape)
    {
        int dayZero = 0, dayOne = 0, dayTwo = 0, dayThree = 0;
        for (int x = 0; x <= landscape.GetUpperBound(0); x++)
            for (int y = 0; y <= landscape.GetUpperBound(1); y++)
            {
                if (landscape[x, y].wet >= 1) dayZero++;
                if (landscape[x, y].wet >= 2) dayOne++;
                if (landscape[x, y].wet >= 3) dayTwo++;
                if (landscape[x, y].wet >= 4) dayThree++;
            }
        return new[] { dayZero, dayOne, dayTwo, dayThree };
    }
}