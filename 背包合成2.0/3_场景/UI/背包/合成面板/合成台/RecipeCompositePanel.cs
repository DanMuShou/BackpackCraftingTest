using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

public partial class RecipeCompositePanel : VBoxContainer
{
    [Export(PropertyHint.GlobalDir)] private string _testDir;
    [Export] private GridContainer _gridContainer;
    [Export] private BackpackItem _output;
    [Export] private Button _compositeBut;
    [Export] private Label _logLab;

    private ReadWritManager _sLManager;
    private ResourceManager _resManager;
    private BackpackItem[] _itemCons;

    private string _fileName = "Recipe";

    public void Init()
    {
        _itemCons = new BackpackItem[9];
        _sLManager = SystemManager.GetManager<ReadWritManager>();
        _resManager = SystemManager.GetManager<ResourceManager>();

        _compositeBut.Pressed += OnCompositeButPressed;

        var nodes = _gridContainer.GetChildren();
        for (var i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is not BackpackItem con) continue;

            con.Index = _itemCons.Length;

            con.Init();

            _itemCons[i] = con;
        }

        _output.Init();
    }

    private void OnCompositeButPressed()
    {
        if (!_itemCons.Select(con => con.HasItem).Any() || !_output.HasItem)
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
        for (var i = 0; i < _itemCons.Length; i++)
        {
            if (!_itemCons[i].HasItem) continue;

            locInfo |= 1 << i;
            itemUIds.Add(_itemCons[i].ItemRes.UniqueItemId);
        }

        itemUIds.Add(_output.ItemRes.UniqueItemId);
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