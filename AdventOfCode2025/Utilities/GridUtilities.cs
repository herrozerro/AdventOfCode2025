using System.Reflection.Metadata;

namespace AdventOfCode2025.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;

public static class GridExtensions
{
    // 1. DENSE GRID: Loads every single character
    public static Dictionary<(int x, int y), T> ToDictionaryGrid<T>(this string[] lines, Func<char, T> map)
    {
        var grid = new Dictionary<(int x, int y), T>();

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                grid[(x, y)] = map(lines[y][x]);
            }
        }

        return grid;
    }

    // 2. SPARSE GRID: Only loads characters that match a condition
    // Useful for ignoring '.' or empty space
    public static Dictionary<(int x, int y), T> ToSparseDictionaryGrid<T>(this string[] lines, Func<char, T> map, char ignoreChar = '.')
    {
        var grid = new Dictionary<(int x, int y), T>();

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                char c = lines[y][x];
                if (c != ignoreChar)
                {
                    grid[(x, y)] = map(c);
                }
            }
        }

        return grid;
    }
    
    // 3. BOUNDS HELPER: Quickly get the min/max of a dictionary grid
    private static (int minX, int maxX, int minY, int maxY) GetBounds(this Dictionary<(int x, int y), char> grid)
    {
        if (grid.Count == 0) return (0, 0, 0, 0);
        
        var keys = grid.Keys;
        return (keys.Min(k => k.x), keys.Max(k => k.x), keys.Min(k => k.y), keys.Max(k => k.y));
    }
    
    public static void PrintDictionaryGrid(Dictionary<(int x, int y), char> grid, char defaultChar = '.')
    {
        var bounds = grid.GetBounds(); // Uses the helper above

        for (int y = bounds.minY; y <= bounds.maxY; y++)
        {
            for (int x = bounds.minX; x <= bounds.maxX; x++)
            {
                if (grid.TryGetValue((x, y), out char c))
                    Console.Write(c);
                else
                    Console.Write(defaultChar);
            }
            Console.WriteLine();
        }
    }
    
    public static T[,] ToArrayGrid<T>(this string[] lines, Func<char, T> map)
    {
        var grid = new T[lines.Length,lines[0].Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                grid[y,x] = map(lines[y][x]);
            }
        }

        return grid;
    }
    
    public static T[,] ToArrayGrid<T>(this List<string> lines, Func<char, T> map)
    {
        return lines.ToArray().ToArrayGrid(map);
    }
    
    public static char[,] ReplaceGridValue(this char[,] grid, char oldValue, char newValue)
    {
        for (var y = 0; y < grid.GetLength(0); y++)
        {
            for (var x = 0; x < grid.GetLength(1); x++)
            {
                grid[y,x] = grid[y,x] == oldValue ? newValue : grid[y,x];
            }
        }

        return grid;
    }
  
    public static void PrintArrayGrid<T>(this T[,] grid)
    {
        // Dimension 0 is the number of Rows (Height / Y)
        var height = grid.GetLength(0);
    
        // Dimension 1 is the number of Columns (Width / X)
        var width = grid.GetLength(1);

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Console.Write(grid[y, x]);
            }
            Console.WriteLine();
        }
    }
}

