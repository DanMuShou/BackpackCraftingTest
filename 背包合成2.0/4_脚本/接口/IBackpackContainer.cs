using Godot;


public interface IBackpackContainer
{
    public bool SetRes(BackpackItemRes itemRes, int count = 1);
    public void SetCount(int count);
    public void RemoveRes();
    public void DecreaseRes(int removeCount = 1);
    public void IncreaseRes(int addCount = 1);
    public void SwapRes(BaseBackpackItem other);
    public bool ContainsPoint(Vector2 point);
}