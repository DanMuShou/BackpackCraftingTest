using System;
using System.Collections.Generic;
using System.IO;
using Godot;

public partial class ResourceManager : Node, IManager
{
    [Export(PropertyHint.Dir)] private string _foodResPath;

    [Export(PropertyHint.GlobalFile, ".mRec")]
    private string _recipePath;

    public Dictionary<EItemType, List<BackpackItemRes>> BackpackResDic { get; private set; }

    private Dictionary<int, BackpackItemRes> _uidToResCache;

    private Dictionary<int, HashSet<int[]>> _countToItemRecipes;

    public void Init()
    {
        BackpackResDic = [];
        _uidToResCache = [];
        _countToItemRecipes = [];

        BackpackResDic.Add(EItemType.Food, []);

        for (var i = 1; i <= 9; i++)
            _countToItemRecipes.Add(i, []);

        LoadRes();
        LoadRecipes();
    }

    private void LoadRes()
    {
        var resNames = ResourceLoader.ListDirectory(_foodResPath);
        BackpackResDic[EItemType.Food].Capacity = resNames.Length;
        foreach (var resName in resNames)
        {
            var path = $"{_foodResPath}/{resName}";
        
            var res = ResourceLoader.Load<BackpackItemRes>(path);
        
            res.TypeId = EItemType.Food;
            res.TypeItemId = BackpackResDic[EItemType.Food].Count;
            res.UniqueItemId = BackpackItemRes.GetItemResId(res);
        
            BackpackResDic[EItemType.Food].Add(res);
            _uidToResCache.Add(res.UniqueItemId, res);
        }
    }

    private void LoadRecipes()
    {
        if (!File.Exists(_recipePath)) return;
        using var reader = new BinaryReader(new FileStream(_recipePath, FileMode.Open));

        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            var count = 0;
            var itemCountByte = reader.ReadInt32();
            for (var i = 0; i <= 8; i++)
            {
                if ((itemCountByte & (1 << i)) != 0)
                    count++;
            }

            // GD.Print($"配方物品个数{count}");
            var itemsUid = new int[count + 1];
            for (var i = 0; i < count + 1; i++)
            {
                itemsUid[i] = reader.ReadInt32();
                // GD.Print($"配方物品id{itemsUid[i]}");
                // GD.Print($"配方物品名称{_uidToResCache[itemsUid[i]].Name}");
            }

            _countToItemRecipes[itemCountByte].Add(itemsUid);
        }
    }


    public BackpackItemRes GetResourceFormItemUid(int itemUid)
    {
        return _uidToResCache.GetValueOrDefault(itemUid);
    }

    public bool AddItemsRecipe(int itemCount, int[] itemsUid)
        => itemCount is >= 1 and <= 9 && _countToItemRecipes[itemCount].Add(itemsUid);
}