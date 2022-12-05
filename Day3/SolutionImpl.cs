using System.Diagnostics;

namespace AdventOfCode.Day3;

public class SolutionImpl : Solution
{
    private readonly string _inputPath = BasePath + @"Day1\input.txt";

    public override void SolvePartOne()
    {
        int result = 0;
        foreach (string line in File.ReadLines(_inputPath))
        {
            result += CalculatePrioritySumPerLine(line);
        }

        Console.Out.WriteLine("Result of day 3 part 1: " + result);
    }

    public override void SolvePartTwo()
    {
        string[] lines = File.ReadAllLines(_inputPath);
        Debug.Assert(lines.Length % 3 == 0);

        int result = 0;
        for (int i = 0; i < lines.Length; i += 3)
        {
            result += CalculatePriorityOfGroupBadge(lines[i..(i + 3)]);
        }
        Console.Out.WriteLine("Result of day 3 part 2: " + result);
    }

    private int CalculatePrioritySumPerLine(string line)
    {
        Debug.Assert(line.Length % 2 == 0);

        HashSet<char> uniqueItemsInFirstCompartment = new HashSet<char>(line[..(line.Length / 2)]);
        HashSet<char> uniqueItemsInSecondCompartment = new HashSet<char>(line[(line.Length / 2)..]);

        // get sum of priorities of letters contained in both compartments
        return uniqueItemsInFirstCompartment.Intersect(uniqueItemsInSecondCompartment)
            .Sum(CalculatePriorityOfLetter);
    }

    private int CalculatePriorityOfGroupBadge(string[] group)
    {
       return new HashSet<char>(group[0]).Intersect(new HashSet<char>(group[1]))
            .Intersect(new HashSet<char>(group[2])).Sum(CalculatePriorityOfLetter);
    }

    private int CalculatePriorityOfLetter(char letter)
    {
        Debug.Assert(char.IsLetter(letter));
        // priorities for A-Z are 27-52, for a-z are 1-26
        if (char.IsUpper(letter))
        {
            return letter - 'A' + 27;
        }

        return letter - 'a' + 1;
    }
}