using System.Diagnostics;
using AdventOfCode2025.Abstractions;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public class Day03 : Day
{
    protected override long PartOneTestAnswer => 357; 
    protected override long PartTwoTestAnswer => 3121910778619;
    
    protected override long SolvePart1(bool isTest = false)
    {
        var input = FileUtility.ReadLinesFromFile(Filename(isTest));

        return input.Sum(n=>FindLargestNumberOfSizeN(n, 2));
    }

    private int GetLargestNumber(string input)
    {
        var largestDigits = (0, 0);
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
    

    protected override long SolvePart2(bool isTest = false)
    {
        var input = FileUtility.ReadLinesFromFile(Filename(isTest));
        var solution = 0L;
        foreach (var line in input)
        {
            var result = FindLargestNumberOfSizeN(line, 12);
            solution += result;
        }

        return solution;
    }

    private long FindLargestNumberOfSizeN(string input, int nSize)
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