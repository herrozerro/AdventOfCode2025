using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public static class Day01
{
    private static readonly string DayName = MethodBase.GetCurrentMethod().DeclaringType.Name;
    public static void RunDay()
    {
        Debug.Assert(SolvePart1(true) == 3);
        Debug.Assert(SolvePart2(true) == 6);
        
        Console.WriteLine($"Part 1: {SolvePart1()}");
        Console.WriteLine($"Part 2: {SolvePart2()}");
    }

    public static int SolvePart1(bool isTest = false)
    {
        var filename = $"Data/{DayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var position = 50;
        var timesAt0 = 0;
        foreach (var step in input)
        {
            int stepInt = int.Parse(step[1..]) * (step[0] == 'R' ? 1 : -1);
            position = TurnKnob(position, stepInt, 100);
            if (position == 0) timesAt0++;
        }
        
        
        return timesAt0;
    }

    private static int TurnKnob(int position, int steps, int max)
    {
        position += steps;
        
        while (position >= max) position -= max;
        while (position < 0) position = max - Math.Abs(position);
        
        return position;
    }

    public static int SolvePart2(bool isTest = false)
    {
        var filename = $"Data/{DayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var position = 50;
        var timesAt0 = 0;
        foreach (var step in input)
        {
            int stepInt = int.Parse(step[1..]) * (step[0] == 'R' ? 1 : -1);
            var result = TurnKnobWithZeros(position, stepInt, 100);
            position = result.Item1;
            timesAt0 += result.Item2;
        }
        
        
        return timesAt0;
    }
    
    private static (int, int) TurnKnobWithZeros(int position, int steps, int max)
    {
        bool startedOnZero = position == 0;
        bool endedOnZero = false;
        position += steps;
        var zeroesHit = position == 0 ? 1 : 0;
        while (position >= max)
        {
            position -= max;
            zeroesHit++;
        }

        while (position < 0)
        {
            position = max - Math.Abs(position);
            if (!startedOnZero)
            {
                zeroesHit++;
            }
            else
            {
                startedOnZero = false;
            }
            if (position == 0) endedOnZero = true;
        }
        
        if (endedOnZero) zeroesHit++;
        
        return (position, zeroesHit);
    }
}