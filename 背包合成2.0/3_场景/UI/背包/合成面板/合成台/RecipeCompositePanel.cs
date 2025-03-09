using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

public partial class RecipeCompositePanel : BaseBackpackItemContainerPanel
{
    [Export(PropertyHint.GlobalDir)] private string _testDir;
    [Export] private BackpackItem _outputItem;
    [Export] private Button _compositeBut;
    [Export] private Label _logLab;

    private ReadWritManager _sLManager;
    private ResourceManager _resManager;

    private string _fileName = "Recipe";

    public override void Init()
    {
        _sLManager = SystemManager.GetManager<ReadWritManager>();
        _resManager = SystemManager.GetManager<ResourceManager>();

        base.Init();

        _compositeBut.Pressed += OnCompositeButPressed;
        _outputItem.Init();
    }

    private void OnCompositeButPressed()
    {
        if (!Items.Select(con => con.HasItem).Any() || !_outputItem.HasItem)
        {
            _logLab.Text = "物品不正确";
            return;
        }

        using var writer = _sLManager.GetSaveWriter(Path.Combine(_testDir, _fileName + ".mRec"));
        if (writer == null)
        {
            GD.PrintErr("文件创建失败");
            return;
        }

        var locInfo = 0;
        List<int> itemUIds = [];
        for (var i = 0; i < Items.Count; i++)
        {
            if (!Items[i].HasItem) continue;

            locInfo |= 1 << i;
            itemUIds.Add(Items[i].ItemRes.UniqueItemId);
        }

        itemUIds.Add(_outputItem.ItemRes.UniqueItemId);
        if (_resManager.AddItemsRecipe(locInfo, itemUIds.ToArray()))
        {
            writer.Write(locInfo);
            foreach (var uid in itemUIds)
            {
                writer.Write(uid);
            }

            _logLab.Text = "添加合成成功";
        }
        else
        {
            _logLab.Text = "添加合成失败";
            _logLab.Text += locInfo;
        }
    }
}