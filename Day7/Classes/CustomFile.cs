namespace AdventOfCode.Day7.Classes;

public class CustomFile
{
    public long Size { get; }
    public string Name { get; }

    public CustomFile(long size, string name)
    {
        Size = size;
        Name = name;
    }
}