using Tile_Flipping_Puzzle_Solver.Exceptions;
using Tile_Flipping_Puzzle_Solver.Extension_Methods;

namespace Tile_Flipping_Puzzle_Solver;

internal class Program
{
    readonly static Random rand = new();
    readonly static Random randCol = new();
    static int[,] grid;
    static List<string> moves = new();
    static bool solutionFound = false;
    static List<string> visitedGrids = new();

    static void Main(string[] args)
    {
        Console.Title = "Tile Flipping Puzzle Solver";

        if (args.Length == 2)
        {
            if (int.TryParse(args[0], out int n) && args[1].Length == n * n)
            {
                Solve(n, args[1]);
            }
            else
            {
                throw new InvalidParametersException();
            }
        }
        else
        {
            throw new IncorrectNumberOfParametersException();
        }

        Console.ReadLine();
    }

    static void Solve(int n, string gridString)
    {
        grid = gridString.ToArray().Select(x => int.Parse(x.ToString())).ToTwoDimensionalArray(n);
        int[,] solvedGrid = new int[n, n].Fill(1);
        PrintGridState("Initial State");

        RecursiveSearch();

        Console.WriteLine($"Solved in {moves.Count} moves:");
        Console.WriteLine(string.Join(' ', moves));
    }

    static void PrintGridState(string move)
    {
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                Console.Write($"{grid[row, col]} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine(move);
        Console.WriteLine();
    }

    static void RecursiveSearch()
    {
        if (solutionFound)
        {
            return;
        }

        HashSet<(int, int)> potentialMoves = new();

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (grid[row, col] == 0)
                {
                    potentialMoves.Add((row, col));
                }
            }
        }

        potentialMoves = potentialMoves
            .Select(x => new { score = GetMoveScore(x), move = x })
            .OrderByDescending(x => x.score)
            .Select(x => x.move)
            .ToHashSet();

        foreach ((int, int) coords in potentialMoves)
        {
            MakeMove(coords);

            if (visitedGrids.Contains(grid.ToMatrixString()))
            {
                return;
            }

            visitedGrids.Add(grid.ToMatrixString());
            if (grid.MatrixEquals(new int[grid.GetLength(0), grid.GetLength(1)].Fill(1)))
            {
                solutionFound = true;
            }
            else
            {
                RecursiveSearch();
                MakeMove(coords, false);
            }
        }
    }

    static int GetMoveScore((int, int) move)
    {
        int score = 0;

        foreach ((int, int) tile in GetAdjacentTiles(move))
        {
            score += grid[tile.Item1, tile.Item2] == 1 ? -2 : 1;
        }

        return score;
    }

    static void MakeMove((int, int) coords, bool insertMove = true)
    {
        if (solutionFound)
        {
            return;
        }

        while (!(grid[coords.Item1, coords.Item2] == 0))
        {
            coords = new(rand.Next(0, grid.GetLength(0)), randCol.Next(0, grid.GetLength(1)));
        }

        string[] moveComponents = new string[2];

        moveComponents[0] = coords.Item1 switch
        {
            0 => "T",
            1 => "M",
            _ => "B"
        };

        moveComponents[1] = coords.Item2 switch
        {
            0 => "L",
            1 => "M",
            _ => "R"
        };

        grid[coords.Item1, coords.Item2] = FlipTile(grid[coords.Item1, coords.Item2]);

        foreach ((int, int) coord in GetAdjacentTiles(coords))
        {
            grid[coord.Item1, coord.Item2] = FlipTile(grid[coord.Item1, coord.Item2]);
        }

        if (insertMove)
        {
            string move = string.Join("", moveComponents);
            moves.Add(move);
            PrintGridState(move);
        }
        else
        {
            moves.RemoveAt(moves.Count - 1);
        }
    }

    static (int, int)[] GetAdjacentTiles((int, int) coords)
    {
        HashSet<(int, int)> result = new();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (Math.Abs(i) + Math.Abs(j) == 2 || (i == 0 && j == 0))
                {
                    continue;
                }

                int dx = coords.Item1 + i;
                int dy = coords.Item2 + j;

                if (dx < 0 || dy < 0 || dx >= grid.GetLength(0) || dy >= grid.GetLength(1))
                {
                    continue;
                }

                result.Add((dx, dy));
            }
        }

        return result.ToArray();
    }

    static int FlipTile(int tile)
    {
        return tile ^ 1;
    }
}