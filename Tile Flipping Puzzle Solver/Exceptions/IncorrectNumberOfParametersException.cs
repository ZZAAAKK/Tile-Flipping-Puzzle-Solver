namespace Tile_Flipping_Puzzle_Solver.Exceptions;

internal class IncorrectNumberOfParametersException : Exception
{
    public IncorrectNumberOfParametersException()
    {
        Console.Write("Wrong number of parameters supplied.");
        throw new BaseSolverException();
    }
}
