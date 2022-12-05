namespace AdventOfCode.Day1;

using System.IO;

/** <summary>
 * Solution for first day of Advent of Code 2022 Challenge. See https://adventofcode.com/2022/day/1.
 * </summary> 
 */
public class SolutionImpl : Solution
{
    private readonly string _inputPath = BasePath + @"Day1\input.txt";

    public override void SolvePartOne()
    {
        Console.WriteLine(_inputPath);
        long result = GetMaximumElfCalories();
        Console.Out.WriteLine("Solution of problem part 1: " + result);
    }

    public override void SolvePartTwo()
    {
        long result = 0;
        PriorityQueue<long, long> queue = ConstructQueueOfElfCalories();
        // sum up the first three queue values
        for (int i = 0; i < 3; i++)
        {
            result += queue.Dequeue();
        }
        
        Console.WriteLine("Solution of problem part 2: " + result);
    }

    private long GetMaximumElfCalories()
    {
        long currentMax = 0;
        long currentSum = 0;
        foreach (string line in File.ReadLines(_inputPath))
        {
            if (line.Length != 0)
            {
                long currentVal = long.Parse(line);
                currentSum += currentVal;
            }
            else
            {
                currentMax = (currentSum > currentMax) ? currentSum : currentMax;
                currentSum = 0;
            }
        }

        // Make sure that the maximum is still correct is the last value alone is the maximum
        if (currentSum > currentMax)
        {
            currentMax = currentSum;
        }

        return currentMax;
    }

    private PriorityQueue<long, long> ConstructQueueOfElfCalories()
    {
        PriorityQueue<long, long> queue = new PriorityQueue<long, long>();
        
        long currentSum = 0;
        foreach (string line in File.ReadLines(_inputPath))
        {
            if (line.Length != 0)
            {
                long currentVal = long.Parse(line);
                currentSum += currentVal;
            }
            else
            {
                // enqueue currentSum with its negative value as the priority to make sure the queue is sorted decreasing
                queue.Enqueue(currentSum, -1 * currentSum);
                currentSum = 0;
              
            }
        }

        // Account for the possibility that the last line alone holds a separate elf calorie value
        if (currentSum > 0)
        {
            queue.Enqueue(currentSum, -1 * currentSum);
        }

        return queue;
    }
}