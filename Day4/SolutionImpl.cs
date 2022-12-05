namespace AdventOfCode.Day4;

public class SolutionImpl : Solution
{
    private readonly string _inputPath = BasePath + @"Day1\input.txt";

    public override void SolvePartOne()
    {
        int result = File.ReadLines(_inputPath).Sum(line => LineContainsSubRange(line) ? 1 : 0);
        Console.Out.WriteLine("Result of day 4 part 1: " + result);
    }

    public override void SolvePartTwo()
    {
        int result = File.ReadLines(_inputPath).Sum(line => LineContainsOverlappingRanges(line) ? 1 : 0);
        Console.Out.WriteLine("Result of day 4 part 2: " + result);
    }

    private bool LineContainsSubRange(string line)
    {
        string[] ranges = line.Split(',');
        HashSet<int> firstRange = GetRangeFromString(ranges[0]);
        HashSet<int> secondRange = GetRangeFromString(ranges[1]);
        int greatestRange = Enumerable.Max(new[] { firstRange.Count, secondRange.Count });
        // if set union does not make the bigger range longer, the smaller range must be a subset of the greater one
        return firstRange.Union(secondRange).Count() == greatestRange;
    }

    private bool LineContainsOverlappingRanges(string line)
    {
        string[] ranges = line.Split(',');
        HashSet<int> firstRange = GetRangeFromString(ranges[0]);
        HashSet<int> secondRange = GetRangeFromString(ranges[1]);
        return firstRange.Intersect(secondRange).Any();
    }

    private HashSet<int> GetRangeFromString(string rangeString)
    {
        int from, to, indexOfDash;
        indexOfDash = rangeString.IndexOf('-');
        from = int.Parse(rangeString[..indexOfDash]);
        to = int.Parse(rangeString[(indexOfDash + 1)..]);
        return Enumerable.Range(from, to - from + 1).ToHashSet();
    }
}