using Godot;

public enum EItemType
{
    Resources = 0,
    Food = 1,
    Weapon = 2,
    Other3 = 3,
    Other4 = 4,
    Other5 = 5,
    Other6 = 6,
    Other7 = 7,
}

public interface IItemRes
{
    public bool IsCanComposite { get; }
    public bool IsCanDecompose { get; }
    public EItemType TypeId { get; }
    public int TypeItemId { get; }
}