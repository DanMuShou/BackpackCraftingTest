using System.Collections.Generic;
using Godot;

public partial class ResourceManager : Node, IManager
{
    [Export(PropertyHint.Dir)] private string _foodResPath;

    public Dictionary<EItemType, List<BackpackItemRes>> BackpackResDic { get; private set; }

    public void Init()
    {
        BackpackResDic = [];

        BackpackResDic.Add(EItemType.Food, []);

        LoadFoodRes();
    }

    private void LoadFoodRes()
    {
        using var dir = DirAccess.Open(_foodResPath);
        if (dir == null) return;

        var fileNames = dir.GetFiles();
        for (var i = 0; i < fileNames.Length; i++)
        {
            var fileName = fileNames[i];
            var path = $"{_foodResPath}/{fileName}";
            var res = ResourceLoader.Load<BackpackItemRes>(path);
            if (res == null) continue;

            res.TypeId = EItemType.Food;
            res.TypeItemId = i;
            res.UniqueItemId = BackpackItemRes.GetItemResId(res);

            BackpackResDic[EItemType.Food].Add(res);
        }
    }
}