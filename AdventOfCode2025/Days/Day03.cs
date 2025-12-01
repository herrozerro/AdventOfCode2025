using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public static class Day03
{
    private static string dayName = MethodBase.GetCurrentMethod().DeclaringType.Name;
    public static void RunDay()
    {
        Debug.Assert(SolvePart1(true) == 161);
        Debug.Assert(SolvePart2(true) == 48);
        
        Console.WriteLine($"Part 1: {SolvePart1()}");
        Console.WriteLine($"Part 2: {SolvePart2()}");
    }

    private static int SolvePart1(bool isTest = false)
    {
        var filename = $"Data/{dayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0;
        
        var regex = new Regex(@"(mul\([0-9]{1,3},[0-9]{1,3}\))");
        
        foreach (var line in input)
        {
            var matches = regex.Matches(line);
            foreach (Match match in matches)
            {
                var values = match.Value.Replace("mul(", "").Replace(")", "").Split(",");
                var a = int.Parse(values[0]);
                var b = int.Parse(values[1]);
                solution += a * b;
            }
        }
        
        
        
        return solution;
    }

    private static int SolvePart2(bool isTest = false)
    {
        var filename = $"Data/{dayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0;
        
        var regex = new Regex(@"(mul\([0-9]{1,3},[0-9]{1,3}\)|don't|do)");
        var enabled = true;
        foreach (var line in input)
        {
            var matches = regex.Matches(line);
            foreach (Match match in matches)
            {
                if (match.Value == "don't")
                {
                    enabled = false;
                    continue;
                }
                else if (match.Value == "do")
                {
                    enabled = true;
                    continue;
                }
                if (!enabled)
                {
                    continue;
                }
                var values = match.Value.Replace("mul(", "").Replace(")", "").Split(",");
                var a = int.Parse(values[0]);
                var b = int.Parse(values[1]);
                solution += a * b;
            }
        }
        
        
        
        return solution;
    }
}