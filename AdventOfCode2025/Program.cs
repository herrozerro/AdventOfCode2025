using System.Reflection;
using AdventOfCode2025.Abstractions;

// Find all days
var days = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.IsSubclassOf(typeof(Day)) && !t.IsAbstract)
    .Select(t => (Day)Activator.CreateInstance(t)!)
    .OrderBy(d => d.GetType().Name)
    .ToList();

// Logic: Check if user provided an argument, otherwise run latest
if (args.Length > 0)
{
    if (args[0].Equals("all", StringComparison.CurrentCultureIgnoreCase))
    {
        days.ForEach(d => d.RunDay());
    }
    else
    {
        // Try to match "Day05" or just "5"
        var target = days.FirstOrDefault(d => 
            d.GetType().Name.ToLower().EndsWith(args[0].PadLeft(2, '0')));
            
        target?.RunDay();
    }
}
else
{
    // Default: Run the last one added
    days.LastOrDefault()?.RunDay();
}

Console.ReadKey();