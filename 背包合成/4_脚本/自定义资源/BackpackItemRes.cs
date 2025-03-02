using System;
using Godot;


[GlobalClass, Icon("res://icon.svg")]
public partial class BackpackItemRes : Resource, IItemRes
{
    [Export] public bool IsCanComposite { get; private set; }
    [Export] public bool IsCanDecompose { get; private set; }

    [Export] public string Name { get; private set; }
    [Export] public string Description { get; private set; }
    [Export] public Texture2D BackpackIcon { get; private set; }

    public EItemType TypeId { get; set; }
    public int TypeItemId { get; set; }
    public int UniqueId { get; set; }

    public static int GetItemResId(BackpackItemRes itemRes)
    {
        var objId = 0;

        objId = (objId << 1) | (itemRes.IsCanComposite ? 1 : 0);
        objId = (objId << 1) | (itemRes.IsCanDecompose ? 1 : 0);

        //TODO: 添加其他属性
        objId <<= 1;

        objId = (objId << 3) | ((int)itemRes.TypeId & 0b111);
        objId = (objId << 9) | (itemRes.TypeItemId & 0b111111111);

        var binary = Convert.ToString(objId, 2);
        GD.Print($"二进制表示: {binary}");

        return objId;
    }
}