namespace AdventOfCode2025.Utilities;

public static class MathUtilities
{
    public static IEnumerable<long> CreateLongRange(long start, long count)
    {
        long limit = start + count;
        for (long i = start; i < limit; i++)
        {
            yield return i;
        }
    }
}