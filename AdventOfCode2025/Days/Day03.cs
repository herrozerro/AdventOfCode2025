using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public static class Day03
{
    private static readonly string? DayName = MethodBase.GetCurrentMethod()?.DeclaringType?.Name;
    public static void RunDay()
    {
        Debug.Assert(SolvePart1(true) == 357);
        Debug.Assert(SolvePart2(true) == 3121910778619);
        
        Console.WriteLine($"Part 1: {SolvePart1()}");
        Console.WriteLine($"Part 2: {SolvePart2()}");
    }

    private static int SolvePart1(bool isTest = false)
    {
        var filename = $"Data/{DayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);

        return input.Sum(GetLargestNumber);
    }

    private static int GetLargestNumber(string input)
    {
        (int,int) largestDigits = (0, 0);
        var largestIndex = 0;
        var index = 0;
        //find the largest digit going forward
        foreach (var digitInt in input[..^1].Select(digit => int.Parse(digit.ToString())))
        {
            if (digitInt > largestDigits.Item1)
            {
                largestDigits = (digitInt, largestDigits.Item2);
                largestIndex = index;
            }
            index++;
        }
        //start after largest digit
        largestIndex++;
        //find second largest digit after largest digit
        foreach (var digitInt in input[largestIndex..].Select(digit => int.Parse(digit.ToString())))
        {
            if (digitInt > largestDigits.Item2)
                largestDigits = (largestDigits.Item1, digitInt);
        }

        return (largestDigits.Item1 * 10) + largestDigits.Item2;
    }
    

    private static long SolvePart2(bool isTest = false)
    {
        var filename = $"Data/{DayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0L;
        foreach (var line in input)
        {
            var result = FindLargestNumberOfSizeN(line, 12);
            solution += result;
        }

        return solution;
    }

    private static long FindLargestNumberOfSizeN(string input, int nSize)
    {
        var digits = new int[nSize];
        
        var largestIndex = 0;
        for (int i = 0; i < nSize; i++)
        {
            var index = largestIndex;
            //find the largest most significant digit with at least nSize digits remaining
            var range = input[largestIndex..^(nSize - (i+1))].Select(digit => int.Parse(digit.ToString())).ToList();
            foreach (var digitInt in range)
            {
                if (digitInt > digits[i])
                {
                    digits[i] = digitInt;
                    largestIndex = index;
                }
                index++;
            }
            largestIndex++;
        }

        return digits.Select((digit, index) => digit * (long)Math.Pow(10, nSize - index - 1)).Sum();
    } 
}