using System.Diagnostics;

namespace AdventOfCode2025.Abstractions;

public abstract class Day
{
    protected string DayName => GetType().Name;
    protected string Filename(bool isTest) => $"Data/{DayName}{(isTest ? ".Test" : "")}.txt";

    protected virtual long PartOneTestAnswer => 0;
    protected virtual long PartTwoTestAnswer => 0;
    
    public void RunDay()
    {
        Console.WriteLine($"Running day {DayName}");
        if (PartOneTestAnswer != 0)
        {
            long testResult = SolvePart1(true);
            Debug.Assert(testResult == PartOneTestAnswer, $"Part 1 Test Failed: Expected {PartOneTestAnswer}, Got {testResult}");
            Console.WriteLine("Part 1 Test Passed");
        }

        if (PartTwoTestAnswer != 0)
        {
            long testResult = SolvePart2(true);
            Debug.Assert(testResult == PartTwoTestAnswer, $"Part 2 Test Failed: Expected {PartTwoTestAnswer}, Got {testResult}");
            Console.WriteLine("Part 2 Test Passed");
        }
        
        Console.WriteLine($"Part 1: {SolvePart1()}");
        Console.WriteLine($"Part 2: {SolvePart2()}");
    }
    
    protected abstract long SolvePart1(bool isTest = false);
    protected abstract long SolvePart2(bool isTest = false);
}