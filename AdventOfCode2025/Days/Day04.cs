using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using AdventOfCode2025.Utilities;

namespace AdventOfCode2025.Days;

public static class Day04
{
    private static string dayName = MethodBase.GetCurrentMethod().DeclaringType.Name;
    public static void RunDay()
    {
        Debug.Assert(SolvePart1(true) == 18);
        Debug.Assert(SolvePart2(true) == 9);
        
        Console.WriteLine($"Part 1: {SolvePart1()}");
        Console.WriteLine($"Part 2: {SolvePart2()}");
    }

    private static int SolvePart1(bool isTest = false)
    {
        var filename = $"Data/{dayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0;
        
        
        
        char[,] grid = new char[input.Count,input.First().Length];
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input.First().Length; j++)
            {
                grid[i,j] = input[i][j];
            }
        }


        solution = FindStraightPatterns("XMAS", grid);
        
        
        
        return solution;
    }

    private static int SolvePart2(bool isTest = false)
    {
        var filename = $"Data/{dayName}{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var solution = 0;
        
        char[,] grid = new char[input.Count,input.First().Length];
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input.First().Length; j++)
            {
                grid[i,j] = input[i][j];
            }
        }
        
        solution = FindXMasPatterns(grid);
        
        
        
        return solution;
    }
    
    private static int FindStraightPatterns(string pattern, char[,] grid)
    {
        var finds = 0;
        
        var north = new List<(int, int)>
        {
            (1,0),
            (2,0),
            (3,0),
        };
        var east = new List<(int, int)>
        {
            (0,1),
            (0,2),
            (0,3),
        };
        var south = new List<(int, int)>
        {
            (-1,0),
            (-2,0),
            (-3,0),
        };
        var west = new List<(int, int)>
        {
            (0,-1),
            (0,-2),
            (0,-3),
        };
        var northwest = new List<(int, int)>
        {
            (1,-1),
            (2,-2),
            (3,-3),
        };
        var northeast = new List<(int, int)>
        {
            (1,1),
            (2,2),
            (3,3),
        };
        var southwest = new List<(int, int)>
        {
            (-1,-1),
            (-2,-2),
            (-3,-3),
        };
        var southeast = new List<(int, int)>
        {
            (-1,1),
            (-2,2),
            (-3,3),
        };
        var directions = new List<List<(int, int)>>
        {
            north, east, south, west, northwest, northeast, southwest, southeast
        };

        for (int c = 0; c < grid.GetLength(0); c++)
        {
            for (int r = 0; r < grid.GetLength(1); r++)
            {
                if (grid[c,r] == pattern[0])
                {
                    foreach (var direction in directions)
                    {
                        var found = true;
                        for (int i = 1; i < pattern.Length; i++)
                        {
                            var newcol = c + direction[i-1].Item1;
                            var newrow = r + direction[i-1].Item2;
                            if (newrow < 0 || newrow >= grid.GetLength(1) || newcol < 0 || newcol >= grid.GetLength(0) ||
                                grid[newcol,newrow] != pattern[i])
                            {
                                found = false;
                                break;
                            }
                        }

                        if (found)
                        {
                            finds += 1;
                        }
                    }
                }
            }
        }


        return finds;
    }

    private static int FindXMasPatterns(char[,] grid)
    {
        var finds = 0;

        var orientations = new List<char[,]>()
        {
            new char[,]
            {
                { 'M', '0', 'S' },
                { '0', '0', '0' },
                { 'M', '0', 'S' },
            },
            new char[,]
            {
                { 'M', '0', 'M' },
                { '0', '0', '0' },
                { 'S', '0', 'S' },
            },
            new char[,]
            {
                { 'S', '0', 'M' },
                { '0', '0', '0' },
                { 'S', '0', 'M' },
            },
            new char[,]
            {
                { 'S', '0', 'S' },
                { '0', '0', '0' },
                { 'M', '0', 'M' },
            }
        };
        var corners = new List<(int,int)>
        {
            (1,1),
            (1,-1),
            (-1,-1),
            (-1,1),
        };
        
        

        for (int c = 1; c < grid.GetLength(0)-1; c++)
        {
            for (int r = 1; r < grid.GetLength(1)-1; r++)
            {
                if (grid[c,r] == 'A')
                {
                    var testgrid = new char[,]
                    {
                        { 'A', '0', 'A' },
                        { '0', '0', '0' },
                        { 'A', '0', 'A' },
                    };

                    foreach (var corner in corners)
                    {
                        testgrid[1+corner.Item1,1+corner.Item2] = grid[c+corner.Item1,r+corner.Item2];
                    }

                    foreach (var orientation in orientations)
                    {
                        var equal =
                            testgrid.Rank == orientation.Rank &&
                            Enumerable.Range(0,testgrid.Rank).All(dimension => testgrid.GetLength(dimension) == orientation.GetLength(dimension)) &&
                            testgrid.Cast<char>().SequenceEqual(orientation.Cast<char>());
                        
                        if (equal)
                        {
                            finds++;
                            break;
                        }
                    }
                }
            }
        }
        
        return finds;
    }
    
    
    
    private static int FindPatterns(string pattern, char[,] grid)
    {
        var finds = 0;
        
        Dictionary<string, int> pointlist = new Dictionary<string,int>();
        

        for (int c = 0; c < grid.GetLength(0); c++)
        {
            for (int r = 0; r < grid.GetLength(1); r++)
            {
                if (grid[c,r] == pattern[0])
                {
                    var patterns = FindNextTargetCharacters(grid, r,c, pattern, 0);
                    finds += patterns.Count(x => x == pattern);
                }
            }
        }
        
        

        return finds;
    }
    
    private static List<string> FindNextTargetCharacters(char[,] grid, int row, int col, string pattern, int patternpos)
    {
        if (patternpos == pattern.Length-1)
        {
            return [pattern.Last().ToString()];
        }
        var currentPattern = pattern[patternpos+1].ToString();
        var finds = new List<string>();
        foreach (var newcol in Enumerable.Range(col - 1, 3))
        {
            foreach (var newrow in Enumerable.Range(row - 1, 3))
            {
                if (newrow >= 0 && newrow < grid.GetLength(1) && newcol >= 0 && newcol < grid.GetLength(0) &&
                    grid[newcol,newrow] == currentPattern[0])
                {
                    finds = FindNextTargetCharacters(grid, newrow, newcol, pattern, patternpos+1);
                }
            }
        }
    
        return finds.Select(x=>currentPattern+x).ToList();
    }
}
