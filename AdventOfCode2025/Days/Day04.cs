using System.Diagnostics;
using AdventOfCode2025.Abstractions;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public class Day04 : Day
{
    protected override long PartOneTestAnswer => 13; 
    protected override long PartTwoTestAnswer => 43;
    
    protected override long SolvePart1(bool isTest = false)
    {
        var input = FileUtility.ReadLinesFromFile(Filename(isTest));
        var solution = 0;

        var grid = input.ToArrayGrid(c => c);

        solution = FindRollWithLessThanNNeighbors(grid, 3);
        return solution;
    }

    private int FindRollWithLessThanNNeighbors(char[,] grid, int neighborsAllowed)
    {
        var rollsWithFewerThanNNeighbors = 0;
        for (var y = 0; y < grid.GetLength(0); y++)
        {
            for (var x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x] == '.' || !NeighborCheck(grid, (x, y), neighborsAllowed)) continue;
                rollsWithFewerThanNNeighbors++;
                grid[y, x] = 'x';
            }
        }

        return rollsWithFewerThanNNeighbors;
    }

    private bool NeighborCheck(char[,] grid, (int, int) position, int neighborsAllowed)
    {
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);
        var neighborSum = 0;
        var neighborCheckMatrix = new List<(int, int)>
        {
            (-1, -1), (0, -1), (1, -1),
            (-1, 0), (1, 0),
            (-1, 1), (0, 1), (1, 1)
        };
        foreach (var point in neighborCheckMatrix)
        {
            var newPoint = (point.Item1 + position.Item1, point.Item2 + position.Item2);
            if (newPoint.Item1 < 0 || newPoint.Item2 < 0 || newPoint.Item1 >= rows || newPoint.Item2 >= cols)
            {
                continue;
            }

            neighborSum += grid[newPoint.Item2, newPoint.Item1] != '.' ? 1 : 0;
        }

        return neighborSum <= neighborsAllowed;
    }

    protected override long SolvePart2(bool isTest = false)
    {
        var filename = $"Data/{DayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0;

        var grid = input.ToArrayGrid(c => c);

        while (true)
        {
            var rollsWithFewerThanNNeighbors = FindRollWithLessThanNNeighbors(grid, 3);

            grid.ReplaceGridValue('x', '.');
            solution += rollsWithFewerThanNNeighbors;

            if (rollsWithFewerThanNNeighbors == 0) break;
        }

        return solution;
    }
}
    
