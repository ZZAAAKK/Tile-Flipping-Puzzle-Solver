namespace Tile_Flipping_Puzzle_Solver.Exceptions;

internal class BaseSolverException : Exception
{
    public BaseSolverException() : base()
    {
        Console.WriteLine(" Parameters should be in the form:");
        Console.WriteLine("\t[0]: The size of the square grid, where the size denotes the length of 1 side. [type of: int]");
        Console.WriteLine("\t[1]: A binary string denoting the current state of the grid.");
        Console.WriteLine("\t\twhere 1 = a face up tile, and 0 = a face down tile. [type of: string]");
    }
}
