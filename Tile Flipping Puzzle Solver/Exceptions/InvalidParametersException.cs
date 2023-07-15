namespace Tile_Flipping_Puzzle_Solver.Exceptions;

internal class InvalidParametersException : Exception
{
    public InvalidParametersException()
    {
        Console.WriteLine("Invalid parameters supplied.");
        throw new BaseSolverException();
    }
}
