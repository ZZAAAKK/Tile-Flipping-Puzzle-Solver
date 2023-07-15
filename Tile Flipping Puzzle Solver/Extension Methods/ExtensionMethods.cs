using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Tile_Flipping_Puzzle_Solver.Extension_Methods;

internal static class ExtensionMethods
{
    public static T[,] ToTwoDimensionalArray<T>(this IEnumerable<T> source, int arrayLength)
    {
        T[,] result = new T[arrayLength, arrayLength];
        T[][] chunks = source.Chunk(arrayLength).ToArray();

        foreach (T[] chunk in chunks)
        {
            int row = Array.IndexOf(chunks, chunk);

            for (int col = 0; col < chunk.Length; col++)
            {
                result[row, col] = chunk[col];
            }
        }

        return result;
    }

    public static T[,] Fill<T>(this T[,] source, T value)
    {
        T[,] result = new T[source.GetLength(0), source.GetLength(1)];

        for (int row = 0; row < source.GetLength(0); row++)
        {
            for (int col = 0; col < source.GetLength(1); col++)
            {
                result[row, col] = value;
            }
        }

        return result;
    }

    public static bool MatrixEquals(this int[,] left, int[,] right)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        if (left.GetLength(0) != right.GetLength(0))
        {
            return false;
        }

        if (left.GetLength(1) != right.GetLength(1))
        {
            return false;
        }

        for (int i = 0; i < left.GetLength(0); i++)
        {
            for (int j = 0; j < left.GetLength(1); j++)
            {
                if (left[i, j] != right[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static string ToMatrixString(this int[,] source)
    {
        string result = "";

        for (int row = 0; row < source.GetLength(0); row++)
        {
            for (int col = 0; col < source.GetLength(1); col++)
            {
                result += source[row, col];
            }
        }

        return result;
    }
}
