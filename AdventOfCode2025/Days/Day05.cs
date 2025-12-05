using AdventOfCode2025.Abstractions;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public class Day05 : Day
{
    protected override long PartOneTestAnswer => 3; 
    protected override long PartTwoTestAnswer => 14;
    
    protected override long SolvePart1(bool isTest = false)
    {
        var filename = $"Data/{DayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0;

        Dictionary<(long,long), int> ranges = [];
        List<long> ingredients = [];
        foreach (var line in input.Where(line => line.Contains('-')))
        {
            var splits = line.Split('-');
            var range = (long.Parse(splits[0]), long.Parse(splits[1]));
            if (ranges.TryAdd(range, 1)) continue;
            
            ranges[range]++;
        }

        foreach (var line in input.Where(line => !line.Contains('-') && line.Length > 0))
        {
            ingredients.Add(long.Parse(line));
        }
        
        solution = ingredients.Count(i => ranges.Any(r => i >= r.Key.Item1 && i <= r.Key.Item2));
        return solution;
    }
    

    protected override long SolvePart2(bool isTest = false)
    {
        var filename = $"Data/{DayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0L;
        
        var ranges = new List<(long Start, long End)>();
        var mergedRanges = new List<(long Start, long End)>();
        
        foreach (var line in input.Where(line => line.Contains('-')))
        {
            var splits = line.Split('-');
            var range = (long.Parse(splits[0]), long.Parse(splits[1]));
            ranges.Add(range);
        }
        ranges.Sort((a,b) => a.Start.CompareTo(b.Start));
        
        var currentRange = ranges[0];
        
        for (int i = 1; i < ranges.Count; i++)
        {
            var nextRange = ranges[i];
            if (currentRange.End + 1 >= nextRange.Start)
            {
                currentRange = (currentRange.Start, Math.Max(currentRange.End, nextRange.End));
            }
            else
            {
                mergedRanges.Add(currentRange);
                currentRange = nextRange;
            }
        }
        mergedRanges.Add(currentRange);

        solution = mergedRanges.Sum(x => x.End - x.Start + 1);


        return solution;
    }
}