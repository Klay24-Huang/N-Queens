internal class Program
{
    private readonly List<IList<string>> result = new();

    // 紀錄已被擺放的皇后佔據的路線
    private readonly HashSet<int> forwardSlash = new();
    private readonly HashSet<int> backwardSlash = new();
    private readonly HashSet<int> column = new();

    // 這次盤面的紀錄
    private readonly List<IList<char>> borad = new();

    private static void Main(string[] args)
    {
        // 可在此更改皇后的數量
        var queens = 8;
        var p = new Program();
        var answer = p.SolveNQueens(queens);
        // 用console 印出結果
        p.PrintResult(queens);

    }

    public IList<IList<string>> SolveNQueens(int n)
    {
        // 初始化棋盤
        for (int i = 0; i < n; i++)
        {
            borad.Add(Enumerable.Repeat('.', n).ToList());
        }

        Backtracking(n, 0);
        return result;
    }

    private bool IsInvalid(int x, int y)
    {
        return column.Contains(x) || forwardSlash.Contains(x - y) || backwardSlash.Contains(x + y);
    }

    private void Backtracking(int numberOfQueens, int y)
    {
        // 以擺放完n個皇后，記錄此次擺放的位置
        if (y == numberOfQueens)
        {
            // 將List char轉換成 string，clone出當下棋盤的new object
            var convertBoard = borad.Select(x => new string(x.ToArray())).ToList();
            result.Add(convertBoard);
            // stop point
            return;
        }

        // x == 行 == column == 直 ;  y == 列 == row == 橫
        for (int x = 0; x < numberOfQueens; x++)
        {
            if (IsInvalid(x, y))
            {
                continue;
            }

            // 紀錄此次皇后的位置
            borad[x][y] = 'Q';
            column.Add(x);
            forwardSlash.Add(x - y);
            backwardSlash.Add(x + y);
            Backtracking(numberOfQueens, y + 1);
            // 將上次擺放皇后的相關紀錄還原
            borad[x][y] = '.';
            column.Remove(x);
            forwardSlash.Remove(x - y);
            backwardSlash.Remove(x + y);
        }
    }

    private void PrintResult(int queens)
    {
        Console.WriteLine($"{queens}個皇后，總共有${result.Count}個結果");
        var count = 1;
        foreach (var answer in result)
        {
            Console.WriteLine($"結果{count}");
            foreach (var row in answer)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine("");
            count++;
        }
    }
}