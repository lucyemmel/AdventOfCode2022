namespace AdventOfCode.Day8;

public class SolutionImpl : Solution
{
    private readonly string _inputPath = BasePath + @"Day8\input.txt";

    public override void SolvePartOne()
    {
        string[] lines = File.ReadAllLines(_inputPath);
        int[][] treeMatrix = new int[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            treeMatrix[i] = parseLineToTreeHeightArray(lines[i]);
        }

        int numberOfVisibleTrees = 0;
        for (int y = 0; y < treeMatrix.Length; y++)
        {
            for (int x = 0; x < treeMatrix[0].Length; x++)
            {
                if (IsTreeAtCoordinateVisible(x, y, treeMatrix))
                {
                    numberOfVisibleTrees++;
                }
            }
        }

        Console.Out.WriteLine("Solution of day 8 part 1: " + numberOfVisibleTrees);
    }

    public override void SolvePartTwo()
    {
        string[] lines = File.ReadAllLines(_inputPath);
        int[][] treeMatrix = new int[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            treeMatrix[i] = parseLineToTreeHeightArray(lines[i]);
        }

        int highestScenicTreeScore = 0;
        // trees on the edges always have score 0 so they are not relevant
        for (int y = 1; y < treeMatrix.Length - 1; y++)
        {
            for (int x = 1; x < treeMatrix[0].Length - 1; x++)
            {
                int scenicScoreAtCoordinate = CalculateScenicScoreAtCoordinate(x, y, treeMatrix);
                if (scenicScoreAtCoordinate > highestScenicTreeScore)
                {
                    highestScenicTreeScore = scenicScoreAtCoordinate;
                }
            }
        }

        Console.Out.WriteLine("Solution of day 8 part 2: " + highestScenicTreeScore);
    }

    private int[] parseLineToTreeHeightArray(string line)
    {
        int[] treeHeightArray = new int[line.Length];
        for (int i = 0; i < treeHeightArray.Length; i++)
        {
            treeHeightArray[i] = int.Parse(line[i].ToString());
        }

        return treeHeightArray;
    }

    private bool IsTreeAtCoordinateVisible(int x, int y, int[][] treeMatrix)
    {
        // trees at the edges are always visible
        if (x == 0 || y == 0)
            return true;

        int currentValue = treeMatrix[y][x];
        // check trees above
        bool allAboveAreSmaller = true;
        for (int i = y - 1; i >= 0; i--)
        {
            if (treeMatrix[i][x] >= currentValue)
            {
                allAboveAreSmaller = false;
            }
        }

        if (allAboveAreSmaller)
            return true;

        // check trees below
        bool allBelowAreSmaller = true;
        for (int i = y + 1; i < treeMatrix.Length; i++)
        {
            if (treeMatrix[i][x] >= currentValue)
            {
                allBelowAreSmaller = false;
            }
        }

        if (allBelowAreSmaller)
            return true;

        // check trees left
        bool allLeftAreSmaller = true;
        for (int i = x - 1; i >= 0; i--)
        {
            if (treeMatrix[y][i] >= currentValue)
            {
                allLeftAreSmaller = false;
            }
        }

        if (allLeftAreSmaller)
            return true;

        // check trees right
        bool allRightAreSmaller = true;
        for (int i = x + 1; i < treeMatrix[y].Length; i++)
        {
            if (treeMatrix[y][i] >= currentValue)
            {
                allRightAreSmaller = false;
            }
        }

        return allRightAreSmaller;
    }

    private int CalculateScenicScoreAtCoordinate(int x, int y, int[][] treeMatrix)
    {
        int currentValue = treeMatrix[y][x];
        int totalScenicScore = 1;
        // check trees above
        int currentScenicScore = 0;
        for (int i = y - 1; i >= 0; i--)
        {
            currentScenicScore++;
            if (treeMatrix[i][x] >= currentValue)
            {
                break;
            }
        }

        totalScenicScore *= currentScenicScore;

        // check trees below
        currentScenicScore = 0;
        for (int i = y + 1; i < treeMatrix.Length; i++)
        {
            currentScenicScore++;
            if (treeMatrix[i][x] >= currentValue)
            {
                break;
            }
        }

        totalScenicScore *= currentScenicScore;

        // check trees left
        currentScenicScore = 0;
        for (int i = x - 1; i >= 0; i--)
        {
            currentScenicScore++;
            if (treeMatrix[y][i] >= currentValue)
            {
                break;
            }
        }

        totalScenicScore *= currentScenicScore;

        // check trees right
        currentScenicScore = 0;
        for (int i = x + 1; i < treeMatrix[y].Length; i++)
        {
            currentScenicScore++;
            if (treeMatrix[y][i] >= currentValue)
            {
                break;
            }
        }

        totalScenicScore *= currentScenicScore;
        
        return totalScenicScore;
    }
}