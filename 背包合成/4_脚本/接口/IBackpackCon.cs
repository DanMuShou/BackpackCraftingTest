public interface IBackpackCon
{
    public bool IsHasItem => ItemCount > 0;
    public int ItemCount { get; }

    public bool SetRes(PickedRes itemRes, int count = 1);
    public void RemoveRes();
    public void DecreaseRes(int removeCount = 1);
    public void IncreaseRes(int addCount = 1);
    public void SwapRes(BaseBackpackCon other);
}