using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars.Test;

public class Loopover
{
    [Fact]
    public void CanMoveFirstRowRight()
    {
        var expectedBoard = new char[][] { new char[] { '3', '1', '2' }, new char[] { '4', '5', '6' }};
        var actualBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '4', '5', '6' }};

        var loopover = new LoopoverPuzzleBoard<char>(actualBoard);
        loopover.Slide(new SlideCommand(SlideDirection.Right, 0));
        Assert.True(Compare(expectedBoard, actualBoard));
    }

    [Fact]
    public void CanMoveSecondRowRight()
    {
        var expectedBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '6', '4', '5' }, new char[] { '7', '8', '9' } };
        var actualBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '4', '5', '6' }, new char[] { '7', '8', '9' } };

        var loopover = new LoopoverPuzzleBoard<char>(actualBoard);
        loopover.Slide(new SlideCommand(SlideDirection.Right, 1));
        Assert.True(Compare(expectedBoard, actualBoard));
    }

    [Fact]
    public void CanMoveThirdRowRight()
    {
        var expectedBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '4', '5', '6' }, new char[] { '9', '7', '8' } };
        var actualBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '4', '5', '6' }, new char[] { '7', '8', '9' } };

        var loopover = new LoopoverPuzzleBoard<char>(actualBoard);
        loopover.Slide(new SlideCommand(SlideDirection.Right, 2));
        Assert.True(Compare(expectedBoard, actualBoard));
    }


    [Fact]
    public void CanMoveFirstRowLeft()
    {
        var expectedBoard = new char[][] { new char[] { '3', '1', '2' }, new char[] { '4', '5', '6' }, new char[] { '7', '8', '9' } };
        var actualBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '4', '5', '6' }, new char[] { '7', '8', '9' } };

        var loopover = new LoopoverPuzzleBoard<char>(actualBoard);
        loopover.Slide(new SlideCommand (SlideDirection.Right, 0));
        Assert.True(Compare(expectedBoard, actualBoard));
    }

    [Fact]
    public void CanMoveSecondRowLeft()
    {
        var expectedBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '6', '4', '5' }, new char[] { '7', '8', '9' } };
        var actualBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '4', '5', '6' }, new char[] { '7', '8', '9' } };

        var loopover = new LoopoverPuzzleBoard<char>(actualBoard);
        loopover.Slide(new SlideCommand(SlideDirection.Right, 1));
        Assert.True(Compare(expectedBoard, actualBoard));
    }

    [Fact]
    public void CanMoveThirdRowLeft()
    {
        var expectedBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '4', '5', '6' }, new char[] { '9', '7', '8' } };
        var actualBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '4', '5', '6' }, new char[] { '7', '8', '9' } };

        var loopover = new LoopoverPuzzleBoard<char>(actualBoard);
        loopover.Slide(new SlideCommand(SlideDirection.Right, 2));
        Assert.True(Compare(expectedBoard, actualBoard));
    }

    [Fact]
    public void CanMoveSecondCollumnDown()
    {
        var expectedBoard = new char[][] { new char[] { '1', '5', '3' }, new char[] { '4', '2', '6' } };
        var actualBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '4', '5', '6' } };

        var loopover = new LoopoverPuzzleBoard<char>(actualBoard);
        loopover.Slide(new SlideCommand(SlideDirection.Down, 1));
        Assert.True(Compare(expectedBoard, actualBoard));
    }

    [Fact]
    public void CanMoveSecondCollumnUp()
    {
        var expectedBoard = new char[][] { new char[] { '1', '5', '3' }, new char[] { '4', '8', '6' }, new char[] { '7', '2', '9' } };
        var actualBoard = new char[][] { new char[] { '1', '2', '3' }, new char[] { '4', '5', '6' }, new char[] { '7', '8', '9' } };

        var loopover = new LoopoverPuzzleBoard<char>(actualBoard);
        loopover.Slide(new SlideCommand(SlideDirection.Up, 1));
        Assert.True(Compare(expectedBoard, actualBoard));
    }


    [Fact]
    public void MixedEqualsSolved_3x3_01()
    {
        var actualBoard = new char[][] {
            new char[] { '3', '7', '2' },
            new char[] { '4', '9', '5' },
            new char[] { '6', '8', '1' }
        };

        var findDelegate = CodeWars.Loopover.Solve(actualBoard, expectedBoard_3x3);

        Assert.True(Compare(expectedBoard_3x3, findDelegate));
    }

    [Fact]
    public void MixedEqualsSolved_3x3_02()
    {
        var actualBoard = new char[][] {
            new char[] { '1', '7', '4' },
            new char[] { '9', '2', '6' },
            new char[] { '8', '3', '5' }
        };

        var findDelegate = CodeWars.Loopover.Solve(actualBoard, expectedBoard_3x3);

        Assert.True(Compare(expectedBoard_3x3, findDelegate));
    }

    [Fact]
    public void MixedEqualsSolved_3x3_03()
    {
        var actualBoard = new char[][] {
            new char[] { '4', '9', '2' },
            new char[] { '5', '3', '6' },
            new char[] { '8', '7', '1' }
        };

        var findDelegate = CodeWars.Loopover.Solve(actualBoard, expectedBoard_3x3);

        Assert.True(Compare(expectedBoard_3x3, findDelegate));
    }

    [Fact]
    public void MixedEqualsSolved_3x3_04()
    {
        var actualBoard = new char[][] {
            new char[] { '9', '2', '1' },
            new char[] { '8', '4', '5' },
            new char[] { '3', '6', '7' }
        };

        var findDelegate = CodeWars.Loopover.Solve(actualBoard, expectedBoard_3x3);

        Assert.True(Compare(expectedBoard_3x3, findDelegate));
    }
    

    [Fact]
    public void MixedEqualsSolved_4x4_01()
    {
        var actualBoard = new char[][] {
            new char[] { 'G', 'J', 'H', 'A' },
            new char[] { 'B', 'O', 'L', 'K' },
            new char[] { 'D', 'M', 'C', 'N' },
            new char[] { 'E', 'F', 'I', 'P' }
        };

        var findDelegate = CodeWars.Loopover.Solve(actualBoard, expectedBoard_4x4);

        Assert.True(Compare(expectedBoard_4x4, findDelegate));
    }

    [Fact]
    public void MixedEqualsSolved_5x5_01()
    {
        var actualBoard = new char[][] {
            new char[] { 'I','M','X','W','E' },
            new char[] { 'O','B','G','N','P' },
            new char[] { 'Y','Q','U','A','J' },
            new char[] { 'D','C','H','F','L' },
            new char[] { 'R','V','S','T','K' }
        };

        var findDelegate = CodeWars.Loopover.Solve(actualBoard, expectedBoard_5x5);

        Assert.True(Compare(expectedBoard_5x5, findDelegate));
    }

    [Fact]
    public void MixedEqualsSolved_6x6_01()
    {
        var actualBoard = new char[][] {
            new char[] { 'W','C','M','D','J','0' },
            new char[] { 'O','R','F','B','A','1' },
            new char[] { 'K','N','G','L','Y','2' },
            new char[] { 'P','H','V','S','E','3' },
            new char[] { 'T','X','Q','U','I','4' },
            new char[] { 'Z','5','6','7','8','9' }
        };

        var findDelegate = CodeWars.Loopover.Solve(actualBoard, expectedBoard_6x6);

        Assert.True(Compare(expectedBoard_6x6, findDelegate));
    }

    [Fact]
    public void FixedTest3()
    {
        var mixedBoard = new char[][] {
            new char[] { 'A','C','D','B','E' },
            new char[] { 'F','G','H','I','J' },
            new char[] { 'K','L','M','N','O' },
            new char[] { 'P','Q','R','S','T' }
        };
        var solvedBoard = new char[][] {
            new char[] { 'A','B','C','D','E' },
            new char[] { 'F','G','H','I','J' },
            new char[] { 'K','L','M','N','O' },
            new char[] { 'P','Q','R','S','T' }
        };

        var findDelegate = CodeWars.Loopover.Solve(mixedBoard, solvedBoard);

        Assert.True(Compare(solvedBoard, findDelegate));
    }

    [Fact]
    public void FixedTest7x2()
    {
        var mixedBoard = new char[][] {
            new char[] { 'J','L' },
            new char[] { 'C','E' },
            new char[] { 'F','B' },
            new char[] { 'H','K' },
            new char[] { 'G','N' },
            new char[] { 'I','M' },
            new char[] { 'D','A' }
        };

        var findDelegate = CodeWars.Loopover.Solve(mixedBoard, expectedBoard_7x2);

        Assert.True(Compare(expectedBoard_7x2, findDelegate));
    }
    [Fact]
    public void FixedTest3x4()
    {
        var mixedBoard = new char[][] {
            new char[] { 'L','J','A','B' },
            new char[] { 'I','E','G','D' },
            new char[] { 'K','C','F','H' }
        };
        var solvedBoard = new char[][] {
            new char[] { 'A','B','C','D' },
            new char[] { 'E','F','G','H' },
            new char[] { 'I','J','K','L' }
        };
        var findDelegate = CodeWars.Loopover.Solve(mixedBoard, solvedBoard);

        Assert.True(Compare(solvedBoard, findDelegate));
    }

    private bool Compare(char[][] solvedBoard, Func<char, (int row, int col)> find)
    {
        for (int row = 0; row < solvedBoard.Length; row++)
        {
            for (int column = 0; column < solvedBoard[0].Length; column++)
            {
                var coords = find(solvedBoard[row][column]);
                if (coords.row != row || coords.col != column)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool Compare(char[][] a, char[][] b)
    {
        for (int i = 0; i < a.Length; i++)
            for (int j = 0; j < a[0].Length; j++)
            {
                if ( a[i][j] != b[i][j] )
                    return false;
            }

        return true;
    }






    private char[][] expectedBoard_3x3 = new char[][] {
        new char[] { '1', '2', '3' },
        new char[] { '4', '5', '6' },
        new char[] { '7', '8', '9' }
    };

    private char[][] expectedBoard_4x4 = new char[][] {
        new char[] { 'A','B','C','D' },
        new char[] { 'E','F','G','H' },
        new char[] { 'I','J','K','L' },
        new char[] { 'M','N','O','P' }
    };
        
    private char[][] expectedBoard_5x5 = new char[][] {
        new char[] { 'A','B','C','D','E' },
        new char[] { 'F','G','H','I','J' },
        new char[] { 'K','L','M','N','O' },
        new char[] { 'P','Q','R','S','T' },
        new char[] { 'U','V','W','X','Y' }
    };

    private char[][] expectedBoard_6x6 = new char[][] {
        new char[] { 'A','B','C','D','E','F' },
        new char[] { 'G','H','I','J','K','L' },
        new char[] { 'M','N','O','P','Q','R' },
        new char[] { 'S','T','U','V','W','X' },
        new char[] { 'Y','Z','0','1','2','3' },
        new char[] { '4','5','6','7','8','9' }
    };

    private char[][] expectedBoard_7x2 = new char[][] {
        new char[] { 'A','B' },
        new char[] { 'C','D' },
        new char[] { 'E','F' },
        new char[] { 'G','H' },
        new char[] { 'I','J' },
        new char[] { 'K','L' },
        new char[] { 'M','N' }
    };
}
