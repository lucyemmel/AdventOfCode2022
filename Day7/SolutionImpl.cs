using System.Diagnostics;
using AdventOfCode.Day7.Classes;
using Directory = AdventOfCode.Day7.Classes.Directory;

namespace AdventOfCode.Day7;

public class SolutionImpl : Solution
{
    private readonly string _inputPath = BasePath + @"Day7\input.txt";

    public override void SolvePartOne()
    {
        string[] lines = File.ReadAllLines(_inputPath);
        Directory rootDirectory = new Directory("/", null);
        ParseCommandLineOutputToDirectoryStructure(lines, rootDirectory);

        long result = 0;
        accumulateDirectoriesSum(100000, rootDirectory, ref result);
        Console.Out.WriteLine("Solution of day 7 part 1: " + result);
    }

    public override void SolvePartTwo()
    {
        string[] lines = File.ReadAllLines(_inputPath);
        Directory rootDirectory = new Directory("/", null);
        ParseCommandLineOutputToDirectoryStructure(lines, rootDirectory);

        long spaceToBeFreed = 30000000 - (70000000 - rootDirectory.GetTotalSize());
        long result = long.MaxValue;
        findSmallestDirectorySizeThatFreesSufficientSpace(spaceToBeFreed, rootDirectory, ref result);
        Console.Out.WriteLine("Solution of day 7 part 2: " + result);
    }

    private void ParseCommandLineOutputToDirectoryStructure(string[] commandLines, Directory rootDirectory)
    {
        Directory currentDirectory = rootDirectory;
        for (int i = 1; i < commandLines.Length; i++)
        {
            // next must always be a command
            Debug.Assert(commandLines[i].StartsWith("$"));
            string command = commandLines[i][2..];
            int processedLines = executeNextCommand(command, commandLines, ref currentDirectory, i);
            // advance loop counter
            i += processedLines;
        }
    }

    private int executeNextCommand(string command, string[] commandLines, ref Directory currentDirectory,
        int indexOfCommand)
    {
        if (command == "ls")
        {
            return createDirectoriesAndFiles(commandLines, indexOfCommand + 1, currentDirectory);
        }

        string directoryToGoTo = command.Split(' ')[1];
        if (directoryToGoTo == "..")
        {
            currentDirectory = currentDirectory.Parent;
        }
        else
        {
            currentDirectory = currentDirectory.FindByName(directoryToGoTo);
        }

        return 0;
    }

    private int createDirectoriesAndFiles(string[] commandLines, int indexOfCommand,
        Directory currentDirectory)
    {
        int currentIndex = indexOfCommand;
        int processedLines = 0;
        while (currentIndex < commandLines.Length && !commandLines[currentIndex].StartsWith("$"))
        {
            string currentLine = commandLines[currentIndex];
            if (currentLine.StartsWith("dir"))
            {
                currentDirectory.addChildDirectory(new Directory(currentLine.Split(' ')[1], currentDirectory));
            }
            else
            {
                long fileSize = long.Parse(currentLine.Split(' ')[0]);
                string fileName = currentLine.Split(' ')[1];
                currentDirectory.addFile(new CustomFile(fileSize, fileName));
            }

            currentIndex++;
            processedLines++;
        }

        return processedLines;
    }

    private void accumulateDirectoriesSum(long maximumValue, Directory rootDirectory, ref long result)
    {
        long dirSize = rootDirectory.GetTotalSize();
        if (dirSize <= maximumValue)
        {
            result += dirSize;
        }

        foreach (Directory childDirectory in rootDirectory.ChildrenDirs)
        {
            accumulateDirectoriesSum(maximumValue, childDirectory, ref result);
        }
    }

    private void findSmallestDirectorySizeThatFreesSufficientSpace(long spaceToBeFreed, Directory rootDirectory,
        ref long result)
    {
        long dirSize = rootDirectory.GetTotalSize();
        if (dirSize >= spaceToBeFreed && dirSize < result)
        {
            result = dirSize;
        }

        foreach (Directory childDirectory in rootDirectory.ChildrenDirs)
        {
            findSmallestDirectorySizeThatFreesSufficientSpace(spaceToBeFreed, childDirectory, ref result);
        }
    }
}