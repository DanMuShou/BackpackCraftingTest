using System.Collections.Generic;
using Godot;

public partial class SystemManager : Node, IManager
{
    public static SystemManager Instance { get; private set; }
    private List<IManager> _managers;
    public override void _EnterTree() => Instance = this;

    public void Init()
    {
        _managers = [];

        foreach (var node in GetChildren())
        {
            if (node is not IManager manager) continue;

            _managers.Add(manager);
            manager.Init();
        }
    }

    public static T GetManager<T>() where T : class, IManager
    {
        return Instance._managers.Find(x => x is T) as T;
    }
}