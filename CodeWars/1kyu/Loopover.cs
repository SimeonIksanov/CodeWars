// https://www.codewars.com/kata/5c1d796370fee68b1e000611

using System.Text;

namespace CodeWars;

public class Loopover
{
    // public static List<string> Solve(char[][] mixedUpBoard, char[][] solvedBoard)
    public static Func<char, (int, int)> Solve(char[][] mixedUpBoard, char[][] solvedBoard)
    {
        var loopover = new LoopoverPuzzleBoard<char>(mixedUpBoard);
        SolveFirstRow(loopover, solvedBoard);         // Solve first row
        SolveTopLeftSubSquare(loopover, solvedBoard); // solve top-left sub-square of size (rows-1) * (cols-1)
        SolveLastColumn(loopover, solvedBoard);       // solve last column first rows-2 rows
        SolveLastRow(loopover, solvedBoard);          // solve last row and column
        SolveParity(loopover, solvedBoard);
        SolveParity2(loopover, solvedBoard);

        System.Console.WriteLine(loopover);
        System.Console.WriteLine(string.Join(", ", loopover.Commands));

        //
        //return loopover.Compare(solvedBoard) ? loopover.Commands : null;
        //
        return loopover.Find;
    }

    private static void SolveFirstRow(LoopoverPuzzleBoard<char> loopover, char[][] solvedBoard)
    {
        var row = 0;
        for (int column = 0; column < loopover.ColumnsCount - 1; column++)
        {
            var targetChar = solvedBoard[row][column];
            var pos = loopover.Find(targetChar);

            if (pos.row == row && pos.column == column)
                continue;

            if (pos.row == row)
            {
                loopover.Slide(new SlideCommand(SlideDirection.Down, pos.row));
                pos = loopover.Find(targetChar);
            }

            while (pos.column != column)
            {
                loopover.Slide(new SlideCommand(SlideDirection.Right, pos.row));
                pos = loopover.Find(targetChar);
            }

            while (pos.row != 0)
            {
                loopover.Slide(new SlideCommand(SlideDirection.Up, column));
                pos = loopover.Find(targetChar);
            }
        }
    }
    private static void SolveTopLeftSubSquare(LoopoverPuzzleBoard<char> loopover, char[][] solvedBoard)
    {
        for (int row = 0; row < loopover.RowsCount - 1; row++)
        {
            for (int column = 0; column < loopover.ColumnsCount - 1; column++)
            {
                var targetChar = solvedBoard[row][column];
                var pos = loopover.Find(targetChar);
                if (pos.row == row && pos.column == loopover.ColumnsCount - 1)
                {
                    loopover.Slide(new SlideCommand(SlideDirection.Left, row));
                    continue;
                }
                if (pos.row == row && pos.column == loopover.ColumnsCount - 2)
                {
                    continue;
                }

                if (pos.row == row && pos.column < loopover.ColumnsCount - 1)
                {
                    int shiftSize = 0;
                    while (pos.column < loopover.ColumnsCount - 1)
                    {
                        loopover.Slide(new SlideCommand(SlideDirection.Left, pos.row));
                        pos = loopover.Find(targetChar);
                        shiftSize++;
                    }
                    loopover.Slide(new SlideCommand(SlideDirection.Down, loopover.ColumnsCount - 1));
                    pos = loopover.Find(targetChar);

                    while (shiftSize-- > 0)
                        loopover.Slide(new SlideCommand(SlideDirection.Right, row));
                }

                pos = loopover.Find(targetChar);
                while (pos.column != loopover.ColumnsCount - 1)
                {
                    loopover.Slide(new SlideCommand(SlideDirection.Right, pos.row));
                    pos = loopover.Find(targetChar);
                }

                while (pos.row != row)
                {
                    loopover.Slide(new SlideCommand(SlideDirection.Up, pos.column));
                    pos = loopover.Find(targetChar);
                }

                loopover.Slide(new SlideCommand(SlideDirection.Left, row));
            }
        }
    }
    private static void SolveLastColumn(LoopoverPuzzleBoard<char> loopover, char[][] solvedBoard)
    {
        var column = loopover.ColumnsCount - 1;
        for (var row = 0; row < loopover.RowsCount - 2; row++)
        {
            var targetChar = solvedBoard[row][column];
            var pos = loopover.Find(targetChar);

            if (pos.column == loopover.ColumnsCount - 1)
            {
                if (pos.row == loopover.RowsCount - 1)
                {
                    loopover.Slide(new SlideCommand(SlideDirection.Up, pos.column));
                    continue;
                }
                if (pos.row == loopover.RowsCount - 2)
                {
                    continue;
                }

                int shiftSize = 0;
                while (pos.row != loopover.RowsCount - 1)
                {
                    loopover.Slide(new SlideCommand(SlideDirection.Down, pos.column));
                    shiftSize++;
                    pos = loopover.Find(targetChar);
                }
                loopover.Slide(new SlideCommand(SlideDirection.Left, pos.row));
                while (shiftSize-- > 0)
                {
                    loopover.Slide(new SlideCommand(SlideDirection.Up, pos.column));
                }
                loopover.Slide(new SlideCommand(SlideDirection.Right, pos.row));
            }
            else if (pos.row == loopover.RowsCount - 1)
            {
                while (pos.column != loopover.ColumnsCount - 1)
                {
                    loopover.Slide(new SlideCommand(SlideDirection.Right, pos.row));
                    pos = loopover.Find(targetChar);
                }
            }
            loopover.Slide(new SlideCommand(SlideDirection.Up, loopover.ColumnsCount - 1));
        }
        loopover.Slide(new SlideCommand(SlideDirection.Up, loopover.ColumnsCount - 1));
    }
    private static void SolveLastRow(LoopoverPuzzleBoard<char> loopover, char[][] solvedBoard)
    {
        int lastRow = loopover.RowsCount - 1, lastColumn = loopover.ColumnsCount - 1;
        var shiftSize = 0;
        int[] insertSlotPositions = new int[2] { 0, loopover.RowsCount - 2 };
        int insertSlotIndex = 1;
        SlideDirection direction;

        for (var column = 0; column < loopover.ColumnsCount; column++)
        {
            var targetChar = solvedBoard[lastRow][column];
            var pos = loopover.Find(targetChar);
            if (pos.column == column && pos.row == lastRow)
                continue;

            // target char is in insert slot
            if (pos.column == lastColumn && pos.row == insertSlotPositions[insertSlotIndex])
            {
                while (loopover.ColumnsCount - shiftSize - 1 - column > 0)
                {
                    loopover.Slide(new SlideCommand(SlideDirection.Right, lastRow));
                    shiftSize++;
                }
                direction = insertSlotIndex == 0 ? SlideDirection.Up : SlideDirection.Down;
                loopover.Slide(new SlideCommand(direction, lastColumn));
                insertSlotIndex = SwapInsertSlot(insertSlotIndex);

                while (shiftSize != 0)
                {
                    loopover.Slide(new SlideCommand(shiftSize > 0 ? SlideDirection.Left : SlideDirection.Right, lastRow));
                    shiftSize += shiftSize > 0 ? -1 : 1;
                }
                continue;
            }

            while (pos.column != lastColumn)
            {
                loopover.Slide(new SlideCommand(SlideDirection.Right, lastRow));
                shiftSize++;
                pos = loopover.Find(targetChar);
            }
            direction = insertSlotIndex == 0 ? SlideDirection.Up : SlideDirection.Down;
            loopover.Slide(new SlideCommand(direction, lastColumn));
            insertSlotIndex = SwapInsertSlot(insertSlotIndex);

            while (loopover.ColumnsCount - shiftSize - 1 - column > 0)
            {
                loopover.Slide(new SlideCommand(SlideDirection.Right, lastRow));
                shiftSize++;
            }
            direction = insertSlotIndex == 0 ? SlideDirection.Up : SlideDirection.Down;
            loopover.Slide(new SlideCommand(direction, lastColumn));
            insertSlotIndex = SwapInsertSlot(insertSlotIndex);

            while (shiftSize != 0)
            {
                loopover.Slide(new SlideCommand(shiftSize > 0 ? SlideDirection.Left : SlideDirection.Right, lastRow));
                shiftSize += shiftSize > 0 ? -1 : 1;
            }
        }

        if (insertSlotIndex != 1)
            loopover.Slide(new SlideCommand(SlideDirection.Up, lastColumn));

        int SwapInsertSlot(int insertSlotIndex) => (insertSlotIndex + 1) % 2;
    }
    private static void SolveParity(LoopoverPuzzleBoard<char> loopover, char[][] solvedBoard)
    {
        int lastRow = loopover.RowsCount - 1, lastColumn = loopover.ColumnsCount - 1;
        var ch = solvedBoard[lastRow][lastColumn];
        var pos = loopover.Find(ch);
        if (pos.row == lastRow)
            return;
        if (loopover.ColumnsCount % 2 == 1)
            return;

        int[] insertSlotPositions = new int[2] { 0, loopover.RowsCount - 2 };
        int insertSlotIndex = 1;
        SlideDirection direction;

        int shiftSize = loopover.ColumnsCount - 1;

        while (shiftSize > 0)
        {
            loopover.Slide(new SlideCommand(SlideDirection.Left, lastRow));
            shiftSize--;

            direction = insertSlotIndex == 0 ? SlideDirection.Up : SlideDirection.Down;
            loopover.Slide(new SlideCommand(direction, lastColumn));
            insertSlotIndex = SwapInsertSlot(insertSlotIndex);

            loopover.Slide(new SlideCommand(SlideDirection.Left, lastRow));
            shiftSize--;

            direction = insertSlotIndex == 0 ? SlideDirection.Up : SlideDirection.Down;
            loopover.Slide(new SlideCommand(direction, lastColumn));
            insertSlotIndex = SwapInsertSlot(insertSlotIndex);
        }
        loopover.Slide(new SlideCommand(SlideDirection.Left, lastRow));

        int SwapInsertSlot(int insertSlotIndex) => (insertSlotIndex + 1) % 2;
    }
    private static void SolveParity2(LoopoverPuzzleBoard<char> loopover, char[][] solvedBoard)
    {
        int lastRow = loopover.RowsCount - 1, lastColumn = loopover.ColumnsCount - 1;
        var ch = solvedBoard[lastRow][lastColumn];
        var pos = loopover.Find(ch);
        if (pos.row == lastRow)
            return;

        loopover.Slide(new SlideCommand(SlideDirection.Left, lastRow));
        loopover.Slide(new SlideCommand(SlideDirection.Down, lastColumn));
        loopover.Slide(new SlideCommand(SlideDirection.Right, lastRow));
        loopover.Slide(new SlideCommand(SlideDirection.Up, lastColumn));

        if (loopover.ColumnsCount < 3)
        {
            loopover.Slide(new SlideCommand(SlideDirection.Left, lastRow));
            return;
        }

        for (var i = 0; i < loopover.RowsCount / 2; i++)
        {
            loopover.Slide(new SlideCommand(SlideDirection.Down, lastColumn));
            loopover.Slide(new SlideCommand(SlideDirection.Right, lastRow));
            loopover.Slide(new SlideCommand(SlideDirection.Down, lastColumn));
            loopover.Slide(new SlideCommand(SlideDirection.Left, lastRow));
        }
        loopover.Slide(new SlideCommand(SlideDirection.Down, lastColumn));

        #region fix last row
        int[] insertSlotPositions = new int[2] { 0, loopover.RowsCount - 2 };
        int insertSlotIndex = 1;
        SlideDirection direction;
        int shiftSize = 0;

        for (int column = 0; column < loopover.ColumnsCount-1; column++)
        {
            ch = solvedBoard[lastRow][column];
            pos = loopover.Find(ch);
            if (pos.row == lastRow && pos.column == column)
                continue;

            while(pos.column < lastColumn)
            {
                loopover.Slide(new SlideCommand(SlideDirection.Right, lastRow));
                shiftSize++;
                pos = loopover.Find(ch);
            }
            direction = insertSlotIndex == 0 ? SlideDirection.Up : SlideDirection.Down;
            loopover.Slide(new SlideCommand(direction, lastColumn));
            insertSlotIndex = SwapInsertSlot(insertSlotIndex);

            while (loopover.ColumnsCount - shiftSize - 1 - column > 0)
            {
                loopover.Slide(new SlideCommand(SlideDirection.Right, lastRow));
                shiftSize++;
            }
            direction = insertSlotIndex == 0 ? SlideDirection.Up : SlideDirection.Down;
            loopover.Slide(new SlideCommand(direction, lastColumn));
            insertSlotIndex = SwapInsertSlot(insertSlotIndex);

            while(shiftSize>0)
            {
                loopover.Slide(new SlideCommand(SlideDirection.Left, lastRow));
                shiftSize--;
            }
        }
        #endregion

        int SwapInsertSlot(int insertSlotIndex) => (insertSlotIndex + 1) % 2;
    }
}

public class LoopoverPuzzleBoard<T>
{
    private readonly int _size_w;
    private readonly int _size_h;
    private readonly T[][] _board;
    private readonly List<SlideCommand> _commandHistory;
    private Dictionary<T, (int row, int column)> _locator;


    public LoopoverPuzzleBoard(T[][] board)
    {
        _size_h = board.Length;
        _size_w = board[0].Length;
        _board = board;
        _commandHistory = new List<SlideCommand>();
        InventorizeBoard();
    }


    public List<string> Commands => _commandHistory.Select(c => $"{c.Direction.ToString()[0]}{c.Index}").ToList();
    public int RowsCount => _size_h;
    public int ColumnsCount => _size_w;
    public void Slide(SlideCommand command, int count = 1)
    {
        for (var c = 0; c < count; c++)
        {
            switch (command.Direction)
            {
                case SlideDirection.Right:
                    SlideRight(command.Index);
                    break;
                case SlideDirection.Left:
                    SlideLeft(command.Index);
                    break;
                case SlideDirection.Up:
                    SlideUp(command.Index);
                    break;
                case SlideDirection.Down:
                    SlideDown(command.Index);
                    break;
            }

            _commandHistory.Add(command);
        }

        switch (command.Direction)
        {
            case SlideDirection.Right:
            case SlideDirection.Left:
                InventorizeRow(command.Index);
                break;
            case SlideDirection.Up:
            case SlideDirection.Down:
                InventorizeColumn(command.Index);
                break;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (int i = 0; i < _size_h; i++)
        {
            sb.AppendJoin(", ", _board[i].Select(x => string.Format("{0,2}", x)));
            sb.Append(Environment.NewLine);
        }

        return sb.ToString();
    }

    public (int row, int column) Find(T charToFind)
        => _locator[charToFind];

    private void InventorizeBoard()
    {
        _locator = new Dictionary<T, (int row, int column)>();
        for (int row = 0; row < _size_h; row++)
            InventorizeRow(row);
    }

    private void InventorizeRow(int row)
    {
        for (int column = 0; column < _size_w; column++)
            _locator[_board[row][column]] = (row, column);
    }

    private void InventorizeColumn(int column)
    {
        for (int row = 0; row < _size_h; row++)
            _locator[_board[row][column]] = (row, column);
    }

    private void SlideRight(int index)
    {
        var tmp = _board[index][_size_w-1];
        for (int i = 0; i < _size_w; i++)
        {
            var t = _board[index][i];
            _board[index][i] = tmp;
            tmp = t;
        }
    }
    private void SlideLeft(int index)
    {
        var tmp = _board[index][0];
        for (int i = _size_w - 1; i >= 0; i--)
        {
            var t = _board[index][i];
            _board[index][i] = tmp;
            tmp = t;
        }
    }

    private void SlideUp(int index)
    {
        var tmp = _board[0][index];
        for (int i = _size_h - 1; i >= 0; i--)
        {
            var t = _board[i][index];
            _board[i][index] = tmp;
            tmp = t;
        }
    }
    private void SlideDown(int index)
    {
        var tmp = _board[_size_h-1][index];
        for (int i = 0; i < _size_h; i++)
        {
            var t = _board[i][index];
            _board[i][index] = tmp;
            tmp = t;
        }

    }

    public bool Compare(T[][] solved)
    {
        for (int i = 0; i < _board.Length; i++)
            for (int j = 0; j < _board[0].Length; j++)
            {
                if (!_board[i][j].Equals(solved[i][j]))
                    return false;
            }

        return true;
    }

}

public class SlideCommand
{
    public readonly SlideDirection Direction;
    public readonly int Index;

    public SlideCommand(SlideDirection direction, int index)
    {
        Direction = direction;
        Index = index;
    }

    public override string ToString()
    {
        return $"{Enum.GetName<SlideDirection>(Direction)?.ToUpper()[0]}{Index}";
    }
}
public enum SlideDirection
{
    Right,
    Left,
    Up,
    Down
}
