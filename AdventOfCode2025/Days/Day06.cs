using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode2025.Abstractions;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public class Day06 : Day
{
    // TODO: Set test values
    protected override long PartOneTestAnswer => 4277556;
    protected override long PartTwoTestAnswer => 3263827;

    protected override long SolvePart1(bool isTest = false)
    {
        var input = FileUtility.ReadLinesFromFile(Filename(isTest));
        var solution = 0L;
        
        var operations = ParseInput(input);

        foreach (var operation in operations)
        {
            switch (operation.Item1)
            {
                case '+':
                    var sum = operation.Item2.Sum();
                    solution += sum;
                    break;
                case '*':
                    var product = operation.Item2.Aggregate(1L, (current, value) => current * value);
                    solution += product;
                    break;
            }
        }

        return solution;
    }

    private List<(char, List<long>)> ParseInput(List<string> lines)
    {
        List<(char, List<long>)> operations = [];
        for (int row = lines.Count - 1; row >= 0; row--)
        {
            var regex = new Regex(@"[\w\+\*]+");
            var matches = regex.Matches(lines[row]);
            var column = 0;
            foreach (Match match in matches)
            {
                if (row == lines.Count - 1)
                {
                    operations.Add((match.Value[0], []));
                }
                else
                {
                    operations[column].Item2.Add(long.Parse(match.Value));
                }
                column++;
            }
        }
        
        
        return operations;
    }

    protected override long SolvePart2(bool isTest = false)
    {
        var input = FileUtility.ReadLinesFromFile(Filename(isTest));
        var solution = 0L;
        
        var operations = ParseInputTransformed(input);

        foreach (var operation in operations)
        {
            switch (operation.Item1)
            {
                case '+':
                    var sum = operation.Item2.Sum();
                    solution += sum;
                    break;
                case '*':
                    var product = operation.Item2.Aggregate(1L, (current, value) => current * value);
                    solution += product;
                    break;
            }
        }
        
        return solution;
    }
    
    private List<(char, List<long>)> ParseInputTransformed(List<string> lines)
    {
        var operations = new List<(char, List<long>)>();
        List<string> linesTransformed = [];
        StringBuilder sb = new StringBuilder();
        for (int col = 0; col < lines[0].Length; col++)
        {
            for (int row = lines.Count-1; row >= 0; row--)
            {
                sb.Append(lines[row][col]);
            }
            linesTransformed.Add(sb.ToString());
            sb.Clear();
        }
        
        Regex regex = new Regex(@"[\d]+");
        
        (char, List<long>) operation = (' ', []);
        foreach (var line in linesTransformed)
        {
            if (line.Trim().Length == 0)
            {
                operations.Add(operation);
                operation = (' ', []);
                continue;
            }
            if (line.Contains('+') || line.Contains('*'))
                operation.Item1 = line[0];
            var reversedLine = new string(line.Reverse().ToArray());
            operation.Item2.Add(long.Parse(regex.Match(reversedLine).Value));
        }
        operations.Add(operation);
        
        return operations;
    }
}
