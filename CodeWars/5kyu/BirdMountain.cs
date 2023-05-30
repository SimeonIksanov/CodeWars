// https://www.codewars.com/kata/5c09ccc9b48e912946000157/train/csharp

using System;

namespace CodeWars;

public partial class Dinglemouse
{
    public static int PeakHeight(char[][] mountain)
    {
        var queue = new Queue<(int X, int Y)>();
        var landscape = GetLandscape(mountain, queue);
        var landWidth = landscape.GetUpperBound(0);
        var landHeight = landscape.GetUpperBound(1);
        while (queue.Count != 0)
        {
            var point = queue.Dequeue();
            var pointHeight = landscape[point.X, point.Y];

            foreach ((int x, int y) dif in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                var newX = point.X + dif.x;
                var newY = point.Y + dif.y;
                bool correctCoords = newX >= 0 && newX <= landWidth && newY >= 0 && newY <= landHeight;
                bool needSpecify = correctCoords
                    && (landscape[newX, newY] < 0 || landscape[newX, newY] > pointHeight + 1);
                if (needSpecify)
                {
                    landscape[newX, newY] = pointHeight + 1;
                    queue.Enqueue((newX, newY));
                }
            }
        }

        return FindMaxHeight(landscape);
    }

    private static int[,] GetLandscape(char[][] mountain, Queue<(int X, int Y)> queue)
    {
        int width = mountain[0].Length;
        int height = mountain.Length;
        var landscape = new int[width, height];

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                bool isEdge = x == 0 || y == 0 || x == width - 1 || y == height - 1;
                if (mountain[y][x] == '^' && isEdge)
                {
                    landscape[x, y] = 1;
                    queue.Enqueue((x, y));
                    continue;
                }

                if (mountain[y][x] == '^')
                {
                    landscape[x, y] = -1;
                    continue;
                }

                landscape[x, y] = 0;
                queue.Enqueue((x, y));
            }

        return landscape;
    }

    private static int FindMaxHeight(int[,] landscape)
    {
        int max = 0;
        for (int x = 0; x <= landscape.GetUpperBound(0); x++)
            for (int y = 0; y <= landscape.GetUpperBound(1); y++)
            {
                max = Math.Max(max, landscape[x, y]);
            }
        return max;
    }
}
