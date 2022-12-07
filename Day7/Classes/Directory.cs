using System.Diagnostics;

namespace AdventOfCode.Day7.Classes;

public class Directory
{
    public string Name { get;}
    public Directory Parent { get; set; }
    public List<Directory> ChildrenDirs { get; set; } = new ();
    public List<CustomFile> Files { get; set; } = new();

    public Directory(string name, Directory parent)
    {
        Name = name;
        Parent = parent;
    }

    public long GetTotalSize()
    {
        return Files.Sum(file => file.Size) + ChildrenDirs.Sum(directory => directory.GetTotalSize());
    }

    public Directory FindByName(string directoryName)
    {
       Directory directory = ChildrenDirs.Find(directory => directory.Name == directoryName);
       Debug.Assert(directory != null);
       return directory;
    }

    public void addChildDirectory(Directory directory)
    {
        ChildrenDirs.Add(directory);
    }

    public void addFile(CustomFile file)
    {
        Files.Add(file);
    }
        
}