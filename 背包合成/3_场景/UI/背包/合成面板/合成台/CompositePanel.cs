using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

public partial class CompositePanel : VBoxContainer
{
    [Export(PropertyHint.GlobalDir)] private string _testDir;
    [Export] private GridContainer _gridContainer;
    [Export] private BackpackItemCon _outputCon;
    [Export] private Button _compositeBut;
    [Export] private Label _logLab;

    private ReadWritManager _sLManager;
    private ResourceManager _resManager;
    private BackpackItemCon[] _itemCons;
    private ItemCompositeEdit _owner;

    private string _fileName = "Recipe";

    public void Init()
    {
        _owner = GetOwner<ItemCompositeEdit>();
        _itemCons = new BackpackItemCon[9];
        _sLManager = SystemManager.GetManager<ReadWritManager>();
        _resManager = SystemManager.GetManager<ResourceManager>();

        _compositeBut.Pressed += OnCompositeButPressed;

        var nodes = _gridContainer.GetChildren();
        for (var i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is not BackpackItemCon con) continue;

            con.BackpackPanel = _owner.BackpackPanel;
            con.Index = _itemCons.Length;

            con.Init();

            _itemCons[i] = con;
        }

        _outputCon.BackpackPanel = _owner.BackpackPanel;
        _outputCon.Init();
    }

    private void OnCompositeButPressed()
    {
        if (!_itemCons.Select(con => con.IsHasItem).Any() || !_outputCon.IsHasItem)
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
            if (!_itemCons[i].IsHasItem) continue;

            locInfo |= 1 << i;
            itemUIds.Add(_itemCons[i].ItemRes.UniqueItemId);
        }

        itemUIds.Add(_outputCon.ItemRes.UniqueItemId);
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