using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public static class Day02
{
    private static string dayName = MethodBase.GetCurrentMethod().DeclaringType.Name;
    public static void RunDay()
    {
        Debug.Assert(SolvePart1(true) == 1227775554);
        Debug.Assert(SolvePart2(true) == 4174379265);
        
        Console.WriteLine($"Part 1: {SolvePart1()}");
        Console.WriteLine($"Part 2: {SolvePart2()}");
    }

    private static long SolvePart1(bool isTest = false)
    {
        var filename = $"Data/{dayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadDelimitedStringsFromFile(filename,',');
        var solution = new List<long>();

        foreach (var ranges in input.Select(range => range.Split('-').Select(long.Parse).ToList()))
        {
            solution.AddRange(FindDoubledPatternsInRange(ranges[0], ranges[1]));
        }
        
        return solution.Sum();
    }

    private static long SolvePart2(bool isTest = false)
    {
        var filename = $"Data/{dayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadDelimitedStringsFromFile(filename,',');
        var solution = new List<long>();

        foreach (var ranges in input.Select(range => range.Split('-').Select(long.Parse).ToList()))
        {
            solution.AddRange(FindAnyDoubledPatternsInRange(ranges[0], ranges[1]));
        }
        
        return solution.Sum();
    }

    private static List<long> FindDoubledPatternsInRange(long start, long end)
    {
        // if start is bigger than end, flip values
        if (start > end) 
            (start, end) = (end, start);
        var range = CreateLongRange(start, end - start + 1);
        var doubledNumbers = range.Where(x =>
            x.ToString().Length % 2 == 0
            && (x.ToString()[..(x.ToString().Length / 2)] == x.ToString()[(x.ToString().Length / 2) ..])).ToList();

        return doubledNumbers;

    }

    private static List<long> FindAnyDoubledPatternsInRange(long start, long end)
    {
        var doubledNumbers = new List<long>();
        if (start > end) 
            (start, end) = (end, start);
        var range = CreateLongRange(start, end - start + 1).Select(x=>x.ToString()).ToList();

        var numLength = range.First().Length;
        var denominators = FindDenominators(numLength);
        foreach (var num in range)
        {
            if (num.Length != numLength)
                denominators = FindDenominators(num.Length);

            foreach (var denominator in denominators)
            {
                var slices = Slices(num, denominator);
                //are all slices equal?
                if (!slices.All(x => x == slices[0])) continue;
                doubledNumbers.Add(long.Parse(num));
                break;
            }
        }
        
        return doubledNumbers;
    }

    private static List<int> FindDenominators(int numerator)
    {
        return Enumerable.Range(1, numerator).Where(divisor => numerator % divisor == 0 && divisor < numerator).ToList();
    }

    private static List<string> Slices(string str, int length)
    {
        //return slices like 1010 would return 10, 10
        return Enumerable.Range(0, str.Length / length).Select(i => str.Substring(i*length, length)).ToList();
    }
    
    
    public static IEnumerable<long> CreateLongRange(long start, long count)
    {
        long limit = start + count;
        for (long i = start; i < limit; i++)
        {
            yield return i;
        }
    }
}
