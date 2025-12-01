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
        Debug.Assert(SolvePart1(true) == 2);
        Debug.Assert(SolvePart2(true) == 4);
        
        Console.WriteLine($"Part 1: {SolvePart1()}");
        Console.WriteLine($"Part 2: {SolvePart2()}");
    }

    private static int SolvePart1(bool isTest = false)
    {
        var filename = $"Data/{dayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0;

        foreach (var report in input)
        {
            var invalid = false;
            var asc = false;
            var seq = report.Split(" ").Select(int.Parse).ToList();
            
            //find the direction of the first difference
            for (int i = 0; i < seq.Count(); i++)
            {
                if (seq[i] - seq[i + 1] != 0)
                {
                    asc = seq[0] > seq[1];
                    break;
                }
            }
            
            for (int i = 1; i < seq.Count(); i++)
            {
                var dif = Math.Abs(seq[i - 1] - seq[i]);
                if (seq[i - 1] == seq[i] || dif > 3)
                {
                    invalid = true;
                    break;
                }
                if (asc)
                {
                    if (seq[i - 1] < seq[i])
                    {
                        invalid = true;
                        break;
                    }
                }
                else
                {
                    if (seq[i - 1] > seq[i])
                    {
                        invalid = true;
                        break;
                    }
                }
            }
            
            solution += !invalid ? 1 : 0;
        }
        
        
        
        return solution;
    }

    private static int SolvePart2(bool isTest = false)
    {
        var filename = $"Data/{dayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0;

        foreach (var report in input)
        {
            var seq = report.Split(" ").Select(int.Parse).ToList();
            var solutionTuple = CheckWithProblemDampener(seq);
            
            for (int i = 0; i < seq.Count(); i++)
            {
                var newseq = seq.ToList();
                newseq.RemoveAt(i);
                solutionTuple = CheckWithProblemDampener(newseq);
                if (solutionTuple.Item1)
                {
                    break;
                }
            }

            if (solutionTuple.Item1)
            {
                solution++;
            }
            
            
        }
        
        
        
        return solution;
    }

    private static (bool, int) CheckWithProblemDampener(List<int> seq)
    {
        var invalid = false;
        var asc = false;
            
        //find the direction of the first difference
        for (int i = 0; i < seq.Count(); i++)
        {
            if (seq[i] - seq[i + 1] != 0)
            {
                asc = seq[0] > seq[1];
                break;
            }
        }

        var problemDampener = 0;
            
        for (int i = 1; i < seq.Count(); i++)
        {
            var dif = Math.Abs(seq[i - 1] - seq[i]);
            if (seq[i - 1] == seq[i] || dif > 3)
            {
                problemDampener = i;
                invalid = true;
                break;
            }
            if (asc)
            {
                if (seq[i - 1] < seq[i])
                {
                    problemDampener = i;
                    invalid = true;
                    break;
                }
            }
            else
            {
                if (seq[i - 1] > seq[i])
                {
                    problemDampener = i;
                    invalid = true;
                    break;
                }
            }
        }
        
        return (!invalid, problemDampener);
    }
}
