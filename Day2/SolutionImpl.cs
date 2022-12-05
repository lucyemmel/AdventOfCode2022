using System.Diagnostics;

namespace AdventOfCode.Day2;

/** <summary>
 * Solution for day 2 of the Advent of Code 2022 Challenge. See https://adventofcode.com/2022/day/2.
 * </summary> 
 */
public class SolutionImpl : Solution
{
    private readonly string _inputPath = BasePath + @"Day2\input.txt";

    private readonly Dictionary<int, int> _scoresPart1 = new()
    {
        // tie
        { 23, 3 },
        // win
        { 21, 6 },
        { 24, 6 },
        // loss
        { 22, 0 },
        { 25, 0 }
    };

    private readonly char[] _playerSymbols = { 'X', 'Y', 'Z' };
    private readonly char[] _enemySymbols = { 'A', 'B', 'C' };

    private readonly Dictionary<Tuple<char, char>, int> _scoresPart2 = new()
    {
        { new Tuple<char, char>('A', 'X'), 3 },
        { new Tuple<char, char>('B', 'X'), 1 },
        { new Tuple<char, char>('C', 'X'), 2 },
        { new Tuple<char, char>('A', 'Y'), 1 },
        { new Tuple<char, char>('B', 'Y'), 2 },
        { new Tuple<char, char>('C', 'Y'), 3 },
        { new Tuple<char, char>('A', 'Z'), 2 },
        { new Tuple<char, char>('B', 'Z'), 3 },
        { new Tuple<char, char>('C', 'Z'), 1 }
    };

    public override void SolvePartOne()
    {
        int totalScore = 0;
        foreach (string line in File.ReadLines(_inputPath))
        {
            totalScore += CalculateScoreForLinePart1(line);
        }

        Console.Out.WriteLine("Solution for part 1 of Day 2 challenge: " + totalScore);
    }

    public override void SolvePartTwo()
    {
        int totalScore = 0;
        foreach (string line in File.ReadLines(_inputPath))
        {
            totalScore += CalculateScoreForLinePart2(line);
        }

        Console.Out.WriteLine("Solution for part 2 of Day 2 challenge: " + totalScore);
    }

    private int CalculateScoreForLinePart1(string line)
    {
        Debug.Assert(line.Length == 3);

        int playerScore = 0;
        int enemyValueInAscii = parseEnemySymbolToAscii(line[0]);
        int playerValueInAscii = parsePlayerSymbolToAscii(line[2], out playerScore);
        return playerScore + _scoresPart1[playerValueInAscii - enemyValueInAscii];
    }

    private int parsePlayerSymbolToAscii(char symbol, out int playerScore)
    {
        int index = Array.IndexOf(_playerSymbols, symbol);
        Debug.Assert(index != -1);
        // X is score 1, Y is score 2, Z is score 3
        playerScore = index + 1;
        // returning symbol as int automatically converts it to ASCII
        return symbol;
    }

    private int parseEnemySymbolToAscii(char symbol)
    {
        Debug.Assert(Array.Exists(_enemySymbols, element => element == symbol));
        // returning symbol as int automatically converts it to ASCII
        return symbol;
    }

    private int CalculateScoreForLinePart2(string line)
    {
        Debug.Assert(line.Length == 3);

        int score = 0;
        char enemySymbol = line[0];
        char outcomeSymbol = line[2];
        
        int index = Array.IndexOf(_playerSymbols, outcomeSymbol);
        Debug.Assert(index != -1);
        //  X = loss, score 0, Y = draw, score 3, Z = win, score 6
        score += index * 3;

        /* add score value that player needs to play to get the outcome as indicated by the outcome symbol
         X = loss, Y = draw, Z = win
         */
        score += _scoresPart2[new Tuple<char, char>(enemySymbol, outcomeSymbol)];
        return score;
    }
    
}