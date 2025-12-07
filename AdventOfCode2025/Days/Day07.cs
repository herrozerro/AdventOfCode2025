using AdventOfCode2025.Abstractions;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public class Day07 : Day
{
    protected override long PartOneTestAnswer => 21;
    protected override long PartTwoTestAnswer => 40;

    protected override long SolvePart1(bool isTest = false)
    {
        var input = FileUtility.ReadLinesFromFile(Filename(isTest));
        var solution = 0L;
        
        var grid = input.ToArrayGrid(c => c);

        solution = ProcessGrid(grid).Item1;
        
        return solution;
    }

    private (long, char[,]) ProcessGrid(char[,] grid)
    {
        var beamsCreated = 0L;
        var beamSources = new Stack<(int col, int row)>(); 
        beamSources.Push(((grid.GetLength(1) / 2), 0));
        
        (int col, int row) gridMaxBounds = (grid.GetLength(1)-1, grid.GetLength(0)-1);
        
        while (beamSources.Count > 0)
        {
            var beamSource = beamSources.Pop();
            //check if lower bounds are reached
            if (beamSource.row + 1 > gridMaxBounds.row) continue;
            
            
            var newRow = beamSource.row + 1;
            
            switch (grid[newRow, beamSource.col])
            {
                case '.':
                    grid[newRow, beamSource.col] = '|';
                    beamSources.Push((beamSource.col, beamSource.row + 1));
                    //beamsCreated++;
                    break;
                case '^':
                    beamsCreated++;
                    if (beamSource.col > 0 && grid[beamSource.row, beamSource.col - 1] == '.')
                    {
                        beamSources.Push((beamSource.col - 1,beamSource.row));
                        //beamsCreated++;
                    }
                    if (beamSource.col < gridMaxBounds.col && grid[beamSource.row, beamSource.col+1] == '.')
                    {
                        beamSources.Push((beamSource.col + 1,beamSource.row));
                        //beamsCreated++;
                    }
                    break;
                case '|':
                    break;
            }
            //grid.PrintArrayGrid();
        }
        
        return (beamsCreated, grid);
    }

    protected override long SolvePart2(bool isTest = false)
    {
        var input = FileUtility.ReadLinesFromFile(Filename(isTest));
        var solution = 0L;
        
        var grid = input.ToArrayGrid(c => c);

        solution = ProcessGridPt2(grid);
        
        return solution;
    }
    
    private long ProcessGridPt2(char[,] grid)
    {
        grid = ProcessGrid(grid).Item2;
        //grid.PrintArrayGrid();
        return TraverseGrid(grid, ((grid.GetLength(1) / 2), 0));
    }
    
    private readonly Dictionary<(int, int), long> _memo = new(); // Defined at class level

    private long TraverseGrid(char[,] grid, (int col, int row) startingPoint)
    {
        if (_memo.TryGetValue(startingPoint, out var traverseGrid)) return traverseGrid;

        for (var i = startingPoint.row; i < grid.GetLength(0); i++)
        {
            if (grid[i, startingPoint.col] != '^') continue;
            var leftPath = TraverseGrid(grid, (startingPoint.col-1, i));
            var rightPath= TraverseGrid(grid, (startingPoint.col+1, i));
            var total = leftPath + rightPath;
            
            _memo[startingPoint] = total;
            return total;
        }

        _memo[startingPoint] = 1;
        return 1;
    }
}
