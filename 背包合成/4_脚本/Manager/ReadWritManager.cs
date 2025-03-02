using Godot;
using System.IO;
using FileAccess = System.IO.FileAccess;

public partial class ReadWritManager : Node, IManager
{
    public void Init()
    {
    }

    public BinaryWriter GetSaveWriter(string filePath)
    {
        if (!Directory.Exists(Path.GetDirectoryName(filePath))) return null;
        return new BinaryWriter(new FileStream(filePath, FileMode.Append, FileAccess.Write));
    }

    public BinaryReader GetLoadRider(string filePath)
    {
        if (!File.Exists(filePath)) return null;
        return new BinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));
    }
}