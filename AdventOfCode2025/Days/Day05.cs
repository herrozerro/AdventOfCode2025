using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public static class Day05
{
    private static string dayName = MethodBase.GetCurrentMethod().DeclaringType.Name;
    public static void RunDay()
    {
        Debug.Assert(SolvePart1(true) == 0);
        Debug.Assert(SolvePart2(true) == 0);
        
        Console.WriteLine($"Part 1: {SolvePart1()}");
        Console.WriteLine($"Part 2: {SolvePart2()}");
    }

    private static int SolvePart1(bool isTest = false)
    {
        var filename = $"Data/{dayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0;
        
        
        
        
        
        return solution;
    }

    private static int SolvePart2(bool isTest = false)
    {
        var filename = $"Data/{dayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0;
        
        
        
        
        
        return solution;
    }
}