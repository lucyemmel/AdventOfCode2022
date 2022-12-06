using System.Diagnostics;

namespace AdventOfCode.Day6;

public class SolutionImpl : Solution
{
    private readonly string _inputPath = BasePath + @"Day6\input.txt";
    public override void SolvePartOne()
    {
        processSequence(File.ReadAllLines(_inputPath)[0]);
    }

    public override void SolvePartTwo()
    {
        processSequence(File.ReadAllLines(_inputPath)[0], false);
    }

    private void processSequence(string sequence, bool solvingPartOne = true)
    {
        HashSet<char> currentSequence = new HashSet<char>();
        int numberOfProcessedCharacters = 0;
        int startingIndexOfCurrentSequence = 0;

        for (int i = 0; i < sequence.Length; i++)
        {
            numberOfProcessedCharacters++;
            
            int setSizeBeforeAdd = currentSequence.Count;
            currentSequence.Add(sequence[i]);
            int setSizeAfterAdd = currentSequence.Count;
            
            if (setSizeAfterAdd == setSizeBeforeAdd)
            {
                // restart sequence from next starting index
                currentSequence.Clear();
                startingIndexOfCurrentSequence++;
                // the loop incrementation increases i by 1 afterwards
                i = startingIndexOfCurrentSequence - 1;
                numberOfProcessedCharacters = startingIndexOfCurrentSequence;
            }
            else if (setSizeAfterAdd == (solvingPartOne ? 4 : 14))
            {
                // solution found
                Console.Out.WriteLine($"Solution of day 6 part {(solvingPartOne ? 1 : 2)}: {numberOfProcessedCharacters}");
                return;
            }
        }
    }
}