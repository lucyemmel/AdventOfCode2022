using System.Text.RegularExpressions;

namespace AdventOfCode.Day5;

public class SolutionImpl : Solution
{
    private readonly string _inputPath = BasePath + "Day5\\input.txt";

    public override void SolvePartOne()
    {
        string[] lines = File.ReadAllLines(_inputPath);
        Stack<char>[] crateStacks = ParseToStacks(lines);
        string result = FindTopCratesAfterMoving(crateStacks, lines);
        Console.Out.WriteLine("Result of day 5 part 1: " + result);
    }

    public override void SolvePartTwo()
    {
        string[] lines = File.ReadAllLines(_inputPath);
        Stack<char>[] crateStacks = ParseToStacks(lines);
        string result = FindTopCratesAfterMoving(crateStacks, lines, false);
        Console.Out.WriteLine("Result of day 5 part 2: " + result);
    }

    private Stack<char>[] ParseToStacks(string[] lines)
    {
        int stacksStartLine = FindStacksStartLine(lines);
        // stacks indexing line is directly below the first stack line
        Stack<char>[] stacks = new Stack<char>[FindNumberOfStacks(lines[stacksStartLine + 1])];

        for (int i = 0; i < stacks.Length; i++)
        {
            stacks[i] = new Stack<char>();
        }

        for (int i = stacksStartLine; i >= 0; i--)
        {
            FillStacksForLine(lines[i], stacks);
        }

        return stacks;
    }

    private string FindTopCratesAfterMoving(Stack<char>[] crateStacks, string[] lines, bool solvingPartOne = true)
    {
        // skip stack line, indexing line and empty line
        int directionStartingLine = FindStacksStartLine(lines) + 3;
        for (int i = directionStartingLine; i < lines.Length; i++)
        {
            ParseAndExecuteInstruction(crateStacks, lines[i], solvingPartOne);
        }

        string result = "";
        foreach (Stack<char> stack in crateStacks)
        {
            result += stack.Peek();
        }

        return result;
    }

    private int FindStacksStartLine(string[] lines)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Length == 0)
            {
                // skip the empty line and the indexing line
                return i - 2;
            }
        }

        throw new Exception("Invalid input");
    }

    private void FillStacksForLine(string line, Stack<char>[] stacks)
    {
        for (int i = 1, j = 0; i < line.Length; i += 4, j++)
        {
            char symbol = line[i];
            if (char.IsLetter(symbol))
            {
                stacks[j].Push(symbol);
            }
        }
    }

    private int FindNumberOfStacks(string stacksIndexingLine)
    {
        return int.Parse((new Regex(@"\d\s$")).Match(stacksIndexingLine).Value);
    }

    private void ParseAndExecuteInstruction(Stack<char>[] stacks, string instruction, bool solvingPartOne = true)
    {
        Regex digitRegex = new Regex(@"\d+");
        MatchCollection numbersInLine = digitRegex.Matches(instruction);
        int numberOfElementsToMove = int.Parse(numbersInLine[0].Value);
        Stack<char> stackToMoveFrom = stacks[int.Parse(numbersInLine[1].Value) - 1];
        Stack<char> stackToMoveTo = stacks[int.Parse(numbersInLine[2].Value) - 1];

        if (solvingPartOne)
            ExecuteInstructionIgnoreElementOrder(stackToMoveFrom, stackToMoveTo, numberOfElementsToMove);
        else
            ExecuteInstructionKeepElementOrder(stackToMoveFrom, stackToMoveTo, numberOfElementsToMove);
    }

    private void ExecuteInstructionIgnoreElementOrder(Stack<char> stackToMoveFrom, Stack<char> stackToMoveTo,
        int numberOfElementsToMove)
    {
        for (int i = 0; i < numberOfElementsToMove; i++)
        {
            stackToMoveTo.Push(stackToMoveFrom.Pop());
        }
    }

    private void ExecuteInstructionKeepElementOrder(Stack<char> stackToMoveFrom, Stack<char> stackToMoveTo,
        int numberOfElementsToMove)
    {
        Stack<char> elementsToMove = new Stack<char>();
        for (int i = 0; i < numberOfElementsToMove; i++)
        {
            elementsToMove.Push(stackToMoveFrom.Pop());
        }

        for (int i = 0; i < numberOfElementsToMove; i++)
        {
            stackToMoveTo.Push(elementsToMove.Pop());
        }
       
    }
}