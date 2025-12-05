using System.Diagnostics;
using AdventOfCode2025.Abstractions;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public class Day01 : Day
{
    protected override long PartOneTestAnswer => 3; 
    protected override long PartTwoTestAnswer => 6;
    
    protected override long SolvePart1(bool isTest = false)
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

    private int TurnKnob(int position, int steps, int max)
    {
        position += steps;
        
        while (position >= max) position -= max;
        while (position < 0) position = max - Math.Abs(position);
        
        return position;
    }

    protected override long SolvePart2(bool isTest = false)
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
    
    private (int, int) TurnKnobWithZeros(int position, int steps, int max)
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